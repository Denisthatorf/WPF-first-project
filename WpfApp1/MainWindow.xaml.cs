using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Media;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
        private List<Figure> FiguresOnCanvas;
        private MediaPlayer mplayer = new MediaPlayer();
        private BackgroundWorker DrowThead = new BackgroundWorker();

        public MainWindow()
        {
            InitializeComponent();

            FiguresOnCanvas = new List<Figure>();

            Figure.pMax = new Point()
            {
                X = Application.Current.MainWindow.Width,
                Y = Application.Current.MainWindow.Height
            };

            #region Threads

             DrowThead.DoWork += DrowFigure;
             DrowThead.RunWorkerAsync();
             DrowThead.WorkerSupportsCancellation = true;

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(10);
            timer.Tick += timerRefresh;
            timer.Start();
            #endregion
        }


        #region Threads
        void timerRefresh(object sender, EventArgs e)
        {
            Figure.pMax = new Point()
            {
                X = PbMain.ActualWidth,
                Y = PbMain.ActualHeight
            };
            foreach (var figure in FiguresOnCanvas)
            {
                foreach (var figure2 in FiguresOnCanvas)
                {
                    if (figure.IsCollide(figure2))
                    {
                        figure.SimulateNewCollision(figure2, figure.PointOfCollision(figure2));
                    }
                }

                try
                {
                    figure.Move();
                }
                catch (FigureOutOfBoundExeption ex)
                {
                    figure.Move(new System.Numerics.Vector2(ex.dX, ex.dY));
                }
                PbMain.Items.Remove(figure);
            }
        }
        #region DrowFigureTread
        void DrowFigure(object sender, EventArgs e)
        {
            while (true)
            {
                if (DrowThead.CancellationPending)
                    break;

                if (FiguresOnCanvas.Count > 0)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        foreach (var figure in FiguresOnCanvas)
                        {
                            if (!PbMain.Items.Contains(figure))
                                PbMain.Items.Add(figure);
                        }

                    });
                }
            }
        }


        private void WindowClosing(object sender, CancelEventArgs e)
        {
            DrowThead.CancelAsync();
        }
        #endregion

        #endregion


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

        #region Desirialization
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
        #endregion


        #region Beep

        private void PlusSound(object sender, RoutedEventArgs e)
        {
            (TreeViewOfShapes.SelectedItem as Figure).Collision += Beep;
        }
        private void MinusSound(object sender, RoutedEventArgs e)
        {
            (TreeViewOfShapes.SelectedItem as Figure).Collision -= Beep;
        }
        public void Beep (object sender, CollisionEventArgs e)
        {
            mplayer.Open(new Uri(@"C:\C#\WPF_Trainee\First_\WpfApp1\WpfApp1\Sounds\BeepSound.mp3"));
            mplayer.Play();
        }
        #endregion
        #region Console
        private void AddDataOnConsole(object sender, RoutedEventArgs e)
        {
            (TreeViewOfShapes.SelectedItem as Figure).Collision += ConsoleOutput;
        }
        private void RemoveDataFromConsole(object sender, RoutedEventArgs e)
        {
            (TreeViewOfShapes.SelectedItem as Figure).Collision -= ConsoleOutput;
        }
        public void ConsoleOutput(object sender, CollisionEventArgs e)
        {
            string text = string.Empty;
            string[] lines = ConsoleBox.Text.Split('\n');

            if (lines.Length > 6)
            {
                for (int i = 1; i < 6; i++)
                {
                    text += lines[i];
                    text += "\n";
                }
            }
            else {
                foreach (var line in lines)
                {
                    text += line;
                    text += "\n";
                }
            }

            text += $"({e.CollisionPoint.X:#.##};{e.CollisionPoint.Y:#.##})";
            ConsoleBox.Text = text;
        }

        #endregion

    }
}
