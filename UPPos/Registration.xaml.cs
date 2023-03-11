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
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Page
    {
        public Frame frame1;
        public Registration(Frame frame)
        {
            InitializeComponent();
            frame1 = frame;
        }
        private void Regis_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string log = login.Text;
            string pas = password.Text;
            string pas1 = password_Copy.Text;
            if (log != "")
            {
                if (pas != "")
                {
                    if (pas == pas1)
                    {
                        List<UPPos.Users> user = new List<UPPos.Users>() { new Users() };
                        List<History> h = new List<History>() { new History() };
                        int count = Entities1.GetContex().Users.Count();
                        int count_h = Entities1.GetContex().History.Count();
                        user[0].id = count + 1;
                        user[0].login = log;
                        user[0].password = pas1;
                        h[0].id = count_h + 1;
                        h[0].login = log;
                        h[0].ip = Dns.GetHostName();
                        h[0].dataZ = DateTime.Now;
                        h[0].Block = DateTime.Now.AddMinutes(-30);
                        Entities1.GetContex().History.Add(h[0]);
                        Entities1.GetContex().Users.Add(user[0]);
                        Entities1.GetContex().SaveChanges();
                        frame1.Navigate(new LogIn(frame1));
                    }
                    else
                    {
                        MessageBox.Show("Пароли не совпадают");
                    }
                }
                else
                {
                    MessageBox.Show("Введите пароль");
                }
            }
            else
            {
                MessageBox.Show("Введите логин");
            }
        }
        private void Back_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            frame1.Navigate(new LogIn(frame1));
        }
    }
}
