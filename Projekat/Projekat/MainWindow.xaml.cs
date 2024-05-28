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
        private static MainViewModel viewModel;

        Point startPoint;
        Klub klub;
        public MainWindow()
        {
            InitializeComponent();
            startPoint = new Point();


            Kosarkasi kosarkasi = new Kosarkasi();
            klub = new Klub();


            kosarkasi.import("igraci.txt");
            ilKosarkasa.ItemsSource = kosarkasi.KOSARKASI;
            TabelaKosarkasi.ItemsSource = kosarkasi.KOSARKASI;

            klub.Import("timovi.txt");
            viewModel = new MainViewModel();
            viewModel.Klubovi = klub.Klubovi;
            Stablo.ItemsSource = viewModel.Klubovi;
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

        private void Slika_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            DodavanjeIgraca DodavanjeIgraca = new DodavanjeIgraca();
            DodavanjeIgraca.Show();
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
                Klub klub1 = e.Data.GetData("myFormat") as Klub;
                var kluboviTreeView = Stablo.SelectedItem as Klub;

                if (klub1 != null)
                {
                    var canvas = sender as Canvas;
                    Point pozicija = e.GetPosition(canvas);

                    klub1.X = pozicija.X;
                    klub1.Y = pozicija.Y;

                    // Proveriti da li slika vec postoji na Canvasu
                    Image existingImage = null;
                    foreach (var child in canvas.Children)
                    {
                        if (child is Image image && image.Tag == klub1)
                        {
                            existingImage = image;
                            break;
                        }
                    }

                    // Ako slika ne postoji, dodaj novu
                    if (existingImage == null)
                    {
                        Image image = new Image
                        {
                            Source = new BitmapImage(new Uri(klub1.LOGO, UriKind.RelativeOrAbsolute)),
                            Width = 50,
                            Height = 50,
                            Tag = klub1
                        };

                        image.MouseDown += Image_MouseDown;
                        image.MouseMove += Image_MouseMove;
                        image.MouseUp += Image_MouseUp;
                        image.PreviewMouseLeftButtonDown += Image_PreviewMouseLeftButtonDown;

                        ContextMenu contextMenu = new ContextMenu();
                        MenuItem editMenuItem = new MenuItem { Header = "Izmeni" };
                        editMenuItem.Click += EditMenuItem_Click;
                        MenuItem deleteMenuItem = new MenuItem { Header = "Obriši" };
                        deleteMenuItem.Click += DeleteMenuItem_Click;
                        contextMenu.Items.Add(editMenuItem);
                        contextMenu.Items.Add(deleteMenuItem);

                        image.ContextMenu = contextMenu;

                        Canvas.SetLeft(image, klub1.X);
                        Canvas.SetTop(image, klub1.Y);

                        canvas.Children.Add(image);
                    }

                    if (kluboviTreeView != null)
                    {
                        viewModel.Klubovi.Remove(klub1);
                    }
                    viewModel.KluboviNaMapi.Add(klub1);
                }
            }
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            draggedElement = sender as Image;
            if (draggedElement != null)
            {
                Mouse.OverrideCursor = Cursors.None;
                isDragging = true;
                clickPosition = e.GetPosition(MapCanvas);
                draggedElement.CaptureMouse();
            }
            Mouse.OverrideCursor = Cursors.Arrow;
        }



        private void Image_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging && draggedElement != null)
            {
                Point currentPosition = e.GetPosition(MapCanvas);
                double offsetX = currentPosition.X - clickPosition.X;
                double offsetY = currentPosition.Y - clickPosition.Y;


                double newLeft = Canvas.GetLeft(draggedElement) + offsetX;
                double newTop = Canvas.GetTop(draggedElement) + offsetY;


                double canvasWidth = MapCanvas.ActualWidth;
                double canvasHeight = MapCanvas.ActualHeight;

                newLeft = Math.Max(0, Math.Min(newLeft, canvasWidth - draggedElement.ActualWidth));
                newTop = Math.Max(0, Math.Min(newTop, canvasHeight - draggedElement.ActualHeight));

                Canvas.SetLeft(draggedElement, newLeft);
                Canvas.SetTop(draggedElement, newTop);

                clickPosition = currentPosition;
            }
        }




        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (isDragging && draggedElement != null)
            {
                Mouse.OverrideCursor = Cursors.Arrow;
                isDragging = false;
                draggedElement.ReleaseMouseCapture();
                draggedElement.MouseMove -= Image_MouseMoveFromCanvas;
                draggedElement = null;
            }
        }

        private void Image_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(null);
            
            (sender as Image).MouseMove += Image_MouseMoveFromCanvas;
        }

        private void Image_MouseMoveFromCanvas(object sender, MouseEventArgs e)
        {
            Point mousePos = e.GetPosition(null);
            Vector diff = startPoint - mousePos;
            if (e.LeftButton == MouseButtonState.Pressed &&
                (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                Image image = sender as Image;
                if (image != null)
                {
                    Klub klub = image.Tag as Klub;
                    if (klub != null)
                    {
                        DataObject dragData = new DataObject("myFormat", klub);
                        DragDrop.DoDragDrop(image, dragData, DragDropEffects.Move);
                    }
                }
            }
        }
        
        private void Stablo_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("myFormat"))
            {
                Klub klub1 = e.Data.GetData("myFormat") as Klub;
                if (klub1 != null)
                {
                    if (!viewModel.Klubovi.Contains(klub1))
                    {
                        viewModel.Klubovi.Add(klub1);

                    }
                    viewModel.KluboviNaMapi.Remove(klub1);
                    Mouse.OverrideCursor = Cursors.Arrow;

                    var canvas = MapCanvas;
                    Image imageToRemove = null;

                    foreach (var child in canvas.Children)
                    {
                        if (child is Image image && image.Tag == klub1)
                        {
                            imageToRemove = image;
                            break;
                        }
                    }

                    if (imageToRemove != null)
                    {
                        canvas.Children.Remove(imageToRemove);
                    }
                }
            }
        }

        private void MapCanvas_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            DodavanjeKluba dk = new DodavanjeKluba(viewModel);
            dk.ShowDialog();
        }
       
        private void EditMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var menuItem = sender as MenuItem;
            var contextMenu = menuItem?.Parent as ContextMenu;
            var element = contextMenu?.PlacementTarget;

            if (element is Image image)
            {
                var klub = image.Tag as Klub;
                if (klub != null)
                {
                    MessageBox.Show($"Izmena Kluba:");
                }
            }
        }

        private void DeleteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var menuItem = sender as MenuItem;
            var contextMenu = menuItem?.Parent as ContextMenu;
            var element = contextMenu?.PlacementTarget;

            if (element is Image image)
            {
                var klub = image.Tag as Klub;
                if (klub != null)
                {
                    viewModel.KluboviNaMapi.Remove(klub);
                    var canvas = MapCanvas;
                    canvas.Children.Remove(image);
                }
            }
        }
    }
}