using ConsoleEngine.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DodgeTheAsteroid.Objects
{
    class Rock : GameObject
    {

        public Rock(int x, int y, ScreenBuffer SB) : base(x, y, Graphics.Rock, new Tuple<int, int>(0, 1), SB)
        {
        }

        public Rock(int x, int y, ScreenBuffer SB, Tuple<int, int> Direction) : base(x, y, Graphics.Rock, Direction, SB)
        {
           
        }

    }
}
