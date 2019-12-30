using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Game
    {
        private void DetermineTurn(string player)
        {
            Console.WriteLine(player + "'s turn.");
            Console.ReadLine();
        }
        private void DetermineWinner(Player player1, Player player2)
        {
            if (player1.initiateShips.HitCounter == 17)
            {
                player1.DetermineWin(player1);
            }
            else if (player2.initiateShips.HitCounter == 17)
            {
                player2.DetermineWin(player2);
            }
        }
        private void GameInstructions()
        {
            Console.WriteLine("\t\t\t\t\t\tWelcome to Battleship.\nSetup:\n5 Ships (Carrier, Battleship, Submarine, Cruiser, Destroyer)\n2 Game Boards (Hit's Board, Ship's Board)\n2 Players Only (Computer or Human)\n");
            Console.WriteLine("Insructions:\nGame starts with Player 1 setting up their board with all 5 ships, then Player 2 takes their turn to setup their board.\nAfter both players have setup their board, each player then takes turn guessing where the other hid their ships.\nPlayer 1 starts first.");
            Console.ReadLine();
        }
        private string GameMenu()
        {
            string response;
            GameInstructions();
            do
            {
                Console.WriteLine("1: Human\n2: CPU");
                response = Console.ReadLine();
            } while (response != "1" && response != "2");
            return response;
        }
        private void DifficultyMenu(Player player)
        {
            string response;
            do
            {
                Console.WriteLine("\nChoose difficulty:\n1: Normal\n2: Hard\n3: Unbeatable");
                response = Console.ReadLine();
            } while (response != "1" && response != "2" && response != "3");
            DifficultyOptions(response, player);    
        }
        private void GameWithCpu(string mode ,string difficulty, Player player)
        {
            Computer computer = new Computer();
            player.ShipSetup();
            DetermineTurn("Computer");
            computer.ShipSetup();
            do
            {
                player.HitOrMissChoice("normal", "cpu", computer);
                computer.HitOrMissChoice(difficulty, "human", player);
            } while (player.initiateShips.HitCounter != 17 && computer.initiateShips.HitCounter != 17);
            DetermineWinner(player, computer);
        }
        private void DifficultyOptions(string response, Player player)
        {
            switch (response)
            {
                case "1":
                    GameWithCpu("normal", "normal", player);
                    break;
                case "2":
                    GameWithCpu("normal", "hard", player);
                    break;
                case "3":
                    GameWithCpu("normal", "unbeatable", player);
                    break;
                default:
                    break;
            }
        }
        private void GameChoice(Player player)
        {
            string response;
            do
            {
                Console.WriteLine("\nChoose game style:\n1: Normal\n2: Speed");
                response = Console.ReadLine();
            } while (response != "1" && response != "2");
            GameChoiceOptions(response, player);
        }
        private void GameChoiceOptions(string response, Player player)
        {
            switch (response)
            {
                case "1":
                    GameWithHuman("normal", player);
                    break;
                case "2":
                    Console.Clear();
                    Console.WriteLine("In 'Speed' game mode, whenever a player hits a ship; their turn does not end and they get to make another guess.\nThis goes on until the player misses.");
                    Console.ReadLine();
                    GameWithHuman("speed", player);
                    break;
                default:
                    break;
            }
        }
        private void GameWithHuman(string mode, Player player)
        {
            Human player2 = new Human();
            player.ShipSetup();
            DetermineTurn("Player 2");
            player2.ShipSetup();
            do
            {
                player.HitOrMissChoice(mode, "human", player2);
                DetermineTurn("Player 2");
                player2.HitOrMissChoice(mode, "human", player);
                DetermineTurn("Player 1");
            } while (player.initiateShips.HitCounter != 17 && player2.initiateShips.HitCounter != 17);
            DetermineWinner(player, player2);
        }
        public void Run()
        {
            string response = GameMenu();
            Human player1 = new Human();
            switch (response)
            {
                case "1":
                    GameChoice(player1);
                    break;
                case "2":
                    DifficultyMenu(player1);
                    break;
                default:
                    break;
            }
        }
    }
}
