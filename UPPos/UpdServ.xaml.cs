using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UPPos
{
    /// <summary>
    /// Логика взаимодействия для UpdServ.xaml
    /// </summary>
    public partial class UpdServ : Page
    {
        public Frame frame1;
        string Log;
        object Item;
        List<Service> List_Services = new List<Service>() { new Service() };
        public UpdServ(string log,Frame frame1, object item)
        {
            frame1 = frame1;
            Log = log;
            Item = item;
            InitializeComponent();

        }

        private void UpdSBut_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string name = NameServUpd.Text;
            double price;
            int count = Entities1.GetContex().Service.Count();
            List_Services = Entities1.GetContex().Service.ToList();
            for (int i = 0; i < count; i++)
            {
                if (List_Services[i].service1 == Item.GetType().GetProperty("service1").GetValue(Item))
                {
                    name = List_Services[i].service1;
                    if (double.TryParse(PriceServUpd.Text, out price))
                    {
                        List_Services[i].price = price;
                        Entities1.GetContex().SaveChanges();
                        frame1.Navigate(new Glavnaya(Log, frame1, Item));
                    }
                    else
                    {
                        MessageBox.Show("Введите число");
                    }
                }
                break;
            }
             
            
        }

        private void BackSBut_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
