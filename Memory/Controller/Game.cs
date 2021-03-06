﻿using System;
using System.Windows;
using System.Windows.Controls;

using Memory.Model;
using Memory.View;
using System.Diagnostics;
using System.Windows.Threading;
using System.IO;

namespace Memory
{
    class Game
    {
        // Attributes

        private Card firstCardSelected; // The first selected card this turn

        private Board board; // The board with all the cards

        private Display display; // The GUI

        // Accessors/Mutators

        public Board GetBoard() => board;


        // Constructors

        /// <summary>
        /// Creates a new game.
        /// </summary>
        /// <param name="X">Number of colons of cards.</param>
        /// <param name="Y">Number of lines of cards.</param>
        public Game(Display display, int X, int Y)
        {
            this.display = display;
            board = new Board(X, Y);
        }

        // Methods

        /// <summary>
        /// Method to be executed when a card is chosen.
        /// </summary>
        /// <param name="sender">The selected image.</param>
        /// <param name="e">The event arguments.</param>
        public void CardChosen(object sender, EventArgs e)
        {
            Window dialog = new Window();
            dialog.Title = "Failure!";
            dialog.SizeToContent = SizeToContent.WidthAndHeight;
            dialog.Left = 1000;
            TextBox failure = new TextBox();
            failure.Text = "Wrong card :(";
            failure.Height = 200;
            failure.Width = 200;
            failure.HorizontalContentAlignment = HorizontalAlignment.Center;
            failure.VerticalContentAlignment = VerticalAlignment.Center;
            dialog.Content = failure;

            Card card = (Card)((Image)sender).Tag; // The card which has been selected

            if (card.GetIsDisplayed()) // A displayed card cannot be chosen
            {
                
            }
            else if (firstCardSelected == null) // If it is the first selected card
            {
                firstCardSelected = card;
                card.SetIsDisplayed(true); // The first card will be displayed while the second is chosen
                display.PrintBoard();
            }
            else // If it is the second selected card
            {
                if (card.GetCardPaired() != firstCardSelected) // The two cards are paired
                {
                    card.SetIsDisplayed(true);
                    display.PrintBoard();

                    dialog.ShowDialog();
                    //System.Threading.Thread.Sleep(2000); // Wait for 2 seconds: the player can see both cards he selected
                    card.SetIsDisplayed(false); // Then the cards are hidden again
                    firstCardSelected.SetIsDisplayed(false);
                    display.PrintBoard();
                }
                else // The two cards do not match
                {
                    card.SetIsFound(true); // The pair is found
                    firstCardSelected.SetIsFound(true);
                    display.PrintBoard();
                }

                board.IncrementTurns(); // Increments the turns counter
                firstCardSelected = null; // Reinitialize the selected card

                NextTurn();
            }
        }


        /// <summary>
        /// Go to the next turn by incrementing the counter and refreshing the display.
        /// </summary>
        public void NextTurn()
        {
            if (IsFinished())
            {
                display.PrintTurns();
            }
            else
            {
                display.PrintBoard();
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


        /// <summary>
        /// Exits the game.
        /// </summary>
        public void Exit(object sender)
        {

        }
    }
}
