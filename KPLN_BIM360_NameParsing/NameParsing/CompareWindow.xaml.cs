using NameParsing.ParsingData;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;


namespace NameParsing
{
    /// <summary>
    /// Interaction logic for CompareWindow.xaml
    /// </summary>
    public partial class CompareWindow : Window
    {
        public CompareWindow()
        {
            InitializeComponent();
        }

        public void AddRowData(LinkedList<SimilarData> sDataList)
        {
            int rowCount = 0;
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
                    BTMSet("/Resource/nextIcon.png", btm);
                }
                else
                    BTMSet("/Resource/equalIcon.png", btm);
                {
                }
                img.Source = btm;

                // Заполнение первого элемента
                TextBox tBox1 = new TextBox();
                tBox1.Text = sData.SimilarNames[0];
                AddTBox(tBox1, rowCount, 0, TextAlignment.Right, img);
            
                // Заполнение второго элемента
                TextBox tBox2 = new TextBox();
                tBox2.Text = sData.SimilarNames[1];
                AddTBox(tBox2, rowCount, 2, TextAlignment.Left, img);

                rowCount++;
            }

        }

        private void AddTBox(TextBox tBox, int row, int col, TextAlignment textAlignment, Image image)
        {
            // Добавление кнопки переименования
            Button renameBtn = new Button();
            renameBtn.Content = image;
            renameBtn.Height = 25;
            renameBtn.Click += OnBtnClick;
            Grid.SetRow(renameBtn, row);
            Grid.SetColumn(renameBtn, 1);
            MainGrid.Children.Add(renameBtn);

            // Заполнение текстом
            Grid.SetRow(tBox, row);
            Grid.SetColumn(tBox, col);
            MainGrid.Children.Add(tBox);
            tBox.TextAlignment = textAlignment;
        }

        private void OnBtnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Кнопка нажата");
        }

        private void BTMSet (string path, BitmapImage btm)
        {
            btm.BeginInit();
            btm.UriSource = new Uri(path, UriKind.RelativeOrAbsolute);
            btm.EndInit();
        }
    }
}
