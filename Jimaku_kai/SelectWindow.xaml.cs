using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Jimaku_kai
{
    /// <summary>
    /// SelectWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class SelectWindow : Window
    {
        private string arg;
        public SelectWindow()
        {
            InitializeComponent();
            this.DataContext = new Settings();
            Combo.SelectedIndex = 0;
        }

        public class Settings : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;

            public ReactiveProperty<string[]> Layout { get; private set; } = new ReactiveProperty<string[]>();

            public Settings()
            {
                this.Layout = new ReactiveProperty<string[]>(new string[] { "横", "縦" });
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            App.Layout = Combo.SelectedIndex;
            App.MakeFile();
            this.Close();
        }
    }
}
