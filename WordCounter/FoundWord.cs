using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordCounter
{
    public struct FoundWord
    {
        /// <summary>
        /// Количество совпадений
        /// </summary>
        public int NumberWord { get; set; }

        /// <summary>
        /// Слово
        /// </summary>
        public string Word { get; set; }
    }
}
