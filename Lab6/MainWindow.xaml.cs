using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using Microsoft.Win32;
using System.Windows.Threading;
using System.Windows.Media;

namespace Lab7
{
    public partial class MainWindow : Window
    {
        private bool mediaPlayerIsPlaying = false;
        private bool userIsDraggingSlider = false;
        private bool isCollapsed = false; 

        public MainWindow()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if ((mePlayer.Source != null) && (mePlayer.NaturalDuration.HasTimeSpan) && (!userIsDraggingSlider))
            {
                sliProgress.Minimum = 0;
                sliProgress.Maximum = mePlayer.NaturalDuration.TimeSpan.TotalSeconds;
                sliProgress.Value = mePlayer.Position.TotalSeconds;
            }
        }

        private void Open_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Media files (*.mp3;*.mpg;*.mpeg;*.mp4)|*.mp3;*.mpg;*.mpeg;*.mp4|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                mePlayer.Source = new Uri(openFileDialog.FileName);
                mePlayer.Play();
                mediaPlayerIsPlaying = true;
            }
        }

        private void Play_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (mePlayer != null) && (mePlayer.Source != null);
        }

        private void Play_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            mePlayer.Play();
            mediaPlayerIsPlaying = true;
        }

        private void Pause_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = mediaPlayerIsPlaying;
        }

        private void Pause_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            mePlayer.Pause();
            mediaPlayerIsPlaying = false;
        }

        private void Stop_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = mediaPlayerIsPlaying || isCollapsed;
        }

        private void Stop_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (!isCollapsed)
            {
                DoubleAnimation scaleAnimation = new DoubleAnimation(0.1, TimeSpan.FromSeconds(0.5));
                mediaScale.BeginAnimation(ScaleTransform.ScaleXProperty, scaleAnimation);
                mediaScale.BeginAnimation(ScaleTransform.ScaleYProperty, scaleAnimation);

                DoubleAnimation translateXAnimation = new DoubleAnimation(ActualWidth - 50, TimeSpan.FromSeconds(0.5));
                DoubleAnimation translateYAnimation = new DoubleAnimation(ActualHeight - 50, TimeSpan.FromSeconds(0.5));

                mediaTranslate.BeginAnimation(TranslateTransform.XProperty, translateXAnimation);
                mediaTranslate.BeginAnimation(TranslateTransform.YProperty, translateYAnimation);

                isCollapsed = true;
            }
            else
            {
                DoubleAnimation scaleAnimation = new DoubleAnimation(1, TimeSpan.FromSeconds(0.5));
                mediaScale.BeginAnimation(ScaleTransform.ScaleXProperty, scaleAnimation);
                mediaScale.BeginAnimation(ScaleTransform.ScaleYProperty, scaleAnimation);

                DoubleAnimation translateXAnimation = new DoubleAnimation(0, TimeSpan.FromSeconds(0.5));
                DoubleAnimation translateYAnimation = new DoubleAnimation(0, TimeSpan.FromSeconds(0.5));

                mediaTranslate.BeginAnimation(TranslateTransform.XProperty, translateXAnimation);
                mediaTranslate.BeginAnimation(TranslateTransform.YProperty, translateYAnimation);

                isCollapsed = false;
            }

            mediaPlayerIsPlaying = false;
        }

        private void sliProgress_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            userIsDraggingSlider = true;
        }

        private void sliProgress_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            userIsDraggingSlider = false;
            mePlayer.Position = TimeSpan.FromSeconds(sliProgress.Value);
        }

        private void sliProgress_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            lblProgressStatus.Text = TimeSpan.FromSeconds(sliProgress.Value).ToString(@"hh\:mm\:ss");
        }

        private void Grid_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            mePlayer.Volume += (e.Delta > 0) ? 0.1 : -0.1;
        }

        private void SpeedUp_Click(object sender, RoutedEventArgs e)
        {
            if (mePlayer.SpeedRatio < 2) 
            {
                mePlayer.SpeedRatio += 0.5;
            }
        }

        private void SlowDown_Click(object sender, RoutedEventArgs e)
        {
            if (mePlayer.SpeedRatio > 0.5) 
            {
                mePlayer.SpeedRatio -= 0.5;
            }
        }
    }
}



