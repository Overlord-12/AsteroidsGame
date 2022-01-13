using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SceneLb.Interface
{
    public interface IScenecs
    {
        void Init(Form form);
        void Draw();
    }
}
