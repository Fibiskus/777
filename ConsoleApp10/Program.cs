using System;
using System.Collections;

namespace ConsoleApp1
{
    public class Document : IComparable
    {
        public string Name;
        public string Number;
        public int Number_of_documents;
        public int Number_of_pages;
        public double Date;

        public Document() { }

        public Document(string Name, string Number, int Number_of_documents, int Number_of_pages, double Date)
        {
            this.Name = Name;
            this.Number = Number;
            this.Number_of_documents = Number_of_documents;
            this.Number_of_pages = Number_of_pages;
            this.Date = Date;
        }

        int IComparable.CompareTo(object obj)
        {
            Document h = obj as Document;
            if (h != null)
            {
                if (this.Number_of_pages < h.Number_of_pages)
                    return -1;
                else if (this.Number_of_pages > h.Number_of_pages)
                    return 1;
                else
                    return 0;
            }
            else
            {
                throw new Exception("Параметр повинен бути типу Document!");
            }
        }

        public class SortByPrice : IComparer
        {
            int IComparer.Compare(object x, object y)
            {
                Document h1 = (Document)x;
                Document h2 = (Document)y;
                if (h1.Number_of_pages < h2.Number_of_pages)
                    return -1;
                else if (h1.Number_of_pages > h2.Number_of_pages)
                    return 1;
                else
                    return 0;
            }
        }

        public class SortByArea : IComparer
        {
            public int Compare(object x, object y)
            {
                Document h1 = (Document)x;
                Document h2 = (Document)y;
                if (h1.Date < h2.Date)
                    return -1;
                else if (h1.Date > h2.Date)
                    return 1;
                else
                    return 0;
            }
        }
    }

    class Houses : IEnumerable
    {
        private Document[] _documents;
        public Houses(Document[] hs)
        {
            _documents = new Document[hs.Length];
            for (int i = 0; i < hs.Length; i++)
                _documents[i] = hs[i];
        }

        public HousesEnum GetEnumerator()
        {
            return new HousesEnum(_documents);
        }


        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        public class HousesEnum : IEnumerator
        {
            public Document[] _houses;

            int position = -1;

            public HousesEnum(Document[] list)
            {
                _houses = list;
            }

            public bool MoveNext()
            {
                position++;
                return (position < _houses.Length);
            }

            public void Reset()
            {
                position = -1;
            }

            object IEnumerator.Current
            {
                get
                {
                    return Current;
                }
            }

            public Document Current
            {
                get
                {
                    try
                    {
                        return _houses[position];
                    }
                    catch (IndexOutOfRangeException)
                    {
                        throw new InvalidOperationException();
                    }
                }
            }
        }
    }

    class Program
    {
        public static void Main(string[] args)
        {
            Document h1 = new Document("Document 1", "2555  adfsjaglal", 6, 120, 04);
            Document h2 = new Document("Document 2", "2543  ahahdhahha", 3, 60, 06);
            Document h3 = new Document("Document 3", "5774  aahadfjfgkj", 3, 40, 10);
            Document h4 = new Document("Document 4", "5694  adjgxfhgfdd", 7, 300, 08);
            Document h5 = new Document("Document 5", "5777  adjgxfhgfdd", 5, 500, 01);
            Document h6 = new Document("Document 6", "485  adjgxfhgfdd", 5, 400, 14);
            Document h7 = new Document("Document 7", "467  adjgxfhgfdd", 2, 200, 17);
            Document h8 = new Document("Document 8", "2785  adjgxfhgfdd", 2, 70, 20);
            Document h9 = new Document("Document 9", "9653 adjgxfhgfdd", 3, 50, 21);
            Document h10 = new Document("Document 10", "3621  adjgxfhgfdd", 1, 140, 22);

            Document[] documentes = new Document[10];
            documentes[0] = h1;
            documentes[1] = h2;
            documentes[2] = h3;
            documentes[3] = h4;
            documentes[4] = h5;
            documentes[5] = h6;
            documentes[6] = h7;
            documentes[7] = h8;
            documentes[8] = h9;
            documentes[9] = h10;

            string prn01 = "Назва:\t\tНомер:\t\t\t\tКiлькiсть документів:\tСторінки ($):\tДата(день):";

            Console.WriteLine("Список без сортування");
            Console.WriteLine(prn01);
            for (int i = 0; i < documentes.Length; i++)
                Console.WriteLine(documentes[i].Name + "\t" + documentes[i].Number + "\t\t" + documentes[i].Number_of_documents + "\t\t\t" + documentes[i].Number_of_pages + "\t\t" + documentes[i].Date);

            while (true)
            {
                Console.WriteLine("\nНатиснiть на одну з вказаних клавiш, щоб вибрати режим роботи: ");
                Console.WriteLine("Реалiзацiя iнтерфейсу  IComparable для порiвняння будинкiв за цiною  - 1");
                Console.WriteLine("Реалiзацiя в класi iнтерфейсу IComparer для порiвняння документів за кількістю сторінок i за датою створення - 2");
                Console.WriteLine("Реалiзацiя iнтерфейсу IEnumerable - 3");
                Console.WriteLine("Вихiд з програми - будь-яка iнша клавiша");

                ConsoleKeyInfo cki;
                cki = Console.ReadKey(true);
                if (cki.Key == ConsoleKey.NumPad1)
                {
                    Array.Sort(documentes);
                    Console.WriteLine("\nСортування за кількістю сторінок ");
                    Console.WriteLine(prn01);
                    for (int i = 0; i < documentes.Length; i++)
                        Console.WriteLine(documentes[i].Name + "\t" + documentes[i].Number + "\t\t" + documentes[i].Number_of_documents + "\t\t\t" + documentes[i].Number_of_pages + "\t\t" + documentes[i].Date);
                }
                else if (cki.Key == ConsoleKey.NumPad2)
                {
                    Array.Sort(documentes, new Document.SortByPrice());
                    Console.WriteLine("\nСортування за кількістю сторінок");
                    Console.WriteLine(prn01);
                    for (int i = 0; i < documentes.Length; i++)
                        Console.WriteLine(documentes[i].Name + "\t" + documentes[i].Number + "\t\t" + documentes[i].Number_of_documents + "\t\t\t" + documentes[i].Number_of_pages + "\t\t" + documentes[i].Date);

                    Array.Sort(documentes, new Document.SortByArea());
                    Console.WriteLine("\nСортування за датою створення");
                    Console.WriteLine(prn01);
                    for (int i = 0; i < documentes.Length; i++)
                        Console.WriteLine(documentes[i].Name + "\t" + documentes[i].Number + "\t\t" + documentes[i].Number_of_documents + "\t\t\t" + documentes[i].Number_of_pages + "\t\t" + documentes[i].Date);
                }
                else if (cki.Key == ConsoleKey.NumPad3)
                {
                    Array.Sort(documentes, new Document.SortByPrice());
                    Houses HouseCollections01 = new Houses(documentes);

                    Console.WriteLine("\nСортування за кількістю сторінок");
                    Console.WriteLine(prn01);
                    foreach (var document in HouseCollections01)
                        Console.WriteLine(document.Name + "\t" + document.Number + "\t\t" + document.Number_of_documents + "\t\t\t" + document.Number_of_pages + "\t\t" + document.Date);

                    Array.Sort(documentes, new Document.SortByArea());
                    Houses HouseCollections02 = new Houses(documentes);

                    Console.WriteLine("\nСортування за датою створення");
                    Console.WriteLine(prn01);
                    foreach (var document in HouseCollections02)
                        Console.WriteLine(document.Name + "\t" + document.Number + "\t\t" + document.Number_of_documents + "\t\t\t" + document.Number_of_pages + "\t\t" + document.Date);
                }
                else
                    break;
            }
        }
    }
}
