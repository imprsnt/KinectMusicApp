using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class AdjustDepth : Form
    {
        public AdjustDepth()
        {
            InitializeComponent();
        }

        public void setMusicPlayer(musicPlayer _musicPlayer)
        {
            this._musicPlayer = _musicPlayer;
        }

        private void modifyCheckBox1_CheckedChanged(object sender, EventArgs e)
        {

            if((sender as CheckBox).Checked)
               this.minTextBox.Enabled = true;
            else
                this.minTextBox.Enabled = false;
        }

        private void modifyCheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            if((sender as CheckBox).Checked)
                this.maxTextBox.Enabled = true;
            else
                this.maxTextBox.Enabled = false;
        }
    }
}
