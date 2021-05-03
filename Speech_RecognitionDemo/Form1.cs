using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.Speech.Recognition;
using System.Threading;
using System.Diagnostics;

namespace Speech_RecognitionDemo
{
    public partial class Form1 : Form
    {
        SpeechSynthesizer ss = new SpeechSynthesizer();
        PromptBuilder pb = new PromptBuilder();
        SpeechRecognitionEngine sre = new SpeechRecognitionEngine();
        Choices clist = new Choices();
        public Form1()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {

            
            btnStart.Enabled = false;
            btnStop.Enabled = true;
            clist.Add( new string[] { "hello", "How are you?"});
            Grammar gr = new Grammar(new GrammarBuilder(clist));
            try
            {
                sre.RequestRecognizerUpdate();
                sre.LoadGrammar(gr);
                sre.SpeechRecognized += sre_SpeechRecognized;
                sre.SetInputToDefaultAudioDevice();
                sre.RecognizeAsync(RecognizeMode.Multiple);



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!");
                
            }
        }

        private void sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            switch (e.Result.Text.ToString())
            {
                case "hello":
                    ss.SpeakAsync("hello Manali!");
                    break;

                case "How are you?":
                    ss.SpeakAsync("I am doing great! Thank you! How about you ");
                    break;

               
                default:
                    txtContents.Text = "Unable to understand";
                    break;

            }
            txtContents.Text += e.Result.Text.ToString() + Environment.NewLine;

        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            sre.RecognizeAsyncStop();
            btnStart.Enabled = true;
            btnStop.Enabled = false;

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
