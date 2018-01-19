using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.UserSkins;
using DevExpress.Skins;
using DevExpress.LookAndFeel;
using System.Diagnostics;
using System.Reflection;

namespace HospitalDevexpress
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            BonusSkins.Register();
            SkinManager.EnableFormSkins();
            UserLookAndFeel.Default.SetSkinStyle("Visual Studio 2013 Light");
            if (CheckInstance()) Application.Run(new FrmMain());
            else Application.Run(new Compoment.Custom.FrmInfoMessage());
        }
        static bool CheckInstance()
        {
            Process Current = Process.GetCurrentProcess();
            Process[] Proccess = Process.GetProcessesByName(Current.ProcessName);
            foreach (Process var in Proccess)
                if (var.Id != Current.Id)
                    if (Assembly.GetExecutingAssembly().Location.Replace("/", "\\") == Current.MainModule.FileName)
                        if (var.MainWindowHandle != IntPtr.Zero) return false;
            return true;
        }
    }
}
