using GameEngine.Interface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameEngine.Objects
{
    abstract class BaseObject : ICollision
    {

        protected Point Pos;
        protected Point Dir;
        protected Size Size;

        public BaseObject(Point pos, Point dir, Size size)
        {
            try
            {
                Pos = pos;
                Dir = dir;
                Size = size;
                if (size.Width < 0 || size.Height < 0) throw new Exception();

            }
            catch (Exception)
            {
                MessageBox.Show("Вы ввели неверные размеры");
            }
        }

        public Rectangle Rect => new Rectangle(Pos, Size);

        public bool Collision(ICollision obj)
        {
            return obj.Rect.IntersectsWith(Rect);
        }

        public virtual void Draw()
        {
            GameProcess.Buffer.Graphics.DrawImage(Properties.Resources.asteroid, Pos.X, Pos.Y, Size.Width, Size.Height);

        }

        public virtual void Update()
        {
            Pos.X = Pos.X + Dir.X;
            Pos.Y = Pos.Y + Dir.Y;

            if (Pos.X < 0) Dir.X = -Dir.X;
            if (Pos.X > GameProcess.Width) Dir.X = -Dir.X;
            if (Pos.Y < 0) Dir.Y = -Dir.Y;
            if (Pos.Y > GameProcess.Height) Dir.Y = -Dir.Y;
        }


        public virtual void UpdateColl()
        {
            Pos.X = 0;
            Pos.Y = 200;
        }

    }
}
