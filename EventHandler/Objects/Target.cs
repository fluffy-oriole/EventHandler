using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Text;

namespace EventHandler.Objects
{
    internal class Target : BaseObject
    {
        public int timeToCatch = 10;
        public Action<Target> TimeIsUp;
        public Target(float x, float y, float angle) : base(x, y, angle)
        {

        }

        public override void Render(Graphics g)
        {
            g.FillEllipse(new SolidBrush(Color.GreenYellow), -25, -25, 50, 50);
            g.DrawString(
                timeToCatch.ToString(),
                new Font("Verdana", 8),
                new SolidBrush(Color.Green),
                20, 20
            );
        }

        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddEllipse(-25, -25, 50, 50);
            return path;
        }

        public void UpdateTimeToCath(BaseObject obj)
        {
            timeToCatch--;
            if (timeToCatch == 0)
            {
                TimeIsUp(obj as Target);
            }
        }
    }
}
