using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using Grepper.ContextMenu;
using GrepperLib.Domain;

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
            IFileDirectory fileDirectory = new FileDirectory();
            string path = fileDirectory.GetPathAtLoadup(args);
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainUI(path));
        }
    }
}
