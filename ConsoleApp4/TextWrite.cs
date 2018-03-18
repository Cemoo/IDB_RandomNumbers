using System;
using System.Collections.Generic;
using System.IO;

namespace IDB_RandomNumbers
{
    public class TextWrite : IText
    {
        private string textPath = @"C:\Users\Cemal\Desktop\randomNumbers.txt";

        public string TextPath { get => textPath; set => textPath = value; }

        public void Write(List<string> list)
        {
            string text = "";
            if (File.Exists(TextPath))
            {
                if (list.Count != 0)
                {
                    foreach (var listItem in list)
                    {
                        text = text + listItem + Environment.NewLine;
                    }
                    File.WriteAllText(TextPath, text);
                    Console.WriteLine("Wrote in text");
                }   
            }
            else
            {
                File.Create(TextPath);
                File.WriteAllText(TextPath, text);
                Console.WriteLine("Wrote in text");
            }

        }

        public void Read()
        {
            Console.WriteLine("This feature will be added soon");
        }
    }
}
