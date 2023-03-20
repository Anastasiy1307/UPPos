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
using Aspose.BarCode.Generation;
using Path = System.IO.Path;

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
        Service thisService;
        List<Service> services = new List<Service>();
        public UpdServ(string log,Frame frame,Service service)
        {
            
            InitializeComponent();
            frame1 = frame;
            Log = log;
            thisService = service;
            services = Entities1.GetContex().Service.ToList();
            for (int i = 0; i < services.Count; i++)
            {
                if (services[i].service1 == thisService.service1)
                {
                    NameServUpd.Text = services[i].service1;
                    PriceServUpd.Text = services[i].price.ToString();
                    
                    
                    break;
                }
            }
        }

        private void Zapis_Delete(object sender, RoutedEventArgs e)
        {
            List<Service> services = new List<Service> { };
            services = Entities1.GetContex().Service.ToList();
            try
            {
                for (int i = 0; i < services.Count; i++)
                {
                    if (services[i].service1 == Item.GetType().GetProperty("service1").GetValue(Item))
                    {
                        Entities1.GetContex().Service.Remove(services[i]);
                        Entities1.GetContex().SaveChanges();
                        break;
                    }
                }
                frame1.Navigate(new Glavnaya(Log, frame1, Item));
            }
            catch
            {
                MessageBox.Show("Вы не можете удалить данный анализ, пока не будет удален результат");
            }
        }
        private void UpdSBut_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            List<Service> List_Services = new List<Service>();
            string name = NameServUpd.Text;
            double price;
            int count = Entities1.GetContex().Service.Count();
            List_Services = Entities1.GetContex().Service.ToList();
            for (int i = 0; i < count; i++)
            {
                if (List_Services[i].service1 ==  thisService.service1)
                {
                    name = List_Services[i].service1;
                    if (double.TryParse(PriceServUpd.Text, out price))
                    {
                        List_Services[i].price = price;

                        if (List_Services[i].image == null)
                        {
                            String allowchar = " ";
                            allowchar += "1,2,3,4,5,6,7,8,9,0";
                            char[] a = { ',' };
                            String[] ar = allowchar.Split(a);
                            String pwd = "";
                            string temp = "";
                            Random r = new Random();
                            for (int j = 0; j < 10; j++)
                            {
                                temp = ar[(r.Next(0, ar.Length))];
                                pwd += temp;
                            }

                            BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, pwd);
                            var imageType = "Png";

                            string imagePath = "barcode" + pwd + ".Png";

                            string path = System.IO.Path.GetFullPath(imagePath);

                            generator.Save(imagePath, BarCodeImageFormat.Png);
                            List_Services[i].image = path;


                        }
                        else
                        {
                           /* if (List_Services.FileName != "")
                            {
                            }*/
                        }

                        Entities1.GetContex().SaveChanges();
                        frame1.Navigate(new Glavnaya(Log, frame1, Item));

                        break;
                    }
                    else
                    {
                        MessageBox.Show("Введите число");
                    }

                }
                
            }
        }
        private void BackSBut_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            frame1.Navigate(new Glavnaya(Log, frame1, Item));
        }
    }
}
