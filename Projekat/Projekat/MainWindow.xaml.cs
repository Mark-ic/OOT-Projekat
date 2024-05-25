using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Projekat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Kosarkasi kosarkasi = new Kosarkasi();

            kosarkasi.import("igraci.txt");
            ilKosarkasa.ItemsSource = kosarkasi.KOSARKASI;
            TabelaKosarkasi.ItemsSource=kosarkasi.KOSARKASI;

        }

        private void export_Click(object sender, RoutedEventArgs e)
        {
            Kosarkasi kosarkasi = new Kosarkasi();

            kosarkasi.import("igraci.txt");
            kosarkasi.exportTabele("tabela.csv");
        }
    }
}