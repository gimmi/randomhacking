using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using System.IO;

namespace SpikeCefSharp
{
    public partial class MainForm : Form
    {
        private readonly Backend _backend;
        private ChromiumWebBrowser _chromeBrowser;

        public MainForm(Backend backend)
        {
            _backend = backend;

            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var cefSettings = new CefSettings();

            Cef.Initialize(cefSettings);

            var indexPath = Path.Combine(Application.StartupPath, "html-resources", "index.html");

            _chromeBrowser = new ChromiumWebBrowser(indexPath) {
                Dock = DockStyle.Fill
            };
            _chromeBrowser.IsBrowserInitializedChanged += ChromeBrowser_IsBrowserInitializedChanged;
            _chromeBrowser.RegisterAsyncJsObject("Backend", _backend);

            Controls.Add(_chromeBrowser);
        }

        private void ChromeBrowser_IsBrowserInitializedChanged(object sender, IsBrowserInitializedChangedEventArgs e)
        {
            if (e.IsBrowserInitialized)
            {
                _chromeBrowser.ShowDevTools();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cef.Shutdown();
        }
    }
}
