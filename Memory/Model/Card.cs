namespace Memory.Model
{
    class Card
    {
        // Attributes
        
        private Card cardPaired; // The ID of the other card in the pair
        private int pair; // The id of the pair
        private bool isDisplayed; // Whether the card is currently displayed
        private bool isFound; // Whether this card's pair has been found


        // Accessors/Mutators

        public Card GetCardPaired() => cardPaired;
        public Card SetCardPaired(Card cardPaired) => this.cardPaired = cardPaired;

        public int GetPair() => pair;
        public void SetPair(int pair) => this.pair = pair;

        public bool GetIsDisplayed() => isDisplayed;
        public void SetIsDisplayed(bool isDisplayed) => this.isDisplayed = isDisplayed;

        public bool GetIsFound() => isDisplayed;
        public void SetIsFound(bool isFound) => this.isFound = isFound;


        // Constructors
        
        /// <summary>
        /// Creates one card.
        /// </summary>
        /// <param name="pair">The symbol to display for this card.</param>
        public Card(int pair)
        {
            this.pair = pair;
            isDisplayed = false;
            isFound = false;
        }


        // Methods

    }
}
