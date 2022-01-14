using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Objects
{
    public class Laser : BaseObject
    {
        public Laser(Point pos, Point dir, Size size) : base(pos, dir, size) { } 
        public override void Draw()
        {
            GameProcess.Buffer.Graphics.DrawImage(Properties.Resources.laser, pos.X, pos.Y, size.Width, size.Height);
        }
        public override void Update()
        {

            pos.X = pos.X + 15;

        }
    }
}
