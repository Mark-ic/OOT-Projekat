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
        private bool isDragging = false;
        private Point clickPosition;
        private Image draggedElement;
        private MainViewModel viewModel;
        Point startPoint;

        public MainWindow()
        {
            InitializeComponent();
             startPoint = new Point();


            Kosarkasi kosarkasi = new Kosarkasi();
            Klub klub = new Klub();


            kosarkasi.import("igraci.txt");
            ilKosarkasa.ItemsSource = kosarkasi.KOSARKASI;
            TabelaKosarkasi.ItemsSource=kosarkasi.KOSARKASI;

            klub.Import("timovi.txt");
            Stablo.ItemsSource = klub.KLUBOVI;
            viewModel = new MainViewModel();
            this.DataContext = viewModel;
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



        private void Stablo_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(null);
        }

        private void Stablo_MouseMove(object sender, MouseEventArgs e)
        {

            Point mousePos = e.GetPosition(null);
            Vector diff = startPoint - mousePos;

            if (e.LeftButton == MouseButtonState.Pressed &&
                (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                // Get the dragged TreeViewItem
                TreeView treeView = sender as TreeView;
                TreeViewItem treeViewItem =
                    FindAncestor<TreeViewItem>((DependencyObject)e.OriginalSource);

                if (treeViewItem != null)
                {
                    // Find the data behind the TreeViewItem
                    Klub klub = (Klub)treeView.ItemContainerGenerator.
                        ItemFromContainer(treeViewItem);

                    // Initialize the drag & drop operation
                    DataObject dragData = new DataObject("myFormat", klub);
                    DragDrop.DoDragDrop(treeViewItem, dragData, DragDropEffects.Move);
                }
            }


        }
        
        private static T FindAncestor<T>(DependencyObject current) where T : DependencyObject
        {
            do
            {
                if (current is T)
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            while (current != null);
            return null;
        }
        

        private void MapCanvas_DragEnter(object sender, DragEventArgs e)
        {
            
            if (!e.Data.GetDataPresent("myFormat") || sender == e.Source)
            {
                e.Effects = DragDropEffects.None;
            }
            

        }


        private void MapCanvas_Drop(object sender, DragEventArgs e)
        {
            

            if (e.Data.GetDataPresent("myFormat"))
            {
                Klub klub = e.Data.GetData("myFormat") as Klub;

                var kluboviTreeView = Stablo.SelectedItem as Klub;
                if (kluboviTreeView != null)
                {
                    kluboviTreeView.UkloniKlub(klub);
                }
            
                var canvas = sender as Canvas;
                Point pozicija = e.GetPosition(canvas);

                // Postavljanje X i Y koordinata kluba
                klub.X = pozicija.X;
                klub.Y = pozicija.Y;

                viewModel.KluboviNaMapi.Add(klub);

             
            }
        }

        
    }
}