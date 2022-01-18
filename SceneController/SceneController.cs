using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SceneController
{
    public class SceneController
    {

        private static SceneController _sceneManager;
        private BaseForm _scene;

        public static SceneController Get()
        {
            if (_sceneManager == null)
                _sceneManager = new SceneController();
            return _sceneManager;
        }

        public IScene Init<T>(Form form) where T : BaseForm, new()
        {
            if (_scene != null)
                _scene.Dispose();

            _scene = new T();
            _scene.Init(form);
            return _scene;
        }
    }
}
