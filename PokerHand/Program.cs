using System;
using System.Linq;
/// <summary>
/// Date : 11 July 2020
/// @Author: Jim Chacko
/// </summary>
namespace PokerHand
{
    class Program
    {
        static string cardValue = "23456789TJQKA";

        static void Main(string[] args)
        {
            int firstPlayerCounter = 0;
            int secondPlayerCounter = 0;
            string line;

            Console.WriteLine("This is the program");
            if (args == null || args.Length == 0)
            {
                Console.WriteLine("********** ERROR **** ");
                Console.WriteLine("The file name is not  provided ");
                Console.WriteLine("Please run  in following format  ");
                Console.WriteLine();
                Console.WriteLine("program poker-hands.txt");
                Console.WriteLine("**********  **** ");
                Console.ReadKey();
                return;
            }
            try
            {
                System.IO.StreamReader file = new System.IO.StreamReader(args[0]);
                while ((line = file.ReadLine()) != null)
                {
                    string inputline = line;

                    if (line.Trim().Length != 29)
                    {
                        Console.WriteLine(" Invalid line , Igroning ");
                        continue;
                    }
                    string[] words = line.Split(' ');
                    string firsthand = "";
                    for (int i = 0; i < 5; i++)
                    {
                        firsthand += words[i];
                    }
                    string secondHand = "";

                    for (int i = 5; i < 10; i++)
                    {
                        secondHand += words[i];
                    }

                    //Rank 10 : Royal Fush 
                    int result = CheckRoyalFlush(firsthand, secondHand);
                    if (result > 0)
                    {
                        Console.WriteLine(inputline + "  (Rank 10) Winner : " + result);
                        if (result == 1)
                            firstPlayerCounter++;
                        else if (result == 2)
                            secondPlayerCounter++;
                        continue;
                    }

                    //Rank 9: Four of a kind
                    result = CheckStraightFlush(firsthand, secondHand);
                    if (result > 0)
                    {
                        Console.WriteLine(inputline + "  (Rank 9) Winner : " + result);
                        if (result == 1)
                            firstPlayerCounter++;

                        else if (result == 2)
                            secondPlayerCounter++;
                        continue;
                    }

                    //Rank 8: Four of a kind
                    result = CheckfourofaKind(firsthand, secondHand);
                    if (result > 0)
                    {
                        Console.WriteLine(inputline + "  (Rank 8) Winner : " + result);
                        if (result == 1)
                            firstPlayerCounter++;
                        else if (result == 2)
                            secondPlayerCounter++;
                        continue;
                    }
                    //Rank 7: Full House
                    result = CheckforFullHouse(firsthand, secondHand);
                    if (result > 0)
                    {
                        Console.WriteLine(inputline + "  (Rank 7) Winner : " + result);
                        if (result == 1)
                            firstPlayerCounter++;
                        else if (result == 2)
                            secondPlayerCounter++;
                        continue;
                    }

                    //Rank 6 : Flush
                    result = CheckForFlush6(firsthand, secondHand);
                    if (result > 0)
                    {
                        Console.WriteLine(inputline + "  (Rank 6) Winner : " + result);
                        if (result == 1)
                            firstPlayerCounter++;
                        else if (result == 2)
                            secondPlayerCounter++;
                        continue;
                    }

                    //Rank 5 : Straight
                    result = CheckStraight(firsthand, secondHand);
                    if (result > 0)
                    {
                        Console.WriteLine(inputline + "  (Rank 5) Winner : " + result);
                        if (result == 1)
                            firstPlayerCounter++;
                        else if (result == 2)
                            secondPlayerCounter++;
                        continue;
                    }

                    //Rank4 : three Pair
                    result = CheckThreePairRank4(firsthand, secondHand);
                    if (result > 0)
                    {
                        Console.WriteLine(inputline + "  (Rank 4) Winner : " + result);
                        if (result == 1)
                            firstPlayerCounter++;
                        else if (result == 2)
                            secondPlayerCounter++;
                        continue;
                    }

                    //Rank3 : Two Pair
                    result = CheckTwoPairRank3(firsthand, secondHand);
                    if (result > 0)
                    {
                        Console.WriteLine(inputline + "  (Rank 3) Winner : " + result);
                        if (result == 1)
                            firstPlayerCounter++;
                        else if (result == 2)
                            secondPlayerCounter++;

                        continue;
                    }

                    //Rank 2 : Pair
                    result = CheckPairCard(firsthand, secondHand);
                    if (result > 0)
                    {
                        Console.WriteLine(inputline + "  (Rank 2) Winner : " + result);
                        if (result == 1)
                            firstPlayerCounter++;
                        else if (result == 2)
                            secondPlayerCounter++;
                        continue;
                    }

                    //Rank 1  : High Card
                    result = CheckHighCard(firsthand, secondHand);
                    {
                        if (result > 0)
                        {
                            Console.WriteLine(inputline + "  (Rank 1) Winner : " + result);
                            if (result == 1)
                                firstPlayerCounter++;
                            else if (result == 2)
                                secondPlayerCounter++;
                            continue;
                        }
                    }

                    Console.WriteLine(" Not expected to run ");
                }
                Console.WriteLine("*************Result**************");
                Console.WriteLine("********** Player 1: " + firstPlayerCounter);
                Console.WriteLine("********** Player 2: " + secondPlayerCounter);
                Console.WriteLine("**********************************");
                Console.WriteLine("*Please press any key to exit*");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error " + ex);
            }

        }
        #region Rank 10

        /// <summary>
        /// Check if ther is Royal Flush
        ///  Rank 10
        /// </summary>
        /// <param name="player1cards"></param>
        /// <param name="player2cards"></param>
        /// <returns></returns>
        private static int CheckRoyalFlush(string player1cards, string player2cards)
        {
            bool player1HasVal = false;
            bool player2HasVal = false;
            string firstpalyercardSuit = CardofSameSuit(player1cards, 5);
            string secondpalyercardSuit = CardofSameSuit(player2cards, 5);
            if (firstpalyercardSuit.Trim().Length == 1 && (player1cards.Contains("T") && player1cards.Contains("J") && player1cards.Contains("Q") && player1cards.Contains("K") && player1cards.Contains("A")))
            {
                player1HasVal = true;
            }
            if (secondpalyercardSuit.Trim().Length == 1 && (player2cards.Contains("T") && player2cards.Contains("J") && player2cards.Contains("Q") && player2cards.Contains("K") && player2cards.Contains("A")))
            {
                player2HasVal = true;
            }
            if (player1HasVal || player2HasVal)
            {
                if (!player2HasVal)
                {
                    Console.WriteLine("Royal Flush for Player 1"); return 1;
                }

                if (!player1HasVal)
                {
                    Console.WriteLine("Royal Flush for Player 2"); return 2;
                }
            }
            return 0;
        }
        #endregion Rank 10

        #region Rank 9
        /// <summary>
        /// All five cards in consecutive value order with same suit
        /// </summary>
        /// <param name="player1cards"></param>
        /// <param name="player2cards"></param>
        /// <returns></returns>
        private static int CheckStraightFlush(string player1cards, string player2cards)
        {
            bool firshandConsecutive = ConsecutiveValues(player1cards);
            bool secondhandConsecutive = ConsecutiveValues(player2cards);

            string checkFirstPlayer = CardofSameSuit(player1cards, 5);
            string checkSecondPlayer = CardofSameSuit(player2cards, 5);

            if ((firshandConsecutive && checkFirstPlayer.Trim().Length != 0) || (secondhandConsecutive && checkSecondPlayer.Trim().Length != 0))
            {
                if ((!secondhandConsecutive || checkSecondPlayer.Trim().Length == 0)) return 1;
                if ((!firshandConsecutive || checkFirstPlayer.Trim().Length == 0)) return 2;
                return CheckHighCard(player1cards, player2cards);
            }
            return 0;

        }
        #endregion Rank 9

        #region Rank  8
        /// <summary>
        /// Four cards of same value
        /// </summary>
        /// <param name="player1cards"></param>
        /// <param name="player2cards"></param>
        /// <returns></returns>
        private static int CheckfourofaKind(string player1cards, string player2cards)
        {
            string checkFirstPlayer = selectThePair(player1cards, 4);
            string checkSecondPlayer = selectThePair(player2cards, 4);
            if (checkFirstPlayer.Trim().Length != 0 || checkSecondPlayer.Trim().Length != 0)
            {
                if (checkSecondPlayer.Trim().Length == 0)
                {
                    return 1;
                }
                if (checkFirstPlayer.Trim().Length == 0)
                {
                    return 2;
                }
                if (cardValue.IndexOf(checkFirstPlayer[0]) > cardValue.IndexOf(checkSecondPlayer[0]))
                {
                    return 1;
                }
                if (cardValue.IndexOf(checkSecondPlayer[0]) > cardValue.IndexOf(checkFirstPlayer[0]))
                {
                    return 2;
                }
            }
            return 0;
        }
        #endregion Rank  8

        #region Rank  7
        /// <summary>
        /// Full house 
        /// Three of a kind and a Pair
        /// </summary>
        /// <param name="player1cards"></param>
        /// <param name="player2cards"></param>
        /// <returns></returns>
        private static int CheckforFullHouse(string player1cards, string player2cards)
        {
            string checkFirstPlayer = selectThePair(player1cards, 3);
            string checkSecondPlayer = selectThePair(player2cards, 3);
            if (checkFirstPlayer.Trim().Length != 0 || checkSecondPlayer.Trim().Length != 0)// Check  pair of 3 there 
            {
                string secondPairPlayer1 = "";
                string secondPairPlayer2 = "";
                if (checkFirstPlayer.Trim().Length != 0)
                {
                    secondPairPlayer1 = selectThePair(player1cards.Replace(checkFirstPlayer, ""), 2);
                }
                if (checkSecondPlayer.Trim().Length != 0)
                {
                    secondPairPlayer2 = selectThePair(player2cards.Replace(checkSecondPlayer, ""), 2);
                }

                if (secondPairPlayer1.Trim().Length != 0 || secondPairPlayer2.Trim().Length != 0)
                {
                    if (secondPairPlayer2.Trim().Length == 0)
                    {
                        return 1;
                    }
                    if (secondPairPlayer1.Trim().Length == 0)
                    {
                        return 2;
                    }

                    if (cardValue.IndexOf(checkFirstPlayer[0]) > cardValue.IndexOf(checkSecondPlayer[0]))
                    {
                        return 1;
                    }
                    if (cardValue.IndexOf(checkSecondPlayer[0]) > cardValue.IndexOf(checkFirstPlayer[0]))
                    {
                        return 2;
                    }

                    if (cardValue.IndexOf(secondPairPlayer1[0]) > cardValue.IndexOf(secondPairPlayer2[0]))
                    {
                        return 1;
                    }
                    if (cardValue.IndexOf(secondPairPlayer2[0]) > cardValue.IndexOf(secondPairPlayer1[0]))
                    {
                        return 2;
                    }
                }

            }
            return 0;
        }
        #endregion 7

        #region Rank  6
        /// <summary>
        /// All five card in same suit
        /// </summary>
        /// <param name="player1cards"></param>
        /// <param name="player2cards"></param>
        /// <returns></returns>
        private static int CheckForFlush6(string player1cards, string player2cards)
        {
            string checkFirstPlayer = CardofSameSuit(player1cards, 5);
            string checkSecondPlayer = CardofSameSuit(player2cards, 5);

            if (checkFirstPlayer.Trim().Length != 0 || checkSecondPlayer.Trim().Length != 0)
            {
                if (checkSecondPlayer.Trim().Length == 0)
                {
                    return 1;
                }
                if (checkFirstPlayer.Trim().Length == 0)
                {
                    return 2;
                }
            }
            return 0;
        }
        /// <summary>
        /// Card of Same suit
        /// </summary>
        /// <param name="playerInputcards"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        private static string CardofSameSuit(string playerInputcards, int count)
        {
            string checkThHand = "";

            if (playerInputcards.Count(f => f == 'D') == count) checkThHand = "D";
            if (playerInputcards.Count(f => f == 'H') == count) checkThHand += "H";
            if (playerInputcards.Count(f => f == 'S') == count) checkThHand += "S";
            if (playerInputcards.Count(f => f == 'C') == count) checkThHand += "C";
            return checkThHand;
        }

        #endregion  Rank 6

        #region Rank 5
        /// <summary>
        /// Check all five card are in Consecutive Value order
        /// </summary>
        /// <param name="firsthand"></param>
        /// <param name="secondHand"></param>
        /// <returns></returns>
        private static int CheckStraight(string firsthand, string secondHand)
        {

            bool firshandConsecutive = ConsecutiveValues(firsthand);
            bool secondhandConsecutive = ConsecutiveValues(secondHand);

            if (firshandConsecutive || secondhandConsecutive)
            {
                if (!secondhandConsecutive) return 1;
                if (!firshandConsecutive) return 2;

                // both are conscutive
                return CheckHighCard(firsthand, secondHand);
            }
            return 0;
        }


        /// <summary>
        /// Is consecutive value
        /// </summary>
        /// <param name="playerInputcards"></param>
        /// <returns></returns>
        private static bool ConsecutiveValues(string playerInputcards)
        {
            int[] firstcardArrray = new int[5];
            int counter = 0;
            for (int i = 0; i < playerInputcards.Length; i += 2)
            {
                firstcardArrray[counter] = cardValue.IndexOf(playerInputcards[i]);
                counter++;
            }
            Array.Sort(firstcardArrray); // sort the array

            bool isconsevitve = true;
            for (int i = 0; i < firstcardArrray.Length - 1; i++)
            {
                if (firstcardArrray[i] != firstcardArrray[i + 1])
                {
                    isconsevitve = false;
                    break;
                }
            }
            return isconsevitve;

        }
        #endregion Rank 5

        #region Rank 4
        /// <summary>
        /// Check if three of a Kind  
        /// </summary>
        /// <param name="player1cards"></param>
        /// <param name="player2cards"></param>
        /// <returns></returns>
        private static int CheckThreePairRank4(string player1cards, string player2cards)
        {

            string checkFirstPlayer = selectThePair(player1cards, 3);
            string checkSecondPlayer = selectThePair(player2cards, 3);

            if (checkFirstPlayer.Trim().Length == 1 || checkSecondPlayer.Trim().Length == 1)
            {
                if (checkSecondPlayer.Trim().Length != 1)
                    return 1;

                if (checkFirstPlayer.Trim().Length != 1)
                    return 2;

                if (cardValue.IndexOf(checkFirstPlayer[0]) > cardValue.IndexOf(checkSecondPlayer[0]))
                    return 1;

                if (cardValue.IndexOf(checkSecondPlayer[0]) > cardValue.IndexOf(checkFirstPlayer[0]))
                    return 2;

            }
            return 0;
        }
        #endregion Rank 4



        #region Rank 3
        /// <summary>
        /// Check Two pair 
        /// Rank 3
        /// </summary>
        /// <param name="firsthand"></param>
        /// <param name="secondHand"></param>
        /// <returns></returns>
        private static int CheckTwoPairRank3(string player1cards, string player2cards)
        {

            string checkFirstPlayer = selectThePair(player1cards, 2);
            string checkSecondPlayer = selectThePair(player2cards, 2);

            if (checkFirstPlayer.Trim().Length == 2 || checkSecondPlayer.Trim().Length == 2)
            {
                if (checkSecondPlayer.Trim().Length != 2)
                    return 1;

                if (checkFirstPlayer.Trim().Length != 2)
                    return 2;
                if (cardValue.IndexOf(checkFirstPlayer[0]) > cardValue.IndexOf(checkSecondPlayer[0]))
                    return 1;

                if (cardValue.IndexOf(checkSecondPlayer[0]) > cardValue.IndexOf(checkFirstPlayer[0]))
                    return 2;

                if (cardValue.IndexOf(checkFirstPlayer[1]) > cardValue.IndexOf(checkSecondPlayer[1]))
                    return 1;

                if (cardValue.IndexOf(checkSecondPlayer[1]) > cardValue.IndexOf(checkFirstPlayer[1]))
                    return 2;

            }
            return 0;
        }

        private static string selectThePair(string playerInputcards, int count)
        {
            string checkThHand = "";

            if (playerInputcards.Count(f => f == 'A') == count) checkThHand = "A";
            if (playerInputcards.Count(f => f == 'K') == count) checkThHand += "K";
            if (playerInputcards.Count(f => f == 'Q') == count) checkThHand += "Q";
            if (playerInputcards.Count(f => f == 'J') == count) checkThHand += "J";
            if (playerInputcards.Count(f => f == 'T') == count) checkThHand += "T";
            if (playerInputcards.Count(f => f == '9') == count) checkThHand += "9";
            if (playerInputcards.Count(f => f == '8') == count) checkThHand += "8";
            if (playerInputcards.Count(f => f == '7') == count) checkThHand += "7";
            if (playerInputcards.Count(f => f == '6') == count) checkThHand += "6";
            if (playerInputcards.Count(f => f == '5') == count) checkThHand += "5";
            if (playerInputcards.Count(f => f == '4') == count) checkThHand += "4";
            if (playerInputcards.Count(f => f == '3') == count) checkThHand += "3";
            if (playerInputcards.Count(f => f == '2') == count) checkThHand += "2";

            return checkThHand;
        }

        #endregion Rank 3

        #region Rank 2
        /// <summary>
        /// Check a pair in card 
        /// </summary>
        /// <param name="player1cards"></param>
        /// <param name="player2cards"></param>
        /// <returns></returns>
        private static int CheckPairCard(string player1cards, string player2cards)
        {
            string checkFirstPlayer = selectThePair(player1cards, 2);
            string checkSecondPlayer = selectThePair(player2cards, 2);


            if (checkFirstPlayer.Trim().Length != 0 || checkSecondPlayer.Trim().Length != 0)
            {
                if (checkSecondPlayer.Trim().Length == 0)
                    return 1;
                if (checkFirstPlayer.Trim().Length == 0)
                    return 2;
                if (cardValue.IndexOf(checkFirstPlayer[0]) > cardValue.IndexOf(checkSecondPlayer[0]))
                    return 1;
                if (cardValue.IndexOf(checkSecondPlayer[0]) > cardValue.IndexOf(checkFirstPlayer[0]))
                    return 2;
            }
            return 0;
        }

        #endregion Region 2

        #region Rank 1
        /// <summary>
        /// Hirgh Card 
        /// Ranking 1
        /// </summary>
        /// <param name="player1cards"></param>
        /// <param name="player2cards"></param>
        /// <returns></returns>
        private static int CheckHighCard(string player1cards, string player2cards)
        {
            int result = 0;
            result = CheckaChar(player1cards, player2cards, "A");
            if (result > 0)
                return result;

            result = CheckaChar(player1cards, player2cards, "K");
            if (result > 0)
                return result;

            result = CheckaChar(player1cards, player2cards, "Q");
            if (result > 0)
                return result;
            result = CheckaChar(player1cards, player2cards, "J");
            if (result > 0)
                return result;

            result = CheckaChar(player1cards, player2cards, "T");
            if (result > 0)
                return result;
            result = CheckaChar(player1cards, player2cards, "9");
            if (result > 0)
                return result;
            result = CheckaChar(player1cards, player2cards, "8");
            if (result > 0)
                return result;
            result = CheckaChar(player1cards, player2cards, "7");
            if (result > 0)
                return result;
            result = CheckaChar(player1cards, player2cards, "6");
            if (result > 0)
                return result;
            result = CheckaChar(player1cards, player2cards, "5");
            if (result > 0)
                return result;
            result = CheckaChar(player1cards, player2cards, "4");
            if (result > 0)
                return result;
            result = CheckaChar(player1cards, player2cards, "3");
            if (result > 0)
                return result;
            result = CheckaChar(player1cards, player2cards, "2");
            if (result > 0)
                return result;

            return result;
        }
        /// <summary>
        /// Checks whether a char is there in both string and return 
        /// </summary>
        /// <param name="player1cards"></param>
        /// <param name="player2cards"></param>
        /// <param name="checkChar"></param>
        /// <returns>0 =  both string has the char or not there </returns>
        private static int CheckaChar(string player1cards, string player2cards, string checkChar)
        {
            if (player1cards.Contains(checkChar) || player2cards.Contains(checkChar))
            {
                if (player2cards.Contains(checkChar) == false)
                {
                    return 1;
                }
                if (player1cards.Contains(checkChar) == false)
                {
                    return 2;
                }
            }
            return 0;
        }
        #endregion Rank 1
    }
}
