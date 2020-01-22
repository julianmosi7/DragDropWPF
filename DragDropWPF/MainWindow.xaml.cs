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

namespace DragDropWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitGrid();
            InitTree();
            InitLists();
            InitDragDrop();
        }

        private void InitDragDrop()
        {
            lblDropStrings.AllowDrop = true;
            lblDropStrings.DragOver += LblDrop_DragOver<string>;
            lblDropStrings.Drop += LblDropStrings_Drop;
            lblDropStrings.DragEnter += (s, e) => lblDropStrings.Background = Brushes.SkyBlue;
            lblDropStrings.DragLeave += (s, e) => lblDropStrings.Background = Brushes.White;

            lblDropPersons.AllowDrop = true;
            lblDropPersons.DragOver += LblDrop_DragOver<Person>;
            lblDropPersons.Drop += LblDropPersons_Drop;
        }

        private void LblDropPersons_Drop(object sender, DragEventArgs e)
        {
            var person = e.Data.GetData(typeof(Person)) as Person;
            lblDropPersons.Content = person.ToString();
        }

        private void LblDropStrings_Drop(object sender, DragEventArgs e)
        {
            string data = e.Data.GetData(typeof(string)) as string;
            lblDropStrings.Content = data;
            lblDropStrings.Background = Brushes.White;
        }

        private void LblDrop_DragOver<T>(object sender, DragEventArgs e)
        {
            bool isStringData = e.Data.GetDataPresent(typeof(T));
            e.Effects = isStringData
                ? DragDropEffects.Copy
                : DragDropEffects.None;
            e.Handled = true;
        }

        private void InitGrid()
        {
            grdPersons.ItemsSource = new List<Person>(){
            new Person { Firstname = "Bertl", Lastname = "Braun" },
            new Person { Firstname = "Paula", Lastname = "Grün" },
            new Person { Firstname = "Arielle", Lastname = "Rot" }
            };
        }

        private void InitTree()
        {
            var root = new TreeViewItem { Header = "A" };
            var b0 = new TreeViewItem { Header = "B0" };
            var b1 = new TreeViewItem { Header = "B1" };
            var b2 = new TreeViewItem { Header = "B2" };
            root.Items.Add(b0);
            root.Items.Add(b1);
            root.Items.Add(b2);
            b0.Items.Add(new TreeViewItem { Header = "C0" });
            b0.Items.Add(new TreeViewItem { Header = "C1" });
            b1.Items.Add(new TreeViewItem { Header = "C2" });
            b1.Items.Add(new TreeViewItem { Header = "C3" });
            b1.Items.Add(new TreeViewItem { Header = "C4" });
            b2.Items.Add(new TreeViewItem { Header = "C5" });
            trvItems.Items.Add(root);
            root.IsExpanded = true;
        }

        private void InitLists()
        {
            lstStrings.Items.Add("Item A");
            lstStrings.Items.Add("Item B");
            lstStrings.Items.Add("Item C");
            lstStrings.Items.Add("Item D");
            lstStrings.Items.Add("Item E");

            lstPersons.Items.Add(new Person { Firstname = "Hansi", Lastname = "Huber" });
            lstPersons.Items.Add(new Person { Firstname = "Susi", Lastname = "Müller" });
            lstPersons.Items.Add(new Person { Firstname = "Heinzi", Lastname = "Prüller" });
            lstPersons.Items.Add(new Person { Firstname = "Maxi", Lastname = "Stadler" });
        }

        private void lblSource_MouseDown(object sender, MouseButtonEventArgs e)
        {
            string data = lblSource.Content as String;
            DragDrop.DoDragDrop(lblSource, data, DragDropEffects.All);
        }

        private void ListBoxItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var listBoxItem = sender as ListBoxItem;
            var person = listBoxItem.Content as Person;
            if (person == null) return;
            DragDrop.DoDragDrop(listBoxItem, person, DragDropEffects.All);
        }

        private void lstStrings_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var ele = lstStrings.InputHitTest(e.GetPosition(lstStrings));
            var listBoxItem = GetListBoxItem(ele as DependencyObject);
            if (listBoxItem == null) return;
            string data = listBoxItem.Content.ToString();
            DragDrop.DoDragDrop(lstStrings, data, DragDropEffects.All);
        }

        private ListBoxItem GetListBoxItem(DependencyObject ele)
        {
            while(ele != null && !(ele is ListBoxItem))
            {
                ele = VisualTreeHelper.GetParent(ele);
            }
            return ele as ListBoxItem;
        }
    }
}
