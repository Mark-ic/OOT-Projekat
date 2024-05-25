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
            Klub klub = new Klub();


            kosarkasi.import("igraci.txt");
            ilKosarkasa.ItemsSource = kosarkasi.KOSARKASI;
            TabelaKosarkasi.ItemsSource=kosarkasi.KOSARKASI;

            klub.import("timovi.txt");
            Stablo.ItemsSource = klub.KLUBOVI;
            
        }

        private void export_Click(object sender, RoutedEventArgs e)
        {
            Kosarkasi kosarkasi = new Kosarkasi();

            kosarkasi.import("igraci.txt");
            kosarkasi.exportTabele("tabela.csv");
        }

        private void Stablo_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.NewValue is Klub selectedKlub)
            {
                var klub = DataContext as Klub;
                if (klub != null)
                {
                    klub.OdabraniKlub = selectedKlub;
                }
            }
        }
    }
}