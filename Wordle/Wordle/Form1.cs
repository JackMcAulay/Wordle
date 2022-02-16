using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace Wordle
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int onRow = 0;
        string guess, word;
        bool solved = false;

        Button[,] grid;

        private void btnGuess_Click(object sender, EventArgs e)
        {
            guess = (tbGuess.Text).ToLower();
            tbGuess.Text = "";

            if (guess.Length == 5)
            {
                if (onRow == 0)
                {
                    //Limited selection of words for testing
                    genWord();

                    //Get a 5 letter word from a Random word API
                    //Can be slow
                    //genWordAPI();

                    setupGrid();
                }
                check();
                onRow++;
            }
            else
            {
                MessageBox.Show("Enter a 5 letter Word");
            }

            if (solved)
            {
                MessageBox.Show("You Win");
                this.Close();
            }

            if (onRow == 6)
            {
                MessageBox.Show("You Lost");
                this.Close();
            }
        }

        private void genWord()
        {
            string[] words = { "peace", "about", "straw", "tower", "catch" };
            var rand = new Random();
            word = words[rand.Next(0,4)];

            Console.Write(word);
        }

        private void genWordAPI()
        {
            string genWord;
            bool wordFound = false;

            while (!wordFound)
            {
                WebRequest request = HttpWebRequest.Create("https://random-word-api.herokuapp.com/word");
                WebResponse response = request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());

                genWord = reader.ReadToEnd();
                genWord = genWord.Remove(0, 2);
                genWord = genWord.Remove(genWord.Length - 2);

                Console.WriteLine(genWord);
                Console.WriteLine(genWord.Length);

                if (genWord.Length == 5)
                {
                    word = genWord;
                    wordFound = true;
                }
            }
            Console.WriteLine(word);
        }

        private void setupGrid()
        {
            grid = new Button[,] { { grid00, grid01, grid02, grid03, grid04 },
                { grid10, grid11, grid12, grid13, grid14 },
                { grid20, grid21, grid22, grid23, grid24 },
                { grid30, grid31, grid32, grid33, grid34 },
                { grid40, grid41, grid42, grid43, grid44 },
                { grid50, grid51, grid52, grid53, grid54 }
                };
        }

        private void check()
        {
            solved = true;

            for (int i = 0; i < 5; i++)
            {
                grid[onRow, i].Text = (guess[i]).ToString().ToUpper();

                if (guess[i] == word[i])
                {
                    grid[onRow, i].BackColor = Color.LimeGreen;
                }
                else
                {
                    solved = false;
                    char c = guess[i];

                    if (word.Contains(c))
                    {
                        grid[onRow, i].BackColor = Color.Orange;
                    }
                    else
                    {
                        grid[onRow, i].BackColor = Color.Red;
                    }
                }
            }
        }
    }
}
