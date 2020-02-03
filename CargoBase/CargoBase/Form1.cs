using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;
using System.Media;
using System.IO;

namespace CargoBase
{
    public partial class Form1 : Form
    {

        [System.Runtime.InteropServices.DllImport("winmm.dll")]
        private static extern
               Boolean PlaySound(string lpszName, int hModule, int dwFlags);

        TruckContext db;       
        public Form1()
        {
            InitializeComponent();
            db = new TruckContext();
            db.TypeTrucks.Load();
            dataGridView1.DataSource = db.TypeTrucks.Local.ToBindingList();
            db.Marks.Load();
            dataGridView2.DataSource = db.Marks.Local.ToBindingList();
            db.Trucks.Load();
            dataGridView3.DataSource = db.Trucks.Local.ToBindingList();
            db.Customers.Load();
            dataGridView4.DataSource = db.Customers.Local.ToBindingList();
            db.Cities.Load();
            dataGridView5.DataSource = db.Cities.Local.ToBindingList();
            db.Drivers.Load();
            dataGridView6.DataSource = db.Drivers.Local.ToBindingList();
            db.Flights.Load();
            dataGridView7.DataSource = db.Flights.Local.ToBindingList();
            db.Flights.Load();
            addTypeButton.FlatAppearance.BorderSize = 0;
            addTypeButton.FlatStyle = FlatStyle.Flat;
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
        private void addTypeButton_Click(object sender, EventArgs e)
        {
            TypeTruckForm typeForm = new TypeTruckForm();
            DialogResult result = typeForm.ShowDialog(this);
            if (result == DialogResult.Cancel)
                return;
            else
            {
                try
                {
                    TypeTruck typeTruck = new TypeTruck();
                    typeTruck.Name = typeForm.textBox1.Text;
                    db.TypeTrucks.Add(typeTruck);
                    db.SaveChanges();
                    PlaySound(Application.StartupPath + "\\exclamationtone.wav", 0, 1);
                    MessageBox.Show("Новый тип грузовика добавлен");
                }
                catch
                {
                    PlaySound(Application.StartupPath + "\\errortone.wav", 0, 1);
                    MessageBox.Show("Не получилось добавить новый объект");
                }
            }          
        }
        private void addMarkTruckButton_Click(object sender, EventArgs e)
        {
            MarkTruckForm markForm = new MarkTruckForm();
            DialogResult result = markForm.ShowDialog(this);
            if (result == DialogResult.Cancel)
                return;
            else
            {
                try
                {
                    Mark markTruck = new Mark();
                    markTruck.Name = markForm.textBox1.Text;
                    db.Marks.Add(markTruck);
                    db.SaveChanges();
                    PlaySound(Application.StartupPath + "\\exclamationtone.wav", 0, 1);
                    MessageBox.Show("Новая марка грузовика добавлена");
                }
                catch
                {
                    PlaySound(Application.StartupPath + "\\errortone.wav", 0, 1);
                    MessageBox.Show("Не получилось добавить новый объект");
                }
            }
            
        }
        private void addTruckButton_Click(object sender, EventArgs e)
        {
            TruckForm truckForm = new TruckForm();
            List<TypeTruck> typeTrucks = db.TypeTrucks.ToList();
            truckForm.comboBox1.DataSource = typeTrucks;
            truckForm.comboBox1.ValueMember = "Id";
            truckForm.comboBox1.DisplayMember = "Name";
            List<Mark> markTruck = db.Marks.ToList();
            truckForm.comboBox2.DataSource = markTruck;
            truckForm.comboBox1.ValueMember = "Id";
            truckForm.comboBox1.DisplayMember = "Name";
            DialogResult result = truckForm.ShowDialog(this);
            if (result == DialogResult.Cancel)
                return;
            else
            {
                try
                {
                    Truck truck = new Truck();
                    truck.Model = truckForm.textBox1.Text;
                    truck.Power = (int)truckForm.numericUpDown1.Value;
                    truck.Consumption = (int)truckForm.numericUpDown2.Value;
                    truck.Mileage = (int)truckForm.numericUpDown3.Value;
                    truck.Photo = ConvertFiletoByte(truckForm.pictureBox1.ImageLocation);
                    truck.TypeTruck = (TypeTruck)truckForm.comboBox1.SelectedItem;
                    truck.Mark = (Mark)truckForm.comboBox2.SelectedItem;
                    db.Trucks.Add(truck);
                    db.SaveChanges();
                    PlaySound(Application.StartupPath + "\\exclamationtone.wav", 0, 1);
                    MessageBox.Show("Новый грузовик добавлен");
                }
                catch
                {
                    PlaySound(Application.StartupPath + "\\errortone.wav", 0, 1);
                    MessageBox.Show("Не получилось добавить новый объект");
                }
            }
            
        }
        private void addCustomerButton_Click(object sender, EventArgs e)
        {
            CustomerForm customerForm = new CustomerForm();
            List<Customer> customers = db.Customers.ToList();
            DialogResult result = customerForm.ShowDialog(this);
            if (result == DialogResult.Cancel)
                return;
            else
            {
                try
                {
                    Customer customer = new Customer();
                    customer.Name = customerForm.textBox1.Text;
                    customer.NameDir = customerForm.textBox2.Text;
                    customer.SurnameDir = customerForm.textBox3.Text;
                    customer.PatronageDir = customerForm.textBox4.Text;
                    customer.Telephone = customerForm.textBox5.Text;
                    db.Customers.Add(customer);
                    db.SaveChanges();
                    PlaySound(Application.StartupPath + "\\exclamationtone.wav", 0, 1);
                    MessageBox.Show("Новый клиент добавлен");
                }
                catch
                {
                    PlaySound(Application.StartupPath + "\\errortone.wav", 0, 1);
                    MessageBox.Show("Не получилось добавить новый объект");
                }
            }
            
        }
        private void addCityButton_Click(object sender, EventArgs e)
        {
            CityForm cityForm = new CityForm();
            List<City> cities = db.Cities.ToList();
            DialogResult result = cityForm.ShowDialog(this);
            if (result == DialogResult.Cancel)
                return;
            else
            {
                try
                {
                    City city = new City();
                    city.Name = cityForm.textBox1.Text;
                    db.Cities.Add(city);
                    db.SaveChanges();
                    PlaySound(Application.StartupPath + "\\exclamationtone.wav", 0, 1);
                    MessageBox.Show("Новый город добавлен");
                }
                catch
                {
                    PlaySound(Application.StartupPath + "\\errortone.wav", 0, 1);
                    MessageBox.Show("Не получилось добавить новый объект");
                }
            }
            
        }
        private void addDriverButton_Click(object sender, EventArgs e)
        {
            DriverForm driverForm = new DriverForm();
            List<Driver> drivers = db.Drivers.ToList();
            DialogResult result = driverForm.ShowDialog(this);
            if (result == DialogResult.Cancel)
                return;
            else
            {
                try
                {
                    Driver driver = new Driver();
                    driver.Name = driverForm.textBox2.Text;
                    driver.Surname = driverForm.textBox1.Text;
                    driver.Patronymic = driverForm.textBox3.Text;
                    driver.DayOfBirth = Convert.ToDateTime(driverForm.textBox4.Text);
                    driver.Experience = (int)driverForm.numericUpDown1.Value;
                    driver.Photo = ConvertFiletoByte(driverForm.pictureBox1.ImageLocation);
                    db.Drivers.Add(driver);
                    db.SaveChanges();
                    PlaySound(Application.StartupPath + "\\exclamationtone.wav", 0, 1);
                    MessageBox.Show("Новый водитель добавлен");
                }
                catch
                {
                    PlaySound(Application.StartupPath + "\\errortone.wav", 0, 1);
                    MessageBox.Show("Не получилось добавить новый объект");
                }
            }
            
        }
        private void typeTruckButton_Click(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count > 0)
            {
                int index = dataGridView1.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(dataGridView1[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;
                TypeTruck typeTruck = db.TypeTrucks.Find(id);
                listBox1.DataSource = typeTruck.Trucks.ToList();
                listBox1.DisplayMember = "Model";
            }
        }
        private void MarkTruckButton_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                int index = dataGridView2.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(dataGridView2[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;
                Mark mark = db.Marks.Find(id);
                listBox2.DataSource = mark.Trucks.ToList();
                listBox2.DisplayMember = "Model";
            }
        }
        private void addReisButton_Click(object sender, EventArgs e)
        {
            ReiseForm reiseForm = new ReiseForm();
            List<Truck> trucks = db.Trucks.ToList();
            reiseForm.comboBox1.DataSource = trucks;
            reiseForm.comboBox1.ValueMember = "Id";
            reiseForm.comboBox1.DisplayMember = "Model";
            List<Driver> drivers = db.Drivers.ToList();
            reiseForm.comboBox2.DataSource = drivers;
            reiseForm.comboBox2.DisplayMember = "Surname";
            List<City> cities = db.Cities.ToList();
            reiseForm.comboBox3.DataSource = cities;
            reiseForm.comboBox3.DisplayMember = "Name";
            List<Customer> customers = db.Customers.ToList();
            reiseForm.comboBox4.DataSource = customers;
            reiseForm.comboBox4.DisplayMember = "Name";
            DialogResult result = reiseForm.ShowDialog(this);
            if (result == DialogResult.Cancel)
                return;
            else
            {
                try
                {
                    Flight flight = new Flight();
                    flight.Cargo = reiseForm.textBox1.Text;
                    flight.Price = Convert.ToDecimal(reiseForm.textBox2.Text);
                    flight.Truck = (Truck)reiseForm.comboBox1.SelectedItem;
                    flight.Driver = (Driver)reiseForm.comboBox2.SelectedItem;
                    flight.City = (City)reiseForm.comboBox3.SelectedItem;
                    flight.Customer = (Customer)reiseForm.comboBox4.SelectedItem;
                    flight.Date = reiseForm.dateTimePicker1.Value;
                    db.Flights.Add(flight);
                    db.SaveChanges();
                    PlaySound(Application.StartupPath + "\\exclamationtone.wav", 0, 1);
                    MessageBox.Show("Новый рейс добавлен");
                }
                catch
                {
                    PlaySound(Application.StartupPath + "\\errortone.wav", 0, 1);
                    MessageBox.Show("Не получилось добавить новый объект");
                }
            }
        }
        private void chTypeButton_Click(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count > 0)
            {
                int index = dataGridView1.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(dataGridView1[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;
                TypeTruck typeTruck = db.TypeTrucks.Find(id);
                TypeTruckForm typeTruckForm = new TypeTruckForm();
                typeTruckForm.textBox1.Text = typeTruck.Name;
                List<TypeTruck> typeTrucks = db.TypeTrucks.ToList();
                DialogResult result = typeTruckForm.ShowDialog(this);
                if (result == DialogResult.Cancel)
                    return;
                typeTruck.Name = typeTruckForm.textBox1.Text;
                db.Entry(typeTruck).State = EntityState.Modified;
                db.SaveChanges();
                PlaySound(Application.StartupPath + "\\strarttone.wav", 0, 1);
                MessageBox.Show("Объект обновлен");
            }
        }
        private void chMarkTruckButton_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                int index = dataGridView2.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(dataGridView2[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;
                Mark mark = db.Marks.Find(id);
                MarkTruckForm markTruckForm = new MarkTruckForm();
                markTruckForm.textBox1.Text = mark.Name;
                List<Mark> marks = db.Marks.ToList();
                DialogResult result = markTruckForm.ShowDialog(this);
                if (result == DialogResult.Cancel)
                    return;
                mark.Name = markTruckForm.textBox1.Text;
                db.Entry(mark).State = EntityState.Modified;
                db.SaveChanges();
                PlaySound(Application.StartupPath + "\\strarttone.wav", 0, 1);
                MessageBox.Show("Объект обновлен");
            }
        }
        private void chCityButton_Click(object sender, EventArgs e)
        {
            if (dataGridView5.SelectedRows.Count > 0)
            {
                int index = dataGridView5.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(dataGridView5[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;
                City city = db.Cities.Find(id);
                CityForm cityForm = new CityForm();
                cityForm.textBox1.Text = city.Name;
                List<City> cities = db.Cities.ToList();
                DialogResult result = cityForm.ShowDialog(this);
                if (result == DialogResult.Cancel)
                    return;
                city.Name = cityForm.textBox1.Text;
                db.Entry(city).State = EntityState.Modified;
                db.SaveChanges();
                PlaySound(Application.StartupPath + "\\strarttone.wav", 0, 1);
                MessageBox.Show("Объект обновлен");
            }
        }
        private void chCustomerButton_Click(object sender, EventArgs e)
        {
            if (dataGridView4.SelectedRows.Count > 0)
            {
                int index = dataGridView4.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(dataGridView4[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;
                Customer customer = db.Customers.Find(id);
                CustomerForm customerForm = new CustomerForm();
                customerForm.textBox1.Text = customer.Name;
                customerForm.textBox2.Text = customer.SurnameDir;
                customerForm.textBox3.Text = customer.NameDir;
                customerForm.textBox4.Text = customer.PatronageDir;
                customerForm.textBox5.Text = customer.Telephone;
                List<Customer> customers = db.Customers.ToList();
                DialogResult result = customerForm.ShowDialog(this);
                if (result == DialogResult.Cancel)
                    return;
                customer.Name = customerForm.textBox1.Text;
                customer.SurnameDir = customerForm.textBox2.Text;
                customer.NameDir = customerForm.textBox3.Text;
                customer.PatronageDir = customerForm.textBox4.Text;
                customer.Telephone = customerForm.textBox5.Text;
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                PlaySound(Application.StartupPath + "\\strarttone.wav", 0, 1);
                MessageBox.Show("Объект обновлен");
            }
        }

        private void chDriverButton_Click(object sender, EventArgs e)
        {
            if(dataGridView6.SelectedRows.Count > 0)
            {
                int index = dataGridView6.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(dataGridView6[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;
                Driver driver = db.Drivers.Find(id);
                DriverForm driverForm = new DriverForm();
                driverForm.textBox1.Text = driver.Surname;
                driverForm.textBox2.Text = driver.Name;
                driverForm.textBox3.Text = driver.Patronymic;
                driverForm.textBox4.Text = driver.DayOfBirth.ToString();
                driverForm.numericUpDown1.Value = driver.Experience;
                driverForm.pictureBox1.Image = ConvertBytetoImage(driver.Photo);
                List<Driver> drivers = db.Drivers.ToList();
                DialogResult result = driverForm.ShowDialog(this);
                if (result == DialogResult.Cancel)
                    return;
                else
                {
                    PlaySound(Application.StartupPath + "\\strarttone.wav", 0, 1);
                    DialogResult dialogResult = MessageBox.Show("Желаете поменять изображение для этого объекта? " +
                        "Если вы не выбрали новое изображение для него и нажали Да, то старое изображение пропадет",
                        "Сменить изображение?", MessageBoxButtons.YesNo);
                    if(dialogResult == DialogResult.Yes)
                    {
                        driver.Surname = driverForm.textBox1.Text;
                        driver.Name = driverForm.textBox2.Text;
                        driver.Patronymic = driverForm.textBox3.Text;
                        driver.DayOfBirth = Convert.ToDateTime(driverForm.textBox4.Text);
                        driver.Experience = Convert.ToInt32(driverForm.numericUpDown1.Value);
                        driver.Photo = ConvertFiletoByte(driverForm.pictureBox1.ImageLocation);
                        db.Entry(driver).State = EntityState.Modified;
                        db.SaveChanges();
                        MessageBox.Show("Объект обновлен");
                    }
                    else if(dialogResult == DialogResult.No)
                    {
                        driver.Surname = driverForm.textBox1.Text;
                        driver.Name = driverForm.textBox2.Text;
                        driver.Patronymic = driverForm.textBox3.Text;
                        driver.DayOfBirth = Convert.ToDateTime(driverForm.textBox4.Text);
                        driver.Experience = Convert.ToInt32(driverForm.numericUpDown1.Value);
                        db.Entry(driver).State = EntityState.Modified;
                        db.SaveChanges();
                        MessageBox.Show("Объект обновлен");
                    }
                }
            }
        }

        private void chTruckButton_Click(object sender, EventArgs e)
        {
            int index = dataGridView3.SelectedRows[0].Index;
            int id = 0;
            bool converted = Int32.TryParse(dataGridView3[0, index].Value.ToString(), out id);
            if (converted == false)
                return;
            Truck truck = db.Trucks.Find(id);
            TruckForm truckForm = new TruckForm();
            truckForm.textBox1.Text = truck.Model;
            truckForm.numericUpDown1.Value = truck.Power;
            truckForm.numericUpDown2.Value = truck.Consumption;
            truckForm.numericUpDown3.Value = truck.Mileage;
            truckForm.pictureBox1.Image = ConvertBytetoImage(truck.Photo);
            List<TypeTruck> typeTrucks = db.TypeTrucks.ToList();
            truckForm.comboBox1.DataSource = typeTrucks;
            truckForm.comboBox1.ValueMember = "Id";
            truckForm.comboBox1.DisplayMember = "Name";
            List<Mark> marks = db.Marks.ToList();
            truckForm.comboBox2.DataSource = marks;
            truckForm.comboBox2.ValueMember = "Id";
            truckForm.comboBox2.DisplayMember = "Name";
            if (truck.TypeTruck != null)
                truckForm.comboBox1.SelectedValue = truck.TypeTruck.Id;
            if (truck.Mark != null)
                truckForm.comboBox2.SelectedValue = truck.Mark.Id;
            DialogResult result = truckForm.ShowDialog(this);
            if (result == DialogResult.Cancel)
                return;
            else
            {
                PlaySound(Application.StartupPath + "\\strarttone.wav", 0, 1);
                DialogResult dialogResult = MessageBox.Show("Желаете поменять изображение для этого объекта? " +
                    "Если вы не выбрали новое изображение для него и нажали Да, то старое изображение пропадет",
                    "Сменить изображение?", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    truck.Model = truckForm.textBox1.Text;
                    truck.Power = (int)truckForm.numericUpDown1.Value;
                    truck.Consumption = (int)truckForm.numericUpDown2.Value;
                    truck.Mileage = (int)truckForm.numericUpDown3.Value;
                    truck.TypeTruck = (TypeTruck)truckForm.comboBox1.SelectedItem;
                    truck.Mark = (Mark)truckForm.comboBox2.SelectedItem;
                    truck.Photo = ConvertFiletoByte(truckForm.pictureBox1.ImageLocation);
                    db.Entry(truck).State = EntityState.Modified;
                    db.SaveChanges();
                    MessageBox.Show("Объект обновлен");
                }
                else if(dialogResult == DialogResult.No)
                {
                    truck.Model = truckForm.textBox1.Text;
                    truck.Power = (int)truckForm.numericUpDown1.Value;
                    truck.Consumption = (int)truckForm.numericUpDown2.Value;
                    truck.Mileage = (int)truckForm.numericUpDown3.Value;
                    truck.TypeTruck = (TypeTruck)truckForm.comboBox1.SelectedItem;
                    truck.Mark = (Mark)truckForm.comboBox2.SelectedItem;
                    db.Entry(truck).State = EntityState.Modified;
                    db.SaveChanges();
                    MessageBox.Show("Объект обновлен");
                }
            }           
        }

        private void chReisButton_Click(object sender, EventArgs e)
        {
            int index = dataGridView7.SelectedRows[0].Index;
            int id = 0;
            bool converted = Int32.TryParse(dataGridView7[0, index].Value.ToString(), out id);
            if (converted == false)
                return;
            Flight flight = db.Flights.Find(id);
            ReiseForm reiseForm = new ReiseForm();
            reiseForm.textBox1.Text = flight.Cargo;
            reiseForm.textBox2.Text = flight.Price.ToString();
            List<Truck> trucks = db.Trucks.ToList();
            reiseForm.comboBox1.DataSource = trucks;
            reiseForm.comboBox1.ValueMember = "Id";
            reiseForm.comboBox1.DisplayMember = "Model";
            List<Driver> drivers = db.Drivers.ToList();
            reiseForm.comboBox2.DataSource = drivers;
            reiseForm.comboBox2.ValueMember = "Id";
            reiseForm.comboBox2.DisplayMember = "Surname";
            List<City> cities = db.Cities.ToList();
            reiseForm.comboBox3.DataSource = cities;
            reiseForm.comboBox3.ValueMember = "Id";
            reiseForm.comboBox3.DisplayMember = "Name";
            List<Customer> customers = db.Customers.ToList();
            reiseForm.comboBox4.DataSource = customers;
            reiseForm.comboBox4.ValueMember = "Id";
            reiseForm.comboBox4.DisplayMember = "Name";
            reiseForm.dateTimePicker1.Value = flight.Date;
            if (flight.Truck != null)
                reiseForm.comboBox1.SelectedValue = flight.Truck.Id;
            if (flight.Driver != null)
                reiseForm.comboBox2.SelectedValue = flight.Driver.Id;
            if (flight.City != null)
                reiseForm.comboBox3.SelectedValue = flight.City.Id;
            if (flight.Customer != null)
                reiseForm.comboBox4.SelectedValue = flight.Customer.Id;
            DialogResult result = reiseForm.ShowDialog(this);
            if (result == DialogResult.Cancel)
                return;
            flight.Cargo = reiseForm.textBox1.Text;
            flight.Price = Convert.ToDecimal(reiseForm.textBox2.Text);
            flight.Truck = (Truck)reiseForm.comboBox1.SelectedItem;
            flight.Driver = (Driver)reiseForm.comboBox2.SelectedItem;
            flight.City = (City)reiseForm.comboBox3.SelectedItem;
            flight.Customer = (Customer)reiseForm.comboBox4.SelectedItem;
            flight.Date = Convert.ToDateTime(reiseForm.dateTimePicker1.Value);
            db.Entry(flight).State = EntityState.Modified;
            db.SaveChanges();
            PlaySound(Application.StartupPath + "\\strarttone.wav", 0, 1);
            MessageBox.Show("Объект обновлен");
        }

        private void delTypeButton_Click(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count > 0)
            {
                int index = dataGridView1.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(dataGridView1[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;
                TypeTruck typeTruck = db.TypeTrucks.Find(id);
                db.TypeTrucks.Remove(typeTruck);
                db.SaveChanges();
                PlaySound(Application.StartupPath + "\\stoptone.wav", 0, 1);
                MessageBox.Show("Тип грузовика удалён");
            }
        }

        private void delMarkTruckButton_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                int index = dataGridView2.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(dataGridView2[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;
                Mark mark = db.Marks.Find(id);
                db.Marks.Remove(mark);
                db.SaveChanges();
                PlaySound(Application.StartupPath + "\\stoptone.wav", 0, 1);
                MessageBox.Show("Марка грузовика удалена");
            }
        }

        private void delDriverButton_Click(object sender, EventArgs e)
        {
            if (dataGridView6.SelectedRows.Count > 0)
            {
                int index = dataGridView6.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(dataGridView6[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;
                Driver driver = db.Drivers.Find(id);
                db.Drivers.Remove(driver);
                db.SaveChanges();
                PlaySound(Application.StartupPath + "\\stoptone.wav", 0, 1);
                MessageBox.Show("Водитель удалён");
            }
        }

        private void delCityButton_Click(object sender, EventArgs e)
        {
            if (dataGridView5.SelectedRows.Count > 0)
            {
                int index = dataGridView5.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(dataGridView5[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;
                City city = db.Cities.Find(id);
                db.Cities.Remove(city);
                db.SaveChanges();
                PlaySound(Application.StartupPath + "\\stoptone.wav", 0, 1);
                MessageBox.Show("Город удалён");
            }
        }

        private void delTruckButton_Click(object sender, EventArgs e)
        {
            if (dataGridView3.SelectedRows.Count > 0)
            {
                int index = dataGridView3.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(dataGridView3[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;
                Truck truck = db.Trucks.Find(id);
                db.Trucks.Remove(truck);
                db.SaveChanges();
                PlaySound(Application.StartupPath + "\\stoptone.wav", 0, 1);
                MessageBox.Show("Грузовик удалён");
            }
        }
        private void delCustomerButton_Click(object sender, EventArgs e)
        {
            if (dataGridView4.SelectedRows.Count > 0)
            {
                int index = dataGridView4.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(dataGridView4[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;
                Customer customer = db.Customers.Find(id);
                db.Customers.Remove(customer);
                db.SaveChanges();
                PlaySound(Application.StartupPath + "\\stoptone.wav", 0, 1);
                MessageBox.Show("Клиент удалён");
            }
        }
        private void delReiseButton_Click(object sender, EventArgs e)
        {
            if (dataGridView7.SelectedRows.Count > 0)
            {
                int index = dataGridView7.SelectedRows[0].Index;
                int id = 0;
                bool converted = Int32.TryParse(dataGridView7[0, index].Value.ToString(), out id);
                if (converted == false)
                    return;
                Flight flight = db.Flights.Find(id);
                db.Flights.Remove(flight);
                db.SaveChanges();
                PlaySound(Application.StartupPath + "\\stoptone.wav", 0, 1);
                MessageBox.Show("Рейс удалён");
            }
        }
        private void работаСДаннымиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SearchDriverForm searchDriverForm = new SearchDriverForm();
            searchDriverForm.Show();
        }

        private void просмотрГрузовиковToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SearchTruckForm searchTruckForm = new SearchTruckForm();
            searchTruckForm.Show();
        }

        private void просмотрРейсовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SearchFlightForm searchFlightForm = new SearchFlightForm();
            searchFlightForm.Show();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            InfoForm infoForm = new InfoForm();
            infoForm.Show();
        }
    }
}