using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SceneLib.Properties;

namespace GameEngine.Objects
{
   public class UFO:BaseObject
    {
        public UFO(Point pos, Point dir, Size size, GameProcess gameProcess) : base(pos, dir, size, gameProcess) { }
        public override void Draw()
        {
            gameProcess.Buffer.Graphics.DrawImage(Resources.UFO, pos.X, pos.Y, size.Width, size.Height);
        }
        public void Update(Point shipPoint)
        {
            int relationshipX = shipPoint.X - pos.X;
            int relationshipY = shipPoint.Y - pos.Y;

            if (relationshipX != 0)
            {
                if (relationshipX < 0)
                    pos.X--;
                if (relationshipX > 0)
                    pos.X++;
            }

            if (relationshipY != 0)
            {
                if (relationshipY < 0)
                    pos.Y--;
                if (relationshipY > 0)
                    pos.Y++;
            }
        }
    }
}
