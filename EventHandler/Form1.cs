using EventHandler.Objects;

namespace EventHandler
{
    public partial class Form1 : Form
    {
        int score = 0;
        Player player;
        Marker marker;
        Random rand = new Random();
        List<BaseObject> objects = new();
        public Form1()
        {
            InitializeComponent();
            player = new Player(pbMain.Width / 2, pbMain.Height / 2, 0);
            objects.Add(player);
            player.OnOverlap += (p, obj) =>
            {
                txtLog.Text = $"[{DateTime.Now:HH:mm:ss:ff}] Игрок пересекся с {obj}\n" + txtLog.Text;
            };

            player.OnMarkerOverlap += (m) =>
            {
                objects.Remove(m);
                marker = null;
            };

            player.OnTargetOverlap += (t) =>
            {
                score++;
                scoreLabel.Text = "Очки: " + score.ToString();
                objects.Remove(t);
                CreateAndLinkTarget();
            };

            CreateAndLinkTarget();
            CreateAndLinkTarget();

            marker = new Marker(50, 100, 0);
            objects.Add(marker);

        }

        private void pbMain_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(Color.White);

            foreach (var obj in objects.ToList())
            {
                if (obj != player && player.OverLabs(obj, g))
                {
                    player.Overlap(obj);
                    obj.Overlap(player);
                }
            }
            foreach (var obj in objects.ToList())
            {
                g.Transform = obj.GetTransform();
                obj.Render(g);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (marker != null)
            {
                float dx = marker.X - player.X;
                float dy = marker.Y - player.Y;
                float length = MathF.Sqrt(dx * dx + dy * dy);
                dx /= length;
                dy /= length;



                player.vX += dx * 1.5f;
                player.vY += dy * 1.5f;

                player.Angle = 90 - MathF.Atan2(player.vX, player.vY) * 180 / MathF.PI;
            }

            player.vX += -player.vX * 0.1f;
            player.vY += -player.vY * 0.1f;
            player.X += player.vX;
            player.Y += player.vY;

            pbMain.Invalidate();
        }

        private void pbMain_MouseClick(object sender, MouseEventArgs e)
        {
            if (marker == null)
            {
                marker = new Marker(0, 0, 0);
                objects.Add(marker);
            }

            marker.X = e.X;
            marker.Y = e.Y;
        }

        private void CreateAndLinkTarget()
        {
            Target newTarget = new Target(rand.Next() % pbMain.Width, rand.Next() % pbMain.Height, 0);
            objects.Add(newTarget);
            newTarget.TimeIsUp += (t) =>
            {
                objects.Remove(t);
                CreateAndLinkTarget();
            };
        }
    }
}
