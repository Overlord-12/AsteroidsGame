using SceneLib.Interface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SceneLib
{
    public class BaseForm : IScene, IDisposable
    {

        private BufferedGraphicsContext _context;
        protected Form _form;
        public BufferedGraphics Buffer;


        public int Width { get; set; }
        public int Height { get; set; }

        public virtual void Init(Form form)
        {
            _form = form;
            Graphics g;
            _context = BufferedGraphicsManager.Current;
            g = _form.CreateGraphics();
            Width = _form.ClientSize.Width;
            Height = _form.ClientSize.Height;
            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));

            _form.KeyDown += SceneKeyDown;
        }

        public virtual void SceneKeyDown(object sender, KeyEventArgs e) { }

        public virtual void Draw() { }

        public void Dispose()
        {
            _form.KeyDown -= SceneKeyDown;
        }

        public virtual void Draw(int i)
        {
        }
    }
}
