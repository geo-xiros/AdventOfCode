using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class Day8
    {
        private long _answer1;
        private string _answer2;
        public Day8()
        {
            var image = File.ReadAllText("..\\..\\inputs\\input8.txt");

            _answer1 = ImageLayers(image, 25, 6)
               .OrderBy(layer => CountOf(layer, '0'))
               .Take(1)
               .Select(layer => CountOf(layer, '1') * CountOf(layer, '2'))
               .FirstOrDefault();

            var mergedImage = ImageLayers(image, 25, 6)
                .Aggregate((p, c) => MergeLayers(p, c));
            
            _answer2 = PrintImage(mergedImage, 25);

        }

        private string MergeLayers(string previous, string current)
        {
            return string.Join("", 
                previous.Zip(current, (p, c) => DecodePixel(p, c).ToString()));
        }

        private char DecodePixel(char previous, char current)
        {
            if (previous == '2')
            {
                return current;
            }
            return previous;
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
        private string PrintImage(string image, int imageWidth)
        {
            StringBuilder sb = new StringBuilder();
            for (var index = 0; index < image.Length - 1; index += imageWidth)
            {
                var line = image.Substring(index, imageWidth);
                line = line.Replace("0", " ");
                line = line.Replace("1", "O");
                sb.AppendLine(line);
            }
            return sb.ToString();
        }

        public override string ToString()
        {
            return $"{this.GetType().Name}\nAnswer A:{_answer1}\nAnswer B:\n{_answer2}";

        }
    }
}