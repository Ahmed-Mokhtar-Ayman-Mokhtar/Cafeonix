using System;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Cafeonix.Views
{
    public partial class OrderPage : Page
    {
        public OrderPage()
        {
            InitializeComponent();
            this.Loaded += OrderPage_Loaded;
        }

        private void OrderPage_Loaded(object sender, RoutedEventArgs e)
        {
            // 1. مسار bin\Debug
            string exeDir = AppDomain.CurrentDomain.BaseDirectory;
            // 2. الصعود لمجلد المشروع (Debug → bin → جذر المشروع)
            string projectDir = Path.GetFullPath(Path.Combine(exeDir, "..", ".."));
            // 3. بناء المسار إلى Orders.xml في جذر المشروع
            string path = Path.Combine(projectDir, "Orders.xml");

            if (File.Exists(path))
            {
                var ds = new DataSet();
                ds.ReadXml(path);
                OrdersDataGrid.ItemsSource = ds.Tables[0].DefaultView;
            }
            else
            {
                MessageBox.Show($"لم يتم العثور على ملف الطلبات:\n{path}",
                                "خطأ", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


    }
}
