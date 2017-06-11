using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Elevator
{
    public enum Direction :byte
    {
        Up = 0,
        Down = 1,
        Stop = 2
    }

    public class Lifter
    {

        public int floor { get; private set; }
        public int nextStop { get; private set; }
        public Direction direction { get; private set; }
        public Direction nextDirection { get; private set; }

        public List<Person> lifterList = new List<Person>();
        public Thread lifterRunner;

        public bool alive { get; set; }
        public bool doorClosed { get; set; }
        private frmMain parentForm;

        public Lifter(frmMain parentForm)
        {
            this.parentForm = parentForm;
            alive = true;
            doorClosed = false;
            floor = 0;
            nextStop = 0;
            direction = Direction.Stop;
            lifterRunner = new Thread(new ThreadStart(runner));
            lifterRunner.IsBackground = true;
            lifterRunner.Start();
        }

        public void addPerson(Person p)
        {
            if (lifterList.Count < 12) { lifterList.Add(p);  }
        }


        private void runner()
        {
            while (alive)
            {
                if (doorClosed)
                {
                    if(lifterList.Count > 0)
                    {

                        direction = newDirection();
                        switch (direction)
                        {
                            case Direction.Up:
                                nextStop = lifterList.Min(x => x.nextFloor);
                                Thread.Sleep(2000);
                                floor++;
                                parentForm.moveElevator(floor);
                                break;
                            case Direction.Down:
                                nextStop = lifterList.Max(x => x.nextFloor);
                                Thread.Sleep(2000);
                                floor--;
                                parentForm.moveElevator(floor);
                                break;
                            case Direction.Stop:
                                nextStop = floor;
                                parentForm.moveElevator(floor);
                                doorClosed = false;
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {

                    }
                }
                Thread.Sleep(10);
            }
        }

        public void callElevator(int callingFloor)
        {

        }

        private Direction newDirection()
        {
            foreach(Person p in lifterList)
            {
                if (p.nextFloor == floor) return Direction.Stop;
            }
            if (floor - lifterList.Max(x => x.nextFloor) > 0) { nextDirection = Direction.Down; }
            else nextDirection = Direction.Up;
            if (floor - lifterList.Min(x => x.nextFloor) > 0) return Direction.Down;
            else if (floor - lifterList.Min(x => x.nextFloor) < 0) return Direction.Up;
            else return Direction.Stop;
        }
        
    }
}
