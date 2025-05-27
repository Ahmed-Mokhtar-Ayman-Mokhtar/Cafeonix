using Cafeonix.Views;
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

namespace Cafeonix
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new Uri("Views/MenuPage.xaml", UriKind.Relative));
            home_btn.Background = new SolidColorBrush(Color.FromRgb(51, 21, 5));
        }

        private void NavButton_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton == null) return;

            string pageUri = clickedButton.Tag as string;
            if (string.IsNullOrEmpty(pageUri)) return;

            // تحميل الصفحة داخل الـ Frame
            MainFrame.Navigate(new Uri(pageUri, UriKind.Relative));

            // تغيير ألوان الأزرار
            HighlightButton(clickedButton);
        }

        private void HighlightButton(Button selectedButton)
        {
            foreach (var child in ButtonsPanel.Children)
            {
                if (child is Button btn)
                {
                    if (btn == selectedButton)
                    {
                        // تغيير لون الزر المحدد
                        btn.Background = new SolidColorBrush(Color.FromRgb(51, 21, 5)); // #3B1A04
                       
                    }
                    else
                    {
                        // إعادة لون باقي الأزرار إلى اللون الافتراضي
                        btn.Background = new SolidColorBrush(Color.FromRgb(63, 29, 12)); // #3F1D0C
                    }
                }
            }
        }

        private void NavButton_MouseEnter(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null) return;

            // فقط لو لم يكن الزر محدد (مُختار)
            if (btn.Background != new SolidColorBrush(Color.FromRgb(59, 26, 4))) // #3B1A04
            {
                btn.Background = new SolidColorBrush(Color.FromRgb(59, 26, 4)); // لون عند hover
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            // إنشاء نافذة تسجيل الدخول
            Window loginWindow = new LoginWindow();
            Application.Current.MainWindow = loginWindow;

            // عرضها
            loginWindow.Show();

            // إغلاق نافذة الإدارة الحالية
            this.Close();
        }

    }
}
