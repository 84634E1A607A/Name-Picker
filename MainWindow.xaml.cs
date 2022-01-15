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
        private static readonly string[] IntegratedList = 
            new string[] {
                "付宝文", "何佳宸", "何来", "余秋霆", "刘奕斐", "刘旭", "卫昱萌", "危志欣", "吴泓毅", "周子瑄",
                "姚睿哲", "孔舍予", "宋词", "张天烁", "张楚崧", "张涵", "张玥瑶", "张留鲲", "彭娜", "彭子俊",
                "徐亦萌", "戴沐妍", "朱安雅", "李家乐", "李成蹊", "李欣然", "李沐城", "李泽粲", "李语轩", "杨华铭",
                "殷明昊", "王欣杰", "王沫涵", "王碧宇", "盛立", "石清宇", "程思翔", "罗誉中", "肖欣宇", "苏智渊",
                "范钰明", "葛镐铭", "董业恺", "许泽睿", "郭昕童", "阮婧涵", "韩宇新", "马晨曦", "魏明玮", "龙金哲"
            };

        private readonly Random ran = new Random();
        private readonly List<string> mainNameList = new List<string>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadNames();
        }

        private void LoadNames()
        {
            mainNameList.Clear();
            mainNameList.AddRange(IntegratedList);
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
