using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using GrepperLib.Model;

namespace GrepperView
{
    public partial class DirectoryBrowser : Form
    {
        private string _startDirectory;

        public string NodeSelected
        { 
            get;
            set;
        }

        public string StartDirectory
        {
            get
            {
                return _startDirectory;
            }
            set
            {
                _startDirectory = value;
            }
        }

        public string DriveLetter
        {
            get;
            set;
        }

        public DirectoryBrowser()
        {
            _startDirectory = string.Empty;
            InitializeComponent();
        }

        /// <summary>
        /// Based on code by Danny Battison
        /// http://www.dreamincode.net/code/snippet2591.htm
        /// </summary>
        /// <param name="directory"></param>
        /// <param name="node"></param>
        private void PopulateTree(string directory, TreeNode node)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(directory);
                foreach (DirectoryInfo info in dir.GetDirectories())
                {
                    TreeNode t = new TreeNode(info.Name);
                    t.Name = info.Name;
                    t.Tag = info.FullName;
                    node.Nodes.Add(t);
                }
            }
            catch (DirectoryNotFoundException)
            {
            }
            catch (UnauthorizedAccessException)
            {
            }
        }

        /// <summary>
        /// Attempts to find the starting node as determined by the StartDirectory property.
        /// </summary>
        private TreeNode FindStartNode()
        {
            TreeNode lastNode = null;

            // initially populate the root tree
            string[] directories = StartDirectory.Split('\\');
            string rootDirectory = directories[0];
            Regex reg = new Regex("^[a-zA-Z][:]{1}");
            if (reg.Matches(directories[0], 0).Count > 0)
            {
                DriveLetter = string.Format("{0}\\", directories[0]);
                TreeNode tn = new TreeNode();
                tn.Text = rootDirectory;
                PopulateTree(DriveLetter, tn);
                tvDirectoryTree.Nodes.Add(tn);
                tvDirectoryTree.ExpandAll();

                if ((directories.Count() < 2) || (tvDirectoryTree.Nodes.Count < 1)) return tn;

                if (reg.Matches(directories[0], 0).Count > 0)
                    rootDirectory = string.Format("{0}\\{1}", directories[0], directories[1]);

                var baseNode = tvDirectoryTree.Nodes.Find(directories[1], true);
                if (directories.Count() < 3) return baseNode.First();
                if (baseNode.Count() < 1) return null;

                lastNode = baseNode.First();
                int maxNode = directories.Count();
                for (int i = 2; i < maxNode; i++)
                {
                    PopulateTree(rootDirectory, lastNode);
                    var item = lastNode.Nodes.Find(directories[i], true);
                    if (item != null && item.Count() > 0)
                        lastNode = item.First();
                    rootDirectory += string.Format("\\{0}", directories[i]);
                }
            }

            return lastNode;
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void DirectoryTree_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            PopulateTree(e.Node.FullPath, e.Node);
            e.Node.ExpandAll();
        }

        private void OK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void DirectoryTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            NodeSelected = e.Node.Tag != null ? e.Node.Tag.ToString() : DriveLetter;
        }

        private void DirectoryBrowser_Shown(object sender, EventArgs e)
        {
            TreeNode startNode = FindStartNode();
            if (startNode == null)
                return;

            tvDirectoryTree.SelectedNode = startNode;
            tvDirectoryTree.SelectedNode.ExpandAll();
            NodeSelected = DriveLetter;
            if (startNode.Tag != null)
                NodeSelected = startNode.Tag.ToString();
        }
    }
}
