using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SceneLib.Properties;

namespace GameEngine.Objects
{
    public class Medicine : BaseObject
    {
        public Medicine(Point pos, Point dir, Size size, GameProcess gameProcess) : base(pos, dir, size, gameProcess) { }

        public override void Draw()
        {
            gameProcess.Buffer.Graphics.DrawImage(Resources.medicine, pos.X, pos.Y, size.Width, size.Height);
        }


    }
}
