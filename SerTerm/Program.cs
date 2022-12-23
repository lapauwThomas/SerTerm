using System;
using System.Text;
using System.Text.RegularExpressions;
using DocoptNet;
using NLog;
using NLog.Targets;
using SerTerm.PortManagement;

namespace SerTerm
{


    internal class Program
    {
        private const string Version = "SerTerm 0.1 dev";

        private static int Width => Console.WindowWidth;
        //        const string usage =
        // @"SerTerm
        //    Advanced Serial Terminal. 
        //Usage:
        //  SerTerm.exe <port> <baud> ...
        //  SerTerm.exe <port> --sercfg ...
        //  SerTerm.exe -l | --list
        //  SerTerm.exe -h | --help
        //  SerTerm.exe --version

        //Options:
        //  -h --help                            Show this screen.
        //  -l --list                            List available ports.
        //  -f <filepath> --file <filepath>     Write output to file.
        //  --sercfg <config>                    Serial port configuration (Identical to Putty).
        //  -c                                   Character mode.
        //  -t                                   Add timestamp.  
        //  --version                            Show version.
        //";

        const string usage = @"
Serial terminal with advanced options

Usage:
  SerTerm.exe (-h | --help)
  SerTerm.exe (-l | --list)
  SerTerm.exe (-i | --interactive)
  SerTerm.exe (-j=<path> | --json=<path>)
  SerTerm.exe <port> <baud> [-c -t ]
  SerTerm.exe <port> --sercfg <config> [ -c (-t |-T=<format>) (-x | -X) (-A | -a) -p=<plugin> -f=<path> ]
  SerTerm.exe --settings
  SerTerm.exe --version
  SerTerm.exe -p ?
  SerTerm.exe <session_preset>

Options:
  -i, --interactive                     Interactive mode.
  -l, --list                            List available ports.
  -f <path>, --file <path>              Write output to file.
  -j <path>, --json <path>              Load session from json preset.
  -s <config> , --sercfg <config>       Serial port configuration (Identical to Putty).
  -h --help                             Show this screen.
  --settings                            Open the settings
  --version                             Show version.
-------------------------------------------------------------------------------------------------------------------------------
Display options:
  -c                                    Character mode.
  -t                                    Print timestamp
  -T <format>                           Print formatted timestamp (.net Formatting)
  -x                                    Print hex of each char.
  -X                                    Print hex together with each char.
  -A                                    Ascii w. hex for invisible
  -a                                    Ascii - Hide invisible
  -p <plugin>                           Formatter plugin name (? to list options)

-------------------------------------------------------------------------------------------------------------------------------
Keyboard shortcuts:

Ctrl-C    Exit
Ctrl-Shift-1    Clear session
Ctrl-Shift-9    Open session in Notepad

";
        static int ShowHelp(string help) { Console.WriteLine(help); return 0; }
        static int ShowVersion(string version) { Console.WriteLine(version); return 0; }
        static int OnError(string usage) { Console.Error.WriteLine(usage); return 1; }

        private static Logger logger = LogManager.GetCurrentClassLogger();


        static int Main(string[] args)
        {
            SetupLogging();
            //string[] testArgs = { "--Version" };
            string[] testArgs = new string[] { @"-l" };


            args = testArgs;
            if (args.Length == 0)
            {
                args = new[] { "-h" };
            }
            else
            {

            }

            var parser = Docopt.CreateParser(usage)
                                .WithVersion(Version)
                                .Parse(args)
                                .Match(Run,
                                    result => ShowHelp(result.Help),
                                    result => ShowVersion(result.Version),
                                    result => OnError(result.Usage));


            return parser;
        }

        static int Run(IDictionary<string, ArgValue> arguments)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append("Run started with the following options/args:");
            foreach (var (key, value) in arguments)
                sb.AppendLine($"{key} = {value}");
            logger.Debug(sb);


            if (arguments["--list"].IsTrue)
            {
                return ListPorts();
            }

            if (arguments["-p"].ToString().NormalizeString().Equals("?") )
            {
                logger.Info("Listing plugins");
                Console.WriteLine("Available Plugins: ");
            }





            return 0;




        }

        static void SetupInteractive(IDictionary<string, ArgValue> arguments)
        {

        }

        static int ListPorts()
        {
            var ports = SerialPortSearcher.FindSerialPortsWMI();
            //Console.WriteLine("\n\n");
            //Console.WriteLine(new String('-', Width));
            Console.WriteLine("Available Ports: \n");
            Console.Write("Port");
            var pos = Console.GetCursorPosition();
            Console.SetCursorPosition(pos.Left + 8, pos.Top);
            Console.Write("|  ");
            Console.Write("Description");
            Console.WriteLine();
            Console.WriteLine(new String('-', 80));
            foreach (var port in ports)
            {
                Console.Write(port.Key);
                pos = Console.GetCursorPosition();
                Console.SetCursorPosition(pos.Left + 8, pos.Top);
                Console.Write("|  ");
                Console.Write(port.Value.DisplayString);
                Console.WriteLine();
            }


            return 0;
        }
        static void SetupLogging()
        {
            NetworkTarget target = new NetworkTarget()
            {
                Layout = "${longdate}|${level:uppercase=true}|${callsite}--${callsite-linenumber}|${message}",
                LineEnding = LineEndingMode.CRLF,
                Address = "tcp://127.0.0.1:12345",
                KeepConnection = true
            };

            var config = new NLog.Config.LoggingConfiguration();
            config.AddRule(LogLevel.Trace, LogLevel.Fatal, target);

            // Apply config           
            NLog.LogManager.Configuration = config;


        }


    }

    public static class DocOptHelpers
    {
        public static string NormalizeString(this string str)
        {
            return str.Trim().Trim('\"');
        }

    }
}