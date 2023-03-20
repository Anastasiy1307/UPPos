using Aspose.BarCode.Generation;
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
using Path = System.IO.Path;

namespace UPPos
{
    /// <summary>
    /// Логика взаимодействия для UpdResult.xaml
    /// </summary>
    
    public partial class UpdResult : Page
    {
        public Frame frame1;
        object Item;
        Results thisResults;
        List<Results> results = new List<Results>();
        List<Workers> workers = new List<Workers>();
        private List<char> znak = new List<char>() { '+', '-' };
        public List<char> Znak
        {
            get => znak;
            set { znak = value; }
        }
        string Log;
        public UpdResult(string log, Frame frame, Results result)
        {
            InitializeComponent();
            DataContext = this;
            frame1 = frame;
            Log = log;
            results = Entities1.GetContex().Results.ToList();
            int count = Entities1.GetContex().Results.Count();
            for (int i = 0; i < count; i++)
            {
                if (results[i].id == thisResults.id)
                {
                    id_userUpd.Text = results[i].id_user.ToString();
                    id_workUpd.Text = results[i].id_work.ToString();
                    id_serviceUpd.Text = results[i].id_service.ToString();
                    resultUpd.Text = results[i].result.ToString();
                    dataUpd.Text = results[i].data.ToString();
                    break;
                }
            }
        }
        private void BackSBut_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            frame1.Navigate(new Glavnaya(Log, frame1, Item));
        }
        private void UpdData_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            List<Results> List_Results = new List<Results>();
            int id_u;
            int id_w;
            int id_s;
            char r;
            int count = Entities1.GetContex().Results.Count();
            List_Results = Entities1.GetContex().Results.ToList();
            DateTime d;
            List_Results[0].id = count + 1;
            if (int.TryParse(id_userUpd.Text, out id_u))
            {
                if (int.TryParse(id_workUpd.Text, out id_w))
                {
                    if (int.TryParse(id_serviceUpd.Text, out id_s))
                    {
                        if (char.TryParse(resultUpd.Text, out r))
                        {
                            if (DateTime.TryParse(dataUpd.Text, out d))
                            {
                                List_Results[0].id_user = id_u;
                                List_Results[0].id_work = id_w;
                                List_Results[0].id_service = id_s;
                                List_Results[0].result = r.ToString();
                                List_Results[0].data = d;
                                Entities1.GetContex().SaveChanges();
                                frame1.Navigate(new Glavnaya(Log, frame1, Item));
                            }
                            else
                            {
                                MessageBox.Show("Неверно введёна дата");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Неверно введён результат");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Неверно введён id услуги");
                    }
                }
                else
                {
                    MessageBox.Show("Неверно введён id сотрудника");
                }

            }
            else
            {
                MessageBox.Show("Неверно введён id пользователя");
            }
        }

        private void VerefyPrint_Checked(object sender, RoutedEventArgs e)
        {
            if(D.Visibility == Visibility.Visible)
            {
                D.Visibility = Visibility.Collapsed;
                PrintRes.Visibility = Visibility.Visible;
            }
        }

        private void VerefyPrint_Unchecked(object sender, RoutedEventArgs e)
        {
            PrintRes.Visibility = Visibility.Collapsed;
            D.Visibility = Visibility.Visible;
        }
    }
}
