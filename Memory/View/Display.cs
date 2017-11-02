using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Memory.Model;


namespace Memory.View
{
    class Display
    {
        // Attributes

        private Board board;
        private Game game;

        private MainWindow mainWindow;

        // Accessors/Mutators

        // Constructors

        /// <summary>
        /// Creates a graphical interface for the game.
        /// </summary>
        /// <param name="board">The board used in the game.</param>
        /// <param name="game">The game.</param>
        public Display(Board board, Game game)
        {
            this.board = board;
            this.game = game;

            mainWindow = new MainWindow();
            mainWindow.ShowDialog();
        }

        // Methods

        /// <summary>
        /// Updates the display.
        /// </summary>
        public void PrintBoard()
        {
            //Card[][] cards = board.GetCards(); // The cards to display

            //Console.Clear(); // Hide the previous prints

            //Console.WriteLine($"Turn {board.GetTurns()}");
            //Console.WriteLine("Here's the current board:\n");
            //string abscissa = "  "; // Display the abscissa axis above the board
            //for (int x = 1; x <= board.GetX(); x++)
            //{
            //    abscissa += x.ToString() + " ";
            //}
            //Console.WriteLine(abscissa + "\n");

            //for (int y = 0; y < board.GetY(); y++) // Display the board
            //{
            //    Console.Write((y + 1).ToString() + " "); // Ordinate axis on the left
            //    for (int x = 0; x < board.GetX(); x++)
            //    {
            //        if (cards[y][x].GetIsDisplayed())
            //        {
            //            Console.Write(cards[y][x].GetSymbol() + " ");
            //        }
            //        else
            //        {
            //            Console.Write("* ");
            //        }
            //    }
            //    Console.Write("\n\n");
            //}

            //Console.Write("\n\n");
        }

        /// <summary>
        /// Asks the player to choose a card.
        /// </summary>
        /// <returns>The choosen card.</returns>
        //public Card WaitPlayer()
        //{
            //Console.WriteLine("Which card do you chose?");
            //bool entryCorrect = false;
            //int x = 0;
            //int y = 0;
            //while (!entryCorrect) // Read the colon number, try again until a correct input is entered
            //{
            //    Console.Write("Colon: ");
            //    entryCorrect = Int32.TryParse(Console.ReadLine(), out x); // Check whether the input is an int
            //    if (x < 1 || x > board.GetX()) // Check whether the input is in the right range
            //    {
            //        entryCorrect = false;
            //    }
            //}
            //entryCorrect = false;
            //while (!entryCorrect) // Read the line number, try again until a correct input is entered
            //{
            //    Console.Write("Line: ");
            //    entryCorrect = Int32.TryParse(Console.ReadLine(), out y);
            //    if (y < 1 || x > board.GetY()) // Check whether the input is in the right range
            //    {
            //        entryCorrect = false;
            //    }
            //}

            //Console.Write("\n");

            //return board.GetCards()[y - 1][x - 1];
        //}

        public void PrintTurns()
        {
            //float pairNumber = (float)(board.GetX() * board.GetY()) / (float)2;
            //int proportionCorrect = (int)((float)(100 * board.GetX() * board.GetY()) / (float)(2 * board.GetTurns()));
            //Console.WriteLine($"\nGame finished! It took you {board.GetTurns()} turns to find the {pairNumber} pairs!");
            //Console.WriteLine($"Proportion of correct guesses: {proportionCorrect}%");
            //Console.WriteLine("\nPush a key to end.");
            //Console.ReadLine();
        }
    }
}
