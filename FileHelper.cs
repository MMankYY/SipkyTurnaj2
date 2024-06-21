using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipkyTurnaj1
{
    public static class FileHelper
    {
        public static AutoCompleteStringCollection LoadNamesFromFile(string filePath)
        {
            AutoCompleteStringCollection names = new AutoCompleteStringCollection();
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                names.AddRange(lines);
            }
            else
            {
                MessageBox.Show("Súbor neexistuje.");
            }
            return names;
        }

        public static void AddNameToFile(string playerName, string filePath)
        {
            string newName = playerName;
            if (!string.IsNullOrWhiteSpace(newName))
            {
                var names = File.ReadAllLines(filePath);
                if (Array.IndexOf(names, newName) == -1)
                {
                    File.AppendAllText(filePath, newName + Environment.NewLine);
                }
            }

        }
    }
}
