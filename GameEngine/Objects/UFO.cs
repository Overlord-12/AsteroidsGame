using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Objects
{
   public class UFO:BaseObject
    {
        public UFO(Point pos, Point dir, Size size) : base(pos, dir, size) { }
        public override void Draw()
        {
            GameProcess.Buffer.Graphics.DrawImage(Properties.Resources.UFO, pos.X, pos.Y, size.Width, size.Height);
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
