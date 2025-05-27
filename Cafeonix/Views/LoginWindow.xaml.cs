using Cafeonix.Models;
using System.Linq;
using System.Windows;
using Cafeonix.Data;
using System.Windows.Controls;
using System.Windows.Input;
using Cafeonix.ViewModels;

namespace Cafeonix.Views
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameBox.Text.Trim();
            string password = PasswordBox.Password;

            using (var context = new AppDbContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);

                if (user != null)
                {
                    var userViewModel = new UserViewModel
                    {
                        Username = user.Username
                    };

                    Window nextWindow;

                    if (user.Role == UserRole.Admin)
                    {
                        nextWindow = new AdminWindow(); // نافذة المسؤول
                    }
                    else
                    {
                        var mainViewModel = new MainViewModel(userViewModel);
                        nextWindow = new MainWindow
                        {
                            DataContext = mainViewModel
                        };
                    }

                    Application.Current.MainWindow = nextWindow;
                    nextWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("اسم المستخدم أو كلمة المرور غير صحيحة", "فشل تسجيل الدخول", MessageBoxButton.OK, MessageBoxImage.Error);
                }
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
