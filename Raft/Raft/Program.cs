using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raft
{
    public class Program
    {
        private const string INVALID_DATA = "Invalid data! Try again!";

        public static void Main(string[] args)
        {
            char[] separators = { ' ', ','};
            int[] goatsAndCourses = Console.ReadLine()
                .Split(separators, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            if (!goatsAndCourses.All(w => w >= 1 && w <= 1000))
            {
                Console.WriteLine(INVALID_DATA);
                Environment.Exit(0);
            }

            int numberOfGoats = goatsAndCourses[0];
            int numberOfCourses = goatsAndCourses[1];

            List<int> weightsOfGoats = Console.ReadLine()
                .Split(separators, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .OrderByDescending(w => w)
                .ToList();

            if (!weightsOfGoats.All(w => w >= 1 && w <= 100000))
            {
                Console.WriteLine(INVALID_DATA);
                Environment.Exit(0);
            }

            if (numberOfGoats != weightsOfGoats.Count)
            {
                Console.WriteLine(INVALID_DATA);
                Environment.Exit(0);
            }

            int raftCapacity = weightsOfGoats.Max();
            int completedCourses = 0;
            int occupiedCapacity = 0;
            List<int> weightsOfGoatsCopy = weightsOfGoats;

            while (true)
            {
                if (weightsOfGoatsCopy.Count != 1)
                {
                    occupiedCapacity += weightsOfGoatsCopy.First();
                    weightsOfGoatsCopy.RemoveAt(0);

                    if (weightsOfGoatsCopy.Count >= 2)
                    {
                        for (int i = 0; i < weightsOfGoatsCopy.Count; i++)
                        {
                            if (occupiedCapacity + weightsOfGoatsCopy[i] <= raftCapacity)
                            {
                                occupiedCapacity += weightsOfGoatsCopy[i];
                                i--;
                            }
                        }
                    }
                }
                
                completedCourses++;

                if (completedCourses == numberOfCourses)
                {
                    if (weightsOfGoatsCopy.Count == 0)
                    {
                        break;
                    }

                    completedCourses = 0;
                    weightsOfGoatsCopy = weightsOfGoats;
                    raftCapacity++;
                }

                occupiedCapacity = 0;
            }

            Console.WriteLine(raftCapacity);
        }
    }
}
