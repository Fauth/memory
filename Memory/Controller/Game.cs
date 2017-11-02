using System;

using Memory.Model;
using Memory.View;


namespace Memory
{
    class Game
    {
        // Attributes

        private Card firstCardSelected; // The first selected card this turn

        private Board board; // The board with all the cards

        private CommandLine console; // The CLI interface

        // Accessors/Mutators


        // Constructors

        /// <summary>
        /// Creates a new game.
        /// </summary>
        /// <param name="X">Number of colons of cards.</param>
        /// <param name="Y">Number of lines of cards.</param>
        public Game(int X, int Y)
        {
            board = new Board(X, Y);
            console = new CommandLine(board, this);

            NextTurn();
        }

        // Methods

        /// <summary>
        /// Method to be executed when a card is chosen.
        /// </summary>
        /// <param name="cardId">The selected card.</param>
        public void CardChosen(Card card)
        {
            if (card.GetIsDisplayed()) // A displayed card cannot be chosen
            {
                Console.Write("Card already displayed! Please choose another:\n\n");
                CardChosen(console.WaitPlayer());
            }
            else if (firstCardSelected == null) // If it is the first selected card
            {
                firstCardSelected = card;
                card.SetIsDisplayed(true); // The first card will be displayed while the second is chosen
                console.PrintBoard();
                CardChosen(console.WaitPlayer()); // Wait for the second card
            }
            else // If it is the second selected card
            {
                if (card.GetCardPaired() == firstCardSelected) // The two cards are paired
                {
                    card.SetIsFound(true); // The pair is found
                    firstCardSelected.SetIsFound(true);
                    card.SetIsDisplayed(true);
                    console.PrintBoard();
                }
                else // The two cards do not match
                {
                    card.SetIsDisplayed(true);
                    console.PrintBoard();
                    System.Threading.Thread.Sleep(2000); // Wait for 2 seconds: the player can see both cards he selected
                    card.SetIsDisplayed(false); // Then the cards are hidden again
                    firstCardSelected.SetIsDisplayed(false);
                }
                
                board.IncrementTurns(); // Increments the turns counter
                firstCardSelected = null; // Reinitialize the selected card

                NextTurn();
            }
        }

        public void NextTurn()
        {
            if (IsFinished())
            {
                console.PrintTurns();
            }
            else
            {
                console.PrintBoard();
                CardChosen(console.WaitPlayer());
            }

        }

        /// <summary>
        /// Checks whether two cards are paired.
        /// </summary>
        /// <param name="card1">The first card which must be checked.</param>
        /// <param name="card2">The second card which must be checked.</param>
        /// <returns>Returns true if the two cards are paired, false otherwise.</returns>
        private bool CheckPair(Card card1, Card card2) => (card1.GetCardPaired() == card2);

        /// <summary>
        /// Checks whether the game is finished or not.
        /// </summary>
        /// <returns>Returns true if the game is finished, false otherwise.</returns>
        private bool IsFinished()
        {
            foreach (Card[] line in board.GetCards())
            {
                foreach (Card card in line)
                {
                    if (!card.GetIsFound()) return false;
                }
            }
            return true;
        }
    }
}
