using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kino
{
    public partial class mainForm : Form
    {
        Button[] pictureBTN = new Button[3];
        public string selectedFilm;
        public mainForm()
        {
            InitializeComponent();
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            this.Size = new Size(970,500);
            for (int i = 0; i < 3; i++)
            {
                pictureBTN[i] = new Button();
                this.Text = pictureBTN[i].ToString();
                pictureBTN[i].Size = new Size(250, 300);
                if(i == 0)
                {
                    pictureBTN[i].Location = new Point(50, 50);
                    pictureBTN[i].Image = Image.FromFile("../../images/vzaperti.jpg");
                    pictureBTN[i].Name = "vzaperti";
                }
                if (i == 1)
                {
                    pictureBTN[i].Location = new Point(350, 50);
                    pictureBTN[i].Image = Image.FromFile("../../images/mi.jpg");
                    pictureBTN[i].Name = "mi";
                }
                if (i == 2)
                {
                    pictureBTN[i].Location = new Point(650, 50);
                    pictureBTN[i].Image = Image.FromFile("../../images/legenda.jpg");
                    pictureBTN[i].Name = "legenda";
                }
                pictureBTN[i].Click += MainForm_Click;
                this.Controls.Add(pictureBTN[i]);
            }
            //this.Text = "Кинотеатр Appolo";
        }

        private void MainForm_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1(selectedFilm);
            form.ShowDialog();
        }
    }
}
