using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using RPG.Core.Mapping;
using RPG.Math;
using RPG.NodeVisitors.Collisions;
using RPG.NodeVisitors.Draw;
using RPG.NodeVisitors.Input;
using RPG.NodeVisitors.Pointer;
using RPG.NodeVisitors.Update;
using RPG.Runner.Controls;
using RPG.Runner.Domain;
using RPG.Runner.RazorPainter;
using RPG.SceneGraph;
using Matrix = RPG.Math.Matrix;

namespace RPG.Runner
{
    public partial class Form1 : Form
    {
        private readonly RazorPainterWFCtl _razorPainter;


        private float _curTime = 0;
        private int _fps = 0;
        private int _fpsCounter = 0;
        private float _fpsLastTime = 0;

        private Node _rootNode;
        private bool _closing = false;

        private World _rootAspect;

        private Vector _mouseLocation;

        private Camera _camera;

        private readonly MutableInputState _inputState = new MutableInputState();

        private InputVisitor _inputVisitor;
        private PointerVisitor _pointerVisitor;
        private CameraVisitor _cameraVisitor;
        private CollisionVisitor _collisionVisitor;
        private UpdateVisitor _updateVisitor;
        private DrawVisitor _drawVisitor;

        public Form1()
        {
            InitializeComponent();

            _razorPainter = new RazorPainterWFCtl {Dock = DockStyle.Fill};
            _razorPainter.MouseEnter += (sender, args) => Cursor.Hide();
            _razorPainter.MouseLeave += (sender, args) => Cursor.Show();
            _razorPainter.MouseDown += MouseDownHandler;
            _razorPainter.MouseMove += MouseMoveHandler;
            _razorPainter.MouseUp += MouseUpHandler;
            _razorPainter.KeyDown += KeyDown;
            _razorPainter.KeyUp += KeyUp;
            Controls.Add(_razorPainter);
        }

        private void KeyUp(object sender, KeyEventArgs e)
        {
            _inputState.SetKeyDown(e.KeyCode, false);
        }

        private void KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                _closing = true;
                Application.Exit();
            }
            _inputState.SetKeyDown(e.KeyCode, true);
        }

        private void MouseUpHandler(object sender, MouseEventArgs e)
        {
            _inputState.SetMouseButtonDown(e.Button, false);
        }

        private void MouseMoveHandler(object sender, MouseEventArgs e)
        {
            _mouseLocation.X = e.X;
            _mouseLocation.Y = e.Y;
        }

        private void MouseDownHandler(object sender, MouseEventArgs e)
        {
            _inputState.SetMouseButtonDown(e.Button, true);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitGraph();
            _camera = new Camera(_razorPainter.Width, _razorPainter.Height);
            _camera.Move(-900, -500);


            _inputVisitor = new InputVisitor(_inputState);
            _pointerVisitor = new PointerVisitor(_camera);
            _cameraVisitor = new CameraVisitor(_camera);
            _collisionVisitor = new CollisionVisitor();
            _updateVisitor = new UpdateVisitor();
            _drawVisitor = new DrawVisitor(_razorPainter.RazorGFX, _camera);

            InitGameThread();
        }

        private void InitGraph()
        {

            var mapper = new Mapper();
            mapper.For<World>().Use((a, c) => new SCWorld(a, c));
            mapper.For<Location>().Use((a, c) => new SCLocation(a, c));
            mapper.For<Character>().Use((a, c) => new SCCharacter(a, c));
            mapper.For<Hand>().Use((a, c) => new SCHand(a, c));
            mapper.For<Tree>().Use((a, c) => new SCTree(a, c));
            mapper.For<Bush>().Use((a, c) => new SCBush(a, c));
            mapper.For<Stone>().Use((a, c) => new SCStone(a, c));
            mapper.For<Building>().Use((a, c) => new SCBuilding(a, c));

            _rootAspect = new World();

            _rootNode = mapper.CreateFor(_rootAspect).RootNode;
        }

        private void InitGameThread()
        {
            var sw = Stopwatch.StartNew();
            var renderThread = new Thread(() =>
            {
                while (!_closing)
                {
                    Update(sw.ElapsedMilliseconds / 1000f);
                    sw.Restart();
                    lock (this)
                    {
                        Draw();
                    }
                    Thread.Sleep(1);
                }
            });
            renderThread.Start();
        }

        private void Update(float deltaTime)
        {
            _curTime += deltaTime;
            _fpsCounter++;
            if (_curTime - _fpsLastTime > 1)
            {
                _fpsLastTime = _curTime;
                _fps = _fpsCounter;
                _fpsCounter = 0;
            }

            _inputVisitor.Reset();
            _inputVisitor.Visit(_rootNode);

            _pointerVisitor.Pointer = _mouseLocation;
            _pointerVisitor.Reset();
            _pointerVisitor.Visit(_rootNode);

            _cameraVisitor.Reset();
            _cameraVisitor.Visit(_rootNode);

            _updateVisitor.DeltaTime = deltaTime;
            _updateVisitor.Reset();
            _updateVisitor.Visit(_rootNode);

            _collisionVisitor.Reset();
            _collisionVisitor.Visit(_rootNode);

        }

        private void Draw()
        {
            _drawVisitor.Reset();
            _drawVisitor.Visit(_rootNode);
            _razorPainter.RazorGFX.DrawString($"FPS: {_fps}", new Font("Arial", 32), Brushes.White,
                -_razorPainter.RazorGFX.Transform.OffsetX, -_razorPainter.RazorGFX.Transform.OffsetY);
            _razorPainter.RazorPaint();
        }
    }
}
