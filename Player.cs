using System;
using System.Collections.Generic;
using System.Linq;
using CardManager;
using DeckManager;
using GameManager;
using BlackJack;

namespace PlayerManager {

    public enum Articles
    {
        A,
        AN
    }

    public enum DayOfWeek
    {
        Sunday = 0,
        Monday = 1,
        Tuesday = 2,
        Wednesday = 3,
        Thursday = 4,
        Friday = 5,
        Saturday = 6
    }

    public class Player {
        public string name { get; set; }
        public const string aa = "a";
        public const string an = "an";
        public int winCount = 0;
        public bool Busted = false;
        public bool isBlackjack = false;
        public List<Card> hand = new List<Card>();

        // private Dictionary<DayOfWeek, bool> workingDays = new Dictionary<DayOfWeek, bool>
        // {
        //     [DayOfWeek.Sunday] = false,
        //     [DayOfWeek.Monday] = true,
        //     [DayOfWeek.Tuesday] = true,
        //     [DayOfWeek.Wednesday] = true,
        //     [DayOfWeek.Thursday] = true,
        //     [DayOfWeek.Friday] = true,
        //     [DayOfWeek.Saturday] = false
        // };

        private DayOfWeek[] workingDays = new DayOfWeek[] { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday };

        private HashSet<DayOfWeek> workDays = new HashSet<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday };

        private const int DAYS_IN_WEEK = 7;

        public Player() {
            name = Console.ReadLine();
        }

        public int GetWeekOfYear(int dayOfYear)
        {
            return dayOfYear % DAYS_IN_WEEK;
        }

        public bool IsWorkingDay(DayOfWeek dayOfWeek)
        {
            return this.workingDays.Contains(dayOfWeek);
        }

        public int evalHand()
        {
           if (this.IsWorkingDay(DayOfWeek.Monday))
           {
               // ...
           }

           int totalHand = 0;
           foreach (Card handCard in this.hand)
           {
               if(handCard.numerical_value == "King" || handCard.numerical_value == "Queen"|| handCard.numerical_value == "Jack")
               {
                   handCard.numValue = 10;
               }
               if(handCard.numerical_value == "Ace")
               {  
                   System.Console.WriteLine("Ace can be 1 / 11");
                string input = Console.ReadLine();
                if (input == "1" || input == "11"){
                    int ace =  Convert.ToInt32(input);
                    if (ace == 1){
                        handCard.numValue = 1;
                    }
                    if (ace == 11) {
                        handCard.numValue = 11;
                    }
                }
                else{
                    System.Console.WriteLine("YOU GOT AN ACE! WHAT VALUE DO YOU WANT?? 1 or 11??");
                    evalHand();
                }
                // handCard.numValue = ace;
                showHand();

               }
               totalHand += handCard.numValue;
           }
           return totalHand;
       }

        public void WinHand() {
            this.winCount += 1;
        }
            public bool playerChoice(Deck gameDeck){
                string answer = "";
                if (this.Busted == true) {
                    answer = "stay";
                }
                if (this.isBlackjack == true) {
                    answer = "stay";
                }
                if(answer == "stay") {
                    stay();
                    return true;
                }
                answer = "";
                System.Console.WriteLine("Dealer: Hit or Stay " + this.name);
                answer = Console.ReadLine().ToLower();
                if(answer == "hit") {
                    hit(gameDeck);
                    return false;
                }
                if(answer == "stay") {
                    stay();
                    return true;
                }
                else {
                    Console.WriteLine("Please enter a valid choice :(");
                    playerChoice(gameDeck);
                    return false;
                }
            }
        public void hit(Deck gameDeck)
        {   
            gameDeck.Deal(this);
            this.showHand();
            int handTotal = evalHand();
            if (handTotal > 21) {
                Console.WriteLine("############  -You Busted!- ############");
                Console.WriteLine("     ###### Next Players Turn! #####");
                this.Busted = true;
            }
            if (handTotal == 21) {
                Console.WriteLine("$~$~$~$~$~$~$~$~$~ You got BlackJack! ~$~$~$~$~$~$~$~$~$");
                this.isBlackjack = true;
            }
            else {
                if (this.Busted == true) {
                    stay();
                }
            playerChoice(gameDeck);
            }
        }
        public void stay(){
            int handTotal = 0;
            foreach (Card handCard in this.hand) {
                handTotal += handCard.numValue;
            }
            if (this.Busted == true) {
                System.Console.WriteLine();
            }
            else {
            Console.WriteLine("Your final hand was: " + handTotal);   
            }
            // return true;
        }
        public Card Discard2(int CardIdx) {
            if (hand[CardIdx] != null) {
                Card toReturn = hand[CardIdx];
                hand.RemoveAt(CardIdx);
                return toReturn;
            }
            return null;
        }
        public void Discard(Card card) {
            hand.Remove(card);
        }
        public void showHand(){
            foreach (Card handCard in this.hand)
            {
                Articles article = (handCard.numerical_value == "Ace") ? Articles.AN : (handCard.numerical_value == "8") ? Articles.AN : Articles.A;
                System.Console.WriteLine(this.name + " has " + article.ToString().ToLower() + " " + handCard.numerical_value + " of " + handCard.suit);
            }
        }
        public void resetHand(){
            foreach(Card handCard in this.hand){

                Discard(handCard);
            }
        }
    }
}