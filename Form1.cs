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
        SqlConnection connect = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename =|DataDirectory|\AppData\movies.mdf; Integrated Security = True");
        SqlCommand command;
        SqlDataAdapter adapter, saal_adapter;
        public string film;
        private int Id = 0;
        Label[] infoLabel = new Label[6];
        int[] read_list, kohad_list;
        public int I = 0, J = 0;
        DateTimePicker datetime_;
        ListBox saal;
        Button nextBtn;
        DataGridView data_;
        PictureBox poster;

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
                            this.Text = _arr[i, j].Name + read[i].Name;
                            command = new SqlCommand("insert into client(client_name,film,seat,rjad) values(@n,@f,@s,@r)", connect);
                            connect.Open();
                            command.Parameters.AddWithValue("@n", "name");
                            command.Parameters.AddWithValue("@f", "film");
                            command.Parameters.AddWithValue("@s", i);
                            command.Parameters.AddWithValue("@r", j);
                            command.ExecuteNonQuery();
                            connect.Close();
                            MessageBox.Show("Данные добавлены");
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
            for (int i = I; i < I; i++)
            {
                read[i] = new Label();
                read[i].Text = "Rida " + (i + 1);
                read[i].Size = new Size(100, 100);
                read[i].Location = new Point(1, i * 100);
                read[i].Name = $"rjad" + i;
                this.Controls.Add(read[i]);
                for (int j = J; j < J; j++)
                {
                    _arr[i, j] = new Label();
                    _arr[i, j].Size = new Size(100, 100);
                    _arr[i, j].Image = Image.FromFile("../../images/free2.png");
                    _arr[i, j].BorderStyle = BorderStyle.Fixed3D;
                    _arr[i, j].Location = new Point(j * 100 + 100, i * 100);
                    this.Controls.Add(_arr[i, j]);
                    _arr[i, j].Tag = new int[] { i, j };
                    _arr[i, j].Name = $"mesto" + j;
                    _arr[i, j].Click += new EventHandler(Form1_Click);
                }
            }
            osta = new Button();
            osta.Text = "Osta";
            osta.Location = new Point(500, 200);
            this.Controls.Add(osta);
            osta.Click += Osta_Click;

            for (int i = 0; i < 4; i++)
            {
                for(int j = 0; j < 4; j++)
                {
                    connect.Open();
                    SqlCommand command = new SqlCommand("SELECT seat,rjad from client", connect);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (Convert.ToInt32(reader["seat"]) == i && Convert.ToInt32(reader["rjad"]) == j)
                            {
                                _arr[i, j].Image = Image.FromFile("../../images/red.png");
                            }
                        }
                    }
                    connect.Close();
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Size = new Size(550,500);
            seats_load();
            for (int i = 0; i < 4; i++)
            {
                if(read[i] != null)
                {
                    read[i].Hide();
                    for (int j = 0; j < 4; j++)
                    {
                        _arr[i, j].Hide();
                    }
                }
            }
            osta.Hide();

            data_ = new DataGridView();
            data_.Hide();

            for (int i = 0; i < 5; i++)
            {
                infoLabel[i] = new Label();
                infoLabel[i].Size = new Size(150, 30);
                if (i == 0)
                {
                    infoLabel[i].Text = "Информация о сеансе";
                    infoLabel[i].Location = new Point(350, 25);
                }
                if (i == 1)
                {
                    infoLabel[i].Location = new Point(350, 60);
                }
                if (i == 2)
                {
                    infoLabel[i].Location = new Point(350, 90);
                }
                if (i == 3)
                {
                    infoLabel[i].Location = new Point(350, 120);
                }
                if (i == 4)
                {
                    infoLabel[i].Location = new Point(350, 150);
                }
                this.Controls.Add(infoLabel[i]);
            }

            datetime_ = new DateTimePicker();
            datetime_.Size = new Size(150,30);
            datetime_.Location = new Point(350,180);

            saal = new ListBox();
            saal.Size = new Size(150,150);
            saal.Location = new Point(350,210);
            saal.SelectedIndexChanged += Saal_SelectedIndexChanged;

            nextBtn = new Button();
            nextBtn.Text = "Далее";
            nextBtn.Size = new Size(80,30);
            nextBtn.Location = new Point(350,380);
            nextBtn.Click += NextBtn_Click;

            poster = new PictureBox();
            poster.Size = new Size(300, 400);
            poster.Location = new Point(25,25);
            poster.SizeMode = PictureBoxSizeMode.StretchImage;

            Controls.Add(datetime_);
            Controls.Add(saal);
            Controls.Add(nextBtn);
            Controls.Add(data_);
            Controls.Add(poster);


            connect.Open();

            DataTable table = new DataTable();
            command = new SqlCommand("SELECT * FROM films WHERE film_name=@name", connect);
            command.Parameters.AddWithValue("@name", film);
            adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            adapter.Fill(table);
            data_.DataSource = table;

            saal_adapter = new SqlDataAdapter("SELECT * FROM saalid", connect);
            DataTable table1 = new DataTable();
            saal_adapter.Fill(table1);

            foreach(DataRow row in table1.Rows)
            {
                saal.Items.Add(row["saalinimetus"]);
            }

            read_list = new int[table1.Rows.Count];
            kohad_list = new int[table1.Rows.Count];
            int a = 0;
            foreach (DataRow row in table1.Rows)
            {
                read_list[a] = (int)row["read"];
                kohad_list[a] = (int)row["kohad"];
            }

            connect.Close();

            if(film == "mi")
            {
                infoLabel[1].Text = "Фильм: Мы";
                poster.Image = Image.FromFile("../../images/mi.jpg");
            }
            if (film == "vzaperti")
            {
                infoLabel[1].Text = "Фильм: Взаперти";
                poster.Image = Image.FromFile("../../images/vzaperti.jpg");
            }
            if (film == "legenda")
            {
                infoLabel[1].Text = "Фильм: Легенда";
                poster.Image = Image.FromFile("../../images/legenda.jpg");
            }

            Id = Convert.ToInt32(data_.Rows[0].Cells[0].Value.ToString());
            infoLabel[2].Text = "Год: " + data_.Rows[0].Cells[2].Value.ToString();
            infoLabel[3].Text = "Свободных мест: " + data_.Rows[0].Cells[3].Value.ToString();
            infoLabel[4].Text = "Цена: " + data_.Rows[0].Cells[5].Value.ToString();
        }

        private void Saal_SelectedIndexChanged(object sender, EventArgs e)
        {
            I = read_list[saal.SelectedIndex];
            J = kohad_list[saal.SelectedIndex];
            this.Text = I.ToString();
        }

        private void NextBtn_Click(object sender, EventArgs e)
        {
            this.Size = new Size(650,450);
            for(int i = 0; i < 5; i++)
            {
                infoLabel[i].Hide();
            }
            datetime_.Hide();
            saal.Hide();
            nextBtn.Hide();
            data_.Hide();
            poster.Hide();
            seats_load();
        }
    }
}