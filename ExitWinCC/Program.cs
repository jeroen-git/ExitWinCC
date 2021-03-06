using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

using DMCLIENT;
using OHIOAPI;
using DMDEFS;
using NOTIFY.DM;
namespace ExitWinCC
{
    class Program
    {
        private static DMClient Client;

        static void Main(string[] args)
        {
            CMN_ERROR Error;
            Error = new CMN_ERROR();
            Client = new DMClient(DMNotifyTrigger);
            Client.Connect("ExitWinCC", Error);

            System.Console.WriteLine("ExitWinCC 20140318");

            /*
            public enum DM_SDMODE_ENUM
            {
                DM_SDMODE_SYSTEM = 0, // The operating system is shut down after closing WinCC.
                DM_SDMODE_WINCC = 1, // WinCC is shut down
                DM_SDMODE_LOGOFF = 2, //  After closing WinCC, the user is also logged off on operating system level.
                DM_SDMODE_REBOOT = 3 // The operating system is rebooted after the shutdown of WinCC.
            }
             */

            int nMode = -1;
            if (args.Length > 0)
                if (!int.TryParse(args[0], out nMode))
                {
                    nMode = -1;
                }
                

            //No arguments supplied
            if (args.Length == 0)
            {
                DMClient.DMExitWinCCEx(DM_SDMODE_ENUM.DM_SDMODE_WINCC);
                System.Console.WriteLine("Shutdown mode: DM_SDMODE_WINCC");
            }
            else if (nMode == 0)
            {
                DMClient.DMExitWinCCEx(DM_SDMODE_ENUM.DM_SDMODE_SYSTEM);
                System.Console.WriteLine("Shutdown mode: DM_SDMODE_SYSTEM");
            }
            else if (nMode == 1)
            {
                DMClient.DMExitWinCCEx(DM_SDMODE_ENUM.DM_SDMODE_WINCC);
                System.Console.WriteLine("Shutdown mode: DM_SDMODE_WINCC");
            }
            else if (nMode == 2)
            {
                DMClient.DMExitWinCCEx(DM_SDMODE_ENUM.DM_SDMODE_LOGOFF);
                System.Console.WriteLine("Shutdown mode: DM_SDMODE_LOGOFF");
            }
            else if (nMode == 3)
            {
                DMClient.DMExitWinCCEx(DM_SDMODE_ENUM.DM_SDMODE_REBOOT);
                System.Console.WriteLine("Shutdown mode: DM_SDMODE_REBOOT");
            }
            else
            {
                System.Console.WriteLine("use ExitWinCC with argument:");
                System.Console.WriteLine("0: The operating system is shut down after closing WinCC.");
                System.Console.WriteLine("1: WinCC is shut down");
                System.Console.WriteLine("2: After closing WinCC, the user is also logged off on OS level.");
                System.Console.WriteLine("3: The operating system is rebooted after the shutdown of WinCC.");
            }


            Client.Disconnect();

        }


        private static void DMNotifyTrigger(object sender, DMNotifyEventArgs e)
        {
            DM_NOTIFYCLASS_ENUM dwNotifyClass = DM_NOTIFYCLASS_ENUM.NO_NOTIFY_CLASS;
            DM_NOTIFYCODE_ERROR_ENUM dwNotifyCodeError = DM_NOTIFYCODE_ERROR_ENUM.NO_NOTIFY_CODE;
            DM_NOTIFYCODE_WARNING_ENUM dwNotifyCodeWarning = DM_NOTIFYCODE_WARNING_ENUM.NO_NOTIFY_CODE;
            DM_NOTIFYCODE_DATA_ENUM dwNotifyCodeData = DM_NOTIFYCODE_DATA_ENUM.NO_NOTIFY_CODE;

            String sNotify = "\rNotify[";

            dwNotifyClass = (DM_NOTIFYCLASS_ENUM)e.NotifyClass;

            switch (dwNotifyClass)
            {
                case DM_NOTIFYCLASS_ENUM.NO_NOTIFY_CLASS:
                    sNotify += "NO_CLASS ???: ";
                    break;
                case DM_NOTIFYCLASS_ENUM.DM_NOTIFY_CLASS_ERROR:
                    sNotify += "ERROR: ";
                    dwNotifyCodeError = (DM_NOTIFYCODE_ERROR_ENUM)e.NotifyCode;
                    break;
                case DM_NOTIFYCLASS_ENUM.DM_NOTIFY_CLASS_WARNING:
                    sNotify += "WARNING: ";
                    dwNotifyCodeWarning = (DM_NOTIFYCODE_WARNING_ENUM)e.NotifyCode;
                    break;
                case DM_NOTIFYCLASS_ENUM.DM_NOTIFY_CLASS_DATA:
                    sNotify += "DATA: ";
                    dwNotifyCodeData = (DM_NOTIFYCODE_DATA_ENUM)e.NotifyCode;
                    break;
                default:
                    sNotify += "(unknown class " + e.NotifyClass.ToString() + "/" + dwNotifyClass.ToString() + " ???): ";
                    break;
            }

            switch (dwNotifyCodeError)
            {
                case DM_NOTIFYCODE_ERROR_ENUM.NO_NOTIFY_CODE:
                    //sNotify += "NO_CODE ???";
                    break;
                case DM_NOTIFYCODE_ERROR_ENUM.DM_NOTIFY_SHUTDOWN:
                    sNotify += "shutdown";
                    break;
                case DM_NOTIFYCODE_ERROR_ENUM.DM_NOTIFY_PROCESSNET_ERROR:
                    sNotify += "processnet error";
                    break;
                case DM_NOTIFYCODE_ERROR_ENUM.DM_NOTIFY_SYSNET_ERROR:
                    sNotify += "sysnet error";
                    break;
                default:
                    sNotify += "(unknown error code " + e.NotifyCode.ToString() + "/" + dwNotifyCodeError.ToString() + " ???)";
                    break;
            }

            String sProject;

            switch (dwNotifyCodeWarning)
            {
                case DM_NOTIFYCODE_WARNING_ENUM.NO_NOTIFY_CODE:
                    //sNotify += "NO_CODE ???";
                    break;
                case DM_NOTIFYCODE_WARNING_ENUM.DM_NOTIFY_QUEUE_50_PERCENT:
                    sNotify += "queue 50";
                    break;
                case DM_NOTIFYCODE_WARNING_ENUM.DM_NOTIFY_QUEUE_60_PERCENT:
                    sNotify += "queue 60";
                    break;
                case DM_NOTIFYCODE_WARNING_ENUM.DM_NOTIFY_QUEUE_70_PERCENT:
                    sNotify += "queue 70";
                    break;
                case DM_NOTIFYCODE_WARNING_ENUM.DM_NOTIFY_QUEUE_80_PERCENT:
                    sNotify += "queue 80";
                    break;
                case DM_NOTIFYCODE_WARNING_ENUM.DM_NOTIFY_QUEUE_90_PERCENT:
                    sNotify += "queue 90";
                    break;
                case DM_NOTIFYCODE_WARNING_ENUM.DM_NOTIFY_QUEUE_OVERFLOW:
                    sNotify += "queue overflow";
                    break;
                case DM_NOTIFYCODE_WARNING_ENUM.DM_NOTIFY_CYCLES_CHANGED:
                    sNotify += "cycles changed";
                    break;
                case DM_NOTIFYCODE_WARNING_ENUM.DM_NOTIFY_MACHINES_CHANGED:
                    sNotify += "machines changed";
                    break;
                case DM_NOTIFYCODE_WARNING_ENUM.DM_NOTIFY_PROJECT_OPENED:
                    sProject = Marshal.PtrToStringUni(e.ByData);
                    sNotify += "project opened " + sProject;
                    break;
                case DM_NOTIFYCODE_WARNING_ENUM.DM_NOTIFY_PROJECT_CLOSED:
                    sProject = Marshal.PtrToStringUni(e.ByData);
                    sNotify += "project closed " + sProject;
                    break;
                case DM_NOTIFYCODE_WARNING_ENUM.DM_NOTIFY_SYSTEM_LOCALE:
                    UInt32 dwSLocale = (UInt32)Marshal.ReadInt32(e.ByData);
                    sNotify += "system locale " + dwSLocale.ToString();
                    break;
                case DM_NOTIFYCODE_WARNING_ENUM.DM_NOTIFY_DATA_LOCALE:
                    UInt32 dwDLocale = (UInt32)Marshal.ReadInt32(e.ByData);
                    sNotify += "data locale " + dwDLocale.ToString();
                    break;
                case DM_NOTIFYCODE_WARNING_ENUM.DM_NOTIFY_PROJECT_RUNTIME:
                    sProject = Marshal.PtrToStringUni(e.ByData);
                    sNotify += "project runtime " + sProject;
                    break;
                case DM_NOTIFYCODE_WARNING_ENUM.DM_NOTIFY_PROJECT_EDIT:
                    sProject = Marshal.PtrToStringUni(e.ByData);
                    sNotify += "project edit " + sProject;
                    break;
                case DM_NOTIFYCODE_WARNING_ENUM.DM_NOTIFY_HOTKEY_CHANGE:
                    sNotify += "hotkey change";
                    break;
                case DM_NOTIFYCODE_WARNING_ENUM.DM_NOTIFY_URSEL:
                    sNotify += "URSEL";
                    break;
                case DM_NOTIFYCODE_WARNING_ENUM.DM_NOTIFY_RT_LIC:
                    sNotify += "RT_LIC";
                    break;
                case DM_NOTIFYCODE_WARNING_ENUM.DM_NOTIFY_CS_LIC:
                    sNotify += "CS_LIC";
                    break;
                case DM_NOTIFYCODE_WARNING_ENUM.DM_NOTIFY_SERVERDOWN:
                    sNotify += "server down";
                    break;
                case DM_NOTIFYCODE_WARNING_ENUM.DM_NOTIFY_SERVERUP:
                    sNotify += "server up";
                    break;
                case DM_NOTIFYCODE_WARNING_ENUM.DM_NOTIFY_BODO:
                    sNotify += "BODO";
                    break;
                case DM_NOTIFYCODE_WARNING_ENUM.DM_NOTIFY_BEGIN_PROJECT_EDIT:
                    sProject = Marshal.PtrToStringUni(e.ByData);
                    sNotify += "begin project edit " + sProject;
                    break;
                default:
                    sNotify += "(unknown warning code " + e.NotifyCode.ToString() + "/" + dwNotifyCodeWarning.ToString() + " ???)";
                    break;
            }

            switch (dwNotifyCodeData)
            {
                case DM_NOTIFYCODE_DATA_ENUM.NO_NOTIFY_CODE:
                    //sNotify += "NO_CODE ???";
                    break;
                case DM_NOTIFYCODE_DATA_ENUM.DM_NOTIFY_APPLICATION_DATA:
                    sNotify += "application data";
                    break;
                case DM_NOTIFYCODE_DATA_ENUM.DM_NOTIFY_VARIABLE_DATA:
                    sNotify += "variable data";
                    break;
                default:
                    sNotify += "(unknown data code " + e.NotifyCode.ToString() + "/" + dwNotifyCodeData.ToString() + " ???)";
                    break;
            }

            sNotify += "]\r";

            System.Console.WriteLine(sNotify);
        }


    }
}
