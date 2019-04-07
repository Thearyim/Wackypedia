using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wackypedia
{
    public class Wackyfier
    {
        private static List<string> wackyReplacements = new List<string>
        {
            "Comblobulator",
            "Syllagisms",
            "Muckety-muck-muck",
            "Shazaam-o-gram",
            "Chewbacca",
            "Jupiter",
            "SillyBilly"
        };

        private static Random randomGen = new Random();

        public string MakeWacky(string text)
        {
            string wackyText = this.ApplyMadlibs(text);
            return wackyText;
        }

        private string ApplyMadlibs(string text)
        {
            string[] words = text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int numberOfMadLibs = words.Length / 8;
            for (int madLibCount = 0; madLibCount <= numberOfMadLibs; madLibCount++)
            {
                // Get a random index in the 'words' array.
                int randomWordIndex = Wackyfier.randomGen.Next(0, words.Length - 1);

                // Get a random index in the madlibs/wacky words array.
                int randomMadLibIndex = Wackyfier.randomGen.Next(0, Wackyfier.wackyReplacements.Count);

                // Set the word in the 'words' array to the 'madlib' word.
                words[randomWordIndex] = Wackyfier.wackyReplacements[randomMadLibIndex];
            }

            return string.Join(" ", words);
        }
    }
}
