using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Objects
{
    public class Asteroid : BaseObject
    {
        public Asteroid(Point pos, Point dir, Size size,GameProcess gameProcess) : base(pos, dir, size,gameProcess) { }
    }
}
