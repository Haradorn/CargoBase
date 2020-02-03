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
    public partial class SearchDriverForm : Form
    {
        [System.Runtime.InteropServices.DllImport("winmm.dll")]
        private static extern Boolean PlaySound(string lpszName, int hModule, int dwFlags);

        TruckContext db;
        public SearchDriverForm()
        {
            InitializeComponent();
            db = new TruckContext();
            List<Driver> driver = db.Drivers.ToList();
            this.listBox1.DataSource = driver;
            this.listBox1.ValueMember = "Id";
            this.listBox1.DisplayMember = "Surname";
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
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                TruckContext db = new TruckContext();
                int id = Convert.ToInt32(textBox6.Text);
                Driver driver = db.Drivers.Single(dr => dr.Id == id);
                this.textBox1.Text = driver.Surname;
                this.textBox2.Text = driver.Name;
                this.textBox3.Text = driver.Patronymic;
                this.textBox4.Text = driver.Experience.ToString();
                this.textBox5.Text = driver.DayOfBirth.ToString();
                this.pictureBox1.Image = ConvertBytetoImage(driver.Photo);
            }
            catch
            {
                PlaySound(Application.StartupPath + "\\exittone.wav", 0, 1);
                MessageBox.Show("Не удалось найти объект");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedIndex <= 0)
            {
                this.listBox1.SelectedIndex = 0;
            }
            else
            {
                this.listBox1.SelectedIndex--;
            }
            Driver driver = (Driver)this.listBox1.Items[this.listBox1.SelectedIndex];
            this.textBox1.Text = driver.Surname;
            this.textBox2.Text = driver.Name;
            this.textBox3.Text = driver.Patronymic;
            this.textBox4.Text = driver.Experience.ToString();
            this.textBox5.Text = driver.DayOfBirth.ToString();
            this.pictureBox1.Image = ConvertBytetoImage(driver.Photo);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedIndex == this.listBox1.Items.Count - 1)
            {
                this.listBox1.SelectedIndex = this.listBox1.Items.Count - 1;
            }
            else
            {
                this.listBox1.SelectedIndex++;
            }
            Driver driver = (Driver)this.listBox1.Items[this.listBox1.SelectedIndex];
            this.textBox1.Text = driver.Surname;
            this.textBox2.Text = driver.Name;
            this.textBox3.Text = driver.Patronymic;
            this.textBox4.Text = driver.Experience.ToString();
            this.textBox5.Text = driver.DayOfBirth.ToString();
            this.pictureBox1.Image = ConvertBytetoImage(driver.Photo);
        }

        private void listBox1_MouseClick(object sender, MouseEventArgs e)
        {
            Driver driver = (Driver)this.listBox1.SelectedItem;
            this.textBox1.Text = driver.Surname;
            this.textBox2.Text = driver.Name;
            this.textBox3.Text = driver.Patronymic;
            this.textBox4.Text = driver.Experience.ToString();
            this.textBox5.Text = driver.DayOfBirth.ToString();
            this.pictureBox1.Image = ConvertBytetoImage(driver.Photo);
        }
    }
}
