using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memory
{
    class main
    {
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public partial class App : System.Windows.Application
        {

            /// <summary>
            /// InitializeComponent
            /// </summary>
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void InitializeComponent()
            {
                this.StartupUri = new System.Uri("MainWindow.xaml", System.UriKind.Relative);
            }

            /// <summary>
            /// Application Entry Point.
            /// </summary>
            [System.STAThreadAttribute()]
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public static void Main()
            {
                Memory.App app = new Memory.App();
                app.InitializeComponent();
                app.Run();
            }
        }
    }
}
