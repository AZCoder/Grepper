using System;
using System.Windows.Forms;
using GrepperLib.Domain;

namespace GrepperView
{
    internal static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		private static void Main(string[] args)
        {
            // add context menu if it does not exist
            ContextMenu.ContextMenu.AddContextMenu(Application.ExecutablePath);
            IFileDirectory fileDirectory = new FileDirectory();
            string path = fileDirectory.GetPathAtLoadup(args);
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainUI(path));
        }
    }
}
