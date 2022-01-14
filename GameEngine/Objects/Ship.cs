using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Objects
{
    class Ship : BaseObject
    {
        public static event EventHandler DieShip;
        protected int HP = 100;
        public Ship(Point pos, Point dir, Size size) : base(pos, dir, size) { }
        public int Energy
        {
            get { return HP; }
        }

        public override void Draw()
        {
            GameProcess.Buffer.Graphics.DrawImage(Properties.Resources.ship, pos.X, pos.Y, size.Width, size.Height);
        }

        public override void Update()
        {
            throw new Exception();
        }
        public void HP_Plus(int heal)
        {
            HP += heal;
        }
        public void HP_Minus(int damage)
        {
            HP -= damage;
        }

        public void Up()
        {
            if (pos.Y > 0) pos.Y = pos.Y - dir.Y;
        }
        public void Down()
        {
            if (pos.Y < GameProcess.Height) pos.Y = pos.Y + dir.Y;

        }
        public void Left()
        {
            if (pos.X > 0) pos.X = pos.X - dir.X;
        }
        public void Right()
        {
            if (pos.X < (GameProcess.Width / 2)) pos.X = pos.X + dir.X;
        }
        public void Die()
        {
            if (DieShip != null)
            {
                DieShip.Invoke(this, new EventArgs());
            }
        }
    }
}
