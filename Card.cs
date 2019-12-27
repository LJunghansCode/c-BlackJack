using System;
using GameManager;

namespace CardManager
 {

public class Card {
    public int numValue;
    public string suit { get; set; }
    public string numerical_value { get {
        if (numValue > 1 && numValue < 11) {
            return numValue.ToString();
        }
        else if (numValue == 11) {
            return "Jack";
        }
        else if (numValue == 12) {
            return "Queen";
        }
        else if (numValue == 13) {
            return "King";
        }
        else {
            return "Ace";
        }
    }
}

    public Card(int numval, string suitval) {
        suit = suitval;
        numValue = numval;
        
        }
    public override string ToString() {
        return numerical_value + " of " + suit;
    }
    }
}