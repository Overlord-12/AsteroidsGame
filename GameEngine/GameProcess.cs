using GameEngine.Objects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameEngine
{
    public static class GameProcess
    {
        private static BufferedGraphicsContext _context;
        private static BufferedGraphics _buffer;
        private static List<Asteroid> asteroids = new List<Asteroid>();
        private static List<Bullet> bullets = new List<Bullet>();
        private static List<Medicine> medicines = new List<Medicine>();
        private static List<Laser> lasers = new List<Laser>();
        private static Random random = new Random();

        static Ship ship = new Ship(new Point(10, 400), new Point(5, 5), new Size(70, 70));
        static Timer timer = new Timer();
        private static int score;


        public static int Width { get; set; }
        public static int Height { get; set; }


        public static BufferedGraphics Buffer
        {
            get { return _buffer; }
        }

        static GameProcess() { }

        public static void Init(Form form)
        {
            Graphics g;
            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();

            Width = form.ClientSize.Width;
            Height = form.ClientSize.Height;

            _buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));

            timer.Interval = 100;
            timer.Start();
            timer.Tick += Timer_Tick;

            form.KeyDown += Form_KeyDown;
            Ship.DieShip += Ship_DieShip; ;
        }

        private static void Ship_DieShip(object sender, EventArgs e)
        {
            timer.Stop();

            Buffer.Graphics.DrawString("Игра закончена", new Font(FontFamily.GenericSansSerif, 50, FontStyle.Underline), Brushes.LightGray, 50, 50);
            Buffer.Graphics.DrawString($"Ваш счет {score}", new Font(FontFamily.GenericSansSerif, 50, FontStyle.Regular), Brushes.LightGray, 50, 150);
            Buffer.Render();
        }


        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                bullets.Add(new Bullet(new Point(ship.Rect.X + 10, ship.Rect.Y + 10), new Point(5, 0), new Size(40, 30)));
            }
            if(e.KeyCode == Keys.Alt)
            {
                lasers.Add(new Laser(new Point(ship.Rect.X + 10, ship.Rect.Y + 10), new Point(5, 0), new Size(40, 30)));
            }
            if (e.KeyCode == Keys.W)
            {
                ship.Up();
            }
            if (e.KeyCode == Keys.S)
            {
                ship.Down();
            }
            if (e.KeyCode == Keys.D)
            {
                ship.Right();
            }
            if (e.KeyCode == Keys.A)
            {
                ship.Left();
            }
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        public static void Draw()
        {
            _buffer.Graphics.Clear(Color.Black);
            ship.Draw();


            if (ship.Energy < 50 && medicines.Count < 1)
            {
                var position = new Random();
                var x = position.Next(0, Width / 2);
                var y = position.Next(0, Height / 2);
                medicines.Add(new Medicine(new Point(x, y), new Point(1, 1), new Size(40, 40)));
            }

            foreach (var med in medicines)
            {
                med.Draw();
            }

            foreach (var asteroid in asteroids)
                asteroid.Draw();

            foreach (var _laser in bullets)
                _laser.Draw();


            _buffer.Render();
            if (ship != null)
            {
                ship.Draw();
                Buffer.Graphics.DrawString($"HP{ship.Energy}", SystemFonts.DefaultFont, Brushes.White, 100, 10);
                Buffer.Graphics.DrawString($"У вас осталось {lasers.Count} выстрелов лазера", SystemFonts.DefaultFont, Brushes.White, 100, 30);
                Buffer.Graphics.DrawString($"Score{score}", SystemFonts.DefaultFont, Brushes.White, 100, 20);
                Buffer.Render();
            }


        }

        public static void Update()
        {
            if (asteroids.Count < 15)
            {
                var size = 50;
                var location = random.Next(0, Height);
                asteroids.Add(new Asteroid(new Point(Width - 100, location), new Point(-4, -4), new Size(size, size)));
            }
            foreach (var med in medicines)
            {
                med.Draw();
            }

            if (ship.Energy <= 0)
            {
                ship.Die();
            }

            for (int i = 0; i < asteroids.Count; i++)
            {
                var asteroid = asteroids[i];

                for (int j = 0; j < bullets.Count; j++)
                {
                    if (asteroids[i].Collision(bullets[j]))
                    {
                        asteroids.RemoveAt(i);
                        bullets.RemoveAt(j);
                        if(asteroid.GetSize.Width == 50)
                        {
                            CreateLitleAsteroids(asteroid);
                        }
                        score += 30;
                        if(i != 0)
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

            foreach (var _asteroid in asteroids)
            {
                _asteroid.Update();
            }

            for (int j = 0; j < medicines.Count; j++)
            {
                if (ship.Collision(medicines[j]))
                {
                    medicines.RemoveAt(j);
                    ship.HP_Plus(30);
                }
            }

            foreach (var _laser in bullets)
            {
                _laser.Update();
            }

            for (int i = bullets.Count - 1; i >= 0; i--)
            {
                if (bullets[i].GetPos.X > Width)
                {
                    bullets.RemoveAt(i);
                }
            }
        }

        public static void CreateLitleAsteroids(Asteroid parentAsteroid)
        {
            Random random = new Random();
            for (int i = 0; i < 2; i++)
            {
                Point pos = new Point(parentAsteroid.GetPos.X + random.Next(1,20), parentAsteroid.GetPos.Y + random.Next(1, 20));
                asteroids.Add(new Asteroid(pos, parentAsteroid.GetDir, new Size(25, 25)));
            }
        }

    }
}
