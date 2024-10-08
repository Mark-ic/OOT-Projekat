﻿using System.Collections.ObjectModel;
using System.IO;
using System.Linq.Expressions;
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
        Kosarkas kosarkas;
        public MainWindow()
        {
            InitializeComponent();
            startPoint = new Point();
            viewModel = new MainViewModel();
            kosarkas = new Kosarkas();
            klub = new Klub();


            kosarkas.import("igraci.txt");
            viewModel.Kosarkasi = kosarkas.Kosarkasi;
            ListaKosarkasa.ItemsSource = viewModel.Kosarkasi;
            TabelaKosarkasi.ItemsSource = viewModel.Kosarkasi;

            klub.Import("timovi.txt");
            
            viewModel.Klubovi = klub.Klubovi;
            Stablo.ItemsSource = viewModel.Klubovi;
            this.DataContext = viewModel;
            List<string> pozicije = new List<string>();
            pozicije.Add("C");
            pozicije.Add("PG");
            pozicije.Add("SF");
            pozicije.Add("PF");
            pozicije.Add("SG");
            comboBox.ItemsSource= pozicije;


        }

        private void export_Click(object sender, RoutedEventArgs e)
        {
            StreamWriter sw = null;
            try {
                sw = new StreamWriter("tabela.csv");
                if (TabelaKosarkasi != null)
                {
                    foreach (Kosarkas k in TabelaKosarkasi.Items)
                    {
                        sw.WriteLine(k.zaTabelu());
                    }
                }
                MessageBox.Show("Uspesno ste exportovali tabelu.");
            }
            catch
            {
                MessageBox.Show("Greska prilikom exporta tabele.");
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                }
            }
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
        private void Lista_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                if (e.AddedItems[0] is Kosarkas selectedKosarkas)
                {
                    var viewModel = DataContext as Kosarkas; 
                    if (viewModel != null)
                    {
                        viewModel.OdabraniKosarkas = selectedKosarkas;
                    }
                }
            }
        }

        private void Slika_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {

            DodavanjeIgraca DodavanjeIgraca = new DodavanjeIgraca(viewModel);
            DodavanjeIgraca.Show();
        }

        private void Lista_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ListaKosarkasa.SelectedItem != null)
            {
                var kosarkas = ListaKosarkasa.SelectedItem as Kosarkas;
                InformacijeIgraca ii = new InformacijeIgraca(kosarkas);
                ii.Show();
            }
        }
        private void Lista_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            var item = (sender as ListView).InputHitTest(e.GetPosition(ListaKosarkasa)) as FrameworkElement;
            if (item != null)
            {
                var listViewItem = FindAncestor<ListViewItem>(item);
                if (listViewItem != null)
                {
                    listViewItem.Focus();
                    ContextMenu.IsOpen = true;
                }
            }
        }

        private void Click_izmena(object sender, RoutedEventArgs e)
        {
            if (ListaKosarkasa.SelectedItem != null)
            {
                var kosarkas = ListaKosarkasa.SelectedItem as Kosarkas;
                IzmenaIgraca ii = new IzmenaIgraca(kosarkas);
                ii.Show();
            }
        }

        private void Click_informacije(object sender, RoutedEventArgs e)
        {
            if (ListaKosarkasa.SelectedItem != null)
            {
                var kosarkas = ListaKosarkasa.SelectedItem as Kosarkas;
                InformacijeIgraca ii = new InformacijeIgraca(kosarkas);
                ii.Show();
            }
        }

        private void Stablo_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(null);
        }
        private void Lista_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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
        private void Lista_MouseMove(object sender, MouseEventArgs e)
        {

            Point mousePos = e.GetPosition(null);
            Vector diff = startPoint - mousePos;

            if (e.LeftButton == MouseButtonState.Pressed &&
                (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                // Get the dragged ListViewItem
                ListView listView = sender as ListView;
                ListViewItem listViewItem =
                    FindAncestor<ListViewItem>((DependencyObject)e.OriginalSource);

                if (listViewItem != null)
                {
                    // Find the data behind the ListViewItem
                    Kosarkas kosarkas = (Kosarkas)listView.ItemContainerGenerator.
                        ItemFromContainer(listViewItem);

                    // Initialize the drag & drop operation
                    DataObject dragData = new DataObject("myFormat", kosarkas);
                    DragDrop.DoDragDrop(listViewItem, dragData, DragDropEffects.Move);
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
        private void TerenCanvas_DragEnter(object sender, DragEventArgs e)
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
                        image.PreviewMouseRightButtonDown += Image_PreviewMouseRightButtonDown;

                        ContextMenu contextMenu = new ContextMenu();

                        MenuItem deleteMenuItem = new MenuItem { Header = "Obriši sa mape" };
                        deleteMenuItem.Click += DeleteMenuItem_Click;
                        MenuItem deleteRemove = new MenuItem { Header = "Obriši iz aplikacije" };
                        deleteRemove.Click += DeleteRemove_Click;
                       
                        contextMenu.Items.Add(deleteMenuItem);
                        contextMenu.Items.Add(deleteRemove);

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
        private void TerenCanvas_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("myFormat"))
            {
                Kosarkas kosarkas1 = e.Data.GetData("myFormat") as Kosarkas;
                var kosarkasiListView = ListaKosarkasa.SelectedItem as Kosarkas;

                if (kosarkas1 != null)
                {
                    var canvas = sender as Canvas;
                    Point pozicija = e.GetPosition(canvas);

                    kosarkas1.X = pozicija.X;
                    kosarkas1.Y = pozicija.Y;

                    // Proveriti da li slika vec postoji na Canvasu
                    Image existingImage = null;
                    foreach (var child in canvas.Children)
                    {
                        if (child is Image image && image.Tag == kosarkas1)
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
                            Source = new BitmapImage(new Uri(kosarkas1.SLIKA, UriKind.RelativeOrAbsolute)),
                            Width = 100,
                            Height = 100,
                            Tag = kosarkas1
                        };

                        image.MouseDown += Image_MouseDown2;
                        image.MouseMove += Image_MouseMove2;
                        image.MouseUp += Image_MouseUp2;
                        image.PreviewMouseLeftButtonDown += Image_PreviewMouseLeftButtonDown;
                        image.PreviewMouseRightButtonDown += Image_PreviewMouseRightButtonDown;

                        ContextMenu contextMenu = new ContextMenu();
                        MenuItem editMenuItem = new MenuItem { Header = "Izmeni" };
                        editMenuItem.Click += EditMenuItem_Click2;
                        MenuItem deleteMenuItem = new MenuItem { Header = "Obriši sa terena" };
                        deleteMenuItem.Click += DeleteMenuItem_Click2;
                        MenuItem deleteRemove = new MenuItem { Header = "Obriši iz aplikacije" };
                        deleteRemove.Click += DeleteRemove_Click2;

                        contextMenu.Items.Add(editMenuItem);
                        contextMenu.Items.Add(deleteMenuItem);
                        contextMenu.Items.Add(deleteRemove);

                        image.ContextMenu = contextMenu;

                        Canvas.SetLeft(image, kosarkas1.X);
                        Canvas.SetTop(image, kosarkas1.Y);

                        canvas.Children.Add(image);
                    }

                    if (kosarkasiListView != null)
                    {
                        viewModel.Kosarkasi.Remove(kosarkas1);
                    }
                    viewModel.KosarkasiNaTerenu.Add(kosarkas1);
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
        private void Image_MouseDown2(object sender, MouseButtonEventArgs e)
        {
            draggedElement = sender as Image;
            if (draggedElement != null)
            {
                Mouse.OverrideCursor = Cursors.None;
                isDragging = true;
                clickPosition = e.GetPosition(TerenCanvas);
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
        private void Image_MouseMove2(object sender, MouseEventArgs e)
        {
            if (isDragging && draggedElement != null)
            {
                Point currentPosition = e.GetPosition(TerenCanvas);
                double offsetX = currentPosition.X - clickPosition.X;
                double offsetY = currentPosition.Y - clickPosition.Y;


                double newLeft = Canvas.GetLeft(draggedElement) + offsetX;
                double newTop = Canvas.GetTop(draggedElement) + offsetY;


                double canvasWidth = TerenCanvas.ActualWidth;
                double canvasHeight = TerenCanvas.ActualHeight;

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
        private void Image_MouseUp2(object sender, MouseButtonEventArgs e)
        {
            if (isDragging && draggedElement != null)
            {
                Mouse.OverrideCursor = Cursors.Arrow;
                isDragging = false;
                draggedElement.ReleaseMouseCapture();
                draggedElement.MouseMove -= Image_MouseMoveFromCanvas2;
                draggedElement = null;
            }
        }

        private void Image_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(null);
            
            (sender as Image).MouseMove += Image_MouseMoveFromCanvas;
        }

        private void Image_PreviewMouseLeftButtonDown2(object sender, MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(null);

            (sender as Image).MouseMove += Image_MouseMoveFromCanvas2;
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
        private void Image_MouseMoveFromCanvas2(object sender, MouseEventArgs e)
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
                    Kosarkas kosarkas = image.Tag as Kosarkas;
                    if (kosarkas != null)
                    {
                        DataObject dragData = new DataObject("myFormat", kosarkas);
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
        private void Lista_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("myFormat"))
            {
                Kosarkas kosarkas1 = e.Data.GetData("myFormat") as Kosarkas;
                if (kosarkas1 != null)
                {
                    if (!viewModel.Kosarkasi.Contains(kosarkas1))
                    {
                        viewModel.Kosarkasi.Add(kosarkas1);

                    }
                    viewModel.KosarkasiNaTerenu.Remove(kosarkas1);
                    Mouse.OverrideCursor = Cursors.Arrow;

                    var canvas = TerenCanvas;
                    Image imageToRemove = null;

                    foreach (var child in canvas.Children)
                    {
                        if (child is Image image && image.Tag == kosarkas1)
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
                    Izmena_Kluba ik = new Izmena_Kluba(klub);
                    ik.ShowDialog();
                }
            }
            if (Stablo.SelectedItem is Klub selectedKlub)
            {
                Izmena_Kluba ik = new Izmena_Kluba(selectedKlub);
                ik.ShowDialog();
            }
        }
        private void EditMenuItem_Click2(object sender, RoutedEventArgs e)
        {
            var menuItem = sender as MenuItem;
            var contextMenu = menuItem?.Parent as ContextMenu;
            var element = contextMenu?.PlacementTarget;

            if (element is Image image)
            {
                var kosarkas = image.Tag as Kosarkas;
                if (kosarkas != null)
                {
                    IzmenaIgraca izmena = new IzmenaIgraca(kosarkas);
                    izmena.ShowDialog();
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
                    viewModel.Klubovi.Add(klub);
                    viewModel.KluboviNaMapi.Remove(klub);
                    var canvas = MapCanvas;
                    canvas.Children.Remove(image);
                    MessageBox.Show("Klub je sklonjen sa mape!");
                }
            }
        }

        private void DeleteMenuItem_Click2(object sender, RoutedEventArgs e)
        {
            var menuItem = sender as MenuItem;
            var contextMenu = menuItem?.Parent as ContextMenu;
            var element = contextMenu?.PlacementTarget;

            if (element is Image image)
            {
                var kosarkas = image.Tag as Kosarkas;
                if (kosarkas != null)
                {
                    viewModel.Kosarkasi.Add(kosarkas);
                    viewModel.KosarkasiNaTerenu.Remove(kosarkas);
                    var canvas = TerenCanvas;
                    canvas.Children.Remove(image);
                    MessageBox.Show("Kosarkas je sklonjen sa terena!");
                }
            }
        }


        private void DeleteRemove_Click(object sender, RoutedEventArgs e)
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
                    MessageBox.Show("Klub je izbrisan iz aplikacije!");
                }
            }

        }
        private void DeleteRemove_Click2(object sender, RoutedEventArgs e)
        {
            var menuItem = sender as MenuItem;
            var contextMenu = menuItem?.Parent as ContextMenu;
            var element = contextMenu?.PlacementTarget;

            if (element is Image image)
            {
                var kosarkas = image.Tag as Kosarkas;
                if (kosarkas != null)
                {
                    viewModel.KosarkasiNaTerenu.Remove(kosarkas);
                    var canvas = TerenCanvas;
                    canvas.Children.Remove(image);
                    MessageBox.Show("Kosarkas je izbrisan iz aplikacije!");
                }
            }

        }
        private void Image_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            // hendlanje kolizije eventova
            e.Handled = true;
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Provera();
        }
        private void searchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Provera();
        }

        private void searchBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            Provera();
        }
        private void Provera()
        {
            string ime = txtIME.Text.ToLower();
            string prezime = txtPREZIME.Text.ToLower();
            string pozicija = comboBox.SelectedValue?.ToString();

            var filtriraniKosarkasi = viewModel.Kosarkasi.Where(k =>
              (string.IsNullOrEmpty(ime) || k.IME.ToLower().Contains(ime)) &&
              (string.IsNullOrEmpty(prezime) || k.PREZIME.ToLower().Contains(prezime)) &&
              (string.IsNullOrEmpty(pozicija) || k.POZICIJA == pozicija)
          ).ToList();

            TabelaKosarkasi.ItemsSource = filtriraniKosarkasi;

        }

        private void ocisti_Click(object sender, RoutedEventArgs e)
        {
            comboBox.SelectedIndex = -1;
            txtIME.Text = "";
            txtPREZIME.Text = "";
        }

        private void Stablo_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(Stablo.SelectedItem != null)
            {
                Klub k = Stablo.SelectedItem as Klub;
                PrikazKluba pk = new PrikazKluba(k);
                pk.ShowDialog();
            }
        }
    }
}