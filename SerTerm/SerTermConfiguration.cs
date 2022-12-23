using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerTerm
{
    internal class SerTermConfiguration
    {

        public bool StripEscapecodesInLogfiles = true;

        public bool StripAllEscapeCodes = false;

        public bool AllowColorConsole = true;

        public bool ExitOnDisconnect = false;

        public bool AllowSessionRestart = true;

        public string DefaultFileOpenMode = "a+";

        public bool AutoSaveSessions = false;

        public string SessionFolder = "";

        public string PluginFolder = "./Plugins";

        public string InputForegroundColorHEX = @"#3377FF";
        public string InputBackgroundColorHEX = @"#3377FF";
        public Color InputForegroundColor => System.Drawing.ColorTranslator.FromHtml(InputForegroundColorHEX);
        public Color InputBackgroundColor => System.Drawing.ColorTranslator.FromHtml(InputBackgroundColorHEX);

        public string DefaultLineEnding = "\n";

        public int DefaultLineTimeout = 500;

        public string TimeStampFormat = "hh:mm:ss.fff";

    }
}
