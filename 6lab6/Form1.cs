namespace _6lab6
{
    public partial class Form1 : Form
    {
        List<Emitter> emitters = new List<Emitter>();
        Emitter emitter;

        GravityPoint point1;
        ColorPoint colorPoint;
        public Form1()
        {
            InitializeComponent();
            picDisplay.Image = new Bitmap(picDisplay.Width, picDisplay.Height);

            this.emitter = new Emitter
            {
                Direction = 0,
                Spreading = 10,
                SpeedMin = 10,
                SpeedMax = 10,
                ColorFrom = Color.LightSkyBlue,
                ColorTo = Color.FromArgb(0, Color.DarkBlue),
                ParticlesPerTick = 10,
                X = picDisplay.Width / 2,
                Y = picDisplay.Height / 2,
            };

            emitters.Add(this.emitter);


            point1 = new GravityPoint
            {
                X = picDisplay.Width / 2 + 200,
                Y = picDisplay.Height / 2,
            };

            emitter.impactPoints.Add(point1);

            colorPoint = new ColorPoint
            {
                X = picDisplay.Width / 2 - 200,
                Y = picDisplay.Height / 2,
            };

            emitter.impactPoints.Add(colorPoint);


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            emitter.UpdateState();

            using (var g = Graphics.FromImage(picDisplay.Image))
            {
                g.Clear(Color.White);
                emitter.Render(g);
            }

            picDisplay.Invalidate();
        }

        private void picDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            foreach (var emitter in emitters)
            {
                emitter.MousePositionX = e.X;
                emitter.MousePositionY = e.Y;
            }

        }

        private void tbDirection_Scroll(object sender, EventArgs e)
        {
            emitter.Direction = tbDirection.Value;
            lblDirection.Text = $"{tbDirection.Value}°";
        }

        private void tbGraviton_Scroll(object sender, EventArgs e)
        {
            point1.Power = tbGraviton.Value;
        }

        private void tbColor_Scroll(object sender, EventArgs e)
        {
            colorPoint.Radius = tbColor.Value;
        }
    }
}
