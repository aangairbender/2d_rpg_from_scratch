using System.Collections.Generic;
using RPG.Core;

namespace RPG.Runner.Domain
{
    public class World : IAspect
    {
        public Location Location { get; } = new Location();
    }
}