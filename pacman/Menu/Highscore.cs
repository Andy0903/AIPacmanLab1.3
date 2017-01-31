using System.Collections.Generic;
using System.IO;

namespace Pacman
{
    static class Highscore
    {
        #region Properties
        private static string FileLocation
        {
            get { return "../../../Content/Highscore/Highscores.hs"; }
        }
        #endregion

        #region Public methods
        public static List<string> ReadToFile()
        {
            List<string> strings = new List<string>();
            using (StreamReader streamReader = new StreamReader(FileLocation))
            {
                while (!streamReader.EndOfStream)
                {
                    strings.Add(streamReader.ReadLine());
                }
            }
            return strings;
        }

        public static void WriteToFile(List<string> aStrings)
        {
            using (StreamWriter streamWriter = new StreamWriter(FileLocation))
            {
                for (int i = 0; i < aStrings.Count; i++)
                {
                    streamWriter.WriteLine(aStrings[i]);
                }
            }

        }

        public static void UpdateHighscore(int aNewScore)
        {
            List<string> strings = ReadToFile();
            List<int> scores = new List<int>();

            scores.Add(aNewScore);
            for (int i = 0; i < strings.Count; i++)
            {
                scores.Add(int.Parse(strings[i]));
            }
            scores.Sort();
            scores.Reverse();

            strings = new List<string>();
            for (int i = 0; i < 5; i++)
            {
                strings.Add(scores[i].ToString());
            }

            WriteToFile(strings);
        }
        #endregion
    }
}
