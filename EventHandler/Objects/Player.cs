using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Text;

namespace EventHandler.Objects
{
    internal class Player : BaseObject
    {
        public Action<Marker> OnMarkerOverlap;
        public Action<Target> OnTargetOverlap;

        public Player(float x, float y, float angle) : base(x, y, angle)
        {

        }

        public override void Render(Graphics g)
        {
            g.FillEllipse(new SolidBrush(Color.DeepSkyBlue), -25, -25, 50, 50);
            g.DrawEllipse(new Pen(Color.Black, 2), -25, -25, 50, 50);
            g.DrawLine(new Pen(Color.Black, 2), 0, 0, 50, 0);
        }

        public override GraphicsPath GetGraphicsPath()
        {
            var path = base.GetGraphicsPath();
            path.AddEllipse(-25, -25, 50, 50);
            return path;
        }

        public override void Overlap(BaseObject obj)
        {
            base.Overlap(obj);
            if (obj is Marker)
            {
                OnMarkerOverlap(obj as Marker);
            }
            else if (obj is Target)
            {
                OnTargetOverlap(obj as Target);
            }
        }
    }
}
