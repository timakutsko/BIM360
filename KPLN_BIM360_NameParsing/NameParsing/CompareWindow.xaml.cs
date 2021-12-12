using NameParsing.ParsingData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;


namespace NameParsing
{
    /// <summary>
    /// Interaction logic for CompareWindow.xaml
    /// </summary>
    public partial class CompareWindow : Window
    {
        private string userDir;

        public CompareWindow(string path)
        {
            userDir = path;
            InitializeComponent();
        }

        public void AddRowData(LinkedList<SimilarData> sDataList)
        {
            int rowCount = 1;
            foreach(SimilarData sData in sDataList)
            {
                // Создание новой строки в окне
                RowDefinition newRow = new RowDefinition();
                newRow.Height = new GridLength(50);
                MainGrid.RowDefinitions.Add(newRow);

                // Добавление картинки для кнопки
                BitmapImage btm = new BitmapImage();
                Image img = new Image();
                if (sData.DLDistance > 0)
                {
                    BtmSet("/Resource/nextIcon.png", btm);
                }
                else
                    BtmSet("/Resource/equalIcon.png", btm);
                {
                }
                img.Source = btm;

                // Заполнение первого элемента
                TextBox tBox1 = new TextBox();
                tBox1.Text = sData.SimilarNames[0];
            
                // Заполнение второго элемента
                TextBox tBox2 = new TextBox();
                tBox2.Text = sData.SimilarNames[1];
                AddTBox(tBox1, tBox2, rowCount, img);

                rowCount++;
            }

        }

        private void AddTBox(TextBox tBox1, TextBox tBox2, int row,  Image image)
        {
            // Добавление кнопки переименования
            Button renameBtn = new Button();
            renameBtn.Content = image;
            renameBtn.Height = 25;
            renameBtn.Tag = $"{tBox1.Text}-/-{tBox2.Text}"; // Помечаю кнопку именами файлов из БИМ360 и из папки
            renameBtn.Click += OnBtnClick;
            Grid.SetRow(renameBtn, row);
            Grid.SetColumn(renameBtn, 1);
            _ = MainGrid.Children.Add(renameBtn);

            // Добавление TextBox и заполнение текстом из БИМ360
            SetTextBox(tBox1, row, 0, TextAlignment.Right);

            // Добавление TextBox и заполнение текстом из папки
            SetTextBox(tBox2, row, 2, TextAlignment.Left);
        }

        private void OnBtnClick(object sender, RoutedEventArgs e)
        {
            string[] similarNames = (sender as Button).Tag.ToString().Split("-/-");
            string bim360Name = similarNames[0];
            string dirName = similarNames[1];
            
            // Переименование файла
            FileInfo fi = new FileInfo(@$"{userDir}\{dirName}");
            fi.MoveTo(@$"{userDir}\{bim360Name}");
        }

        private void BtmSet (string path, BitmapImage btm)
        {
            btm.BeginInit();
            btm.UriSource = new Uri(path, UriKind.RelativeOrAbsolute);
            btm.EndInit();
        }

        private void SetTextBox(TextBox tBox, int row, int col, TextAlignment tAligm)
        {
            Grid.SetRow(tBox, row);
            Grid.SetColumn(tBox, col);
            tBox.TextAlignment = tAligm;
            tBox.IsReadOnly = true;
            _ = MainGrid.Children.Add(tBox);

        }
    }
}
