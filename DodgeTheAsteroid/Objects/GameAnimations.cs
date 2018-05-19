using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DodgeTheAsteroid.Objects
{
    static class GameAnimations
    {
        static public List<string[]> TwoTickExplode()
        {

            List<string[]> ani = new List<string[]>();

            string[] explode1 =
            {
                @"     ",
                @" \|/ ",
                @" -*- ",
                @" /|\ ",
                @"     "
            };

            string[] explode2 =
            {
                @"\ | /",
                @" \|/ ",
                @"--*--",
                @" /|\ ",
                @"/ | \"
            };

            string[] explode3 =
{
                @"\ | /",
                @"     ",
                @"- . -",
                @"     ",
                @"/ | \"
            };

            string[] explode4 =
{
                @"     ",
                @"     ",
                @"     ",
                @"     ",
                @"     "
            };

            ani.Add(explode1);
            ani.Add(explode2);
            ani.Add(explode3);
            ani.Add(explode4);
            return ani;
        }
        static public List<string[]> ShipExplode()
        {

            List<string[]> ani = new List<string[]>();

            string[] explode1 =
            {
                @"  ^  ",
                @" /0\ ",
                @"/|_|*",
                @"\|_|/",
            };

            string[] explode2 =
            {
                @"  ^  ",
                @" \0/ ",
                @"/|*|\",
                @"\/_\/",
            };

            string[] explode3 =
{
                @"  ^  ",
                @"\||/ ",
                @"-xxx-",
                @"/|||\",
            };

            string[] explode4 =
            {
                @"  /  ",
                @" /^\ ",
                @"-   /",
                @"\x#|/",
            };


            string[] explode5 =
            {
                @"/ -  ",
                @" /^) ",
                @"-   /",
                @"\(#\/",
            };

            string[] explode6=
{
                @"- - /",
                @" /^) ",
                @"- - /",
                @"\  \/",
            };

            string[] explode7 =
{
                @"  -  ",
                @" /   ",
                @"0   /",
                @"\  \/",
            };


            string[] explode8 =
{
                @"     ",
                @"  .  ",
                @" .   ",
                @"   . ",
            };

            ani.Add(explode1);
            ani.Add(explode2);
            ani.Add(explode3);
            ani.Add(explode4);
            ani.Add(explode5);
            ani.Add(explode6);
            ani.Add(explode7);
            ani.Add(explode8);
            return ani;
        }


    }
}
