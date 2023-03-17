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
    /// Логика взаимодействия для AddServ.xaml
    /// </summary>
    public partial class AddServ : Page
    {
        public Frame frame1;
        object Item;
        List<Service> List_Services = new List<Service>() { new Service()};
        List<Workers> workers = new List<Workers>();
        string Log;
        public AddServ(string log,Frame frame, object item)
        {
            frame1 = frame;
            Log = log; 
            Item = item;
            InitializeComponent();
        }

        private void AddSBut_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
            string name = NameServAdd.Text;
            double price;
            int count = Entities1.GetContex().Service.Count();

            List_Services[0].service1 = name;
            if(double.TryParse(PriceServAdd.Text, out price))
            {
                List_Services[0].price = price;
                Entities1.GetContex().Service.Add(List_Services[0]);
                Entities1.GetContex().SaveChanges();
                frame1.Navigate(new Glavnaya(Log,frame1, Item));
            }
            else
            {
                MessageBox.Show("Введите число");
            }
            

        }

        private void BackSBut_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
            frame1.Navigate(new Glavnaya(Log,frame1, Item));
        }
    }
}
