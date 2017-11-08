using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Diagnostics;

using Memory.Model;


namespace Memory.View
{
    class Display
    {
        // Attributes
        private Game game;
        private Board board;
        private MainWindow mainWindow;
        public const int CARD_X = 127; // Width (in pixels) of every card
        public const int CARD_Y = 200; // Height (in pixels) of every card

        // Accessors/Mutators

        // Constructors

        /// <summary>
        /// Creates a graphical interface for the game and initializes the game itself
        /// </summary>
        /// <param name="X">The number of columns of cards.</param>
        /// <param name="Y">The number of lines of cards.</param>
        public Display(int X, int Y)
        {
            game = new Game(this, X, Y);
            board = game.GetBoard();
            mainWindow = new MainWindow();
            mainWindow.SizeToContent = SizeToContent.WidthAndHeight;
            mainWindow.Height = 900;
            mainWindow.Width = 900;
            mainWindow.Left = 0; // The startup position is at the top left
            mainWindow.Top = 0;

            LoadImages(); 
            PrintBoard();
            game.NextTurn(); // Wait
            game.NextTurn();
            game.NextTurn();
        }

        // Methods

        /// <summary>
        /// Load all the images present in the ./resources/cards directory 
        /// </summary>
        public void LoadImages()
        {
            string[] filePaths = Directory.GetFiles(@"./Resources/Cards/"); // All available files
            Board.ShuffleArray(filePaths); // Shuffles the list to get different images each game
            BitmapImage[] symbols = new BitmapImage[filePaths.Length]; // An array containing all the images
            for (int i = 0;  i < filePaths.Length; i ++)
            {
                symbols[i] = new BitmapImage(new Uri(filePaths[i], UriKind.Relative));
            }
        }

        /// <summary>
        /// Updates the display.
        /// </summary>
        public void PrintBoard()
        {
            mainWindow.grid.ShowGridLines = true;

            for (int x = 0; x < board.GetX(); x++)
            {
                ColumnDefinition column = new ColumnDefinition();
                mainWindow.grid.ColumnDefinitions.Add(column);
            }
            for (int y = 0; y < board.GetY(); y++)
            {
                RowDefinition row = new RowDefinition();
                mainWindow.grid.RowDefinitions.Add(row);
            }
            //mainWindow.wrapPanel.Margin = new Thickness(20); // 20 pixels margin around the panel: the right and bottom margins will be created by the cards
            //mainWindow.wrapPanel.MaxHeight = board.GetY() * (CARD_Y + 20); // The height of the board
            //mainWindow.wrapPanel.MaxWidth = board.GetX() * (CARD_X + 20); // The width of the board
            //mainWindow.wrapPanel.MaxHeight = 1000;
            //mainWindow.wrapPanel.MaxWidth = 1850;
            

            Card[][] cards = board.GetCards(); // The cards to display

            //ObservableCollection<Button> buttons = new ObservableCollection<Button>();

            for (int x = 0; x < board.GetX(); x++)
            {
                for (int y = 0; y < board.GetY(); y++)
                {
                    //buttons.Add(mainWindow.CreateCard(mainWindow.grid, CARD_X, CARD_Y, marginLeft: 10, marginTop: 10, marginRight: 10, marginBottom: 10, content: i.ToString()));
                    mainWindow.CreateCard(mainWindow.grid, x, y, CARD_X, CARD_Y, marginLeft: 10, marginTop: 10, marginRight: 10, marginBottom: 10, content: "J'en ai MARRE");
                }                
            }



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
