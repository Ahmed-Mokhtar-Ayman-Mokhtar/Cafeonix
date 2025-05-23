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
using System.Windows.Shapes;

namespace Cafeonix.Views
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            // مثال تحقق ثابت
            if (UsernameBox.Text == "123" && PasswordBox.Password == "123")
            {
                // إنشاء MainWindow
                var mainWindow = new MainWindow();

                // إعادة تعيين Application.Current.MainWindow إلى النافذة الجديدة
                Application.Current.MainWindow = mainWindow;

                // عرض MainWindow
                mainWindow.Show();

                // 4) إغلاق LoginWindow (التي كانت MainWindow سابقاً)
                this.Close();
            }
            else
            {
                MessageBox.Show(
                    "Invalid credentials",
                    "Login Failed",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
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
    }
}
