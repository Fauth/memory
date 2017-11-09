using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
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
        private MainWindow mainWindow;
        private Game game;
        private Board board;
        private Card[][] cards;
        private Button[][] buttons;

        private ImageBrush[] symbols; // Pictures to show on the cards
        ImageBrush hidden; // Picture to show on hidden cards

        public const int CARD_X = 127; // Width (in pixels) of every card
        public const int CARD_Y = 200; // Height (in pixels) of every card

        // Accessors/Mutators

        // Constructors

        /// <summary>
        /// Creates a graphical interface for the game and initializes the game itself
        /// </summary>
        /// <param name="X">The number of columns of cards.</param>
        /// <param name="Y">The number of lines of cards.</param>
        public Display(MainWindow mainWindow, int X, int Y)
        {
            game = new Game(this, X, Y);
            board = game.GetBoard();
            this.mainWindow = mainWindow;
            mainWindow.SizeToContent = SizeToContent.WidthAndHeight;

            mainWindow.Left = 0; // The startup position is at the top left
            mainWindow.Top = 0;

            LoadImages(X*Y/2);
            CreateBoard();
            PrintBoard();
        }

        // Methods

        /// <summary>
        /// Loads all the images present in the ./resources/cards directory 
        /// <param name="number">The number of images to load</param>
        /// </summary>
        public void LoadImages(int number)
        {
            string[] filePaths = Directory.GetFiles(@"./Resources/Cards/"); // All available files
            Board.ShuffleArray(filePaths); // Shuffles the list to get different images each game
            symbols = new ImageBrush[filePaths.Length]; // An array containing all the images
            for (int i = 0;  i < filePaths.Length && i < number; i ++)
            {
                symbols[i] = new ImageBrush(new BitmapImage(new Uri(filePaths[i], UriKind.Relative)));
            }
            
            hidden = new ImageBrush(new BitmapImage(new Uri(@"./Resources/hidden.png", UriKind.Relative))); 
        }


        /// <summary>
        /// Creates the graphical controls for the board and the cards
        /// </summary>
        public void CreateBoard()
        {
            mainWindow.cardGrid.Margin = new Thickness(10); // Exterior margin

            for (int x = 0; x < board.GetX(); x++) // Create the grid columns
            {
                ColumnDefinition column = new ColumnDefinition();
                mainWindow.cardGrid.ColumnDefinitions.Add(column);
            }
            for (int y = 0; y < board.GetY(); y++) // Create the grid rows
            {
                RowDefinition row = new RowDefinition();
                mainWindow.cardGrid.RowDefinitions.Add(row);
            }

            buttons = new Button[board.GetY()][];
            for (int y = 0; y < board.GetY(); y++)
            {
                buttons[y] = new Button[board.GetX()];
            }
            cards = board.GetCards(); // The cards to display

            for (int x = 0; x < board.GetX(); x++) // Creates the buttons
            {
                for (int y = 0; y < board.GetY(); y++)
                {
                    buttons[y][x] = CreateCard(mainWindow.cardGrid, x, y, CARD_X, CARD_Y, marginLeft: 10, marginTop: 10, marginRight: 10, marginBottom: 10, content: "");
                }
            }
        }


        /// <summary>
        /// Updates the display.
        /// </summary>
        public void PrintBoard()
        {
            cards = board.GetCards(); // The cards to display

            //ObservableCollection<Button> buttons = new ObservableCollection<Button>();

            for (int x = 0; x < board.GetX(); x++)
            {
                for (int y = 0; y < board.GetY(); y++)
                {
                    Button button = buttons[y][x];
                    Card card = cards[y][x];

                    if (card.GetIsDisplayed())
                    {
                        button.Background = symbols[card.GetPair()];
                        if (card.GetIsFound())
                        {
                            button.Opacity = 0.7;
                        }
                    }
                    else
                    {
                        button.Background = hidden;
                    }
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

        /// <summary>
        /// Creates a button
        /// </summary>
        /// <param name="panel">The parent to which add this button</param>
        /// <param name="x">The button column</param>
        /// <param name="y">The button row</param>
        /// <param name="width">The button width</param>
        /// <param name="height">The button height</param>
        /// <param name="content">The text displayed in the button. Optional</param>
        /// <param name="marginLeft">The margin to keep at the left of the button. Optional</param>
        /// <param name="marginTop">The margin to keep at the top of the button. Optional</param>
        /// <param name="marginRight">The margin to keep at the right of the button. Optional</param>
        /// <param name="marginBottom">The margin to keep at the bottom of the button. Optional</param>
        /// <returns>The created button</returns>
        public Button CreateCard(Panel panel, int x, int y, int width, int height, string content = "", int marginLeft = 0, int marginTop = 0, int marginRight = 0, int marginBottom = 0)
        {
            Button card = new Button();
            Grid.SetRow(card, y);
            Grid.SetColumn(card, x);
            card.Margin = new Thickness(marginLeft, marginTop, marginRight, marginBottom);
            card.Height = height;
            card.Width = width;
            card.Content = content;
            panel.Children.Add(element: card);

            return card;
        }

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
