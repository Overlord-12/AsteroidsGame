using GameEngine.GameLogic;
using GameEngine.Objects;
using SceneLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameEngine
{
    public class GameProcess
    {
        private BufferedGraphicsContext _context;
        private BufferedGraphics _buffer;

        private List<Asteroid> asteroids = new List<Asteroid>();
        private List<Bullet> bullets = new List<Bullet>();
        private List<Medicine> medicines = new List<Medicine>();
        private List<Laser> lasers = new List<Laser>();
        private List<UFO> ufo = new List<UFO>();
        private Ship ship;

        private Timer timer = new Timer();
        private Timer laserTimer = new Timer();

        private Form _form = new Form();
        private GameProcess _gameProcess;

        private int score;
        private int maxLaserCount;

        private Random random = new Random();


        public int Width { get; set; }
        public int Height { get; set; }


        public BufferedGraphics Buffer
        {
            get { return _buffer; }
        }

        static GameProcess() { }

        public void Init(Form form, GameProcess gameProcess)
        {
            Graphics g;
            _gameProcess = gameProcess;
            CollisionLogic collision = new CollisionLogic();
            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();
            _form = form;

            Width = form.ClientSize.Width;
            Height = form.ClientSize.Height;

            ship = new Ship(new Point(Height / 2, Width / 2), new Point(5, 5), new Size(70, 70), _gameProcess);
            _buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));

            timer.Interval = 100;
            maxLaserCount = 3;

            timer.Start();

            timer.Tick += Timer_Tick;

            form.KeyDown += Form_KeyDown;
            Ship.DieShip += Ship_DieShip; 
        }

        private void Ship_DieShip(object sender, EventArgs e)
        {
            Ship.DieShip -= Ship_DieShip;
            timer.Tick -= Timer_Tick;

            SceneController
              .Get()
              .Init<EndScene>(_form)
              .Draw(score);

        }


        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {         
                bullets.Add(new Bullet(new Point(ship.Rect.X + 10, ship.Rect.Y + 10), new Point(5, 0), new Size(40, 30), _gameProcess));
            }
            if(e.KeyCode == Keys.Z)
            {
                if(!(lasers.Count+1 > maxLaserCount))
                {
                    lasers.Add(new Laser(new Point(ship.Rect.X + 10, ship.Rect.Y + 10), new Point(5, 0), new Size(40, 30), _gameProcess));
                }
                
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

        private  void Timer_Tick(object sender, EventArgs e)
        {
            _gameProcess.Draw();
            Update();
        }

        public void Draw()
        {
            _buffer.Graphics.Clear(Color.Black);
            ship.Draw();


            if (ship.Energy < 50 && medicines.Count < 1)
            {
                var position = new Random();
                var x = position.Next(0, Width / 2);
                var y = position.Next(0, Height / 2);
                medicines.Add(new Medicine(new Point(x, y), new Point(1, 1), new Size(40, 40), _gameProcess));
            }

            foreach (var med in medicines)
            {
                med.Draw();
            }

            foreach(var _ufo in ufo)
            {
                _ufo.Draw();
            }

            foreach (var asteroid in asteroids)
                asteroid.Draw();

            foreach (var _bullet in bullets)
                _bullet.Draw();

            foreach (var _laser in lasers)
                _laser.Draw();

            _buffer.Render();
            if (ship != null)
            {
                ship.Draw();
                Buffer.Graphics.DrawString($"HP{ship.Energy}", SystemFonts.DefaultFont, Brushes.White, 100, 10);
                Buffer.Graphics.DrawString($"У вас осталось {maxLaserCount-lasers.Count} выстрелов лазера", SystemFonts.DefaultFont, Brushes.White, 100, 30);
                Buffer.Graphics.DrawString($"Score{_gameProcess.score}", SystemFonts.DefaultFont, Brushes.White, 100, 20);
                Buffer.Render();
            }


        }

        public void Update()
        {
            if (asteroids.Count < 15)
            {
                var rand = random.Next(1, 5);
                if(rand == 3)
                {
                    var size = 50;
                    var location = random.Next(0, Height);
                    asteroids.Add(new Asteroid(new Point(random.Next(0,Width), random.Next(0, Height)), new Point(-4, -4), new Size(size, size), _gameProcess));
                }
              
            }

            if(ufo.Count < 1)
            {
                var size = 50;
                var location = random.Next(0, Height);
                ufo.Add(new UFO(new Point(Width - 100, location), new Point(-4, -4), new Size(size, size), _gameProcess));
            }
            foreach (var med in medicines)
            {
                med.Draw();
            }
            foreach(var _ufo in ufo)
            {
                _ufo.Update(ship.GetPos);
            }

            if (ship.Energy <= 0)
            {
                ship.Die();
            }

            CollisionLogic.AsteroidsCollision(asteroids, bullets,  ship, ref _gameProcess.score, _gameProcess);
            CollisionLogic.LaserCollision(asteroids,lasers, ref _gameProcess.score, _gameProcess);
            CollisionLogic.UFOCollision(ship, ufo);
            CollisionLogic.MedicineCollision(medicines, ship);
            CollisionLogic.BulletAndUFOCollision(ufo,bullets,ref _gameProcess.score);

            foreach (var _asteroid in asteroids)
            {
                _asteroid.Update();
            }

            foreach (var _bullet in bullets)
            {
                _bullet.Update();
            }

            foreach(var _laser in lasers)
            {
                _laser.Update();
            }

            for (int i = lasers.Count - 1; i >= 0 ; i--)
            {
                if(lasers[i].GetPos.X > Width)
                {
                    lasers.RemoveAt(i);
                }
                
            }

            for (int i = bullets.Count - 1; i >= 0; i--)
            {
                if (bullets[i].GetPos.X > Width)
                {
                    bullets.RemoveAt(i);
                }
            }
        }

    }
}
