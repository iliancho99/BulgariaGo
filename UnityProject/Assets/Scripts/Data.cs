using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Assets.Scripts
{
    public static class Data
    {
        static Data()
        {
            BuildingIdToName = new Dictionary<int, string>();
            BuildingIdToQuestions = new Dictionary<int, List<Question>>();
            BuildingIdToHints = new Dictionary<int, Hint[]>();
            BuildingURL = new Dictionary<int, string>();
            LoadNames();
            LoadQuestions();
            LoadHints();
            LoadURLs();
        }

    

        public static Dictionary<int, string> BuildingIdToName { get; set; }
        public static Dictionary<int, List<Question>> BuildingIdToQuestions { get; set; }
        public static Dictionary<int, Hint[]> BuildingIdToHints { get; set; }
        public static Dictionary<int, string> BuildingURL { get; set; }

        private static void LoadNames()
        {
            BuildingIdToName.Add(0, "Софтуерен Университет");
            BuildingIdToName.Add(1, "Софийски Университет");
            BuildingIdToName.Add(2, "Националана библиотека");
            BuildingIdToName.Add(3, "Българска академия на науките");
            BuildingIdToName.Add(4, "НАТФИЗ");
            BuildingIdToName.Add(5, "Централни Софийски Хали");
        }

        private static void LoadQuestions()
        {
            BuildingIdToQuestions.Add(0, new List<Question>
            {
                new Question("В кой град се намира СофтУни?", new[] {"Пловдив", "Монтана", "Враца", "София"}, 4),
                new Question("През коя година започва първия курс в СофтУни?", new[] {"2014", "2012", "2015", "1971"}, 1),
                new Question("Как е името на първата зала в СофтУни?", new[] {"Open Source", "Ballistic", "Inpiration", "Code Ground"}, 2)
            });

            BuildingIdToQuestions.Add(1, new List<Question>
            {
                new Question("През коя година е съдаден Софийския университет?", new[] {"1888", "1998", "2015", "1890"}, 1),
                new Question("Кой е първия ректор на Софийския университет?", new[] {  "Георги Шишков", "Никола Попов", "Александър Теодоров-Балан", "Анастас Герджиков" }, 3),
                new Question("Колко факултета има Софийския университет?", new[] {  "10", "16", "17", "25" }, 2)
            });
            BuildingIdToQuestions.Add(2, new List<Question>
            {
                new Question("През коя година е основана Националната библиотека?", new[] {"1879", "1889", "1900", "1911"}, 1),
                new Question("Какво е първото име на Националната библиотека?", new[] { "Българска народна библиотека", "Българска библиотека", "Първа Българска библиотека", "Национална библиотека" }, 1),
                new Question("Кой е първия директор на Националната библиотека?", new[] {"Петко Славейков", "Пенчо Славейков", "Георги Кирков", "Орлин Василев" }, 3)
            });

            BuildingIdToQuestions.Add(3, new List<Question>
            {
                new Question("През коя година е основана Българско книжовно дружество?", new[] {"1898", "1869", "1938", "1878"}, 2),
                new Question("През коя година БКД става Българска академия на науките?", new[] {"1920", "1910", "1911", "1915"}, 3)
            });

            BuildingIdToQuestions.Add(4, new List<Question>
            {
                new Question("През коя година е основана НАТВИЗ?", new[] {"1898", "1948", "1938", "1950"}, 2),
                new Question("Кой е първият ректор на НАТФИЗ?", new[] { "Стефан Каракостов", "Стефан Данаилов", "Димитър Митов", "Боян Дановски" }, 3)
            });

            BuildingIdToQuestions.Add(5, new List<Question>
            {
                new Question("През коя година започва строежа на Централните софийски хали?", new[] {"1909", "1986", "1938", "1860"}, 1),
            });
        }

        private static void LoadHints()
        {
            BuildingIdToHints.Add(0, new[] {new Hint("София", 1), new Hint("СофтУни", 2)});
            BuildingIdToHints.Add(1, new[] {new Hint("София", 1), new Hint("Софийски университет", 2)});
            BuildingIdToHints.Add(2, new[] {new Hint("София", 1), new Hint("Национална библиотека", 2)});
            BuildingIdToHints.Add(3, new[] {new Hint("София", 1), new Hint("БАН", 1)});
            BuildingIdToHints.Add(4, new[] {new Hint("София", 1), new Hint("НАТФИЗ", 1)});
            BuildingIdToHints.Add(5, new[] {new Hint("София", 1), new Hint("Халите", 1)});
        }

        private static void LoadURLs()
        {
            BuildingURL.Add(0, "https://bg.wikipedia.org/wiki/%D0%A1%D0%BE%D1%84%D1%82%D1%83%D0%B5%D1%80%D0%B5%D0%BD_%D1%83%D0%BD%D0%B8%D0%B2%D0%B5%D1%80%D1%81%D0%B8%D1%82%D0%B5%D1%82_(%D0%A1%D0%BE%D1%84%D1%82%D0%A3%D0%BD%D0%B8)");
            BuildingURL.Add(1, "https://bg.wikipedia.org/wiki/%D0%A1%D0%BE%D1%84%D0%B8%D0%B9%D1%81%D0%BA%D0%B8_%D1%83%D0%BD%D0%B8%D0%B2%D0%B5%D1%80%D1%81%D0%B8%D1%82%D0%B5%D1%82");
            BuildingURL.Add(2, "https://bg.wikipedia.org/wiki/%D0%9D%D0%B0%D1%86%D0%B8%D0%BE%D0%BD%D0%B0%D0%BB%D0%BD%D0%B0_%D0%B1%D0%B8%D0%B1%D0%BB%D0%B8%D0%BE%D1%82%D0%B5%D0%BA%D0%B0_%E2%80%9E%D0%A1%D0%B2._%D1%81%D0%B2._%D0%9A%D0%B8%D1%80%D0%B8%D0%BB_%D0%B8_%D0%9C%D0%B5%D1%82%D0%BE%D0%B4%D0%B8%D0%B9%E2%80%9C");
            BuildingURL.Add(3, "https://bg.wikipedia.org/wiki/%D0%91%D1%8A%D0%BB%D0%B3%D0%B0%D1%80%D1%81%D0%BA%D0%B0_%D0%B0%D0%BA%D0%B0%D0%B4%D0%B5%D0%BC%D0%B8%D1%8F_%D0%BD%D0%B0_%D0%BD%D0%B0%D1%83%D0%BA%D0%B8%D1%82%D0%B5");
            BuildingURL.Add(4, "https://bg.wikipedia.org/wiki/%D0%9D%D0%B0%D1%86%D0%B8%D0%BE%D0%BD%D0%B0%D0%BB%D0%BD%D0%B0_%D0%B0%D0%BA%D0%B0%D0%B4%D0%B5%D0%BC%D0%B8%D1%8F_%D0%B7%D0%B0_%D1%82%D0%B5%D0%B0%D1%82%D1%80%D0%B0%D0%BB%D0%BD%D0%BE_%D0%B8_%D1%84%D0%B8%D0%BB%D0%BC%D0%BE%D0%B2%D0%BE_%D0%B8%D0%B7%D0%BA%D1%83%D1%81%D1%82%D0%B2%D0%BE");
            BuildingURL.Add(5, "https://bg.wikipedia.org/wiki/%D0%A6%D0%B5%D0%BD%D1%82%D1%80%D0%B0%D0%BB%D0%BD%D0%B8_%D1%81%D0%BE%D1%84%D0%B8%D0%B9%D1%81%D0%BA%D0%B8_%D1%85%D0%B0%D0%BB%D0%B8");


        }
    }
}