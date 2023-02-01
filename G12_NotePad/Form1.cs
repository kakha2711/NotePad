using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace G12_NotePad
{
	public partial class Form1 : Form
	{
		private string FilePath { get; set; }

		public Form1()
		{
			InitializeComponent();
		}

		private void newToolStripMenuItem_Click(object sender, EventArgs e) => NewFile();

		private void openToolStripMenuItem_Click(object sender, EventArgs e) => OpenFile();

		private void saveToolStripMenuItem_Click(object sender, EventArgs e) => SaveFile(false);

		private void saveAsToolStripMenuItem_Click(object sender, EventArgs e) => SaveFile(true);

		private void exitToolStripMenuItem_Click(object sender, EventArgs e) => Close();

		private void backgroundToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string currentStatusText = lblStatus.Text;
			lblStatus.Text = "Setting text background color...";

			dlgColor.Color = txtContent.BackColor;
			if (dlgColor.ShowDialog() == DialogResult.OK)
			{
				txtContent.BackColor = dlgColor.Color;
			}

			lblStatus.Text = currentStatusText;
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			var tt = txtContent.AcceptsReturn;

			if (txtContent.AcceptsReturn)
				SaveFile(true);

			e.Cancel = false;
		}

		private void NewFile()
		{
			txtContent.Clear();
			FilePath = null;
		}

		private void OpenFile()
		{
			dlgOpen.FileName = string.Empty;
			if (dlgOpen.ShowDialog() == DialogResult.OK)
			{
				try
				{
					txtContent.Text = File.ReadAllText(dlgOpen.FileName);
					FilePath = dlgOpen.FileName;
				}
				catch (Exception exception)
				{
					MessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void SaveFile(bool isSaveAs)
		{
			if (FilePath == null || isSaveAs)
			{
				dlgSave.FileName = FilePath;
				if (dlgSave.ShowDialog() == DialogResult.OK) FilePath = dlgSave.FileName;
				else return;
			}

			try
			{
				File.WriteAllText(FilePath, txtContent.Text);
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

        private void txtContent_TextChanged(object sender, EventArgs e)
        {
			txtContent.AcceptsReturn= true;
        }
    }
}
