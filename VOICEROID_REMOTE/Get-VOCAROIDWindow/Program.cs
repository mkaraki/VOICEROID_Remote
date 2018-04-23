using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Get_VOCAROIDWindow
{
    class Program
    {
        static void Main(string[] args)
        {
            Process[] ps = Process.GetProcesses();
            foreach (Process pitem in ps)
            {
                if (pitem.MainWindowHandle != IntPtr.Zero)
                {
                    Console.WriteLine("[{0}:{1}] {2}", pitem.Id, pitem.ProcessName, pitem.MainWindowTitle);
                }
            }
            Console.ReadLine();
        }
    }
}
