using System;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace GrepperView
{
    public partial class DirectoryBrowser : Form
    {
        public string NodeSelected { get; set; }

        public string StartDirectory { get; set; }

        public string DriveLetter { get; set; }

        public DirectoryBrowser()
        {
            StartDirectory = string.Empty;
            InitializeComponent();
        }

        /// <summary>
        /// Based on code by Danny Battison
        /// http://www.dreamincode.net/code/snippet2591.htm
        /// </summary>
        /// <param name="directory"></param>
        /// <param name="node"></param>
        private static void PopulateTree(string directory, TreeNode node)
        {
            try
            {
                var dir = new DirectoryInfo(directory);
                foreach (var info in dir.GetDirectories())
                {
                    var t = new TreeNode(info.Name)
                    {
                        Name = info.Name,
                        Tag = info.FullName
                    };
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
            // initially populate the root tree
            var directories = StartDirectory.Split('\\');
            var rootDirectory = directories[0];
            var reg = new Regex("^[a-zA-Z][:]{1}");
            if (reg.Matches(directories[0], 0).Count <= 0) return null;

            DriveLetter = $"{directories[0]}\\";
            var tn = new TreeNode {Text = rootDirectory};
            PopulateTree(DriveLetter, tn);
            tvDirectoryTree.Nodes.Add(tn);
            tvDirectoryTree.ExpandAll();

            if ((directories.Count() < 2) || (tvDirectoryTree.Nodes.Count < 1)) return tn;

            if (reg.Matches(directories[0], 0).Count > 0)
                rootDirectory = $"{directories[0]}\\{directories[1]}";

            var baseNode = tvDirectoryTree.Nodes.Find(directories[1], true);
            if (directories.Count() < 3) return baseNode.First();
            if (!baseNode.Any()) return null;

            var lastNode = baseNode.First();
            var maxNode = directories.Length;
            for (var i = 2; i < maxNode; i++)
            {
                PopulateTree(rootDirectory, lastNode);
                var item = lastNode.Nodes.Find(directories[i], true);
                if (item.Any())
                    lastNode = item.First();
                rootDirectory += $"\\{directories[i]}";
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
            NodeSelected = e.Node.Tag?.ToString() ?? DriveLetter;
        }

        private void DirectoryBrowser_Shown(object sender, EventArgs e)
        {
            var startNode = FindStartNode();
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