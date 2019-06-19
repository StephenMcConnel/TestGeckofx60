using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Gecko;
using System.Drawing;

namespace TestGeckofx60
{
    static public class Program
    {
        [STAThread]
        static public void Main(string[] args)
        {
			var firefoxDir = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase.Replace("file://", "")), "Firefox");
            Xpcom.Initialize(firefoxDir);
			GeckoPreferences.User["media.video-queue.default-size"] = 1;
            var gwb = new GeckoWebBrowser {Dock=DockStyle.Fill};
			var f = new Form { Size = new Size(700, 550)};
            f.Controls.Add(gwb);
			var filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase.Replace("/bin/Debug","")), "html","Test.html");
            gwb.Navigate(filePath);
            Application.Run(f);
        }
    }
}
