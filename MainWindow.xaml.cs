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
using System.IO;
using Microsoft.Win32;

namespace Name_Picker
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

        private readonly Random ran = new Random();
        private readonly List<string> mainNameList = new List<string>();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadLists();
            }
            catch
            {
                mainNameList.Clear();
                Names.Items.Clear();
            }
        }

        private void LoadLists()
        {
            string home = Environment.CurrentDirectory + "\\lists";
            foreach (string fname in Directory.GetFiles(home, "*.lst"))
            {
                MenuItem i = new MenuItem() { Header = fname.Substring(home.Length + 1, -4) };
                i.Click += LoadNames;
                Lists.Items.Add(i);
                if (i.Header == "main") { i.Click(); }
            }
        }

        private void LoadNames(object sender, RoutedEventArgs e)
        {
            mainNameList.Clear();
            mainNameList.AddRange(File.ReadAllLines( "lists\\" + ((MenuItem)sender).Header.ToString()) + ".lst");
            Names.Items.Clear();
            Selected.Text = "";
            foreach (string n in mainNameList) Names.Items.Add(n);
        }

        private void Random_Click(object sender, RoutedEventArgs e)
        {
            if (Names.Items.Count == 0) { foreach (string n in mainNameList) Names.Items.Add(n); }
            Selected.Text = Names.Items[ran.Next(0, Names.Items.Count)].ToString();
        }

        private void Pick_Click(object sender, RoutedEventArgs e)
        {
            if (Names.Items.Count == 0) { foreach (string n in mainNameList) Names.Items.Add(n); }
            int selection = ran.Next(0, Names.Items.Count);
            Selected.Text = Names.Items[selection].ToString();
            Names.Items.Remove(Names.Items[selection]);
        }

        private void Sort_Click(object sender, RoutedEventArgs e)
        {
            SortedList<int, string> list = new SortedList<int, string>();
            foreach (string s in Names.Items) list.Add(ran.Next(), s);
            Names.Items.Clear();
            foreach (var s in list) Names.Items.Add(s.Value);
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            Names.Items.Clear();
            foreach (string n in mainNameList) Names.Items.Add(n);
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "Text File|*.txt||",
                Multiselect = false
            };
            if (!(openFileDialog.ShowDialog() == true)) return;
            var ls = File.ReadAllLines(openFileDialog.FileName);
            mainNameList.Clear();
            mainNameList.AddRange(ls);
            Names.Items.Clear();
            foreach (string s in mainNameList) Names.Items.Add(s);
        }

        private void Names_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = Names.SelectedItem;
            Selected.Text = item.ToString();
            Names.Items.Remove(item);
        }

        private void Names_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = Names.SelectedItem;
            if (item == null) return;
            Selected.Text = item.ToString();
        }
    }
}
