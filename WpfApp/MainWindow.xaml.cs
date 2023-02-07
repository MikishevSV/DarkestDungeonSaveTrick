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
using DarkestDungeonSaveTrick;


namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SavesData Data;
        public MainWindow()
        {
            InitializeComponent();            
        }        
        private void WindowMain_Initialized(object sender, EventArgs e)
        {            
            Data = new SavesData();
            if (Data.IsReady)
            {
                LabelStatus.Content = "Готов к работе";
                ButtonLoad.IsEnabled = true;
                ButtonSave.IsEnabled = true;
            }
            else
            {
                LabelStatus.Content = "Не готов к работе!";
                ButtonLoad.IsEnabled = false;
                ButtonSave.IsEnabled = false;
            }
        }
        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (Data.Save())
            {
                LabelStatus.Content = "Сохранено успешно";
            }
            else
            {
                LabelStatus.Content = "Ошибка сохранения!";
            }
        }

        private void ButtonLoad_Click(object sender, RoutedEventArgs e)
        {
            if (Data.Load())
            {
                LabelStatus.Content = "Загружено успешно";
            }
            else
            {
                LabelStatus.Content = "Ошибка загрузки!";
            }
        }
    }
}
