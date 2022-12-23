using System.IO.Ports;
using System.Text;

namespace SerTerm;


    internal class LineReader: ISerialPortReader
{
        private Thread _thread;
        private CancellationTokenSource cancelToken;
        private CancellationToken token => cancelToken.Token;
        private SerialPort port;

        public event EventHandler<LineReadEventArgs> LineReceivedEventHandler;

        private int lineTimeout;

        private byte[] lineBuffer;
        private int lineIndex;

        private bool StripNewLine = true;

        public string NewLine { get; set; } = "\n";

        protected LineReader(int timeOutMillis, SerialPort port, int buffersize = 200)
        {
            lineBuffer = new byte[buffersize];
            this.port = port;
            lineTimeout = timeOutMillis;
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
                        if (ChecklineEnd(lineBuffer,lineIndex))
                        {
                            if (StripNewLine)
                            {
                                lineIndex -= NewLine.Length;
                            }
                            break;
                        }
                    }
                    catch (TimeoutException ex)
                    {
                    }



                }
                //dispatch on separate thread
                Task.Run(() =>
                {
                    LineReceivedEventHandler.Invoke(port,
                        new LineReadEventArgs(lineBuffer, lineIndex, lineTimedOut.IsSet));
                });

            }


        }

        private bool ChecklineEnd(byte[] buffer, int lineIndex)
        {
            for (int i = 0; i < NewLine.Length; i--) //read backwards over Newline array and buffer
            {
                if (buffer[(lineIndex - 1) - i] != NewLine[(NewLine.Length-1) - i]) return false; //return false on character mismatch
            }
            return true;
        }


        public void Stop()
        {
            cancelToken.Cancel();
        }


       

    }
