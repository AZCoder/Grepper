using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Grepper.ContextMenu;

namespace GrepperView
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			// add context menu if it does not exist
			RegistrySettings.AddContextMenu(Application.ExecutablePath);

			// set default search path if no args passed
            string path;

			// if string argument passed then set it as the path
            if (args.Count() > 0)
            {
                if (args[0].StartsWith("-p"))
                    path = args[0].Substring(2);
                else
                    path = args[0];
            }
            else
            {
                path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }

            Regex reg = new Regex("^[a-zA-Z][:]{1}");
            // right click on root of a drive causes a double-quote such as --> C:"
            if (reg.Matches(path.Substring(0, 2)).Count > 0)
                if (path.Substring(2,1) == "\"")
                    path = string.Format("{0}\\", path.Substring(0, 2));
            if (path == null) path = @"C:\";

			// exit if invoked by installer
			if ((path != null) && (path != "installer"))
			{
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Application.Run(new MainUI(path));
			}
		}
	}
}
