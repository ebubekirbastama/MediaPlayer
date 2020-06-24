using System;
using System.Windows.Forms;

namespace MediaPlayer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        OpenFileDialog op;

        private void media_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            if (media.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                progressBar1.Maximum = (int)media.Ctlcontrols.currentItem.duration;
                timer1.Start();
            }
            else if (media.playState == WMPLib.WMPPlayState.wmppsPaused)
            {
                timer1.Stop();
            }
            else if (media.playState == WMPLib.WMPPlayState.wmppsStopped)
            {
                timer1.Stop();
                progressBar1.Value = 0;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (media.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                progressBar1.Value = (int)media.Ctlcontrols.currentPosition;
            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                op = new OpenFileDialog();
                op.Filter = "Media File(*.mpg,*.dat,*.avi,*.wmv,*.wav,*.mp3)|*.wav;*.mp3;*.mpg;*.dat;*.avi;*.wmv";
                if (op.ShowDialog() == DialogResult.OK)
                {
                    media.URL = op.FileName.ToString();
                    for (int i = 0; i < op.FileNames.Length; i++)
                    {
                        listBox1.Items.Add(op.FileNames[i].ToString());
                    }
                }
                else
                {
                    MessageBox.Show("Müzik Seçilmedi..");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
