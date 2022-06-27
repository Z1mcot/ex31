using System;
using System.Collections;
using System.Linq;

namespace ex31
{
    
    public class WordsList
    {
        WordsList? nextItem;
        WordsList? previousItem;
        string name;
        int index;

        public WordsList(int index, string name, WordsList? nextItem, WordsList? previousItem)
        {
            this.index = index;
            this.name = name;
            this.nextItem = nextItem;
            this.previousItem = previousItem;
        }

        public string Name { get => name; }
        public WordsList? NextItem { get => nextItem; }
        public WordsList? PreviousItem { get => previousItem; }
        public int Index { get => index; }
        public void AddNextItem(WordsList anotherItem) => nextItem = anotherItem; 
    }
    
    internal class Program
    {
        static void FindItem(string nameQuery, WordsList AnotherItem, ref int searchedName)
        {
            if (nameQuery == AnotherItem.Name)
                searchedName = AnotherItem.Index;
            
            if (AnotherItem.NextItem != null)
                FindItem(nameQuery, AnotherItem.NextItem, ref searchedName);
        }

        static void Main()
        {
            WordsList FirstWord; 
            using (var file = new StreamReader("../../../IO/Input.txt"))
            {
                var words = file.ReadToEnd().Split(new char[] { ' ', '\n' });
                FirstWord = new WordsList(0, words[0], null, null);

                WordsList? previousWord = null;
                WordsList? currentWord = FirstWord;
                for (int i = 1; i < words.Length; i++)
                {
                    previousWord = currentWord;
                    currentWord = new WordsList(i, words[i], null, previousWord);
                    previousWord.AddNextItem(currentWord);
                }
            }
            int searchedName = -1;
            FindItem("Иван", FirstWord, ref searchedName);

            using (var file = new StreamWriter("../../../IO/Output.txt"))
            {
                if (searchedName == -1)
                    file.WriteLine("Имя не найдено!");
                else
                {
                    file.WriteLine(searchedName);
                }
            }
        }
    }
}