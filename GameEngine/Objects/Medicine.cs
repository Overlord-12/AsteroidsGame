using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Objects
{
    class Medicine : BaseObject
    {
        public Medicine(Point pos, Point dir, Size size) : base(pos, dir, size) { }

        public override void Draw()
        {
            GameProcess.Buffer.Graphics.DrawImage(Properties.Resources.medicine, Pos.X, Pos.Y, Size.Width, Size.Height);
        }


    }
}
