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
    /// Логика взаимодействия для LogIn.xaml
    /// </summary>
    public partial class LogIn : Page
    {
        public Frame frame1;
        public LogIn(Frame frame)
        {
            frame1 = frame;
            InitializeComponent();
        }
        private void Reg_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            frame1.Navigate(new Registration(frame1));
        }
        private void LogIn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            frame1.Navigate(new Glavnaya(frame1));
        }
        private void Gues_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            frame1.Navigate(new Glavnaya(frame1));
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
