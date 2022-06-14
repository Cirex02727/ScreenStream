using AForge.Video;
using AForge.Video.DirectShow;

namespace StreamServer
{
    public partial class Form1 : Form
    {
        private FilterInfoCollection? devices;
        private VideoCaptureDevice? videoSource;

        private Bitmap picture = new(1, 1);
        private bool isStreaming = false;

        private HttpController controller = new();

        public Form1()
        {
            Task.Run(() =>
                controller.Run());

            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            devices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo videoDevice in devices)
            {
                devicesC.Items.Add(videoDevice.Name);
            }
            devicesC.SelectedIndex = 0;
        }

        private void StartCamera()
        {
            videoSource = new VideoCaptureDevice(devices?[devicesC.SelectedIndex]?.MonikerString);
            videoSource.NewFrame += Video_NewFrame;
            videoSource.Start();

            isStreaming = true;
        }

        private void Video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            lock (picture)
            {
                if (picture != null)
                    picture.Dispose();
                else
                    return;

                picture = (Bitmap)eventArgs.Frame.Clone();
                controller.UpdateFrame(picture);
            }
        }

        private void Time_looper_Tick(object sender, EventArgs e)
        {
            if (!isStreaming)
                return;

            lock (pictureBox)
            {
                if (pictureBox.Image != null)
                    pictureBox.Image.Dispose();

                lock (picture)
                    pictureBox.Image = (Bitmap?)picture.Clone();
            }
        }

        private void CloseCamera()
        {
            isStreaming = false;

            if (videoSource != null && videoSource.IsRunning)
            {
                videoSource.NewFrame -= Video_NewFrame;
                videoSource.SignalToStop();
                videoSource.WaitForStop();
            }
        }

        private void devicesC_SelectedValueChanged(object sender, EventArgs e)
        {
            CloseCamera();
            StartCamera();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseCamera();
            controller.Stop();
        }

        /*
        private void Draw()
        {
            using Graphics g = Graphics.FromImage(screen);
            g.CopyFromScreen(0, 0, 0, 0, screen.Size);
            Cursor.Draw(g, new Rectangle(Cursor.Position.X - 5, Cursor.Position.Y - 5, 10, 10));
        }
        */
    }
}
