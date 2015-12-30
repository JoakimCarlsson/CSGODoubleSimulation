using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace CSGODoubleSimulation
{
    class Program
    {
        static int _balance;
        static int _initialBet;
        static int _newBet;
        static string _initialBetColor;
        static int _numberOfRolls;
        static int _betsWon;
        static bool _switchColorAfterWin = false;

        static void Main(string[] args)
        {
            Thread thread1 = new Thread(PrintOutToConsole);
            UserInput();
            thread1.Start();
            StartSimulation();
        }

        private static void StartSimulation()
        {
            while (true)
            {
                _numberOfRolls++;
                if (DidWin())
                {
                    _balance = _balance + _newBet;
                    _newBet = _initialBet;

                    if (_switchColorAfterWin)
                    {
                        switch (_initialBetColor)
                        {
                            case "black":
                                _initialBetColor = "red";
                                continue;
                            case "red":
                                _initialBetColor = "black";
                                break;
                        }
                    }

                }
                else
                {
                    _balance = _balance - _newBet;
                    _newBet = _newBet * 2;
                    if (_newBet > _balance)
                    {
                        Console.WriteLine("You can't make this bet you are out.");
                        Console.ReadKey();
                    }
                }
            }
        }

        private static bool DidWin()
        {
            //return _initialBetColor == RandomColor();
            if (_initialBetColor == RandomColor())
            {
                _betsWon++;
                return true;
            }
            return false;
        }

        private static void PrintOutToConsole()
        {
            while (true)
            {
                if (_balance > 0)
                {
                    Console.WriteLine("Balance: \t\t\t{0}", _balance);
                    Console.WriteLine("Initial bet: \t\t\t{0}", _initialBet);
                    Console.WriteLine("Color to bet on: \t\t{0}", _initialBetColor);
                    Console.WriteLine("Number rolls: \t\t\t{0}", _numberOfRolls);
                    Console.WriteLine("Bets won: \t\t\t{0}", _betsWon);
                    double result = (double)_betsWon / (double)_numberOfRolls * 100;
                    Console.WriteLine("Win rate: {0}%", result);

                }
                else
                {
                    Console.WriteLine("You are dead.");
                    Console.WriteLine("Balance: {0}", _balance);
                    Console.WriteLine("Next Bet: {0}", _newBet);
                    Console.ReadKey();
                }
                Thread.Sleep(500);
                Console.Clear();
            }
        }

        private static string RandomColor()
        {
            Dictionary<int, string> colors = new Dictionary<int, string>
            {
                {4, "Red" },
                {0, "Green"},
                {11, "Black" },
                {5, "Red" },
                {10, "Black" },
                {6, "Red" },
                {9, "Black" },
                {7, "Red" },
                {8, "Black" },
                {1, "Red" },
                {14, "Black" },
                {2, "Red" },
                {13, "Black" },
                {3, "Red" },
                {12, "Black" },
            };
            Random rand = new Random();
            Thread.Sleep(30);
            string randomValue = colors[rand.Next(0, colors.Count)];
            return randomValue.ToLower();
        }

        private static void UserInput()
        {
            Console.Write("Enter Balance: ");
            string tempBalance = Console.ReadLine();
            while (!Int32.TryParse(tempBalance, out _balance))
            {
                Console.Write("That is not a valid number try again: ");
                tempBalance = Console.ReadLine();
            }

            Console.Write("Enter Initial Bet: ");
            string tempBet = Console.ReadLine();
            while (!Int32.TryParse(tempBet, out _initialBet))
            {
                Console.Write("That is not a valid number try again: ");
                tempBet = Console.ReadLine();
            }
            _newBet = Convert.ToInt32(tempBet);

            string[] allowedColor = { "black", "green", "red", };
            Console.Write("Color To Bet on: ");
            string tempColor = Console.ReadLine().ToLower();

            while ((String.IsNullOrWhiteSpace(tempColor)) || (!allowedColor.Contains(tempColor)))
            {
                Console.Write("You need to enter Red, Green or Black. try again: ");
                tempColor = Console.ReadLine().ToLower();
            }
            _initialBetColor = tempColor;

            string[] allowedInput = { "yes", "no" };
            Console.Write("Do you want to switch color after a win?(True/False) ");
            string tempInput = Console.ReadLine().ToLower();
            while ((String.IsNullOrWhiteSpace(tempColor)) || (!allowedColor.Contains(tempColor)))
            {
                Console.Write("Do you want to switch color after a win?(True/False) ");
                tempInput = Console.ReadLine().ToLower();
            }
            _switchColorAfterWin = Convert.ToBoolean(tempInput);
        }
    }
}
