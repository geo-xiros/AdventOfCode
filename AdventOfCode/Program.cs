using System;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(new Day1());
            //Console.WriteLine(new Day2());
            //Console.WriteLine(new Day3());
            //Console.WriteLine(new Day4());
            //Console.WriteLine(new Day5());
            //Console.WriteLine(new Day6());
            Console.WriteLine(new Day8());
            //var x = new Day8();
            Console.ReadKey();
        }

    }
    public class Day8
    {
        private long _answer1;
        private long _answer2;
        public Day8()
        {
            var image = File.ReadAllText("..\\..\\input8.txt");

            _answer1 = ImageLayers(image, 25, 6)
               .OrderBy(layer => CountOf(layer, '0'))
               .Take(1)
               .Select(layer => CountOf(layer, '1') * CountOf(layer, '2'))
               .FirstOrDefault();
        }
        
        private int CountOf(string str, char charDigit)
        {
            return str.Count(c => c.Equals(charDigit));
        }

        private IEnumerable<string> ImageLayers(string image, int imageWidth, int imageHeight)
        {
            int layerSize = imageWidth * imageHeight;

            for (var index = 0; index < image.Length - 1; index += layerSize)
            {
                yield return image.Substring(index, layerSize);
            }
        }
        
        public override string ToString()
        {
            return $"{this.GetType().Name} => Answer A:{_answer1}, Answer B:{_answer2}";
        }
    }
}