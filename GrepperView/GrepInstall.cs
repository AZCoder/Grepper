using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.IO;
using System.Reflection;
using System.Diagnostics;

namespace GrepperView
{
	[RunInstaller(true)]
	public partial class GrepInstall : Installer
	{
		public GrepInstall()
		{
			InitializeComponent();
			this.Committed += GrepInstall_Committed;
		}

		private void GrepInstall_Committed(object sender, InstallEventArgs e)
		{
			try
			{
				Directory.SetCurrentDirectory(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
				string path = String.Format("{0}\\Grepper.exe", Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
				Process.Start(path, "installer");
			}
			catch { }
		}

		public override void Install(System.Collections.IDictionary stateSaver)
		{
			base.Install(stateSaver);
		}

		protected override void OnCommitted(System.Collections.IDictionary savedState)
		{
			base.OnCommitted(savedState);
		}

		public override void Commit(System.Collections.IDictionary savedState)
		{
			base.Commit(savedState);
		}

		public override void Rollback(System.Collections.IDictionary savedState)
		{
			base.Rollback(savedState);
		}
	}
}
