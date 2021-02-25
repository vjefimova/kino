using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kino
{
    public partial class mainForm : Form
    {
        SqlConnection connect = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename =|DataDirectory|\AppData\movies.mdf; Integrated Security = True");
        Button[] pictureBTN = new Button[6];
        public string selectedFilm;
        DateTimePicker date;
        string seans;
        public mainForm()
        {
            InitializeComponent();
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            this.Size = new Size(970,850);
            this.BackColor = Color.LightGray;

            date = new DateTimePicker();
            date.Size = new Size(150, 30);
            date.Location = new Point(20, 20);
            Controls.Add(date);
            date.ValueChanged += Date_ValueChanged;

            for (int i = 0; i < 6; i++)
            {
                pictureBTN[i] = new Button();
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
                if (i == 3)
                {
                    pictureBTN[i].Location = new Point(50, 450);
                    pictureBTN[i].Image = Image.FromFile("../../images/fightclub.jpg");
                    pictureBTN[i].Name = "fightclub";
                }
                if (i == 4)
                {
                    pictureBTN[i].Location = new Point(350, 450);
                    pictureBTN[i].Image = Image.FromFile("../../images/greenmile.jpg");
                    pictureBTN[i].Name = "greenmile";
                }
                if (i == 5)
                {
                    pictureBTN[i].Location = new Point(650, 450);
                    pictureBTN[i].Image = Image.FromFile("../../images/pobeg.jpg");
                    pictureBTN[i].Name = "pobeg";
                }
                this.Controls.Add(pictureBTN[i]);
            }
            this.Text = "Кинотеатр Appolo";
            pictureBTN[0].Click += MainForm_Click;
            pictureBTN[1].Click += MainForm_Click1;
            pictureBTN[2].Click += MainForm_Click2;
            pictureBTN[3].Click += MainForm_Click3;
            pictureBTN[4].Click += MainForm_Click4;
            pictureBTN[5].Click += MainForm_Click5;
        }

        private void Date_ValueChanged(object sender, EventArgs e)
        {
            seans = date.Value.ToString("yyyy.MM.dd");

            connect.Open();



            connect.Close();
        }

        private void MainForm_Click5(object sender, EventArgs e)
        {
            selectedFilm = "pobeg";
            openForm1(selectedFilm);
        }

        private void MainForm_Click4(object sender, EventArgs e)
        {
            selectedFilm = "greenmile";
            openForm1(selectedFilm);
        }

        private void MainForm_Click3(object sender, EventArgs e)
        {
            selectedFilm = "fightclub";
            openForm1(selectedFilm);
        }

        private void MainForm_Click2(object sender, EventArgs e)
        {
            selectedFilm = "legenda";
            openForm1(selectedFilm);
        }

        private void MainForm_Click1(object sender, EventArgs e)
        {
            selectedFilm = "mi";
            openForm1(selectedFilm);
        }

        private void MainForm_Click(object sender, EventArgs e)
        {
            selectedFilm = "vzaperti";
            openForm1(selectedFilm);
        }

        private void openForm1(string selectedFilm)
        {
            Form1 form = new Form1(selectedFilm);
            form.ShowDialog();
        }
    }
}
