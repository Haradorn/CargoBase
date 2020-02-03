using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using System.IO;

namespace CargoBase
{
    public partial class SearchFlightForm : Form
    {
        [System.Runtime.InteropServices.DllImport("winmm.dll")]
        private static extern Boolean PlaySound(string lpszName, int hModule, int dwFlags);
        TruckContext db;
        public SearchFlightForm()
        {
            InitializeComponent();
            db = new TruckContext();
            List<Flight> flight = db.Flights.ToList();
            this.listBox1.DataSource = flight;
            this.listBox1.ValueMember = "Id";
            this.listBox1.DisplayMember = "Date";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                TruckContext db = new TruckContext();
                int id = Convert.ToInt32(textBox8.Text);
                Flight flight = db.Flights.Single(fl => fl.Id == id);
                this.textBox1.Text = flight.Cargo;
                this.textBox2.Text = flight.Truck.ToString();
                this.textBox3.Text = flight.Price.ToString();
                this.textBox4.Text = flight.Date.ToString();
                this.textBox5.Text = flight.Driver.ToString();
                this.textBox6.Text = flight.City.ToString();
                this.textBox7.Text = flight.Customer.ToString();
            }
            catch
            {
                PlaySound(Application.StartupPath + "\\exittone.wav", 0, 1);
                MessageBox.Show("Не удалось найти объект");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedIndex <= 0)
            {
                this.listBox1.SelectedIndex = 0;
            }
            else
            {
                this.listBox1.SelectedIndex--;
            }
            Flight flight = (Flight)this.listBox1.Items[this.listBox1.SelectedIndex];
            this.textBox1.Text = flight.Cargo;
            this.textBox2.Text = flight.Truck.ToString();
            this.textBox3.Text = flight.Price.ToString();
            this.textBox4.Text = flight.Date.ToString();
            this.textBox5.Text = flight.Driver.ToString();
            this.textBox6.Text = flight.City.ToString();
            this.textBox7.Text = flight.Customer.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedIndex == this.listBox1.Items.Count - 1)
            {
                this.listBox1.SelectedIndex = this.listBox1.Items.Count - 1;
            }
            else
            {
                this.listBox1.SelectedIndex++;
            }
            Flight flight = (Flight)this.listBox1.Items[this.listBox1.SelectedIndex];
            this.textBox1.Text = flight.Cargo;
            this.textBox2.Text = flight.Truck.ToString();
            this.textBox3.Text = flight.Price.ToString();
            this.textBox4.Text = flight.Date.ToString();
            this.textBox5.Text = flight.Driver.ToString();
            this.textBox6.Text = flight.City.ToString();
            this.textBox7.Text = flight.Customer.ToString();
        }
    }
}
