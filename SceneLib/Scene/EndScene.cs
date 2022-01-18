using GameEngine;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SceneLib
{
    public class EndScene: BaseForm
    {
        public override void Draw(int score)
        {
            Buffer.Graphics.Clear(Color.Black);
            Buffer.Graphics.DrawString("Поражение", new Font(FontFamily.GenericSansSerif, 50, FontStyle.Underline), Brushes.White, 200, 100);
            Buffer.Graphics.DrawString($"Вы набрали {score} очков", new Font(FontFamily.GenericSansSerif, 20, FontStyle.Underline), Brushes.White, 200,200);
            Buffer.Graphics.DrawString("<Enter> - Начать заново", new Font(FontFamily.GenericSansSerif, 20, FontStyle.Underline), Brushes.White, 200, 300);
            Buffer.Graphics.DrawString("<Esc> - Выйти", new Font(FontFamily.GenericSansSerif, 20, FontStyle.Underline), Brushes.White, 200, 400);      
            Buffer.Render();
        }

        public override void SceneKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                _form.Close();
            }
            if (e.KeyCode == Keys.Enter)
            {
                GameProcess gameProcess = new GameProcess();
                SceneController.Get();
                gameProcess.Init(_form,gameProcess);
                gameProcess.Draw();
            }
        }
    }
}