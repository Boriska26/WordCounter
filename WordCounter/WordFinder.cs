using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WordCounter.Exceptions;

namespace WordCounter
{
    public class WordFinder
    {
        /// <summary>
        /// Путь к каталогу
        /// </summary>
        private string _pathDirectory;

        /// <summary>
        /// Путь к каталогу где храниться текст
        /// </summary>
        public string PathDirectory
        {
            get { return _pathDirectory; }
            set {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new PathException();
                }
                _pathDirectory = value;
            }
        }

        /// <summary>
        /// Путь к файлу со словами
        /// </summary>
        private string _pathWordFile;

        /// <summary>
        /// Путь к файлу со словами
        /// </summary>
        public string PathWordFile
        {
            get { return _pathWordFile; }
            set {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new PathException();
                }
                _pathWordFile = value;
            }
        }

        /// <summary>
        /// Регистр
        /// </summary>
        private bool _register;

        /// <summary>
        /// Поиск
        /// </summary>
        /// <returns></returns>
        public ICollection<FoundWord> Find(bool register)
        {
            _register = register;
            ICollection<FoundWord> FoundWords = new List<FoundWord>();
            var words = ReadingWordFile();
            foreach (string word in words)
            {
                FoundWord foundWord = new FoundWord();
                foundWord.NumberWord = ReadingTextFile(word);
                foundWord.Word = word;
                FoundWords.Add(foundWord);
            }
            return FoundWords;
        }

        /// <summary>
        /// Чтение файла со словами
        /// </summary>
        /// <returns></returns>
        private IEnumerable<string> ReadingWordFile()
        {
            using (StreamReader reader = new StreamReader(PathWordFile))
            {
                List<string> words = new List<string>();
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    words.AddRange(Regex.Split(line, @"\W"));
                }
                if(words.Count < 1)
                {
                    throw new EmptyFileWordsException();
                }
                return words;
            }
        }

        /// <summary>
        /// Чтение файла с текстом
        /// </summary>
        /// <param name="word">слово</param>
        /// <returns></returns>
        private int ReadingTextFile(string word)
        {
            int count = 0;
            foreach (string file in Directory.GetFiles(PathDirectory))
            {
                using (StreamReader reader = new StreamReader(file))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        count += Сomparison(line, word);
                    }
                }
            }
            return count;
        }

        /// <summary>
        /// Сравение
        /// </summary>
        /// <param name="line">строка из текста</param>
        /// <param name="word">слово</param>
        /// <returns></returns>
        private int Сomparison(string line, string word)
        {
            var textWords = Regex.Split(line, @"\W").Where(n => !string.IsNullOrWhiteSpace(n));
            if (_register == true)
            {
                return textWords.Where(n => n.Equals(word)).Count();
            }
            else
            {
                var a = textWords.Select(n => n.ToLower()).ToList();
                var b = word.ToLower();
                return textWords.Where(n => n.ToLower().Equals(word.ToLower())).Count();
            }
        }
    }
}
