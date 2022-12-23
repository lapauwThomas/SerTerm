using System.IO.Ports;
using System.Text;

namespace SerTerm;

internal interface ISerialPortReader
{
    event EventHandler<LineReadEventArgs> LineReceivedEventHandler;
    void Start();
    void Stop();
}

internal class ByteReader : ISerialPortReader
{
        private Thread _thread;
        private CancellationTokenSource cancelToken;
        private CancellationToken token => cancelToken.Token;
        private SerialPort port;

        public event EventHandler<LineReadEventArgs> LineReceivedEventHandler;

        public Action<byte> ByteReceivedAction { get; set; }


        private int lineTimeout;

        private byte[] lineBuffer;
        private int lineIndex;

        private bool StripNewLine = true;

        private int bytesPerLine;

        protected ByteReader(SerialPort port, int timeOutMillis, int bytesPerLine)
        {
            lineBuffer = new byte[bytesPerLine];
            this.port = port;
            lineTimeout = timeOutMillis;
            this.bytesPerLine = bytesPerLine;
            _thread = new Thread(new ThreadStart(this.Threadloop));
        }


        public void Start()
        {
            var cancelToken = new CancellationTokenSource();
            _thread.Start();
        }

        private void Threadloop()
        {
            StringBuilder sb = new StringBuilder();
            System.Timers.Timer lineTimer = new System.Timers.Timer(lineTimeout)
            {
                AutoReset = false,
                Enabled = false
            };
            ManualResetEventSlim lineTimedOut = new ManualResetEventSlim();
            lineTimer.Elapsed += (sender, args) => lineTimedOut.Set();

            while (true)
            {

                lineTimedOut.Reset();
                lineIndex = 0;

                lineTimer.Start();
                while (!lineTimedOut.IsSet)
                {
                    if (token.IsCancellationRequested) return;
                    try
                    {
                        byte newByte = (byte)port.ReadByte();
                        lineBuffer[lineIndex++] = newByte;
                        ByteReceivedAction?.Invoke(newByte);
                        if ( lineIndex == bytesPerLine-1 )
                        {
                            break;
                        }
                    }
                    catch (TimeoutException _) { }

                }
                //dispatch on separate thread
                Task.Run(() =>
                {
                    LineReceivedEventHandler.Invoke(port,
                        new LineReadEventArgs(lineBuffer, lineIndex, lineTimedOut.IsSet));
                });

            }


        }
    

        public void Stop()
        {
            cancelToken.Cancel();
        }

    

    }
