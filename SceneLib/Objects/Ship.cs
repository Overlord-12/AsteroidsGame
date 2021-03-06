using System;
using System.Collections.Generic;
using SceneLib.Properties;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Objects
{
    public class Ship : BaseObject
    {
        public static event EventHandler DieShip;
        protected int HP = 100;
        public Ship(Point pos, Point dir, Size size, GameProcess gameProcess) : base(pos, dir, size, gameProcess) { }
        public int Energy
        {
            get { return HP; }
        }

        public override void Draw()
        {
            gameProcess.Buffer.Graphics.DrawImage(Resources.ship, pos.X, pos.Y, size.Width, size.Height) ;
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
            pos.Y = pos.Y - dir.Y;
            if (pos.Y <= 0) pos.Y = gameProcess.Height;
        }
        public void Down()
        {
            pos.Y = pos.Y + dir.Y;
            if (pos.Y >= gameProcess.Height) pos.Y = 0-10;

        }
        public void Left()
        {
            pos.X = pos.X - dir.X;
            if (pos.X <= 0) pos.X = gameProcess.Width;
        }
        public void Right()
        {
           pos.X = pos.X + dir.X;
            if (pos.X >= gameProcess.Width) pos.X = 0 - 10;
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
