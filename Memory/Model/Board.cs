using System;


namespace Memory.Model
{
    class Board
    {
        // Attributes

        private Card[][] cards; // Array of cards sorted by their position
        private int turns; // The number of turns played
        private int X; // The number of colons of cards
        private int Y; // The number of lines of cards


        // Accessors/Mutators

        public Card[][] GetCards() => cards;

        public int GetTurns() => turns;

        public int GetX() => X;

        public int GetY() => Y;


        // Constructors

        /// <summary>
        /// Creates a new board.
        /// </summary>
        /// <param name="X">Number of colons of cards.</param>
        /// <param name="Y">Number of lines of cards.</param>
        public Board(int X, int Y)
        {
            turns = 0; // Initialization

            this.X = X;
            this.Y = Y;

            // Cards creation
            cards = new Card[Y][]; // The cards are sorted by their position
            for(int i = 0; i < cards.Length; i++)
            {
                cards[i] = new Card[X];
            }

            int pair = 0; // Here the pair number will be an incremented int
            for (int y = 0; y < Y; y++) // Creation of every pair of cards
            {
                for (int x = 0; x < X; x++)
                {
                    string symbol = "symbols[" + pair + "]";
                    cards[y][x] = new Card(symbol); // The first card of the pair.
                    if (x+1 >= X) // If the line is finished the program goes to the next line and takes the first card as the paired card
                    {
                        cards[y + 1][0] = new Card(symbol); // Same symbol as the first card of the pair
                        cards[y + 1][0].SetCardPaired(cards[y][x]);
                        cards[y][x].SetCardPaired(cards[y + 1][0]);
                        y++;
                        x = 0;
                    }
                    else // If the line is not finished the program takes the next card in the line as the paired card
                    {
                        cards[y][x + 1] = new Card(symbol);
                        cards[y][x + 1].SetCardPaired(cards[y][x]);
                        cards[y][x].SetCardPaired(cards[y][x + 1]);
                        x++;
                    }
                    pair++; // The symbol is incremented, as it must be unique to each pair
                }
            }

            Card[] arrayCards = new Card[X*Y]; // Transform the bidimensional Card[][] cards to a unidimensional array, easier to shuffle
            int z = 0;
            for (int x = 0; x < X; x++) // Copy the cards into arrayCards
            {
                for (int y = 0; y < Y; y++)
                {
                    arrayCards[z] = cards[y][x];
                    z++;
                }
            }

            ShuffleArray(arrayCards); // Shuffle the array of cards

            int k = 0;
            for (int x = 0; x < X; x++) // Relocate the shuffled cards in the bidimensional array
            {
                for (int y = 0; y < Y; y++)
                {
                    cards[y][x] = arrayCards[k];
                    k++;
                }
            }
            CheckSymbols(); // Check whether every pair shares the same symbol
        }


        // Methods

        /// <summary>
        /// Adds one turn to the counter.
        /// </summary>
        public void IncrementTurns() => turns++;

        /// <summary>
        /// Shuffles an array of any objects.
        /// </summary>
        /// <param name="array">The array to shuffle.</param>
        public static void ShuffleArray(object[] array)
        {
            Random random = new Random(); // Generates random numbers
            for (int i = 0; i < array.Length; i++)
            {
                object temp = array[i]; // Each element is switched with one of those following
                int randomIndex = random.Next(i, array.Length);
                array[i] = array[randomIndex];
                array[randomIndex] = temp;
            }
        }

        /// <summary>
        /// Checks whether each paired cards have the same symbols. If not, correct that.
        /// </summary>
        private void CheckSymbols()
        {
            foreach (Card[] line in cards)
            {
                foreach (Card card in line)
                {
                    if ((card.GetCardPaired()).GetSymbol() != card.GetSymbol())
                    {
                        card.SetSymbol(card.GetCardPaired().GetSymbol());
                    }
                }                    
            }
        }
    }
}
