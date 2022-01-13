using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Objects
{
    public class Bullet:BaseObject
    {
        public Bullet(Point pos, Point dir, Size size) : base(pos, dir, size) { }
        public Point GetPos
        {
            get { return Pos; }
        }
        public override void Draw()
        {
            GameProcess.Buffer.Graphics.DrawImage(Properties.Resources.laser, Pos.X, Pos.Y, Size.Width, Size.Height);
        }
        public override void Update()
        {

            Pos.X = Pos.X + 15;

        }
    }
}
