using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xml.Serialization;
using WpfApp1.FiguresOnCanvas;
using WpfApp1.Infrastructure;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Figure> FiguresOnCanvas;

        Random random = new Random();

        public MainWindow()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(20);
            timer.Tick += timerRefresh;
            timer.Start();


            FiguresOnCanvas = new List<Figure>();

            Figure.pMax = new Point()
            {
                X = Application.Current.MainWindow.Width,
                Y = Application.Current.MainWindow.Height
            };

        }


        void timerRefresh(object sender, EventArgs e)
        {

            Figure.pMax = new Point()
            {
                X = PbMain.ActualWidth,
                Y = PbMain.ActualHeight
            };
            foreach (
                var figure in FiguresOnCanvas)
            {
                figure.Move();
            }
               
            
                
        }

        #region Btns that create figures
        private void btn_Triengle_Click(object sender, RoutedEventArgs e)
        {
            var triangle = new TriangleOnCanvas();
            FiguresOnCanvas.Add(triangle);
            PbMain.Items.Add(triangle);
            Triangle.Items.Add(triangle);

        }
        private void btn_Ellipse_Click(object sender, RoutedEventArgs e)
        {
            CircleOnCanvas circle = new CircleOnCanvas();
            FiguresOnCanvas.Add(circle);
            PbMain.Items.Add(circle);
            Circle.Items.Add(circle);
        }
        private void btn_Rectangle_Click(object sender, RoutedEventArgs e)
        {
            RectangleOnCanvas rec = new RectangleOnCanvas();
            FiguresOnCanvas.Add(rec);
            PbMain.Items.Add(rec);
            Rectangle.Items.Add(rec);

        }
        #endregion

      

        private  void  btnStop_Click(object sender, RoutedEventArgs e)
        {
            Figure figure = TreeViewOfShapes.SelectedItem as Figure;

            if (figure != null) {

                if (figure.IsMoving)
                {
                    figure.IsMoving = !figure.IsMoving;
                    btnStop.Content = Application.Current.Resources["Start"].ToString();
                }
                else
                {
                    figure.IsMoving = !figure.IsMoving;
                    btnStop.Content = Application.Current.Resources["Stop"].ToString();
                }
            }
        }

        private void TreeViewOfShapes_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            Figure figure = TreeViewOfShapes.SelectedItem as Figure;

            if (figure != null)
            {
                btnStop.Visibility = Visibility.Visible;

                if (figure.IsMoving)
                {
                    btnStop.Content = Application.Current.Resources["Stop"].ToString();
                }
                else
                {
                    btnStop.Content = Application.Current.Resources["Start"].ToString();
                }
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var language = (SelectedLanguage_Combox.SelectedItem as ComboBoxItem).Content.ToString();
            Localize.LoadStringResource(language);
        }

        private void SaveBin_Click(object sender, RoutedEventArgs e)
        {
            var binFormatter = new BinaryFormatter();
            using (var file = new FileStream("figures.bin", FileMode.OpenOrCreate))
            {
                binFormatter.Serialize(file, FiguresOnCanvas);
            }
            #region binOpenComments


            /*    using (var file = new FileStream("figures.bin", FileMode.OpenOrCreate))
                {

                    FiguresOnCanvas = binFormatter.Deserialize(file) as List<Figure>;
                    PbMain.Items.Clear();

                    foreach (var item in FiguresOnCanvas)
                    {
                        PbMain.Items.Add(item);
                    }

                }*/
            #endregion
        }
        private void SaveXml_Click(object sender, RoutedEventArgs e)
        {
            var xml = new XmlSerializer(typeof(List<Figure>));

            using (var file = new FileStream("figures.xml", FileMode.OpenOrCreate))
            {
                file.SetLength(0);
                xml.Serialize(file, FiguresOnCanvas);
            }
            #region xmlOpenComments

         /*   using (var file = new FileStream("figures.xml", FileMode.OpenOrCreate))
            {

                FiguresOnCanvas = xml.Deserialize(file) as List<Figure>;
                PbMain.Items.Clear();

                foreach (var item in FiguresOnCanvas)
                {
                    PbMain.Items.Add(item);
                }

            }*/
            #endregion
        }
        private void SaveJson_Click(object sender, RoutedEventArgs e)
        {

            var json = new DataContractJsonSerializer(typeof(List<Figure>));

            using (var file = new FileStream("figures.json", FileMode.OpenOrCreate))
            {
                file.SetLength(0);
                json.WriteObject(file, FiguresOnCanvas);
            }
        }

        private void Open_SavedFile_Click(object sender, RoutedEventArgs e)
        {
            var json = new DataContractJsonSerializer(typeof(List<Figure>));

            using (var file = new FileStream("figures.json", FileMode.OpenOrCreate))
            {
                FiguresOnCanvas = json.ReadObject(file) as List<Figure>;
                PbMain.Items.Clear();

                foreach (var item in FiguresOnCanvas)
                {
                    PbMain.Items.Add(item);
                }
            }

        }

        private void PlusSound(object sender, RoutedEventArgs e)
        {

        }
        private void MinusSound(object sender, RoutedEventArgs e)
        {

        }
        private void AddDataOnConsole(object sender, RoutedEventArgs e)
        {

        }
        private void RemoveDataFromConsole(object sender, RoutedEventArgs e)
        {

        }
    }
}
