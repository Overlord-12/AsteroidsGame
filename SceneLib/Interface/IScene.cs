using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SceneLib.Interface
{
    public interface IScene
    {
        void Init(Form form);
        void Draw();
        void Draw(int i);
    }
}
