using System;
using System.Collections.Generic;
using PlayerManager;
using DeckManager;
using CardManager;
using GameManager;

namespace BlackJack {
public class Program
{
    public static void Main(){
        BlackJack.Program.GameSetup();
    }


    public static void GameSetup()
    {
        List<Player> gamePlayers = new List<Player>();
        Console.WriteLine("How many players would like to play? [1-4]");
        string numOfPlayers = Console.ReadLine();
        int players = Convert.ToInt32(numOfPlayers);
        DeckManager.Deck gameDeck = new Deck ();
        gameDeck.Shuffle();
        //Create players based on the input of the readline
        for (int i = 1; i <= players; i++) {
            Console.Write("Please Enter A Name For Player " + i +"-");
            PlayerManager.Player playerInstance = new Player();
            gamePlayers.Add(playerInstance);
        }
        GamePlay(gamePlayers, gameDeck);
    }
    public static void GamePlay(List<Player> gamePlayers, Deck gameDeck) {

        int[] turns =  {1,2,3,4};
        for (int i = 0; i < turns.Length; i++)
        {
           System.Console.WriteLine("New Round Started"); 
           
           foreach (Player gamePlayer in gamePlayers){
            int cardCount = gamePlayer.hand.Count;
            gamePlayer.hand.RemoveRange(0,cardCount);
            gamePlayer.Busted = false;
            gameDeck.Deal(gamePlayer);
            gameDeck.Deal(gamePlayer);
            }
           foreach (Player gamePlayer in gamePlayers)
        {
            gamePlayer.showHand();
            System.Console.WriteLine("Your current hand value is " + gamePlayer.evalHand().ToString());
            bool doneWithHand = false;
            while (doneWithHand == false && gamePlayer.Busted == false && gamePlayer.isBlackjack == false) {
                doneWithHand = gamePlayer.playerChoice(gameDeck);
            }
        }
    }
        GameSetup();
        }
    }
}