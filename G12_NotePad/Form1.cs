using System;
using System.IO;
using System.Windows.Forms;

namespace G12_NotePad
{
	public partial class Form1 : Form
	{
		private string FilePath { get; set; }
		string copyText;


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

        private void cutToolStripButton_Click(object sender, EventArgs e)
        {
			string text = txtContent.SelectedText;

			int length = txtContent.SelectionLength;

			int indeqsiDackeba = txtContent.SelectionStart;

			string darcheniliTeqsti = txtContent.Text.Remove(indeqsiDackeba, length);

            txtContent.Text = darcheniliTeqsti;

        }

        private void copyToolStripButton_Click(object sender, EventArgs e)
        {
			copyText = txtContent.SelectedText;
        }

        private void pasteToolStripButton_Click(object sender, EventArgs e)
        {
			int index = txtContent.SelectionStart;

			txtContent.Text = txtContent.Text.Insert(index, copyText);

        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtContent.Undo();

        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
			cutToolStripButton_Click(sender, e);
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            copyToolStripButton_Click(sender, e);
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pasteToolStripButton_Click(sender, e);
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtContent.SelectAll();
        }
    }
}
