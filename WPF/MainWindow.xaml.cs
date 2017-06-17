using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using API;
using DAL;

namespace WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IPersonDAO personDao = PersonDAO_Mock.GetInstance();
        private string _filterString = "";

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        public void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            MainTable.ItemsSource = personDao.Read();
            MainTable.Items.Filter = Filter;
        }

        private void Slider_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            Canvas.DefaultDrawingAttributes.Width = ((Slider)sender).Value;
            Canvas.DefaultDrawingAttributes.Height = ((Slider)sender).Value;
        }

        private void SaveCanvasBtn_Click(object sender, RoutedEventArgs e)
        {
            
            string path = NormalizePath(System.AppDomain.CurrentDomain.BaseDirectory) + "Images\\" + Guid.NewGuid() + ".bmp";
            IDS ds = DSFactory.GetInstance(path);
            RenderTargetBitmap targetBitmap = 
                new RenderTargetBitmap((int)Canvas.ActualWidth,
                (int)Canvas.ActualHeight, 96d, 96d, PixelFormats.Default);
            targetBitmap.Render(Canvas);
            BmpBitmapEncoder encoder = new BmpBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(targetBitmap));
            ds.Save(encoder);
        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            Canvas.Strokes.Clear();
        }

        public void ColorBtn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            SolidColorBrush brush = (SolidColorBrush)btn.Background;
            Canvas.DefaultDrawingAttributes.Color = brush.Color;
        }

        public void RemoveBtn_Click(object sender, RoutedEventArgs e)
        {
            Person p = ((sender as Button)?.DataContext as Person);
            if (p != null)
            {
                PersonDAO_Mock.GetInstance().Delete(p);
                ItemsSourceUpdated();
            }
        }

        public void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string fName = FirstName.Text;
                string lName = LastName.Text;
                int age = int.Parse(Age.Text);
                Person p = new Person(0, fName, lName, age);
                PersonDAO_Mock.GetInstance().Create(p);
                FirstName.Text = "";
                LastName.Text = "";
                Age.Text = "";
                ItemsSourceUpdated();
            }
            catch (Exception exc)
            {
                Debug.WriteLine("SaveBtn_Click -> " + exc.Message);
            }

        }

        public void FilterText_Changed(object sender, RoutedEventArgs e)
        {
            _filterString = (sender as TextBox)?.Text;
            ItemsSourceUpdated();
        }

        private bool Filter(object obj)
        {
            bool result = true;
            Person current = obj as Person;
            if (!string.IsNullOrWhiteSpace(_filterString)
                && current != null
                && !(current.FirstName.ToLower().Contains(_filterString.ToLower()))
                && !(current.LastName.ToLower().Contains(_filterString.ToLower()))
                && !(current.Age.ToString().Contains(_filterString)))
            {
                result = false;
            }
            return result;
        }

        private void ItemsSourceUpdated()
        {
            MainTable.ItemsSource = null;
            MainTable.ItemsSource = PersonDAO_Mock.GetInstance().Read();
            MainTable.Items.Refresh();
        }

        private string NormalizePath(string path)
        {
            return path.Replace("\\bin\\", "").Replace("Debug", "").Replace("Release", "");
        }
    }
}
