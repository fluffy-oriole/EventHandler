using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Text;

namespace EventHandler.Objects
{
    internal class Marker : BaseObject
    {
        public Marker(float x, float y, float angle) : base(x, y, 0)
        {

        }

        public override void Render(Graphics g)
        {
            g.FillEllipse(new SolidBrush(Color.Red), -6, -6, 12, 12);
            g.DrawEllipse(new Pen(Color.Red, 2), -12, -12, 24, 24);
            g.DrawEllipse(new Pen(Color.Red, 2), -20, -20, 40, 40);
        }

        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddEllipse(-6, -6, 12, 12);
            return path;
        }
    }
}
