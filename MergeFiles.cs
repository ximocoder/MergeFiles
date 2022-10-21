using System.Collections.Generic;

namespace MergeFiles
{
    public partial class MergeFiles : Form
    {
        List<string> files = new List<string>();

        public MergeFiles()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Stream st1;
            this.richTextBox1.Text = String.Empty;

            this.openFileDialog1.Multiselect = true;
            this.openFileDialog1.ShowHelp = false;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                foreach (String file in openFileDialog1.FileNames)
                {
                    try
                    {
                        if ((st1 = openFileDialog1.OpenFile()) != null)
                        {
                            using (st1)
                            {
                                files.Add(file);
                                this.richTextBox1.Text += "file " + file + " added" + Environment.NewLine;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Text += "Merging.... " + Environment.NewLine + Environment.NewLine;

            using (Stream destStream = File.OpenWrite(files.FirstOrDefault() + ".merged"))
            {
                foreach (string srcFileName in files)
                {
                    using (Stream srcStream = File.OpenRead(srcFileName))
                    {
                        srcStream.CopyTo(destStream);
                        this.richTextBox1.Text += "file " + srcFileName + " merged" + Environment.NewLine;
                    }
                }
            }
        }
    }
}