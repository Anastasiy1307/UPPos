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
    /// Логика взаимодействия для UpdResult.xaml
    /// </summary>
    
    public partial class UpdResult : Page
    {
        public Frame frame1;
        object Item;
        List<Results> List_Results = new List<Results>() { new Results() };
        List<Workers> workers = new List<Workers>();
        private List<char> znak = new List<char>() { '+', '-' };
        public List<char> Znak
        {
            get => znak;
            set { znak = value; }
        }
        string Log;
        public UpdResult(string log, Frame frame, object item)
        {
            DataContext = this;
            frame1 = frame;
            Log = log;
            Item = item;
            InitializeComponent();
        }
        private void BackSBut_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            frame1.Navigate(new Glavnaya(Log, frame1, Item));
        }
    }
}
