using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Computer : Player
    {
        Random cpuChoice;
        public Computer()
        {
            cpuChoice = new Random();
        }
        private void GameModePlay(int guess, int difficultNum, string difficulty, string person, Player player)
        {
            int[] hitCoord = new int[2];
            initiateShips.HitOrMiss = hitCoord;
            if (guess > difficultNum)
            {
                do
                {
                    hitCoord[0] = cpuChoice.Next(9);
                    hitCoord[1] = cpuChoice.Next(9);
                    initiateShips.HitOrMiss = hitCoord;
                } while (player.shipsBoard[initiateShips.HitOrMiss[1], initiateShips.HitOrMiss[0]] == "[ ]" || CheckIfGuessAlready(player) == true);
                DetermineHumanOrCpu(person, player);
            }
            else
            {
                hitCoord[0] = cpuChoice.Next(9);
                hitCoord[1] = cpuChoice.Next(9);
                if (CheckIfGuessAlready(player) == true)
                {
                    GameModePlay(guess, difficultNum, difficulty, person, player);
                }
                else
                {
                    DetermineHumanOrCpu(person, player);
                }
            }
        }
        private void ComputerHitChoice(string difficulty, string person, Player player)
        {
            int[] hitCoord = new int[2];
            switch (difficulty)
            {
                case "normal":
                    GameModePlay(0, 10, difficulty, person, player);
                    break;
                case "hard":
                    int guessAccuracy = cpuChoice.Next(100);
                    GameModePlay(guessAccuracy, 50, difficulty, person, player);
                    break;
                case "unbeatable":
                    int guessAcc = cpuChoice.Next(100);
                    GameModePlay(guessAcc, 25, difficulty, person, player);
                    break;
                default:
                    break;
            }
        }
        protected override void ChooseShip()
        {
            chosenShip = Convert.ToString(cpuChoice.Next(1, 6));
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
                ChooseShip();
            }
            else
            {
                usedShips.Add(chosenShip);
                SetSize(initiateShips);
                InitializeShipStartCoordinates();
            }
        }
        protected override void InitializeShipStartCoordinates()
        {
            int[] startCoord = new int[2];
            startCoord[0] = cpuChoice.Next(9);
            startCoord[1] = cpuChoice.Next(9);
            initiateShips.ShipInitialCoordinates = startCoord;
            PlaceShips();
        }
        protected override void ShipPlacement(int yCoord, int xCoord)
        {
            for (int i = 0; i < initiateShips.ShipSize; i++)
            {
                try
                {
                    if (shipsBoard[initiateShips.ShipInitialCoordinates[1] + (i * yCoord), initiateShips.ShipInitialCoordinates[0] + (i * xCoord)] != null)
                    {
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
                shipsBoard[initiateShips.ShipInitialCoordinates[1] + (i * yCoord), initiateShips.ShipInitialCoordinates[0] + (i * xCoord)] = null;
            }
        }
        protected override void PlaceShips()
        {
            string direction = Convert.ToString(cpuChoice.Next(1, 5));
            switch (direction)
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
        protected override bool DetermineHitOrMiss(string icon, Player player)
        {
            if (player.shipsBoard[initiateShips.HitOrMiss[1], initiateShips.HitOrMiss[0]] != icon)
            {
                Console.WriteLine("Computer hit a ship!");
                Console.ReadLine();
                hitsBoard[initiateShips.HitOrMiss[1], initiateShips.HitOrMiss[0]] = "[X]";
                player.shipsBoard[initiateShips.HitOrMiss[1], initiateShips.HitOrMiss[0]] = "[X]";
                player.initiateShips.HitCounter++;
                return true;
            }
            else
            {
                Console.WriteLine("Computer missed.");
                Console.ReadLine();
                hitsBoard[initiateShips.HitOrMiss[1], initiateShips.HitOrMiss[0]] = "[O]";
                player.shipsBoard[initiateShips.HitOrMiss[1], initiateShips.HitOrMiss[0]] = "[O]";
                return false;
            }
        }
        protected override bool CheckIfGuessAlready(Player player)
        {
            return hitsBoard[initiateShips.HitOrMiss[1], initiateShips.HitOrMiss[0]] == "[X]" || hitsBoard[initiateShips.HitOrMiss[1], initiateShips.HitOrMiss[0]] == "[O]";
        }
        public override void DetermineWin(Player player)
        {
            Console.WriteLine("Computer win");
            Console.ReadLine();
        }
        public override void HitOrMissChoice(string difficulty, string person, Player player)
        {
            switch (difficulty)
            {
                case "normal":
                    ComputerHitChoice(difficulty, person, player);
                    break;
                case "hard":
                    ComputerHitChoice(difficulty, person, player);
                    break;
                case "unbeatable":
                    ComputerHitChoice(difficulty, person, player);
                    break;
                default:
                    break;
            }
        }
    }
}
