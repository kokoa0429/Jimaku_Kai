using System;
using System.ComponentModel;
using System.Configuration;
using System.Windows;

using Reactive.Bindings;

namespace Jimaku_kai
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new Settings();
            Combo.SelectedIndex = App.Layout;
        }

        private void Button_Click(object sender, RoutedEventArgs e)//Sav
        {
            App.SaveConfig(Combo.SelectedValue.ToString(), FlameText.Text, GapText.Text, OverWriteCheckBox.IsChecked.ToString());
            Application.Current.Shutdown();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)//Can
        {
            Application.Current.Shutdown();
        }
    }

    public class Settings : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ReactiveProperty<string[]> Layout { get; private set; } = new ReactiveProperty<string[]>();
        public ReactiveProperty<int> Length { get; private set; } = new ReactiveProperty<int>();
        public ReactiveProperty<int> Gap { get; private set; } = new ReactiveProperty<int>();
        public ReactiveProperty<bool> OverWrite { get; private set; } = new ReactiveProperty<bool>();

        public Settings()
        {
            this.Layout = new ReactiveProperty<string[]>(new string[]{"横","縦"});
            this.Length = new ReactiveProperty<int>(App.Length);
            this.Gap = new ReactiveProperty<int>(App.Gap);
            this.OverWrite = new ReactiveProperty<bool>(App.OverWrite);

        }


    }
    

}
