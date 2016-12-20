using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Recognition;
using System.Speech.Synthesis;

namespace SpeechRecognition
{

    class Program
    {
        
        static void Main(string[] args)
        {
            SpeechRecognitionEngine sr = new SpeechRecognitionEngine();
            intiSpeetch(sr);
            sr.RecognizeAsync(RecognizeMode.Multiple);
            Console.ReadLine();
        }

        private static void intiSpeetch(SpeechRecognitionEngine sr)
        {
            Choices colors = new Choices();
            colors.Add(new string[] { "start", "stop", "system" });

            GrammarBuilder gb = new GrammarBuilder();
            gb.Append(colors);

            // Create the Grammar instance.
            Grammar g = new Grammar(gb);
            sr.LoadGrammarAsync(g);
            sr.SetInputToDefaultAudioDevice();
            sr.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(sr_SpeechRecognized);
        }

        private static void sr_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            Console.WriteLine("You said to me: " + e.Result.Text);
        }
    }
}
