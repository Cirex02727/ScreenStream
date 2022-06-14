using AForge.Video;
using System.Drawing.Imaging;
using System.Text;

namespace StreamServer
{
    public class MJPEGController
    {
        private Stream stream;
        private bool isRunning = true;

        private Bitmap? frame = null;

        private byte[] footer = Encoding.ASCII.GetBytes("\r\n");

        public MJPEGController(Stream stream)
        {
            this.stream = stream;
        }

        private void Stream_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            frame = new Bitmap(eventArgs.Frame);
        }

        public void StartStream()
        {
            while (isRunning)
            {
                if (frame != null)
                {
                    try
                    {
                        lock (frame)
                        {
                            WriteFrame(stream, frame);

                            if (frame != null)
                            {
                                frame.Dispose();
                                frame = null;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.ToString());
                    }
                }
                else
                {
                    Thread.Sleep(30);
                }
            }
        }

        public void UpdateFrame(Bitmap frame)
        {
            lock (frame)
            {
                if (this.frame != null)
                    this.frame.Dispose();

                this.frame = frame;
            }
        }

        private void WriteFrame(Stream stream, Bitmap frame)
        {
            // prepare image data
            byte[]? imageData = null;

            // this is to make sure memory stream is disposed after using
            using (MemoryStream ms = new ())
            {
                frame.Save(ms, ImageFormat.Jpeg);
                imageData = ms.ToArray();
            }

            // prepare header
            byte[] header = CreateHeader(imageData?.Length ?? 0);

            // Start writing data
            stream.Write(header, 0, header.Length);
            if(imageData != null)
                stream.Write(imageData, 0, imageData.Length);
            stream.Write(footer, 0, footer.Length);

            Console.WriteLine("Sent Frame!");
        }

        private byte[] CreateHeader(int length)
        {
            string header = "--myboundary\r\n" +
                "Content-Type:image/jpeg\r\n" +
                "Content-Length:" + length + "\r\n\r\n"; // there are always 2 new line character before the actual data

            return Encoding.UTF8.GetBytes(header);
        }

        public void StopStream()
        {
            isRunning = false;
            stream.Close();
        }
    }
}
