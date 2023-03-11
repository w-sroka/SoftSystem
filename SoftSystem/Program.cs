using System;
using System.Collections.Generic;
using System.Linq;

namespace SoftSystem
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            int numberOfClassrooms = Scheduler();
            decimal percentOfThem = PercentageOfPerfectNumbers(RandomNumbers());

            Console.WriteLine($"1. Minimalna liczba sal do przydzielenia zajęć to: {numberOfClassrooms}");
            Console.WriteLine($"2. {percentOfThem} procent z wylosowanych liczb jest liczbami doskonałymi");

            Console.WriteLine("3. SQL Query:");

            Console.WriteLine(@"
            SELECT a.first_name AS AuthorName,
	                a.middle_name AS AuthorMiddleName,
	                a.last_name AS AuthorLastName,
	                b.title AS BookTittle,
	                p.[name] AS PublisherName,
	                g.genre AS Genre
            FROM books AS b
            LEFT JOIN book_authors AS ba ON b.book_id = ba.book_id
            LEFT JOIN authors AS a ON a.author_id = ba.author_id
            LEFT JOIN publishers AS p ON p.publsher_id = b.publisher_id
            LEFT JOIN book_genres AS bg ON bg.book_id = b.book_id
            LEFT JOIN genres AS g ON bg.genre_id = g.genre_id
            LEFT JOIN genres AS parent ON g.genre_id <> parent.genre_id
            ORDER BY g.genre");

            Console.ReadKey();
        }

        private static decimal PercentageOfPerfectNumbers(List<int> randList)
        {
            int perfectNumberCount = 0;

            foreach (var number in randList)
            {
                int sum = 0;

                for (int i = 1; i < number; i++)
                {
                    if (number % i == 0)
                    {
                        sum = sum + i;
                    }
                }
                if (sum == number)
                    perfectNumberCount++;
            }

            return Convert.ToDecimal(perfectNumberCount) / Convert.ToDecimal(randList.Count) * 100;
        }

        private static List<int> RandomNumbers()
        {
            List<int> randList = new List<int>();

            int rnd = 4;
            int a = 34;
            int b = 10000;

            while (randList.Count != 40)
            {
                rnd = (a * rnd + b) % 99;

                if (!randList.Any(x => x == rnd) && rnd != 0)
                {
                    randList.Add(rnd);
                }
            }

            var rrr = randList.OrderBy(x => x).ToList();

            return randList;
        }

        private static int Scheduler()
        {
            List<ClassRoom> classRoomsList = new List<ClassRoom> { new ClassRoom() };

            List<ClassData> exampleDataList = new List<ClassData>
            {
                new ClassData(1,8,10),
                new ClassData(2,8,11),
                new ClassData(3,9,11),
                new ClassData(4,9,11),
                new ClassData(5,12,14),
                new ClassData(6,12,13),
                new ClassData(7,11,13),
                new ClassData(8,8,11),
                new ClassData(9,12,13)
            };

            foreach (var classdata in exampleDataList)
            {
                List<int> hours = new List<int>();
                for (int i = classdata.pi; i <= classdata.ki; i++)
                {
                    hours.Add(i);
                }

                foreach (var hour in hours)
                {
                    var unusedClassRoom = classRoomsList.FirstOrDefault(x => x.bookedHours[hour] == 0);
                    if (unusedClassRoom is null)
                    {
                        ClassRoom newClassRoom = new ClassRoom();
                        newClassRoom.bookedHours[hour] = 1;
                        classRoomsList.Add(newClassRoom);
                        continue;
                    }
                    unusedClassRoom.bookedHours[hour] = 1;
                }
            }

            return classRoomsList.Count;
        }

        public class ClassData
        {
            public int i = 1;
            public int pi = 8;
            public int ki = 10;

            public ClassData(int i, int pi, int ki)
            {
                this.i = i;
                this.pi = pi;
                this.ki = ki;
            }
        }

        public class ClassRoom
        {
            public Guid id;
            public int[] bookedHours;

            public ClassRoom()
            {
                id = new Guid();
                bookedHours = new int[24];
            }
        }
    }
}