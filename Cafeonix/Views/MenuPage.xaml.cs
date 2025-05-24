using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Cafeonix.Models;

namespace Cafeonix.Views
{
    public partial class MenuPage : Page
    {
        // قائمة الأصناف الوهمية
        public ObservableCollection<Models.MenuItem> MenuItems { get; set; }
        // الطلب الجاري
        public ObservableCollection<Models.MenuItem> CurrentOrder { get; set; }

        public decimal TotalAmount => CalculateTotal();

        public MenuPage()
        {
            InitializeComponent();

            // تهيئة التجميعتين
            MenuItems = new ObservableCollection<Models.MenuItem>
            {
                new Models.MenuItem { Id=1, Name="كابتشينو", Price=2.50m, ImagePath="/Resources/Images/meal.png", Category="قهوة" },
                new Models.MenuItem { Id=2, Name="لاتيه",     Price=3.00m, ImagePath="/Resources/Images/meal.png",       Category="قهوة" },
                new Models.MenuItem { Id=3, Name="موكا",       Price=3.50m, ImagePath="/Resources/Images/meal.png",       Category="قهوة" },
                new Models.MenuItem { Id=4, Name="شاي أخضر",   Price=1.80m, ImagePath="/Resources/Images/meal.png",   Category="شاي"  },
                new Models.MenuItem { Id=5, Name="كابتشينو", Price=2.50m, ImagePath="/Resources/Images/meal.png", Category="قهوة" },
                new Models.MenuItem { Id=6, Name="لاتيه",     Price=3.00m, ImagePath="/Resources/Images/meal.png",       Category="قهوة" },
                new Models.MenuItem { Id=7, Name="موكا",       Price=3.50m, ImagePath="/Resources/Images/meal.png",       Category="قهوة" },
                new Models.MenuItem { Id=8, Name="شاي أخضر",   Price=1.80m, ImagePath="/Resources/Images/meal.png",   Category="شاي"  },
                new Models.MenuItem { Id=9, Name="كابتشينو", Price=2.50m, ImagePath="/Resources/Images/meal.png", Category="قهوة" },
                new Models.MenuItem { Id=10, Name="لاتيه",     Price=3.00m, ImagePath="/Resources/Images/meal.png",       Category="قهوة" },
                new Models.MenuItem { Id=11, Name="موكا",       Price=3.50m, ImagePath="/Resources/Images/meal.png",       Category="قهوة" },
                new Models.MenuItem { Id=12, Name="شاي أخضر",   Price=1.80m, ImagePath="/Resources/Images/meal.png",   Category="شاي"  },
                // أضف المزيد حسب الحاجة...
            };

            CurrentOrder = new ObservableCollection<Models.MenuItem>();

            // نربط البيانات بالـ DataContext
            this.DataContext = this;
        }

        private decimal CalculateTotal()
        {
            decimal sum = 0;
            foreach (var item in CurrentOrder)
                sum += item.Price;
            return sum;
        }

        // عند الضغط على زر + بجوار الصنف
        private void AddToOrder_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is Models.MenuItem menuItem)
            {
                CurrentOrder.Add(menuItem);
                // نحدث المجموع
                TotalTextBlock.Text = $"المجموع: {TotalAmount:0.00} ﷼";
            }
        }

        // إزالة صنف من الطلب
        private void RemoveFromOrder_Click(object sender, RoutedEventArgs e)
        {
            // نأخذ المؤشر الحالي في الـ ListBox
            int idx = CurrentOrderList.SelectedIndex;
            if (idx >= 0 && idx < CurrentOrder.Count)
            {
                CurrentOrder.RemoveAt(idx);
                // تحديث المجموع بعد الحذف
                TotalTextBlock.Text = $"المجموع: {TotalAmount:C}";
            }
            else
            {
                MessageBox.Show("رجاءً اختر صنفاً أولاً من الطلب الحالي.", "تحذير",
                                MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        // تأكيد الطلب (فقط نموذجي)
        private void ConfirmOrder_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"تم تأكيد الطلب بمبلغ {TotalAmount:C}", "نجاح", MessageBoxButton.OK, MessageBoxImage.Information);
            CurrentOrder.Clear();
            TotalTextBlock.Text = $"المجموع: {TotalAmount:C}";
        }
    }
}
