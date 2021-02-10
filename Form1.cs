using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kino
{
    public partial class Form1 : Form
    {
        SqlConnection connect = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename =C:\Users\opilane\source\repos\Jefimova\cinema\kino\AppData\movies.mdf; Integrated Security = True");
        SqlCommand command;
        SqlDataAdapter adapter;
        public string film;
        private int Id = 0;
        Label info_film, lblYear, freeSeat, priceTicket;
        DateTimePicker datetime_;
        ListBox time;
        Button nextBtn;
        DataGridView data_;

        public Form1(string selectedFilm)
        {
            InitializeComponent();
            film = selectedFilm;
        }
        Label[,] _arr = new Label[4, 4];
        Label[] read = new Label[4];
        Button osta;
        
        private void Osta_Click(object sender, EventArgs e)
        {
            var vastus = MessageBox.Show("Kas oled kindel?",
                "Appolo küsib", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (vastus == DialogResult.Yes)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (_arr[i, j].BackColor == Color.LightGreen)
                        {
                            _arr[i, j].BackColor = Color.Transparent;
                            _arr[i, j].Image = Image.FromFile("../../images/red.png");
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (_arr[i, j].BackColor == Color.LightGreen)
                        {
                            _arr[i, j].Text = " Koht" + (j + 1);
                            _arr[i, j].Image = Image.FromFile("../../images/free2.png");
                        }
                    }
                }
            }

        }

        void Form1_Click(object sender, EventArgs e)
        {
            var label = (Label)sender;//запомникли на какую надпись нажали
            var tag = (int[])label.Tag;//определили координаты надписи

            if (_arr[tag[0], tag[1]].Text != "Kinni")
            {
                _arr[tag[0], tag[1]].Text = "Kinni";
                _arr[tag[0], tag[1]].BackColor = Color.LightGreen;
                _arr[tag[0], tag[1]].Image = Image.FromFile("../../images/vibrano.png");
            }
            else
            {
                MessageBox.Show("Koht " + (tag[0] + 1) + (tag[1] + 1) + " juba ostetud!");
            }


        }

        private void seats_load()
        {
            for (int i = 0; i < 4; i++)
            {
                read[i] = new Label();
                read[i].Text = "Rida " + (i + 1);
                read[i].Size = new Size(100, 100);
                read[i].Location = new Point(1, i * 100);
                this.Controls.Add(read[i]);
                for (int j = 0; j < 4; j++)
                {
                    _arr[i, j] = new Label();
                    _arr[i, j].Size = new Size(100, 100);
                    _arr[i, j].Image = Image.FromFile("../../images/free2.png");
                    _arr[i, j].BorderStyle = BorderStyle.Fixed3D;
                    _arr[i, j].Location = new Point(j * 100 + 100, i * 100);
                    this.Controls.Add(_arr[i, j]);
                    _arr[i, j].Tag = new int[] { i, j };
                    _arr[i, j].Click += new System.EventHandler(Form1_Click);
                }
            }
            osta = new Button();
            osta.Text = "Osta";
            osta.Location = new Point(500, 200);
            this.Controls.Add(osta);
            osta.Click += Osta_Click;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            seats_load();
            for (int i = 0; i < 4; i++)
            {
                read[i].Hide();
                for (int j = 0; j < 4; j++)
                {
                    _arr[i, j].Hide();
                }
            }
            osta.Hide();

            data_ = new DataGridView();
            data_.Hide();

            info_film = new Label
            {
                Text = "Информация о сеансе"
            };
            info_film.Size = new Size(150, 30);
            info_film.Location = new Point(150,25);

            lblYear = new Label();
            lblYear.Text = "label";
            lblYear.Size = new Size(150,30);
            lblYear.Location = new Point(150, 60);

            freeSeat = new Label();
            freeSeat.Text = "label";
            freeSeat.Size = new Size(150, 30);
            freeSeat.Location = new Point(150, 90);

            priceTicket = new Label();
            priceTicket.Text = "label";
            priceTicket.Size = new Size(150, 30);
            priceTicket.Location = new Point(150, 120);

            datetime_ = new DateTimePicker();
            datetime_.Size = new Size(150,30);
            datetime_.Location = new Point(160,150);

            time = new ListBox();
            time.Size = new Size(150,150);
            time.Location = new Point(160,180);
            time.Items.Add("13.00-14.50");
            time.Items.Add("15.00-17.50");

            nextBtn = new Button();
            nextBtn.Text = "Далее";
            nextBtn.Size = new Size(80,30);
            nextBtn.Location = new Point(160,340);
            nextBtn.Click += NextBtn_Click;

            Controls.Add(info_film);
            Controls.Add(datetime_);
            Controls.Add(time);
            Controls.Add(nextBtn);
            Controls.Add(data_);
            Controls.Add(lblYear);
            Controls.Add(freeSeat);
            Controls.Add(priceTicket);


            connect.Open();

            DataTable table = new DataTable();
            command = new SqlCommand("SELECT * FROM films WHERE film_name=@name", connect);
            command.Parameters.AddWithValue("@name", film);
            info_film.Text = film;
            adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(table);
            data_.DataSource = table;

            connect.Close();

            Id = Convert.ToInt32(data_.Rows[0].Cells[0].Value.ToString());
            lblYear.Text = "Год: " + data_.Rows[0].Cells[2].Value.ToString();
            freeSeat.Text = "Свободных мест: " + data_.Rows[0].Cells[3].Value.ToString();
            priceTicket.Text = "Цена: " + data_.Rows[0].Cells[5].Value.ToString();
        }

        private void NextBtn_Click(object sender, EventArgs e)
        {
            info_film.Hide();
            datetime_.Hide();
            time.Hide();
            nextBtn.Hide();
            data_.Hide();
            lblYear.Hide();
            freeSeat.Hide();
            priceTicket.Hide();
            seats_load();
        }
    }
}