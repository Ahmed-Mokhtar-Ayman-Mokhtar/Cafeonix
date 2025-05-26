using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;
using Cafeonix.Data;
using Cafeonix.Models;
using Cafeonix.ViewModels;

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

            CurrentOrder = new ObservableCollection<Models.MenuItem>();

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


        // زر تأكيد الطلب
        private void ConfirmOrder_Click(object sender, RoutedEventArgs e)
        {
            //  مسار bin\Debug
            string exeDir = AppDomain.CurrentDomain.BaseDirectory;
            //  الصعود لمجلد المشروع
            string projectDir = Path.GetFullPath(Path.Combine(exeDir, "..", ".."));
            //  ملف Orders.xml في جذر المشروع
            string path = Path.Combine(projectDir, "OrderArchives.xml");

            //  تحميل أو إنشاء مستند XML
            XDocument doc;
            if (File.Exists(path))
                doc = XDocument.Load(path);
            else
                doc = new XDocument(new XElement("Order"));

            //  حساب الـ ID التالي
            int nextId = 1;
            var existing = doc.Root.Elements("Order");
            if (existing.Any())
                nextId = existing.Max(x => (int)x.Element("ID")) + 1;

            //  جلب اسم المستخدم من MainViewModel
            var mainVm = (Application.Current.MainWindow as MainWindow)?
                             .DataContext as MainViewModel;
            string cashierName = mainVm?.CurrentUser.Username ?? "Unknown";

            //  تجهيز السجل الجديد
            var newOrder = new XElement("Order",
                new XElement("ID", nextId),
                new XElement("Price", TotalAmount),
                new XElement("Emp", cashierName),
                new XElement("Time", DateTime.Now.ToString("yyyy-MM-dd      HH:mm:ss"))
            );

            //  إضافة السجل وحفظ الملف
            doc.Root.Add(newOrder);
            doc.Save(path);

            //  إعلام المستخدم وتنظيف الطلب
            MessageBox.Show($"تم تأكيد الطلب بمبلغ {TotalAmount:C}", "نجاح",
                            MessageBoxButton.OK, MessageBoxImage.Information);
            CurrentOrder.Clear();
            TotalTextBlock.Text = $"المجموع: {TotalAmount:C}";
        }
    }
}
