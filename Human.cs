using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Human : Player
    {
        public Human()
        {
            SetGameBoardIcon(hitsBoard);
            SetGameBoardIcon(shipsBoard);
        }
        private void SetGameBoardIcon(string[,] gameBoard)
        {
            for (int i = 0; i < gameBoard.GetLength(0); i++)
            {
                for (int j = 0; j < gameBoard.GetLength(1); j++)
                {
                    gameBoard[i, j] = "[ ]";
                }
            }
        }
        private void HitsBoard()
        {
            Console.WriteLine("\t" + "     " + "HITS' BOARD");
            Console.WriteLine("   A   B   C   D   E   F   G   H   I   J");
            for (int k = 0; k < hitsBoard.GetLength(0); k++)
            {
                Console.Write(k + " ");
                for (int l = 0; l < hitsBoard.GetLength(1); l++)
                {
                    Console.Write(hitsBoard[k, l] + " ");
                }
                Console.WriteLine("\n");
            }
        }
        private void ShipsBoard()
        {
            Console.WriteLine("\t" + "     " + "SHIPS' BOARD");
            Console.WriteLine("   A   B   C   D   E   F   G   H   I   J");
            for (int k = 0; k < shipsBoard.GetLength(0); k++)
            {
                Console.Write(k + " ");
                for (int l = 0; l < shipsBoard.GetLength(1); l++)
                {
                    Console.Write(shipsBoard[k, l] + " ");
                }
                Console.WriteLine("\n");
            }
        }
        private void SetUp()
        {
            Console.Clear();
            HitsBoard();
            ShipsBoard();
        }
        private void ValidateInput(string choice)
        {
            List<char> alph = new List<char>() { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j' };
            List<char> num = new List<char>() { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            if (choice == "" || !(choice.Length != 3 || alph.Contains(choice[0])) || !(num.Contains(choice[2])) || !choice.Contains(","))
            {
                Console.WriteLine("Invalid input.");
                Console.ReadLine();
                InitializeShipStartCoordinates();
            }
            else
            {
                var choiceSplit = choice.Split(',');
                var convertInput = ConvertToIntArray(ConvertInputToCoordinates(choiceSplit));
                initiateShips.ShipInitialCoordinates = convertInput;
                PlaceShips();
            }
        }
        private string[] ConvertInputToCoordinates(string[] input)
        {
            switch (input[0])
            {
                case "a":
                    input[0] = "0";
                    break;
                case "b":
                    input[0] = "1";
                    break;
                case "c":
                    input[0] = "2";
                    break;
                case "d":
                    input[0] = "3";
                    break;
                case "e":
                    input[0] = "4";
                    break;
                case "f":
                    input[0] = "5";
                    break;
                case "g":
                    input[0] = "6";
                    break;
                case "h":
                    input[0] = "7";
                    break;
                case "i":
                    input[0] = "8";
                    break;
                case "j":
                    input[0] = "9";
                    break;
                default:
                    break;
            }
            return input;
        }
        private int[] ConvertToIntArray(string[] input)
        {
            int[] intInput = new int[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                intInput[i] = Convert.ToInt32(input[i]);
            }
            return intInput;
        }
        private bool ValidateHitChoice(string mode, string response, string person, Player player)
        {
            List<char> alph = new List<char>() { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j' };
            List<char> num = new List<char>() { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            bool result = false;
            if (response == "" || !(response.Length != 3 || alph.Contains(response[0])) || !(num.Contains(response[2])) || !response.Contains(","))
            {
                Console.WriteLine("Invalid input.");
                Console.ReadLine();
                HitOrMissChoice(mode, person, player);
            }
            else
            {
                var choiceSplit = response.Split(',');
                var convertInput = ConvertToIntArray(ConvertInputToCoordinates(choiceSplit));
                initiateShips.HitOrMiss = convertInput;
                if (CheckIfGuessAlready(player) == true)
                {
                    HitOrMissChoice(mode, person, player);
                }
                else
                {
                    result = DetermineHumanOrCpu(person, player);
                }
            }
            return result;
        }
        protected override void ShipHit(Player player)
        {
            string value = player.shipsBoard[initiateShips.HitOrMiss[1], initiateShips.HitOrMiss[0]];
            switch (value)
            {
                case "[C]":
                    if (initiateShips.CarrierHitCount == 4)
                    {
                        Console.WriteLine("You sunk their carrier.");
                    }
                    else
                    {
                        initiateShips.CarrierHitCount++;
                    }
                    break;
                case "[B]":
                    if (initiateShips.BattleshipHitCount == 3)
                    {
                        Console.WriteLine("You sunk their battleship.");
                    }
                    else
                    {
                        initiateShips.BattleshipHitCount++;
                    }
                    break;
                case "[S]":
                    if (initiateShips.SubHitCount == 2)
                    {
                        Console.WriteLine("You sunk their submarine.");
                    }
                    else
                    {
                        initiateShips.SubHitCount++;
                    }
                    break;
                case "[c]":
                    if (initiateShips.CruiserHitCount == 2)
                    {
                        Console.WriteLine("You sunk their cruiser.");
                    }
                    else
                    {
                        initiateShips.CruiserHitCount++;
                    }
                    break;
                case "[D]":
                    if (initiateShips.DestroyerHitCoount == 1)
                    {
                        Console.WriteLine("You sunk their destroyer.");
                    }
                    else
                    {
                        initiateShips.DestroyerHitCoount++;
                    }
                    break;
                default:
                    break;
            }
        }
        protected override void ChooseShip()
        {
            do
            {
                SetUp();
                Console.WriteLine("Choose which ship to place on the board:");
                for (int i = 0; i < initiateShips.ShipsList.Count; i++)
                {
                    Console.WriteLine((i + 1) + ": " + initiateShips.ShipsList[i]);
                }
                chosenShip = Console.ReadLine().ToLower();
                switch (chosenShip)
                {
                    case "1":
                        chosenShip = "carrier";
                        break;
                    case "2":
                        chosenShip = "battleship";
                        break;
                    case "3":
                        chosenShip = "submarine";
                        break;
                    case "4":
                        chosenShip = "cruiser";
                        break;
                    case "5":
                        chosenShip = "destroyer";
                        break;
                    default:
                        break;
                }
                if (usedShips.Contains(chosenShip))
                {
                    Console.WriteLine("This ship is already on the board. Choose a different one.");
                    Console.ReadLine();
                }
            } while (!(initiateShips.ShipsList.Contains(chosenShip)) || usedShips.Contains(chosenShip));
            usedShips.Add(chosenShip);
            SetSize(initiateShips);
            InitializeShipStartCoordinates();
        }
        protected override void InitializeShipStartCoordinates()
        {
            string choice;
            SetUp();
            Console.WriteLine("You're currently placing the " + chosenShip + ".");
            Console.WriteLine("Type initial coordinates for the ship. So if you want the ship in the first spot you would type A,0.");
            choice = Console.ReadLine().ToLower();
            ValidateInput(choice);
        }
        protected override void ShipPlacement(int yCoord, int xCoord)
        {
            for (int i = 0; i < initiateShips.ShipSize; i++)
            {
                try
                {
                    if (shipsBoard[initiateShips.ShipInitialCoordinates[1] + (i * yCoord), initiateShips.ShipInitialCoordinates[0] + (i * xCoord)] != "[ ]")
                    {
                        Console.WriteLine("Can't place there, another ship is in the way.");
                        Console.ReadLine();
                        ResetBoardIcon(i, yCoord, xCoord);
                        InitializeShipStartCoordinates();
                        break;
                    }
                    else
                    {
                        shipsBoard[initiateShips.ShipInitialCoordinates[1] + (i * yCoord), initiateShips.ShipInitialCoordinates[0] + (i * xCoord)] = initiateShips.ShipIcon;
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("The ship placement is not in-bounds of the game board.");
                    Console.ReadLine();
                    ResetBoardIcon(i, yCoord, xCoord);
                    InitializeShipStartCoordinates();
                    break;
                }
            }
        }
        protected override void ResetBoardIcon(int count, int yCoord, int xCoord)
        {
            for (int i = 0; i < count; i++)
            {
                shipsBoard[initiateShips.ShipInitialCoordinates[1] + (i * yCoord), initiateShips.ShipInitialCoordinates[0] + (i * xCoord)] = "[ ]";
            }
        }
        protected override void PlaceShips()
        {
            string response;
            do
            {
                Console.WriteLine("Which direction do you want to place the ship?\n1: Up\n2: Down\n3: Left\n4: Right");
                response = Console.ReadLine().ToLower();
            } while (response != "1" && response != "2" && response != "3" && response != "4");
            switch (response)
            {
                case "1":
                    ShipPlacement(-1, 0);
                    break;
                case "2":
                    ShipPlacement(1, 0);
                    break;
                case "3":
                    ShipPlacement(0, -1);
                    break;
                case "4":
                    ShipPlacement(0, 1);
                    break;
                default:
                    break;
            }
        }
        protected override bool CheckIfGuessAlready(Player player)
        {
            if (hitsBoard[initiateShips.HitOrMiss[1], initiateShips.HitOrMiss[0]] == "[X]" || hitsBoard[initiateShips.HitOrMiss[1], initiateShips.HitOrMiss[0]] == "[O]")
            {
                Console.WriteLine("You already guess this spot. Choose a different location.");
                Console.ReadLine();
                return true;
            }
            else
            {
                return false;
            }
        }
        protected override bool DetermineHitOrMiss(string icon, Player player)
        {
            if (player.shipsBoard[initiateShips.HitOrMiss[1], initiateShips.HitOrMiss[0]] != icon)
            {
                Console.WriteLine("You hit a ship!");
                ShipHit(player);
                Console.ReadLine();
                hitsBoard[initiateShips.HitOrMiss[1], initiateShips.HitOrMiss[0]] = "[X]";
                player.shipsBoard[initiateShips.HitOrMiss[1], initiateShips.HitOrMiss[0]] = "[X]";
                player.initiateShips.HitCounter++;
                SetUp();
                return true;
            }
            else
            {
                Console.WriteLine("You missed.");
                Console.ReadLine();
                hitsBoard[initiateShips.HitOrMiss[1], initiateShips.HitOrMiss[0]] = "[O]";
                player.shipsBoard[initiateShips.HitOrMiss[1], initiateShips.HitOrMiss[0]] = "[O]";
                SetUp();
                return false;
            }
        }
        public override void HitOrMissChoice(string mode, string person, Player player)
        {
            string response;
            SetUp();
            switch (mode)
            {
                case "normal":
                    Console.WriteLine("Type the coordinates you want to hit.");
                    response = Console.ReadLine().ToLower();
                    ValidateHitChoice(mode, response, person, player);
                    break;
                case "speed":
                    bool result;
                    do
                    {
                        Console.WriteLine("Type the coordinates you want to hit.");
                        response = Console.ReadLine().ToLower();
                        result = ValidateHitChoice(mode, response, person, player);
                    } while (result == true);
                    break;
                default:
                    break;
            }
        }
        public override void DetermineWin(Player player)
        {
            Console.WriteLine("You win");
            Console.ReadLine();
        }
    }
}
