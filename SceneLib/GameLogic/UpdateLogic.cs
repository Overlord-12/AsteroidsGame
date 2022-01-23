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
    public class UpdateLogic
    {

        public static void UpdateAsteroids(List<Asteroid> asteroids, GameProcess gameProcess)
        {
            Random random = new Random();

            if (asteroids.Count < 15)
            {
                var rand = random.Next(1, 5);
                if (rand == 3)
                {
                    var size = 50;
                    var location = random.Next(0, gameProcess.Height);
                    asteroids.Add(new Asteroid(new Point(random.Next(0, gameProcess.Width), random.Next(0, gameProcess.Height)), 
                        new Point(-4, -4), new Size(size, size), gameProcess));
                }

            }
        }

        public static void UpdateUFO(List<UFO> ufo, Ship ship, GameProcess gameProcess)
        {
            AddUFO(ufo,ship,gameProcess);

            foreach (var _ufo in ufo)
                _ufo.Update(ship.GetPos);   
        }

        public static void UpdateAsteroids(List<Asteroid> asteroids)
        {
            foreach (var _asteroid in asteroids)
                _asteroid.Update();
        }

        public static void UpdateBullet(List<Bullet> bullets)
        {
            foreach (var _bullet in bullets)
                _bullet.Update();
        }

        public static void UpdateLaser(List<Laser> lasers)
        {
            foreach (var _laser in lasers)
            {
                _laser.Update();
            }
        }

        private static void AddUFO(List<UFO> ufo, Ship ship, GameProcess gameProcess)
        {
            Random random = new Random();
            if (ufo.Count < 1)
            {
                var size = 50;
                var location = random.Next(0, gameProcess.Height);
                ufo.Add(new UFO(new Point(gameProcess.Width - 100, location), new Point(-4, -4), new Size(size, size), gameProcess));
            }
        }



    }
}
