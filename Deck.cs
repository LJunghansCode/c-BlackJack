using System;
using System.Collections.Generic;
using CardManager;
using GameManager;
using PlayerManager;

namespace DeckManager {
    public class Deck {
        public List<Card> cards;
        public Deck() {
            Reset();
        }
        public void Reset() {
            cards = new List<Card>();
            string[] suits = new string[4] {"Hearts", "Clubs", "Spades", "Diamonds"};
            for (int i = 0; i < suits.Length; i++) {
                for (int n = 1; n < 14; n++) {
                    cards.Add(new CardManager.Card(n, suits[i]));
                }
            }
        }
        public void Deal(Player player) {
            CardManager.Card toReturn = cards[0];
            cards.RemoveAt(0);
            player.hand.Add(toReturn);
        }
        public void Shuffle() {
            Random rand = new Random(); 
            for(var i = cards.Count -1; i > 0; i--) {
                int randIdx = rand.Next(cards.Count- i);
                CardManager.Card swap = cards[i];
                cards[i] = cards[randIdx];
                cards[randIdx] = swap;

            }
        }
        public override string ToString() {
            string str = "";
            foreach(Card card in cards) {
                str += card + "\n";
            }
            return str;
        }

    }
}