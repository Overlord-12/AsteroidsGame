using GameEngine.Interface;
using GameEngine.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameEngine.Objects
{
    public abstract class BaseObject : ICollision
    {

        protected Point pos;
        protected Point dir;
        protected Size size;
        public Size GetSize => this.size;
        public Point GetPos => this.pos;
        public Point GetDir => this.dir;

        public BaseObject(Point pos, Point dir, Size size)
        {
            try
            {
                this.pos = pos;
                this.dir = dir;
                this.size = size;
                if (size.Width < 0 || size.Height < 0) throw new Exception();

            }
            catch (Exception)
            {
                MessageBox.Show("Вы ввели неверные размеры");
            }
        }

        public Rectangle Rect => new Rectangle(pos, size);

        public bool Collision(ICollision obj)
        {
            return obj.Rect.IntersectsWith(Rect);
        }

        public virtual void Draw()
        {
            GameProcess.Buffer.Graphics.DrawImage(Resources.asteroid, pos.X, pos.Y, size.Width, size.Height);

        }

        public virtual void Update()
        {
            pos.X = pos.X + dir.X;
            pos.Y = pos.Y + dir.Y;

            if (pos.X < 0) dir.X = -dir.X;
            if (pos.X > GameProcess.Width) dir.X = -dir.X;
            if (pos.Y < 0) dir.Y = -dir.Y;
            if (pos.Y > GameProcess.Height) dir.Y = -dir.Y;
        }

        public virtual void UpdateColl()
        {
            pos.X = 0;
            pos.Y = 200;
        }
    }
}
