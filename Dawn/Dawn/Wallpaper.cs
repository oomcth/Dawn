using System;
using System.Diagnostics;
using System.IO;
using ToolBox.Bridge;
using ToolBox.Notification;
using ToolBox.Platform;
using ToolBox.System;

namespace Dawn
{
    public class Wallpaper
    {

        public static INotificationSystem _notificationSystem { get; set; }
        public static IBridgeSystem _bridgeSystem { get; set; }
        public static ShellConfigurator _shell { get; set; }


        public Wallpaper()
        {
        }

        public static void SET(int num)
        {
            _notificationSystem = NotificationSystem.Default;
            switch (OS.GetCurrent())
            {
                case "win":
                    _bridgeSystem = BridgeSystem.Bat;
                    break;
                case "mac":
                case "gnu":
                    _bridgeSystem = BridgeSystem.Bash;
                    break;
            }
            _shell = new ShellConfigurator(_bridgeSystem, _notificationSystem);

            _shell.Term("osascript /Users/mathisscheffler/DAWN/Dawn/Dawn/bin/Debug/netcoreapp3.1/script.scpt /Users/mathisscheffler/DAWN/Dawn/Dawn/bin/Debug/netcoreapp3.1/" + num.ToString() + ".png");

        }
    }
}