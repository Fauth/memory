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
        private Image[][] images;

        private BitmapImage[] symbols; // Pictures to show on the cards
        BitmapImage hidden; // Picture to show on hidden cards

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
            symbols = new BitmapImage[filePaths.Length]; // An array containing all the images
            for (int i = 0;  i < filePaths.Length && i < number; i ++)
            {
                symbols[i] = new BitmapImage();
                symbols[i].BeginInit();
                symbols[i].UriSource = new Uri(filePaths[i], UriKind.Relative);
                symbols[i].CacheOption = BitmapCacheOption.OnLoad;
                symbols[i].EndInit();
            }

            hidden = new BitmapImage();
            hidden.BeginInit();
            hidden.UriSource = new Uri(@"./Resources/hidden.png", UriKind.Relative);
            hidden.CacheOption = BitmapCacheOption.OnLoad;
            hidden.EndInit();
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

            images = new Image[board.GetY()][];
            for (int y = 0; y < board.GetY(); y++)
            {
                images[y] = new Image[board.GetX()];
            }
            cards = board.GetCards(); // The cards to display

            for (int x = 0; x < board.GetX(); x++) // Creates the cards
            {
                for (int y = 0; y < board.GetY(); y++)
                {
                    images[y][x] = CreateCard(mainWindow.cardGrid, x, y, CARD_X, CARD_Y, marginLeft: 10, marginTop: 10, marginRight: 10, marginBottom: 10);
                    images[y][x].Tag = cards[y][x];
                    images[y][x].MouseLeftButtonUp += game.CardChosen;
                }
            }
        }


        /// <summary>
        /// Updates the display.
        /// </summary>
        public void PrintBoard()
        {
            cards = board.GetCards(); // The cards to display

            for (int x = 0; x < board.GetX(); x++)
            {
                for (int y = 0; y < board.GetY(); y++)
                {
                    Image image = images[y][x];
                    Card card = cards[y][x];

                    if (card.GetIsFound())
                    {
                        image.Source = symbols[card.GetPair()];
                        image.Opacity = 0.7;
                    }
                    else if (card.GetIsDisplayed())
                    {
                        image.Source = symbols[card.GetPair()];
                        image.Opacity = 1;
                    }
                    else
                    {
                        image.Source = hidden;
                        image.Opacity = 1;
                    }
                }                   
            }
        }

        /// <summary>
        /// Creates an image
        /// </summary>
        /// <param name="panel">The parent to which add this image</param>
        /// <param name="x">The image column</param>
        /// <param name="y">The image row</param>
        /// <param name="width">The image width</param>
        /// <param name="height">The image height</param>
        /// <param name="marginLeft">The margin to keep at the left of the image. Optional</param>
        /// <param name="marginTop">The margin to keep at the top of the image. Optional</param>
        /// <param name="marginRight">The margin to keep at the right of the image. Optional</param>
        /// <param name="marginBottom">The margin to keep at the bottom of the image. Optional</param>
        /// <returns>The created image</returns>
        public Image CreateCard(Panel panel, int x, int y, int width, int height, int marginLeft = 0, int marginTop = 0, int marginRight = 0, int marginBottom = 0)
        {
            Image card = new Image();
            Grid.SetRow(card, y);
            Grid.SetColumn(card, x);
            card.Margin = new Thickness(marginLeft, marginTop, marginRight, marginBottom);
            card.Height = height;
            card.Width = width;
            panel.Children.Add(element: card);

            return card;
        }

        public void PrintTurns()
        {
            Window dialog = new Window();
            dialog.Title = "Game finished!";
            dialog.SizeToContent = SizeToContent.WidthAndHeight;
            //dialog.Closing += game.Exit();

            TextBox failure = new TextBox();
            failure.Margin = new Thickness(20);
            float pairNumber = (float)(board.GetX() * board.GetY()) / (float)2;
            int proportionCorrect = (int)((float)(100 * board.GetX() * board.GetY()) / (float)(2 * board.GetTurns()));
            string turns = $"\nGame finished! It took you {board.GetTurns()} turns to find the {pairNumber} pairs!";
            string correct = $"Proportion of correct guesses: {proportionCorrect}%";
            string end = "Close to end";
            failure.Text = turns + "\n" + correct + "\n\n" + end;
            //failure.Height = 200;
            //failure.Width = 200;
            failure.HorizontalContentAlignment = HorizontalAlignment.Center;
            failure.VerticalContentAlignment = VerticalAlignment.Center;
            dialog.Content = failure;

            dialog.ShowDialog();
            
        }
    }
}
