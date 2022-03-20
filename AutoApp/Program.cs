using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoApp
{
    class Program
    {
        static void Main(string[] args)
        {
            AutosEntities db = new AutosEntities();

            var distmake = db.Autos.Select(a => a.Make).Distinct();

            Console.WriteLine("Distinct Makes");
            Console.WriteLine("");
            foreach (string a in distmake)
            {
                Console.WriteLine($"{a}");
            }
            Console.WriteLine("");

            ShowAutos("All autos ordered by year decending:", db.Autos.OrderByDescending(a => a.Year));

            var fords = db.Autos.Where(a => a.Make == "Ford");

            var fordOlder = db.Autos.Where(a => a.Make == "Ford" && a.Year < 2000);

            var fordOver10 = db.Autos.Where(a => a.Make == "Ford" && a.Cost > 10000.00M);

            ShowAutos("All Fords:", fords);

            ShowAutos("Older Fords:", fordOlder);

            ShowAutos("Fords over $10000.00:", fordOver10);

            Console.WriteLine();

            var sumFords = fords.Select(a => a.Cost).Sum();

            Console.WriteLine($"Sum of Cost of all Fords: {sumFords.ToString("C")}");

            var searchVIN = db.Autos.ToList().Where(a => a.VIN == "1234567890123456" );

            if(searchVIN.Any())
            {
                db.Autos.Remove(db.Autos.Where(a => a.Make == "Fusion").FirstOrDefault());
                db.SaveChanges();
            }
            else
            {
                //db.Autos.Add(new Auto() {Year = 2020, Make = "Ford", Model = "Fusion", VIN = "1234567890123456", Cost = 28000}); Kept getting a primary key error
                //db.SaveChanges();
            }

            Console.WriteLine($"Sum of Cost of all Fords: {sumFords.ToString("C")}");

            Console.ReadKey();

            void ShowAutos(string title, IEnumerable<Auto> set)
            {
                Console.WriteLine($"\n {title}");
                Console.WriteLine($" {"Year",-5} {"MakeModel",-25} {"Cost",10} {"Price",10}");
                Console.WriteLine($" {"----",-5} {"---------",-25} {"----",10} {"-----",10}");
                foreach (var a in set)
                {
                    Console.WriteLine($" {a.Year,-5} {a.MakeModel,-25} {a.Cost,10:C} {a.Price,10:C}");
                }

            }

        }
    }
    partial class Auto
    {
        public decimal Price
        {
            get
            {
                return Cost * 0.01M;
            }
        }
        public string MakeModel
        {
            get => $"{Make} {Model}";
        }
    }

}
