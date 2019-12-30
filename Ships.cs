using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Ships
    {
        private List<string> shipsList;
        private int shipSize;
        private int[] shipInitialCoordinates;
        private int[] hitOrMiss;
        private string shipIcon;
        private int hitCounter;
        private int carrierHitCount;
        private int subHitCount;
        private int cruiserHitCount;
        private int battleshipHitCount;
        private int destroyerHitCoount;
        public Ships()
        {
            shipsList = new List<string> { "carrier", "battleship", "submarine", "cruiser", "destroyer" };
        }
        public List<string> ShipsList { get { return shipsList; } }
        public int ShipSize { get { return shipSize; } set{ shipSize = value; } }
        public int[] ShipInitialCoordinates { get { return shipInitialCoordinates; } set { shipInitialCoordinates = value; } }
        public int[] HitOrMiss { get { return hitOrMiss; } set { hitOrMiss = value; } }
        public string ShipIcon { get { return shipIcon; } set { shipIcon = value; } }
        public int HitCounter { get { return hitCounter; } set { hitCounter = value; } }
        public int CarrierHitCount { get { return carrierHitCount; } set { carrierHitCount = value; } }
        public int SubHitCount { get { return subHitCount; } set { subHitCount = value; } }
        public int CruiserHitCount { get { return cruiserHitCount; } set { cruiserHitCount = value; } }
        public int BattleshipHitCount { get { return battleshipHitCount; } set { battleshipHitCount = value; } }
        public int DestroyerHitCoount { get { return destroyerHitCoount; } set { destroyerHitCoount = value; } }
    }
}
