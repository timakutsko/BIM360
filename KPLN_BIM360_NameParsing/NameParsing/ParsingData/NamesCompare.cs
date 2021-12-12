using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;


namespace NameParsing.ParsingData
{
    public static class NamesCompare
    {
        public static void Compare(List<string> list1, List<string> list2)
        {
            // Открываю новое окно (для сравнения)
            CompareWindow compareWind = new CompareWindow();
            compareWind.Show();

            // Анализ имени файлов и заполнение окна сравнения
            LinkedList<SimilarData> compDataList = new LinkedList<SimilarData>();
            foreach (string str1 in list1)
            {
                SimilarData sData = new SimilarData();

                // Проверяю второе значение на предмет совпадения с первым
                int tempLenght = 1000;
                string tempStr2 = " ";
                foreach (string str2 in list2)
                {
                    int damLevDist = DamerauLevenshteinDistance(str1, str2);
                    if (damLevDist < tempLenght)
                    {
                        tempLenght = damLevDist;
                        tempStr2 = str2;
                    }
                }
                sData.DLDistance = tempLenght;
                sData.SimilarNames = new string[] { str1, tempStr2 };
                // compareWind.AddRowData(str1, tempStr2, count++, tempLenght);
                AddSorted(compDataList, sData);
            }

            // Передача данных в пользовательское окно
            //compareWind.AddRowData(compDataList);
        }
        
        // Все ниже - расчет расстояния Дамерау-Левенштейна, для нахождения разницы между строками
        static int Minimum(int a, int b) => a < b ? a : b;

        static int Minimum(int a, int b, int c) => (a = a < b ? a : b) < c ? a : c;

        static int DamerauLevenshteinDistance(string firstText, string secondText)
        {
            var n = firstText.Length + 1;
            var m = secondText.Length + 1;
            var arrayD = new int[n, m];

            for (var i = 0; i < n; i++)
            {
                arrayD[i, 0] = i;
            }

            for (var j = 0; j < m; j++)
            {
                arrayD[0, j] = j;
            }

            for (var i = 1; i < n; i++)
            {
                for (var j = 1; j < m; j++)
                {
                    var cost = firstText[i - 1] == secondText[j - 1] ? 0 : 1;

                    arrayD[i, j] = Minimum(arrayD[i - 1, j] + 1,          // удаление
                                            arrayD[i, j - 1] + 1,         // вставка
                                            arrayD[i - 1, j - 1] + cost); // замена

                    if (i > 1 && j > 1
                        && firstText[i - 1] == secondText[j - 2]
                        && firstText[i - 2] == secondText[j - 1])
                    {
                        arrayD[i, j] = Minimum(arrayD[i, j],
                                           arrayD[i - 2, j - 2] + cost); // перестановка
                    }
                }
            }

            return arrayD[n - 1, m - 1];
        }

        private static void AddSorted(LinkedList<SimilarData> linkList, SimilarData data)
        {
            if(linkList.Count == 0)
            {
                linkList.AddFirst(data);
            }
            else
            {
                int curDist = data.DLDistance;
                foreach(SimilarData sd in linkList)
                {

                }

            }
        }
    }
}
