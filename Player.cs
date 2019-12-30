using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    abstract class Player
    {
        protected string chosenShip;
        protected List<string> usedShips;
        protected int shipCount;
        public string[,] hitsBoard;
        public string[,] shipsBoard;
        public Ships initiateShips;
        public Player()
        {
            initiateShips = new Ships();
            usedShips = new List<string>();
            hitsBoard = new string[10, 10];
            shipsBoard = new string[10, 10];
        }
        public abstract void HitOrMissChoice(string mode, string person, Player player);
        public abstract void DetermineWin(Player player);
        public void ShipSetup()
        {
            if (shipCount < 5)
            {
                shipCount++;
                ChooseShip();
                ShipSetup();
            }
        }
        protected void SetSize(Ships initiateShips)
        {
            switch (chosenShip)
            {
                case "carrier":
                    initiateShips.ShipSize = 5;
                    initiateShips.ShipIcon = "[C]";
                    break;
                case "battleship":
                    initiateShips.ShipSize = 4;
                    initiateShips.ShipIcon = "[B]";
                    break;
                case "submarine":
                    initiateShips.ShipSize = 3;
                    initiateShips.ShipIcon = "[S]";
                    break;
                case "cruiser":
                    initiateShips.ShipSize = 3;
                    initiateShips.ShipIcon = "[c]";
                    break;
                case "destroyer":
                    initiateShips.ShipSize = 2;
                    initiateShips.ShipIcon = "[D]";
                    break;
                default:
                    break;
            }
        }
        protected bool DetermineHumanOrCpu(string person, Player player)
        {
            bool result = false;
            switch (person)
            {
                case "human":
                    result = DetermineHitOrMiss("[ ]", player);
                    break;
                case "cpu":
                    result = DetermineHitOrMiss(null, player);
                    break;
                default:
                    break;
            }
            return result;
        }
        protected abstract void InitializeShipStartCoordinates();
        protected abstract void ChooseShip();
        protected abstract void ShipPlacement(int yCoord, int xCoord);
        protected abstract void ResetBoardIcon(int count, int yCoord, int xCoord);
        protected abstract void PlaceShips();
        protected abstract bool CheckIfGuessAlready(Player player);
        protected abstract bool DetermineHitOrMiss(string icon, Player player);
        protected abstract void ShipHit(Player player);
    }
}
