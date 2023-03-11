using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    /// Логика взаимодействия для LogIn.xaml
    /// </summary>
    public partial class LogIn : Page
    {
        public Frame frame1;    
        public int vx = 0;
        int error = 0;
        DateTime date;
        public LogIn(Frame frame)
        {
            frame1 = frame;
            InitializeComponent();
            date = DateTime.Now;
        }
        List<UPPos.Users> users = new List<UPPos.Users>();
        List<UPPos.Workers> workers = new List<UPPos.Workers>();
        List<UPPos.History> history = new List<UPPos.History>();

        private void Reg_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            frame1.Navigate(new Registration(frame1));
        }

  
        List<UPPos.History> history_login = new List<UPPos.History> { new History() };

        public void vxod()
        {
          
            string log = login.Text;
            string pas = password.Password;
            int count = Entities1.GetContex().Users.Count();
            users = Entities1.GetContex().Users.ToList();
            int count1 = Entities1.GetContex().Workers.Count();
            workers = Entities1.GetContex().Workers.ToList();
            int count_hh = Entities1.GetContex().History.Count();
            history = Entities1.GetContex().History.ToList();
            for (int i = 0; i < count; i++)
            {
                if (users[i].login == log)
                {
                    if (users[i].password == pas)
                    {
                        for (int j = count_hh - 1; j >= 0; j--)
                        {
                            if (history[j].login == log)
                            {
                                DateTime t = DateTime.Now;
                                if (history[j].Block != null)
                                {
                                    t = (DateTime)history[j].Block;
                                    t = t.AddMinutes(30);
                                }
                                if (DateTime.Now < history[j].Block || t <= DateTime.Now)
                                {
                                    vx = 1;
                                    int count_h = Entities1.GetContex().History.Count();
                                    history_login[0].login = log;
                                    history_login[0].dataZ = DateTime.Now;
                                    history_login[0].ip = Dns.GetHostName();
                                    if (history[j].Block < DateTime.Now)
                                    {
                                        history_login[0].Block = date.AddHours(2.5);
                                    }
                                    else
                                    {
                                        history_login[0].Block = history[j].Block;
                                    }
                                    Entities1.GetContex().History.Add(history_login[0]);
                                    Entities1.GetContex().SaveChanges();
                                    frame1.Navigate(new Glavnaya(users[i].login, frame1));
                                    break;
                                }
                                else
                                {
                                    MessageBox.Show("Перерыв пол часа");
                                    vx = 1;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < count1; i++)
            {
                if (workers[i].login == log)
                {
                    if (workers[i].password == pas)
                    {
                        for (int j = count_hh - 1; j >= 0; j--)
                        {
                            if (history[j].login == log)
                            {
                                DateTime t = DateTime.Now;
                                if (history[j].Block != null)
                                {
                                    t = (DateTime)history[j].Block;
                                    t = t.AddMinutes(30);
                                }
                                if (DateTime.Now < history[j].Block || t <= DateTime.Now)
                                {
                                    vx = 1;
                                    int count_h = Entities1.GetContex().History.Count();
                                    history_login[0].id = count_h + 1;
                                    history_login[0].login = log;
                                    history_login[0].dataZ = DateTime.Now;
                                    history_login[0].ip = Dns.GetHostName();
                                    if (history[j].Block < DateTime.Now)
                                    {
                                        history_login[0].Block = date.AddHours(2.5);
                                    }
                                    else
                                    {
                                        history_login[0].Block = history[j].Block;
                                    }
                                    Entities1.GetContex().History.Add(history_login[0]);
                                    Entities1.GetContex().SaveChanges();
                                    frame1.Navigate(new Glavnaya(workers[i].login, frame1));
                                    vx = 1;
                                    break;
                                }
                                else
                                {
                                    MessageBox.Show("Перерыв пол часа");
                                    vx = 1;
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            if (vx == 0)
            {
                MessageBox.Show("Неверный логин или пароль");
                error++;
            }
        }

        private void Avtoriz_Vxod(object sender, MouseButtonEventArgs e)
        {
            Capcha captcha = new Capcha();
            if (error > 3)
            {
                captcha.Show();
            }
            else
            {
                vx = 0;
                vxod();
            }

        }

       
        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            nevisu.Visibility = Visibility.Hidden;
            Visu.Visibility = Visibility.Visible;
            pwdVisu.Text = password.Password; // скопируем в TextBox из PasswordBox
            pwdVisu.Visibility = Visibility.Visible; // TextBox - отобразить
            password.Visibility = Visibility.Hidden; // PasswordBox - скрыть
        }

        private void Visu_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            nevisu.Visibility = Visibility.Visible;
            Visu.Visibility = Visibility.Hidden;
            password.Password = pwdVisu.Text; // скопируем в PasswordBox из TextBox 
            pwdVisu.Visibility = Visibility.Hidden; // TextBox - скрыть
            password.Visibility = Visibility.Visible; // PasswordBox - отобразить
        }
    }
}
