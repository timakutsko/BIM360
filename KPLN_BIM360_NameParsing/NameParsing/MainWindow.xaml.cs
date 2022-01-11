using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using NameParsing.ParsingData;

namespace NameParsing
{
    public partial class MainWindow : Window
    {
        
        private readonly string FilePath;
        
        public MainWindow()
        {
            InitializeComponent();
            
            // Путь к файл предыдущего запуска
            FilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "KPLN_BIM360_NameParsing.json");
            
            // Запись или создание файла конфига
            try
            {
                // Попытка чтения (если конфиг был создан)
                string dataJson = File.ReadAllText(FilePath);
                JsonData data = JsonSerializer.Deserialize<JsonData>(dataJson);
                emailDef.Text = data.Email;
                passwDef.Password = data.Password;
                urlDef.Text = data.URL;
                dirDef.Text = data.DIR;
                slider.Value = data.EthrnSensivity;
                foreach (string ext in data.Extensions)
                {
                    if (ext == "rvt")
                    {
                        rvtCheckBox.IsChecked = true;
                    }
                    if (ext == "pdf")
                    {
                        pdfCheckBox.IsChecked = true;
                    }
                    if (ext == "dwg")
                    {
                        dwgCheckBox.IsChecked = true;
                    }
                }
            }
            catch 
            {
                // Если файла не было, имитируется пользовательский ввод на слайдер, а остальные поля остаются пустыми.
                // Также создается конфиг файл
                slider.Value = 1000;
                File.Create(FilePath);
            }
        }
        
        private void BtnStrClick(object sender, EventArgs e)
        {
            // Серилизации запуска
            JsonData JD = new JsonData();
            JD.Email = emailDef.Text;
            JD.Password = passwDef.Password;
            JD.URL = urlDef.Text;
            JD.DIR = dirDef.Text;
            JD.EthrnSensivity = slider.Value;
            JD.Extensions = new List<string>();
            List<string> userExt = SetExtList(JD);
            string json = JsonSerializer.Serialize(JD);
            File.WriteAllText(FilePath, json);

            // Обработка BIM360
            BIM360Data bim360 = new BIM360Data(emailDef.Text, passwDef.Password, urlDef.Text, Convert.ToInt32(slider.Value));
            List<string> namesBIM360 = bim360.Names(userExt);
            /*
            List<string> namesBIM360 = new List<string> { "СН17_ПП_АР.rvt", "СН17_ПП_АР_Фасад.rvt", "СН17_ПП_КР.rvt" };
            */

            // Обработка пользовательской папки
            DIRData userDir = new DIRData(dirDef.Text);
            List<string> namesDIR = userDir.Names(userExt);

            // Сравнение имен файлов
            NamesCompare.Compare(namesBIM360, namesDIR, dirDef.Text);
            
            //Закрытие основного окна
            this.Close();
        }
        
        private List<string> SetExtList(JsonData jsD)
        {
            var extChBxs = ExtCheckBoxes.Children;
            foreach(CheckBox curChBx in extChBxs)
            {
                string value = curChBx.Content.ToString().ToLower();
                if ((bool)curChBx.IsChecked)
                {
                    jsD.Extensions.Add(value);
                }
                else
                {
                    if (jsD.Extensions.Contains(value))
                    {
                        jsD.Extensions.Remove(value);
                    }
                }
            }
            return jsD.Extensions;
        }
    }
}
