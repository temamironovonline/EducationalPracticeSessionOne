using System.Windows;
using System.Windows.Controls;

namespace EducationalPracticeSessionOne
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        public static DBEntities entities;
        public static Frame frame;
        public MainWindow()
        {
            InitializeComponent();
            entities = new DBEntities();
            frame = mainFrame;

            frame.Navigate(new LoginPage());
        }
    }
}
