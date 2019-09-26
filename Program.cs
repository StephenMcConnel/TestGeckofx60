﻿using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Gecko;
using System.Drawing;

namespace TestGeckofx60
{
    static public class Program
    {
		static GeckoWebBrowser _browser;
		static Label _label;
		static string _urlFolder;
		static string _currentPage = "Test0.html";
		static int _count;

        [STAThread]
        static public void Main(string[] args)
		{
			InitializeGeckofx();
			_urlFolder = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase), "html");
			using (var form = InitializeControls())
			{
				Application.Run(form);
			}
			MemoryManagement.CheckMemory("Program finished");
		}

		private static Form InitializeControls()
		{
			_browser = new GeckoWebBrowser { Dock = DockStyle.Fill };
			var button = new Button
			{
				Location = new Point(600, 5),
				Size = new Size(70, 20),
				Anchor = AnchorStyles.Bottom | AnchorStyles.Right,
				Text = "Switch"
			};
			button.Click += ButtonClicked;
			_label = new Label
			{
				Location = new Point(10, 5),
				Anchor = AnchorStyles.Bottom | AnchorStyles.Left,
				Text = "Unset",
				AutoSize = true
			};
			var panel = new Panel
			{
				Size = new Size(675, 30),
				Dock = DockStyle.Bottom
			};
			panel.Controls.Add(_label);
			panel.Controls.Add(button);
			var f = new Form { Size = new Size(700, 550) };
			f.Controls.Add(_browser);
			f.Controls.Add(panel);
			MemoryManagement.CheckMemory("Controls created", false);
			Navigate();
			return f;
		}

		private static void InitializeGeckofx()
		{
			var firefoxDir = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase.Replace("file://", "")), "Firefox");
			Xpcom.Initialize(firefoxDir);

			// Settings used in Bloom (https://github.com/BloomBooks/BloomDesktop).  More comments on them there.
			GeckoPreferences.User["network.proxy.http"] = string.Empty;
			GeckoPreferences.User["network.proxy.http_port"] = 80;
			GeckoPreferences.User["network.proxy.type"] = 1; // 0 = direct (uses system settings on Windows), 1 = manual configuration
			GeckoPreferences.User["memory.free_dirty_pages"] = true;
			// Do NOT set this to zero. Somehow that disables following hyperlinks within a document
			GeckoPreferences.User["browser.sessionhistory.max_entries"] = 1;
			GeckoPreferences.User["browser.sessionhistory.max_total_viewers"] = 0;
			GeckoPreferences.User["browser.cache.memory.enable"] = false;
			GeckoPreferences.User["image.mem.max_decoded_image_kb"] = 40960;        // 40MB (default = 256000 == 250MB)
			GeckoPreferences.User["javascript.options.mem.max"] = 40960;            // 40MB (default = -1 == automatic)
			GeckoPreferences.User["javascript.options.mem.high_water_mark"] = 20;   // 20MB (default = 128 == 128MB)
			if (Platform.IsLinux)
				GeckoPreferences.User["image.mem.surfacecache.max_size_kb"] = 102400;   // 100MB
			else
				GeckoPreferences.User["image.mem.surfacecache.max_size_kb"] = 40960;    // 40MB
			GeckoPreferences.User["image.mem.surfacecache.min_expiration_ms"] = 500;    // 500ms (default = 60000 == 60sec)
			GeckoPreferences.User["browser.cache.memory.capacity"] = 0;             // 0 disables feature
			GeckoPreferences.User["network.http.max-persistent-connections-per-server"] = 200;
			GeckoPreferences.User["network.http.pipelining.maxrequests"] = 200;
			GeckoPreferences.User["network.http.pipelining.max-optimistic-requests"] = 200;
			GeckoPreferences.User["gfx.font_rendering.graphite.enabled"] = true;
			GeckoPreferences.User["mousewheel.with_control.action"] = 0;
			GeckoPreferences.User["media.navigator.enabled"] = true;
			GeckoPreferences.User["media.navigator.permission.disabled"] = true;
			if (Platform.IsLinux)
				GeckoPreferences.User["layers.acceleration.force-enabled"] = true;
			GeckoPreferences.User["browser.zoom.full"] = true;
			GeckoPreferences.User["layout.spellcheckDefault"] = 0;
		}

		static void ButtonClicked(object sender, EventArgs e)
		{
			switch (_currentPage)
			{
				case "Test0.html":  _currentPage = "Test1.html"; break;
				case "Test1.html": _currentPage = "Test2.html"; break;
				case "Test2.html": _currentPage = "Test3.html"; break;
				case "Test3.html": _currentPage = "Test4.html"; break;
				case "Test4.html": _currentPage = "Test5.html"; break;
				case "Test5.html": _currentPage = "Test6.html"; break;
				case "Test6.html": _currentPage = "Test7.html"; break;
				case "Test7.html": _currentPage = "Test8.html"; break;
				case "Test8.html": _currentPage = "tictactoe/index.html"; break;
				case "tictactoe/index.html": _currentPage = "Test0.html"; break;
			}
			Navigate();
		}

		static void Navigate()
		{
			var url = _urlFolder + "/" + _currentPage;
			_browser.DocumentCompleted += WebBrowser_ReadyStateChanged;
			_browser.ReadyStateChange += WebBrowser_ReadyStateChanged;
			_browser.Navigate(url);
			++_count;
			_label.Text = _count == 1 ? "1 page seen" : String.Format("{0} pages seen", _count);
		}

		static void WebBrowser_ReadyStateChanged(object sender, EventArgs e)
		{
			if (_browser.Document.ReadyState != "complete")
				return; // Keep receiving until it is complete.
			_browser.ReadyStateChange -= WebBrowser_ReadyStateChanged; // just do this once per navigation
			_browser.DocumentCompleted -= WebBrowser_ReadyStateChanged;
			MemoryManagement.CheckMemory(String.Format("{0} loaded", _currentPage), false);
		}
	}
}
