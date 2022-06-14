using System.Net;

namespace StreamServer
{
    public class HttpController
    {
        private HttpListener listener = new();

        private Dictionary<IPEndPoint, MJPEGController> controllers = new();

        public bool isLooping = true;

        private int port = 1414;

        public void Run()
        {
            //Create HTTP listener
            listener = new HttpListener();
            //Monitoring path
            listener.Prefixes.Add($"http://+:{port}/");
            //Set anonymous access
            listener.AuthenticationSchemes = AuthenticationSchemes.Anonymous;
            //Start monitoring
            listener.Start();

            // NOTE: If throw "access error" on start run as administrator

            while (isLooping)
            {
                HttpListenerContext? contex = null;
                try
                {
                    contex = listener.GetContext();
                }
                catch (Exception) {}

                if(contex == null)
                {
                    Stop();
                    break;
                }

                Task.Run(() =>
                {
                    MJPEGController controller = new(contex.Response.OutputStream);

                    lock (controllers)
                    {
                        if(!controllers.ContainsKey(contex.Request.RemoteEndPoint))
                            controllers.Add(contex.Request.RemoteEndPoint, controller);
                    }

                    controller.StartStream();
                    controller.StopStream();
                });
            }
        }

        public void UpdateFrame(Bitmap frame)
        {
            lock (controllers)
            {
                foreach (MJPEGController controller in controllers.Values)
                {
                    controller.UpdateFrame((Bitmap) frame.Clone());
                }
            }
        }

        public void Stop()
        {
            foreach (MJPEGController controller in controllers.Values)
            {
                controller.StopStream();
            }

            listener.Stop();
        }
    }
}
