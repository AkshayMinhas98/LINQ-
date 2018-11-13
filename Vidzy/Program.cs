
using System;
using System.Linq;

namespace Vidzy
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new VidzyContext();

            var query1 = context.Videos.Where(c => c.Genre.Name == "Action")
                                .OrderBy(s => s.Name);

            Console.WriteLine("Action movies sorted by name \n");
            foreach (var item in query1)
            {
                Console.WriteLine(item.Name);
            }

            //===================================================================================================================
            
            var query2 = context.Videos.Where(c => c.Genre.Name == "Drama"&&c.Classification==Classification.Gold)
                                .OrderByDescending(s=>s.ReleaseDate);

            Console.WriteLine("\nGold drama movies sorted by release date (newest first) \n");
            foreach (var item in query2)
            {
                Console.WriteLine(item.Name);
            }

            //===================================================================================================================

            var query4 = context.Videos.Select(c => new
            {
                MovieName = c.Name,
                Genre=c.Genre.Name
            }).OrderBy(c=>c.MovieName);
            Console.WriteLine("\nAll movies projected into an anonymous type with two properties:MovieName and Genre\n");
            foreach (var item in query4)
            {
                Console.WriteLine(item.MovieName);
            }

            //====================================================================================================================

            var query5 = context.Videos.GroupBy(c => c.Classification)
                .Select(d => new
                {
                    Classification = d.Key.ToString(),
                    Video = d.OrderBy(c => c.Name)
                });

            Console.WriteLine("All movies grouped by their classification:");
            foreach (var item in query5)
            {
                Console.WriteLine($" classification:{item.Classification}");
                foreach (var i in item.Video)
                {
                    Console.WriteLine(i.Name);
                }
            }
            //====================================================================================================================

            var query6 = context.Videos.GroupBy(c => c.Classification).Select(x => new
            {
                Classification = x.Key.ToString(),
                count = x.Count()

            }).OrderBy(p=>p.Classification);
            Console.WriteLine("List of classifications sorted alphabetically and count of videos in them");
            foreach (var item in query6)
            {
                Console.WriteLine("{0}({1})",item.Classification,item.count);
            }
            //=====================================================================================================================

            var query7 = context.Genres.GroupJoin(context.Videos, o => o.Id, i => i.GenreId, (x, y) => new
            {
                name=x.Name,
                videocount=y.Count()
               
            });

            foreach (var item in query7)
            {
                Console.WriteLine("{0}({1})", item.name, item.videocount);
            }
            Console.ReadKey();
        }

    }
}
