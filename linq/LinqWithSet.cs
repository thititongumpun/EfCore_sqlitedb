using System;
using System.Collections.Generic;
using System.Linq;

namespace linq
{
    public class LinqWithSet
    {
        static void Output(IEnumerable<string> name, string description = "")
        {
            if (!string.IsNullOrEmpty(description))
            {
                Console.WriteLine(description);
            }
            Console.Write(" ");
            Console.WriteLine(string.Join(", ", name.ToArray()));
        }

        public static void setOfLinq()
        {
            var name1 = new string[] {"Rachel", "Gareth", "Jonathan", "George"};
            var name2 = new string[] {"Jack", "Stephen", "Daniel", "Jack", "Jared"};
            var name3 = new string[] {"Declan", "Jack", "Jack", "Jasmine", "Conor"};

            Output(name1, "name 1");
            Output(name2, "name 2");
            Output(name3, "name 3");
            Output(name2.Distinct(), "name2 distinct");
            Output(name2.Union(name3), "name3 union");
        }

    }
}