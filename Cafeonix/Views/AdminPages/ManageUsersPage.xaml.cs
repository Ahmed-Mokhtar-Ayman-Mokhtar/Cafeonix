using Cafeonix.Data;
using Cafeonix.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Cafeonix.Views
{
    public partial class ManageUserPage : Page
    {
        public ObservableCollection<User> Users { get; set; }

        public ManageUserPage()
        {
            InitializeComponent();
            LoadUsers();
        }

        private void LoadUsers()
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    // قراءة المستخدمين من قاعدة البيانات إلى القائمة
                    var users = context.Users.ToList();
                    Users = new ObservableCollection<Models.User>(users);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("حدث خطأ أثناء تحميل المستخدمين: " + ex.Message);
                Users = new ObservableCollection<User>();
            }

            this.DataContext = this;
        }

        private void UsersGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (UsersGrid.SelectedItem is User user)
            {
                IdTextBox.Text = user.ID.ToString();
                UsernameTextBox.Text = user.Username;
                PasswordBox.Password = user.Password;
                RoleComboBox.SelectedIndex = (int)user.Role;
            }
        }

        private void NewButton_Click(object sender, RoutedEventArgs e)
        {
            IdTextBox.Text = "";
            UsernameTextBox.Text = "";
            PasswordBox.Password = "";
            RoleComboBox.SelectedIndex = 1;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var newUser = new User
                {
                    Username = UsernameTextBox.Text,
                    Password = PasswordBox.Password,
                    Role = (UserRole)RoleComboBox.SelectedIndex
                };

                using (var context = new AppDbContext())
                {
                    context.Users.Add(newUser);
                    context.SaveChanges();
                }

                Users.Add(newUser);
                NewButton_Click(null, null);
            }
            catch
            {
                MessageBox.Show("تأكد من صحة البيانات.", "خطأ");
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(IdTextBox.Text, out int id))
            {
                MessageBox.Show("معرف غير صالح للتعديل.");
                return;
            }

            using (var context = new AppDbContext())
            {
                var user = context.Users.Find(id);
                if (user == null) return;

                user.Username = UsernameTextBox.Text;
                user.Password = PasswordBox.Password;
                user.Role = (UserRole)RoleComboBox.SelectedIndex;

                context.SaveChanges();

                var localUser = Users.FirstOrDefault(x => x.ID == id);
                if (localUser != null)
                {
                    localUser.Username = user.Username;
                    localUser.Password = user.Password;
                    localUser.Role = user.Role;
                    UsersGrid.Items.Refresh();
                }
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(IdTextBox.Text, out int id))
            {
                MessageBox.Show("الرجاء اختيار مستخدم للحذف.");
                return;
            }

            using (var context = new AppDbContext())
            {
                var user = context.Users.Find(id);
                if (user == null) return;

                context.Users.Remove(user);
                context.SaveChanges();
            }

            var localUser = Users.FirstOrDefault(x => x.ID == id);
            if (localUser != null)
                Users.Remove(localUser);

            NewButton_Click(null, null);
        }
    }
}
