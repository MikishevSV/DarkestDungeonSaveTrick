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
                WriteStatus("Готов к работе");
                ButtonLoad.IsEnabled = true;
                ButtonSave.IsEnabled = true;
            }
            else
            {
                WriteStatus("Не готов к работе!");
                ButtonLoad.IsEnabled = false;
                ButtonSave.IsEnabled = false;
            }
        }
        private void WriteStatus(string aStatus)
        {
            DateTime dateTime = DateTime.Now;
            LabelStatus.Content = dateTime.ToString() + " : " + aStatus;
        }
        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (Data.Save())
            {
                WriteStatus("Сохранено успешно");
            }
            else
            {
                WriteStatus("Ошибка сохранения!");
            }
        }

        private void ButtonLoad_Click(object sender, RoutedEventArgs e)
        {
            if (Data.Load())
            {
                WriteStatus("Загружено успешно");
            }
            else
            {
                WriteStatus("Ошибка загрузки!");
            }
        }
    }
}
