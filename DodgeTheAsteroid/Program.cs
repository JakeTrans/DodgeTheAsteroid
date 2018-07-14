using ConsoleEngine;
using ConsoleEngine.Engine;
using DodgeTheAsteroid.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DodgeTheAsteroid
{
    class Program
    {
       

        static void Main(string[] args)
        {
            Game GameInstance = new Game();
        }
    }

    public class Game
    {
        readonly int  RefreshRate = 50;

        public Game()
        {
           
            RunGame();
        }

        public int Score { get; set; }
        public DateTime LastShot { get; set; }

        public List<GameObject> objlist;
        public ScreenBuffer SB;


        public void RunGame()
        {
            
            // setup of base screen
            Score = 0;
           
            Console.WindowHeight = 50;

            RespawnPoint:
            objlist = new List<GameObject>();
            SB = new ScreenBuffer(Console.WindowWidth, Console.WindowHeight);

            Ship Player1 = new Ship(SB);
            Random newrockpos = new Random();

            SB.FixedText.Add(new FixedText(new string('-',Console.WindowWidth),  new ConsoleEngine.Coordinate(0, Console.WindowHeight - 4)));
            SB.FixedText.Add(new FixedText("Dodge the Asteroid by JakeTrans", new ConsoleEngine.Coordinate(0, Console.WindowHeight - 3)));
            SB.FixedText.Add(new FixedText("Cursors to move, Space to shoot '0' is a rock", new ConsoleEngine.Coordinate(0, Console.WindowHeight - 2)));
            
            Controls Play1con = CreateControls(Player1);
            SB.Draw("READY????", new ConsoleEngine.Coordinate((Console.WindowWidth / 2) - 10, Console.WindowHeight / 2));
        
            SB.UpdateScreen(objlist);
            Console.ReadKey();

            Play1con.Activate();

            objlist.Add(Player1);

//if in test mode drop one asteroid only (see TestMode Build)
#if test
            Rock Temprock = new Rock(37, 2, SB);
            objlist.Add(Temprock);
#endif 

            // game loop
            do
            {
                //if in test mode drop one asteroid on each tick
#if !test
                Rock Temprock = new Rock(newrockpos.Next(1, Console.WindowWidth - 1), 2, SB);
                //int XToUse = newrockpos.Next(2, Console.WindowWidth - 2);

                //Rock Temprock = new Rock(XToUse, 2, SB, DirectionRandomiser(XToUse, SB));
                objlist.Add(Temprock);
#endif 
                //Despawn
                for (int i = 0; i < objlist.Count; i++)
                {
                    if (objlist[i].Position.Y > SB.Height - 6 || objlist[i].Position .Y == 1 || objlist[i].Position.X <= 2 || objlist[i].Position.X >= Console.WindowWidth - 2 ||
                        (objlist[i].Hit == true && objlist[i].GetType() != typeof(Ship)))
                    {
                        objlist[i].DestroyObject();
                        objlist.RemoveAt(i);
                    }
                    else if (objlist[i].Hit == true)
                    {
                        objlist[i].DestroyObject();
                        objlist.RemoveAt(i);
                    }
                }

                Thread.Sleep(RefreshRate);
                CollisionDetection CD = new CollisionDetection(objlist);

                foreach (GameObject  item in objlist.ToList<GameObject>())
                {
                        item.SelfMove();
                        CD.RunDetection(item);                        
                }

                Score = Score + 1; // add a point on each cycle

                //get Item that have been hit
                List<GameObject> Hitobjects = objlist.Where(o => o.Hit == true).ToList();

                foreach (GameObject item in Hitobjects)
                {
                    if (item.GetType() == typeof(Ship))
                    {

                        //Tuple<int, int> TopLeft = GeneralFunctions.FindTopLeft(GameAnimations.TwoTickExplode()[0], new Tuple<int, int>(item.X, item.Y));
                        Animation Anim2 = new Animation(GameAnimations.ShipExplode(), new Coordinate(item.Position.X, item.Position.Y));
                        Animation Anim = new Animation(GameAnimations.TwoTickExplode(), new Coordinate(item.HitLocation.X - 2, item.HitLocation.Y - 2));

                        SB.AnimatationtoRUN.Add(Anim2);
                        SB.AnimatationtoRUN.Add(Anim);

                        SB.FixedText.Add(new FixedText("YOU HAVE BEEN HIT", new Coordinate( 0, 0)));

                        SB.FixedText.Add(new FixedText("SCORE -- " + Score.ToString(), new Coordinate( 0, 1)));

                        item.DestroyObject();

                        SB.UpdateScreen(objlist );

                        goto Endpoint;
                    }
                    else if (item.GetType() == typeof(Rock))
                    {
                        Animation Anim = new Animation(GameAnimations.TwoTickExplode(), new Coordinate(item.HitLocation.X - 2, item.HitLocation.Yn - 2));
                        SB.AnimatationtoRUN.Add(Anim);
                    }
                }
                SB.UpdateScreen(objlist);
            } while (Player1.Hit == false);

            Endpoint:
            Play1con.Deactivate();

            SB.FinishAnimations(RefreshRate, objlist);


            foreach (GameObject item in objlist)
            {
                item.DestroyObject();
            }
            SB.FixedText.Add(new FixedText("PRESS Y TO RESTART OR N TO EXIT", new Coordinate(0, 4)));
            SB.UpdateScreen(objlist);
            RespawnQPoint:
            ConsoleKeyInfo CK = Console.ReadKey(true);

            

            if (CK.Key == ConsoleKey.Y)
            {
                goto RespawnPoint;
            }
            else if (CK.Key == ConsoleKey.N)
            {
                //end program
            }
            else
            {
                goto RespawnQPoint;
            }

            SB.Draw("EXITING", new Coordinate(0, 3));
            SB.UpdateScreen(objlist);
        }
        private Controls CreateControls(Ship Shiptocontrol)
        {
            Controls controller = new Controls();

            controller.Gamecontrols.Add(new Control(ConsoleKey.RightArrow, new Action(() => Shiptocontrol.MoveObject(new Coordinate(1, 0)))));
            controller.Gamecontrols.Add(new Control(ConsoleKey.LeftArrow, new Action(() => Shiptocontrol.MoveObject(new Coordinate(-1, 0)))));
            controller.Gamecontrols.Add(new Control(ConsoleKey.UpArrow, new Action(() => Shiptocontrol.MoveObject(new Coordinate(0, -1)))));
            controller.Gamecontrols.Add(new Control(ConsoleKey.DownArrow, new Action(() => Shiptocontrol.MoveObject(new Coordinate(0, 1)))));
            controller.Gamecontrols.Add(new Control(ConsoleKey.Spacebar, new Action(() => Firing(Shiptocontrol))));
            return controller;
        }

        private void Firing(Ship Shipfiring)
        {
            if ((DateTime.Now - LastShot) <= new TimeSpan(RefreshRate * 25000))
            {
                return;
            }
            Shot NewShot = new Shot(Shipfiring, SB);
            objlist.Add(NewShot);
            LastShot = DateTime.Now;
        }


        private Tuple<int,int> DirectionRandomiser (int X, ScreenBuffer SB)
        {
            Random Directionsetting = new Random();

            if (X == 1)
            {
               int direction = Directionsetting.Next(1, 2);
                if (direction == 1)
                {
                    return new Tuple<int, int>(0, 1);
                }
                else
                {
                    return new Tuple<int, int>(1, 1);
                }

            }
            else if (X == SB.Width)
            {
                int direction = Directionsetting.Next(1, 2);
                if (direction == 1)
                {
                    return new Tuple<int, int>(0, 1);
                }
                else
                {
                    return new Tuple<int, int>(-1, 1);
                }

            }
            else
            {

                int direction = Directionsetting.Next(1, 4);
                if (direction == 1)
                {
                    return new Tuple<int, int>(0, 1);
                }
                else if (direction == 2)
                {

                    return new Tuple<int, int>(1, 1);
                }
                else
                {
                    return new Tuple<int, int>(-1, 1);
                }

            }


        }

    }

}
