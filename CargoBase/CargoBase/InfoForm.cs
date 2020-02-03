using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CargoBase
{
    public partial class InfoForm : Form
    {
        public InfoForm()
        {
            InitializeComponent();
            label1.Text = "Информационная система для ведения учёта" +
                "\n" + "грузовых перевозок. Позволяет вести базу" +
                "\n" + "автомобилей, водителей, компаний-заказчиков и рейсов" +
                "\n" + "Автор: Лемешко Алексей Юрьевич";
        }
    }
}
