using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordCounter.Exceptions;

namespace WordCounter
{
    class Program
    {
        private static void EnterPath  (WordFinder wordFinder)
        {
            Console.WriteLine("Введите путь к папке с текстовыми файлами (.txt)");
            wordFinder.PathDirectory = Console.ReadLine();
            Console.WriteLine("Введите путь к текстовому файлу со словами (.txt)");
            wordFinder.PathWordFile = Console.ReadLine();
        }
    static void Main(string[] args)
        {
            try
            {
                WordFinder wordFinder = new WordFinder();
                EnterPath(wordFinder);
                var foundWords = wordFinder.Find(true);
                int count = 1;
                foreach (var foundWord in foundWords)
                {
                    Console.WriteLine($"{count}. {foundWord.Word} = {foundWord.NumberWord}");
                    count++;
                }
                Console.WriteLine($"Общее число совпадений {foundWords.Sum(n => n.NumberWord)}");

            }
            catch(PathException)
            {
                Console.WriteLine("Ошибка в пути файла");
            }
            catch(EmptyFileWordsException)
            {
                Console.WriteLine("Файл со словами пустой");
            }
            catch(FileNotFoundException ex)
            {
                Console.WriteLine($"{ex.FileName} - Файл не найден");
            }
            finally
            {
                Console.WriteLine("Для выхода нажмите любую клавишу");
                Console.ReadLine();
            }
        }
    }
}
