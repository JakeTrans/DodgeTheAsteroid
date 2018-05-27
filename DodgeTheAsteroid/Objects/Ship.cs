using ConsoleEngine;
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



        public Ship(ScreenBuffer SB) : base(new ConsoleEngine.Coordinate(35, 35), Graphics.ShipIcon, SB )
        {
            ShotFired = new List<Shot>();
            Buffer = SB;
        }



    }
    class Shot : GameObject
    {
        public Shot (Ship ShipToAttach, ScreenBuffer SB) : base (new ConsoleEngine.Coordinate(ShipToAttach.Position.X+2 , ShipToAttach.Position.Y - 1), new string[] { "|" } ,new Coordinate(0,-1) , SB )
        {

        }
    }

}
