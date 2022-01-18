using GameEngine.Objects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.GameLogic
{
    public class CollisionLogic
    {

        public static void AsteroidsCollision(List<Asteroid> asteroids, List<Bullet> bullets, Ship ship, ref int score, GameProcess gameProcess)
        {
            for (int i = 0; i < asteroids.Count; i++)
            {
                var asteroid = asteroids[i];

                for (int j = 0; j < bullets.Count; j++)
                {
                    if (asteroids[i].Collision(bullets[j]))
                    {
                        asteroids.RemoveAt(i);
                        bullets.RemoveAt(j);
                        if (asteroid.GetSize.Width == 50)
                        {
                            CollisionLogic.CreateLitleAsteroids(asteroid, asteroids, gameProcess);
                        }
                        score += 30;
                        if (i != 0)
                            i--;
                        continue;
                    }
                }

                if (ship != null && asteroids[i].Collision(ship))
                {
                    asteroids.RemoveAt(i);
                    ship.HP_Minus(10);
                    i--;
                    continue;
                }
            }
        }

        public static void BulletAndUFOCollision(List<UFO> ufo, List<Bullet> bullets, ref int score)
        {
            for (int i = 0; i < ufo.Count; i++)
            {

                for (int j = 0; j < bullets.Count; j++)
                {
                    if (ufo.Count !=0 && ufo[i].Collision(bullets[j]))
                    {
                        ufo.RemoveAt(i);
                        bullets.RemoveAt(j);
                        score += 30;
                        if (i != 0)
                            i--;
                        continue;
                    }
                }
            }

        }

        public static void UFOCollision(Ship ship, List<UFO> ufo)
        {

            for (int i = 0; i < ufo.Count; i++)
            {
                var _ufo = ufo[i];
                if (ship.Collision(_ufo))
                {
                    ufo.Remove(_ufo);
                    ship.HP_Minus(50);
                }
            }
            
        }

        public static void MedicineCollision(List<Medicine> medicines, Ship ship)
        {
            for (int j = 0; j < medicines.Count; j++)
            {
                if (ship.Collision(medicines[j]))
                {
                    medicines.RemoveAt(j);
                    ship.HP_Plus(30);
                }
            }
        }
        public static void CreateLitleAsteroids(Asteroid parentAsteroid, List<Asteroid> asteroids, GameProcess gameProcess)
        {
            Random random = new Random();
            for (int i = 0; i < 2; i++)
            {
                Point pos = new Point(parentAsteroid.GetPos.X + random.Next(1, 20), parentAsteroid.GetPos.Y + random.Next(1, 20));
                asteroids.Add(new Asteroid(pos, parentAsteroid.GetDir, new Size(25, 25), gameProcess));
            }
        }
    }
}
