using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Automation;
using System.Threading;
using System.Diagnostics;

namespace Talk_VOICEROID
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length == 2)
            {
                if (GetVoiceroid2hWnd() == IntPtr.Zero) { Console.WriteLine("Could not find VOICEROID2 Editor"); return; }
                talk(args[0],args[1]);
            }
            else if (args.Length == 0)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
            else
            {
                callHelp();
            }
        }

        static void callHelp() {
            Console.WriteLine ("Talk-VOICEROID\r\n" +
                "made by mkaraki\r\n" +
                "\r\n" +
                "This proglam is only support for VOICEROID2\r\n" +
                "\r\n" +
                "usage:\r\n" +
                "<This Proglam> <Character> <Message>");
        }

        internal static IntPtr GetVoiceroid2hWnd()
        {
            IntPtr hWnd = IntPtr.Zero;

            string winTitle1 = "VOICEROID2";
            string winTitle2 = winTitle1 + "*";
            int RetryCount = 3;
            int RetryWaitms = 1000;

            for (int i = 0; i < RetryCount; i++)
            {
                Process[] ps = Process.GetProcesses();

                foreach (Process pitem in ps)
                {
                    if ((pitem.MainWindowHandle != IntPtr.Zero) &&
                           ((pitem.MainWindowTitle.Equals(winTitle1)) || (pitem.MainWindowTitle.Equals(winTitle2))))
                    {
                        hWnd = pitem.MainWindowHandle;
                    }
                }
                if (hWnd != IntPtr.Zero) break;
                if (i < (RetryCount - 1)) { Console.WriteLine("Retry..."); Thread.Sleep(RetryWaitms); }
            }

            return hWnd;
        }

        internal static void talk(string tchar,string talkText)
        {
            IntPtr hWnd = GetVoiceroid2hWnd();

            if (hWnd == IntPtr.Zero) return;

            AutomationElement ae = AutomationElement.FromHandle(hWnd);
            TreeScope ts1 = TreeScope.Descendants | TreeScope.Element;
            TreeScope ts2 = TreeScope.Descendants;

            AutomationElement editorWindow = ae.FindFirst(ts1, new PropertyCondition(AutomationElement.ClassNameProperty, "Window"));

            AutomationElement customC = ae.FindFirst(ts1, new PropertyCondition(AutomationElement.AutomationIdProperty, "c"));

            AutomationElement textBox = customC.FindFirst(ts2, new PropertyCondition(AutomationElement.AutomationIdProperty, "TextBox"));
            ValuePattern elem1 = textBox.GetCurrentPattern(ValuePattern.Pattern) as ValuePattern;
            elem1.SetValue(tchar+ "＞" + talkText);

            AutomationElementCollection buttons = customC.FindAll(ts2, new PropertyCondition(AutomationElement.LocalizedControlTypeProperty, "ボタン"));
            InvokePattern elem2 = buttons[4].GetCurrentPattern(InvokePattern.Pattern) as InvokePattern;
            elem2.Invoke();

            object playing = buttons[9].GetCurrentPropertyValue(AutomationElement.IsEnabledProperty);
        }
    }
}
