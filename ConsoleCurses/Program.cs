

namespace ConsoleCurses
{
    internal class Program
    {
        private static int i = 0;

        static char[] inputBuff = new char[100];
        static void Main(string[] args)
        {
            Console.ResetColor();
            Thread tr = new Thread(() =>
            {
                consolePrinter();
            });
            tr.Start();

            Thread kr = new Thread(() =>
            {
                keyReader();
            });
            kr.Start();


            while (true)
            {
                
            }
            //Application.Init();

            //Application.Run(new TerminalCurses.Test1());

            //Application.Shutdown();
        }

        static void consolePrinter()
        {
            while (true)
            {

                ClearCurrentConsoleLine();

                Console.WriteLine($"{i++}");
                //Console.BackgroundColor = ConsoleColor.Blue;
                //Console.ForegroundColor = ConsoleColor.White;
                Console.Write(new string(inputBuff,0,index));
                Console.ResetColor();

                Thread.Sleep(1000);
            }
        }

        private static int index = 0;
        static void keyReader()
        {
            while (true)
            {
                try
                {
                    var key = Console.ReadKey();
                    switch (key.Key)
                    {
                        case ConsoleKey.Backspace:
                            index = (index-1)>=0 ? (index-1):0;
                            ClearCurrentConsoleLine();
                            Console.Write(new string(inputBuff, 0, index));

                            break;
                        case ConsoleKey.Enter:
                            Console.WriteLine(new string(inputBuff, 0, index));
                            index = 0;
                            break;
                        default:
                            char c = (char)key.KeyChar;
                            inputBuff[index++] = c;
                            
                            break;
                    }
                   
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }
    }
}