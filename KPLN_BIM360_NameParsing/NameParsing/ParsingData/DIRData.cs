using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NameParsing.ParsingData
{
    class DIRData
    {
        
        private string[] FilesDIR { get; set; }
        
        public DIRData(string dirPath)
        {
            FilesDIR = Directory.GetFiles(dirPath);
        }
        
        // Сбор информации
        public List<string> Names(List<string> extensions)
        {
            int lenght = FilesDIR.Length;
            List<string> namesList = new List<string>();
            for(int i = 0; i < lenght; i++)
            {
                string file = FilesDIR[i];
                if (extensions.Any(s => file.EndsWith(s)))
                {
                    string fileName = Path.GetFileName(file);
                    namesList.Add(fileName);
                }
            }
            return namesList;
        }
    }
}
