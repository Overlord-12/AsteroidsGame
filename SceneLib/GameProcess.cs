using GameEngine.GameLogic;
using GameEngine.Objects;
using SceneLib;
using SceneLib.GameLogic;
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

        private Form _form = new Form();
        private GameProcess _gameProcess;

        private int score;
        private int maxLaserCount;

        public int GetLaserCount { get { return lasers.Count; } }
        public int GetMaxLaserCount { get { return maxLaserCount; } }
        public int GetTotalScore { get { return score; } }

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

            DrawLogic.AddMedicine(ship,medicines,_gameProcess);
            DrawLogic.DrawMedicine(medicines);

            DrawLogic.DrawUFO(ufo);

            DrawLogic.DrawBullets(bullets);

            DrawLogic.DrawAsteroids(asteroids);

            DrawLogic.DrawLaser(lasers);

            _buffer.Render();

            DrawLogic.DrawShip(ship, _gameProcess);


        }

        public void Update()
        {

            if (ship.Energy <= 0)
            {
                ship.Die();
            }

            DrawLogic.DrawMedicine(medicines);

            CollisionLogic.AsteroidsCollision(asteroids, bullets,  ship, ref _gameProcess.score, _gameProcess);
            CollisionLogic.LaserCollision(asteroids,lasers, ref _gameProcess.score, _gameProcess);
            CollisionLogic.UFOCollision(ship, ufo);
            CollisionLogic.MedicineCollision(medicines, ship);
            CollisionLogic.BulletAndUFOCollision(ufo,bullets,ref _gameProcess.score);
            CollisionLogic.LaserAndUFOCollison(ufo,lasers,ref _gameProcess.score);

            UpdateLogic.UpdateBullet(bullets);
            UpdateLogic.UpdateLaser(lasers);
            UpdateLogic.UpdateAsteroids(asteroids);
            UpdateLogic.UpdateUFO(ufo, ship, _gameProcess);
            UpdateLogic.UpdateAsteroids(asteroids, _gameProcess);


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
