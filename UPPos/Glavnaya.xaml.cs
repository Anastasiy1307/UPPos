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
        List<Users> users = new List<Users>();
        List<Workers> workers = new List<Workers>();
        List<History> historys = new List<History>();
        int kolvo_zapice = 3;
        public Glavnaya(string log, Frame frame)
        {
            frame1 = frame;
            InitializeComponent();
            frame1 = frame;
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
        }

        public int TickCounter
        {
            get { return (int)GetValue(TickCounterProperty); }
            set { SetValue(TickCounterProperty, value); }
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
    }
}
