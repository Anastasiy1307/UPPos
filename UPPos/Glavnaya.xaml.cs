using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
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
using System.Windows.Threading;
using UPPos.Class;

namespace UPPos
{
    /// <summary>
    /// Логика взаимодействия для Glavnaya.xaml
    /// </summary>
    public partial class Glavnaya : Page
    {
        private DispatcherTimer _timer;
        public static readonly DependencyProperty TickCounterProperty = DependencyProperty.Register(
           "TickCounter", typeof(int), typeof(Glavnaya), new PropertyMetadata(default(int)));
        public Frame frame1;
        string User;
        string Workers;
        Navig sp = new Navig();
        List<Service> List_Service = new List<Service>();
        List<Results> List_Result = new List<Results>();
        List<Users> users = new List<Users>();
        List<Workers> workers = new List<Workers>();
        List<History> historys = new List<History>();
        int kolvo_zapice = 3;
        public Glavnaya(string log, Frame frame)
        {
            frame1 = frame;
            InitializeComponent();
            User = log;
            Workers = log;
            int count_hh = Entities1.GetContex().History.Count();
            historys = Entities1.GetContex().History.ToList();
            int time = 0;
            for (int j = count_hh - 1; j >= 0; j--)
            {
                if (historys[j].login == log)
                {
                    DateTime b = (DateTime)historys[j].Block;
                    DateTime d = (DateTime)historys[j].dataZ;
                    int h = b.Hour - d.Hour;
                    int m = b.Minute - d.Minute;
                    time = 60 * h + m;
                    break;
                }
            }
            DataContext = sp;
            DateTime dateTime = DateTime.Now;
            TickCounter = time;
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMinutes(1d);
            _timer.Tick += new EventHandler(Timer_Tick);
            _timer.Start();

            workers = Entities1.GetContex().Workers.ToList();
            int count = Entities1.GetContex().Workers.Count();
            for (int i = 0; i < count; i++)
            {
                if (workers[i].login == log && workers[i].id_dolgnost == 2 || workers[i].login == log && workers[i].id_dolgnost == 3)
                {
                    //Image.Visibility= Visibility.Hidden;
                }
                if (workers[i].login == log && workers[i].id_dolgnost == 1)
                {
                    //Image.Visibility = Visibility.Hidden;
                }
            }
            ////Вывод таблицы Сервис
            //List_Service = Entities1.GetContex().Service.ToList();
            //int countS = Entities1.GetContex().Service.Count();
            //sp.CountPageFlower = 3;
            //sp.CountlistFlower = countS;
            //LViewServ.ItemsSource = List_Service.Skip(0).Take(sp.CountPagesFlower).ToList();
            ////Вывод таблицы 
            //List_Result = Entities1.GetContex().Results.ToList();
            //int countR = Entities1.GetContex().Results.Count();
            //sp.CountPageFlower = 3;
            //sp.CountlistFlower = countR;
            //LViewResult.ItemsSource = List_Result.Skip(0).Take(sp.CountPagesFlower).ToList();
        }

        public int TickCounter
        {
            get { return (int)GetValue(TickCounterProperty); }
            set { SetValue(TickCounterProperty, value); }
        }

        private void Glavnaya_Rezultat(object sender, MouseButtonEventArgs e)
        {
           
        }

       


        public int soxr = 0;
        private void Timer_Tick(object sender, EventArgs e)
        {

            if (--TickCounter <= 0)
            {
                var timer = (DispatcherTimer)sender;
                timer.Stop();
                if (soxr == 0)
                {
                    if (MessageBox.Show("Чтобы закончить работу и закрыть кабинет на кварцевание нажмите да, если хотите продолжить работу на 5 минут нажмите нет", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                    {
                        TickCounter = 5;
                        _timer = new DispatcherTimer();
                        _timer.Interval = TimeSpan.FromMinutes(1d);
                        _timer.Tick += new EventHandler(Timer_Tick);
                        _timer.Start();
                        soxr = 1;
                    }
                    else
                    {
                        MessageBox.Show("Закрытие программы");
                        Environment.Exit(0);
                    }
                }
                else
                {
                    MessageBox.Show("Закрытие программы");
                    Environment.Exit(0);
                }
            }
        }
    
        private void Back_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            frame1.Navigate(new LogIn(frame1));
        }

        private void Time_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Service_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Service.Visibility = Visibility.Hidden;
            Result.Visibility = Visibility.Visible;
            LViewServ.Visibility = Visibility.Hidden;
            LViewResult.Visibility = Visibility.Visible;
         

            var all = Entities1.GetContex().Service.ToList();

            ComboType.ItemsSource = all;
            LViewServ.ItemsSource = all;

            LViewServ.Visibility = Visibility.Visible;
            LViewResult.Visibility = Visibility.Hidden;
            Str.Visibility = Visibility.Visible;
            TBoxSearch.Text = "";
            List_Service = Entities1.GetContex().Service.ToList();
            LViewServ.ItemsSource = Entities1.GetContex().Service.ToList();

            sp.CountPageFlower = Entities1.GetContex().Service.ToList().Count;
            DataContext = sp;

            try
            {
                sp.CountPageFlower = Convert.ToInt32(kolvo_zapice); // если в текстовом поле есnь значение, присваиваем его свойству объекта, которое хранит количество записей на странице
            }
            catch
            {
                sp.CountPageFlower = List_Service.Count; // если в текстовом поле значения нет, присваиваем свойству объекта, которое хранит количество записей на странице количество элементов в списке
            }
            sp.CountlistFlower = List_Service.Count;  // присваиваем новое значение свойству, которое в объекте отвечает за общее количество записей
            LViewServ.ItemsSource = List_Service.Skip(0).Take(sp.CountPageFlower).ToList();  // отображаем первые записи в том количестве, которое равно CountPage
            sp.CurrentPage = 1; // текущая страница - это страница 1
        }

        private void Result_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Result.Visibility = Visibility.Hidden;
            Service.Visibility = Visibility.Visible;
            LViewResult.Visibility = Visibility.Hidden;
            LViewServ.Visibility = Visibility.Visible;

            List<Results> result = new List<Results>();
            List<Users> use = new List<Users>();
            TBoxSearch.Text = "";
            result = Entities1.GetContex().Results.ToList();
            use = Entities1.GetContex().Users.ToList();
            int counts1 = Entities1.GetContex().Results.Count();
            //for (int i = 0; i < counts1; i++)
            //{
            //    if (result[i].login != user)
            //    {
            //        result.RemoveAt(i);
            //        i--;
            //        counts1--;
            //    }
            //}
            LViewResult.ItemsSource = result;
            LViewServ.Visibility = Visibility.Hidden;
            LViewResult.Visibility = Visibility.Visible;
        }
        private void Glavnaya_GoPage(object sender, MouseButtonEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;

            switch (tb.Uid)  // определяем, куда конкретно было сделано нажатие
            {
                case "prev":
                    sp.CurrentPage--;
                    break;
                case "next":
                    sp.CurrentPage++;
                    break;
                case "prev1":
                    sp.CurrentPage = 1;
                    break;
                case "next1":
                    {
                        List<Service> fl = Entities1.GetContex().Service.ToList();
                        int a = fl.Count;
                        int b = Convert.ToInt32(kolvo_zapice);

                        if (a % b == 0)
                        {
                            sp.CurrentPage = a / b;
                        }
                        else
                        {
                            sp.CurrentPage = a / b + 1;
                        }
                    }
                    break;
                default:
                    sp.CurrentPage = Convert.ToInt32(tb.Text);
                    break;
            }
            LViewServ.ItemsSource = List_Service.Skip(sp.CurrentPage * sp.CountPageFlower - sp.CountPageFlower).Take(sp.CountPageFlower).ToList();
        }
    }
}
