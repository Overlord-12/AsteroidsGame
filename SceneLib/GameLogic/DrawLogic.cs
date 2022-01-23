using GameEngine;
using GameEngine.Objects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SceneLib.GameLogic
{
    public class DrawLogic
    {
        public static void AddMedicine(Ship ship, List<Medicine> medicines, GameProcess gameProcess)
        {
            if (ship.Energy < 50 && medicines.Count < 1)
            {
                var position = new Random();
                var x = position.Next(0, gameProcess.Width / 2);
                var y = position.Next(0, gameProcess.Height / 2);
                medicines.Add(new Medicine(new Point(x, y), new Point(1, 1), new Size(40, 40), gameProcess));
            }
        }

        public static void DrawShip(Ship ship, GameProcess gameProcess)
        {
            if (ship != null)
            {
                ship.Draw();
                gameProcess.Buffer.Graphics.DrawString($"HP{ship.Energy}", SystemFonts.DefaultFont, Brushes.White, 100, 10);
                gameProcess.Buffer.Graphics.DrawString($"У вас осталось {gameProcess.GetMaxLaserCount - gameProcess.GetLaserCount} выстрелов лазера", SystemFonts.DefaultFont, Brushes.White, 100, 30);
                gameProcess.Buffer.Graphics.DrawString($"Score{gameProcess.GetTotalScore}", SystemFonts.DefaultFont, Brushes.White, 100, 20);
                gameProcess.Buffer.Render();
            }
        }

        public static  void DrawMedicine(List<Medicine> medicines)
        {
            foreach (var med in medicines)
                med.Draw();
        }

        public static void DrawUFO(List<UFO> ufo)
        { 
            foreach (var _ufo in ufo)
                _ufo.Draw();
        }

        public static void DrawBullets(List<Bullet> bullets)
        {
            foreach (var asteroid in bullets)
                asteroid.Draw();
        }

        public static void DrawAsteroids(List<Asteroid> asteroids)
        {
            foreach (var _bullet in asteroids)
                _bullet.Draw();
        }

        public static void DrawLaser(List<Laser> lasers)
        {
            foreach (var _laser in lasers)
                _laser.Draw();
        }
    }
}
