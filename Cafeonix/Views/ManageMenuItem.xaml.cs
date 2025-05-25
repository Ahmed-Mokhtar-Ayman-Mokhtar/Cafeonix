using Cafeonix.Data;
using Cafeonix.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Cafeonix.Views
{
    public partial class ManageMenuItems : Page
    {
        
        public ObservableCollection<Models.MenuItem> MenuItems { get; set; }

        public ManageMenuItems()
        {
            InitializeComponent();
            LoadMenuItems();
        }

        private void LoadMenuItems()
        {
            try
            {
                using (var context = new AppDbContext())
                {
                    // قراءة البيانات من قاعدة البيانات إلى القائمة
                    var items = context.MenuItems.ToList();
                    MenuItems = new ObservableCollection<Models.MenuItem>(items);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("خطأ في تحميل الأصناف: " + ex.Message);
                MenuItems = new ObservableCollection<Models.MenuItem>();
            }

            this.DataContext = this;
        }

        private void MenuItemsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MenuItemsGrid.SelectedItem is Models.MenuItem selected)
            {
                IdTextBox.Text = selected.Id.ToString();
                NameTextBox.Text = selected.Name;
                PriceTextBox.Text = selected.Price.ToString();
                ImagePathTextBox.Text = selected.ImagePath;
                CategoryTextBox.Text = selected.Category;
            }
        }

        private void NewButton_Click(object sender, RoutedEventArgs e)
        {
            IdTextBox.Text = "";
            NameTextBox.Text = "";
            PriceTextBox.Text = "";
            ImagePathTextBox.Text = "";
            CategoryTextBox.Text = "";
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var newItem = new Models.MenuItem
                {
                    Name = NameTextBox.Text,
                    Price = decimal.Parse(PriceTextBox.Text),
                    ImagePath = ImagePathTextBox.Text,
                    Category = CategoryTextBox.Text
                };

                using (var context = new AppDbContext())
                {
                    context.MenuItems.Add(newItem);
                    context.SaveChanges();
                }


                MenuItems.Add(newItem);
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
                MessageBox.Show("لا يمكن تعديل الحقل ID.", "خطأ");
                return;
            }

            using (var context = new AppDbContext())
            {
                var item = context.MenuItems.Find(id);
                if (item == null) return;
            item.Name = NameTextBox.Text;
            item.Price = decimal.Parse(PriceTextBox.Text);
            item.ImagePath = ImagePathTextBox.Text;
            item.Category = CategoryTextBox.Text;

            context.SaveChanges();

            var localItem = MenuItems.FirstOrDefault(x => x.Id == id);
            if (localItem != null)
            {
                localItem.Name = item.Name;
                localItem.Price = item.Price;
                localItem.ImagePath = item.ImagePath;
                localItem.Category = item.Category;
                MenuItemsGrid.Items.Refresh();
            }
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(IdTextBox.Text, out int id))
            {
                MessageBox.Show("الرجاء اختيار صنف للحذف.", "خطأ");
                return;
            }
            using (var context = new AppDbContext())
            {
                var item = context.MenuItems.Find(id);
                if (item == null) return;

            context.MenuItems.Remove(item);
            context.SaveChanges();

            }
            var localItem = MenuItems.FirstOrDefault(x => x.Id == id);
            if (localItem != null)
                MenuItems.Remove(localItem);

            NewButton_Click(null, null);
        }
    }
}
