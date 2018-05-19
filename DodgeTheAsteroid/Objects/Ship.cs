using ConsoleEngine.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DodgeTheAsteroid.Objects
{
    class Ship : GameObject
    {
        public ScreenBuffer Buffer { get; set; }
        public List<Shot> ShotFired { get; set; }



        public Ship(ScreenBuffer SB) : base(35, 35, Graphics.ShipIcon, SB)
        {
            ShotFired = new List<Shot>();
            Buffer = SB;
        }



    }
    class Shot : GameObject
    {
        public Shot (Ship ShipToAttach, ScreenBuffer SB) : base (ShipToAttach.X+2 , ShipToAttach.Y - 1, new string[] { "|" } ,new Tuple<int,int>(0,-1) , SB )
        {

        }
    }

}
