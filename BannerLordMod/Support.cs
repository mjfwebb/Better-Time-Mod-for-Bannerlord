using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.Core;

namespace BetterTime
{
    public static class Support
    {
        public static Settings settings = new Settings();

        public static void LogMessage(string message) => InformationManager.DisplayMessage(new InformationMessage(message));
    }
}
