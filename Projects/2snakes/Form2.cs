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
    public partial class Form2 : Form
    {
        public int newplayers,newbots,newverojatnost,newdimenzii,newusporuvanje;
        public int newbrzina, newovosja, newstones;
        public bool izbrano=false;
        public Form2()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 1;
            comboBox2.SelectedIndex = 1;
            comboBox3.SelectedIndex = 1;
            radioButton2.Select();
            radioButton11.Select();
           
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label7.Text = trackBar1.Value.ToString();
        }

        private void radioButton14_CheckedChanged(object sender, EventArgs e)
        {

        }
     //   98576
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            radioButton8.Checked = true;
            
            if (radioButton1.Checked)
            {
                radioButton9.Enabled = true;
                radioButton5.Enabled=false;
                radioButton7.Enabled=false;
                radioButton6.Enabled=false;
                radioButton8.Enabled = true;
                radioButton8.Checked=true;
            } else
                if (radioButton2.Checked)
                {
                    radioButton9.Enabled = true;
                    radioButton5.Enabled = true;
                     radioButton7.Enabled=false;
                radioButton6.Enabled=false;
                radioButton8.Enabled = true;
                     radioButton8.Checked=true;
                } else
                    if (radioButton3.Checked)
                    {
                        radioButton9.Enabled = false;
                        radioButton5.Enabled = true;
                        radioButton7.Enabled = true;
                        radioButton6.Enabled = false;
                        radioButton8.Enabled = true;
                        radioButton8.Checked = true;
                    }

                    else {
                        radioButton9.Enabled = false;
                        radioButton8.Enabled = false;
                        radioButton7.Enabled = true; 
                        radioButton5.Enabled = true; 
                        radioButton6.Enabled = true;
                        radioButton5.Checked = true;
                    }

        }

        private void label10_Click(object sender, EventArgs e)
        {
        
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            label10.Text = trackBar2.Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked) newplayers = 1;
            else
                if (radioButton2.Checked) newplayers = 2;
                else
                    if (radioButton3.Checked) newplayers = 3;
                    else newplayers = 4;


            if (radioButton9.Checked) newbots = 0;
            else
                if (radioButton8.Checked) newbots = 1;
                else
                    if (radioButton5.Checked) newbots = 2;
                    else
                        if (radioButton7.Checked) newbots = 3;
                        else newbots = 4;

            newusporuvanje = trackBar2.Value;

            newdimenzii = trackBar1.Value;

            newverojatnost = comboBox1.SelectedIndex * 5;

            newovosja = comboBox2.SelectedIndex + 1;

            newstones = comboBox3.SelectedIndex * 5;

            if (radioButton10.Checked) newbrzina = 200;
            else if (radioButton11.Checked) newbrzina = 150;
            else newbrzina = 100;

            izbrano = true;
            this.Close();
           // this.Hide();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
         //   this.Close();
        }
    }
}
