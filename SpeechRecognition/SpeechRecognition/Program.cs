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
        private static bool run = false;
        private static SpeechRecognitionEngine sr = new SpeechRecognitionEngine();
        private static SpeechSynthesizer synth = new SpeechSynthesizer();
        static void Main(string[] args)
        {
            intiSpeech();
            sr.RecognizeAsync(RecognizeMode.Multiple);
            initSpeechSynt();
            Console.ReadLine();
        }

        private static void initSpeechSynt()
        {
            synth.SetOutputToDefaultAudioDevice();
            synth.Speak("Speech Synthesizer enabled");
        }

        private static void intiSpeech()
        {
            Choices colors = new Choices();
            colors.Add(new string[] { "Exit program", "Start", "Stop" });

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
            synth.Speak(e.Result.Text);
            switch (e.Result.Text)
            {
                case "Start":
                    run = true;
                    Task.Factory.StartNew(() => DoSome());
                    break;
                case "Stop":
                    run = false;
                    break;

                case "Exit program":
                    Environment.Exit(0);
                    break;
            }
        }

        private static void DoSome()
        {
            int i = 0;
            while (run)
            {
                Console.WriteLine("Conting.. " + i);
                i++;
                System.Threading.Thread.Sleep(250);
            }
             
        }
    }
}
