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
        public Direction direction { get; private set; }
        public Direction previousDirection { get; private set; }

        public List<Person> lifterList = new List<Person>();
        public Thread lifterRunner;

        public bool alive { get; set; }
        public bool doorClosed { get; set; }
        private frmMain parentForm;
        private Building building;
        private int callingFloor;
        private bool canCall = true;

        public Lifter(frmMain parentForm, Building build)
        {
            this.building = build;
            this.parentForm = parentForm;
            alive = true;
            doorClosed = false;
            floor = 0;
            direction = Direction.Stop;
            lifterRunner = new Thread(new ThreadStart(runner));
            lifterRunner.IsBackground = true;
            lifterRunner.Start();
        }

        public void addPerson(Person p)
        {
            if (lifterList.Count < 5) { lifterList.Add(p); }
        }


        private void runner()
        {
            while (alive)
            {
                if (callingFloor == floor) canCall = true;
                if (doorClosed)
                {

                    direction = newDirection();

                    switch (direction)
                    {
                        case Direction.Up:
                            previousDirection = direction;
                            Thread.Sleep(400);
                            floor++;
                            parentForm.moveElevator(floor);
                            checkFloor(floor);
                            break;
                        case Direction.Down:
                            previousDirection = direction;
                            Thread.Sleep(400);
                            floor--;
                            parentForm.moveElevator(floor);
                            checkFloor(floor);
                            break;
                        case Direction.Stop:
                            doorClosed = false;
                            break;
                        default:
                            break;
                    }
                }
                
                Thread.Sleep(5);
            }
        }

        private void checkFloor(int lvl)
        {
            switch (lvl)
            {
                case 0:
                    if (building.floor0List.Count > 0) { doorClosed = false; }
                    break;

                case 1:
                    foreach (Person p in building.floor1List)
                        if (p.inQueue && (p.direction == previousDirection || lifterList.Count == 0)) doorClosed = false;
                    break;

                case 2:
                    foreach (Person p in building.floor2List)
                        if (p.inQueue && (p.direction == previousDirection || lifterList.Count == 0)) doorClosed = false;
                    break;

                case 3:
                    foreach (Person p in building.floor3List)
                        if (p.inQueue && (p.direction == previousDirection || lifterList.Count == 0)) doorClosed = false;
                    break;

                case 4:
                    foreach (Person p in building.floor4List)
                        if (p.inQueue && (p.direction == previousDirection || lifterList.Count == 0)) doorClosed = false;
                    break;

                case 5:
                    foreach (Person p in building.floor5List)
                        if (p.inQueue && (p.direction == previousDirection || lifterList.Count == 0)) doorClosed = false;
                    break;

                case 6:
                    if (building.floor6List.Count > 0) {  doorClosed = false; }
                    break;

                default:
                    break;
            }
        }

        public void callElevator(int callingFloor)
        {
            if (canCall)
            {
                this.callingFloor = callingFloor;
                canCall = false;
            }
            if (callingFloor == floor) canCall = true;
        }

        private Direction newDirection()
        {
            if (lifterList.Count > 0)
            {
                foreach (Person p in lifterList)
                {
                    if (p.nextFloor == floor) {  return Direction.Stop; }
                }

                switch (previousDirection)
                {
                    case Direction.Up:
                        if (floor - lifterList.Max(x => x.nextFloor) > 0) return Direction.Down;
                        else return Direction.Up;
                    case Direction.Down:
                        if (floor - lifterList.Min(x => x.nextFloor) > 0) return Direction.Down;
                        else return Direction.Up;
                    default:
                        return Direction.Stop;
                }
            }
            else if(!canCall)
            {
                if (floor - callingFloor > 0) { return Direction.Down; }
                else if (floor - callingFloor < 0) {  return Direction.Up; }
                else return Direction.Stop;
            }
            else return Direction.Stop;
        }
        
    }
}
