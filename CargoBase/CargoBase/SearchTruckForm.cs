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
    public partial class SearchTruckForm : Form
    {
        [System.Runtime.InteropServices.DllImport("winmm.dll")]
        private static extern Boolean PlaySound(string lpszName, int hModule, int dwFlags);
        TruckContext db;
        public SearchTruckForm()
        {
            InitializeComponent();
            db = new TruckContext();
            List<Truck> truck = db.Trucks.ToList();
            this.listBox1.DataSource = truck;
            this.listBox1.ValueMember = "Id";
            this.listBox1.DisplayMember = "Model";
        }
        //методы конвертирования файла изображения в поток байтов и наоборот
        //----------------------------------------------------------------------------------------------------------
        private byte[] ConvertFiletoByte(string sPath)
        {
            byte[] data = null;
            if (sPath == null)
            {
                sPath = @"C:\Users\Amadeus\Documents\Visual Studio 2019\Projects\CargoBase\empty.jpg";
                FileInfo fInfo = new FileInfo(sPath);
                long numBytes = fInfo.Length;
                FileStream fStream = new FileStream(sPath, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fStream);
                data = br.ReadBytes((int)numBytes);
                return data;
            }
            else
            {
                FileInfo fInfo = new FileInfo(sPath);
                long numBytes = fInfo.Length;
                FileStream fStream = new FileStream(sPath, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fStream);
                data = br.ReadBytes((int)numBytes);
                return data;
            }
        }
        private Image ConvertBytetoImage(byte[] photo)
        {
            Image newImage;
            using (MemoryStream ms = new MemoryStream(photo, 0, photo.Length))
            {
                ms.Write(photo, 0, photo.Length);
                newImage = Image.FromStream(ms, true);
                return newImage;
            }
        }
        //-----------------------------------------------------------------------------------------------------------

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                TruckContext db = new TruckContext();
                int id = Convert.ToInt32(textBox7.Text);
                Truck truck = db.Trucks.Single(tr => tr.Id == id);
                this.textBox1.Text = truck.Model;
                this.textBox2.Text = truck.Power.ToString();
                this.textBox3.Text = truck.Consumption.ToString();
                this.textBox4.Text = truck.Mileage.ToString();
                this.textBox5.Text = truck.TypeTruck.ToString();
                this.textBox6.Text = truck.Mark.ToString();
                this.pictureBox1.Image = ConvertBytetoImage(truck.Photo);
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
            Truck truck = (Truck)this.listBox1.Items[this.listBox1.SelectedIndex];
            this.textBox1.Text = truck.Model;
            this.textBox2.Text = truck.Power.ToString();
            this.textBox3.Text = truck.Consumption.ToString();
            this.textBox4.Text = truck.Mileage.ToString();
            this.textBox5.Text = truck.TypeTruck.ToString();
            this.textBox6.Text = truck.Mark.ToString();
            this.pictureBox1.Image = ConvertBytetoImage(truck.Photo);
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
            Truck truck = (Truck)this.listBox1.Items[this.listBox1.SelectedIndex];
            this.textBox1.Text = truck.Model;
            this.textBox2.Text = truck.Power.ToString();
            this.textBox3.Text = truck.Consumption.ToString();
            this.textBox4.Text = truck.Mileage.ToString();
            this.textBox5.Text = truck.TypeTruck.ToString();
            this.textBox6.Text = truck.Mark.ToString();
            this.pictureBox1.Image = ConvertBytetoImage(truck.Photo);
        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            Truck truck = (Truck)this.listBox1.SelectedItem;
            this.textBox1.Text = truck.Model;
            this.textBox2.Text = truck.Power.ToString();
            this.textBox3.Text = truck.Consumption.ToString();
            this.textBox4.Text = truck.Mileage.ToString();
            this.textBox5.Text = truck.TypeTruck.ToString();
            this.textBox6.Text = truck.Mark.ToString();
            this.pictureBox1.Image = ConvertBytetoImage(truck.Photo);
        }
    }
}
