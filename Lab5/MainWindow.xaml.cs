using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;

namespace TextAnalysisApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void NewFile_Click(object sender, RoutedEventArgs e)
        {
            InputTextBox.Clear();
        }

        private void SaveFile_Click(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text files (.txt)|.txt|All files (.)|.";
            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveFileDialog.FileName, InputTextBox.Text);
            }
        }

        private void LoadFile_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (.txt)|.txt|All files (.)|.";
            if (openFileDialog.ShowDialog() == true)
            {
                InputTextBox.Text = File.ReadAllText(openFileDialog.FileName);
            }
        }

        private void InputTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateCursorPosition();
        }

        private void InputTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            UpdateCursorPosition();
        }

        private void UpdateCursorPosition()
        {
            int row = InputTextBox.GetLineIndexFromCharacterIndex(InputTextBox.CaretIndex);
            int column = InputTextBox.CaretIndex - InputTextBox.GetCharacterIndexFromLineIndex(row);
            cursorPosition.Text = $"Позиція курсора: {row + 1}, {column + 1}";
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Розробник: Карпачова Валерія");
        }

        private void FindWordButton_Click(object sender, RoutedEventArgs e)
        {
            string text = InputTextBox.Text;
            string searchWord = SearchWordTextBox.Text;

            if (string.IsNullOrWhiteSpace(searchWord))
            {
                MessageBox.Show("Будь ласка, введіть слово для пошуку.");
                return;
            }

            int totalWords = Regex.Matches(text, @"\b\w+\b").Count;

            int wordCount = Regex.Matches(text, $@"\b{Regex.Escape(searchWord)}\b", RegexOptions.IgnoreCase).Count;
            
            double saturation = (totalWords > 0) ? ((double)wordCount / totalWords) * 100 : 0;

            wordCountResult.Text = $"Кількість слів: {totalWords}";
            wordOccurrencesResult.Text = $"Повторення: {wordCount}";
            saturationResult.Text = $"Тошнота: {saturation:F3}%"; 
        }
    }
}


