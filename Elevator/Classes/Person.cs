using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Elevator
{
    public class Person
    {
        public int ID { get; private set; }
        public int nextFloor { get; private set; }
        public int floor { get; set; }
        public int maxFloor { get; set; }
        public int waitOnFloor { get; private set; }
        public bool finished = false, inQueue;
        public Direction direction;
        private Thread personRunner;
        private Lifter lifter;
        private frmMain parentForm;
        private bool inElevator = false;

        public Person(int maxFloor,int ID, Lifter lifter, frmMain parent)
        {
            parentForm = parent;
            this.ID =  ID;
            this.lifter = lifter;
            floor = 0;
            inQueue = true;
            this.maxFloor = maxFloor;
            nextFloor = getRandomNumber(1, maxFloor + 1);
            waitOnFloor = 2000;
            direction = Direction.Up;
            personRunner = new Thread(new ThreadStart(runner));
            personRunner.IsBackground = true;
            personRunner.Start();
        }


        public override string ToString()
        {
            if (ID < 10)
            {
                if (inQueue) return String.Format("{0} {1} {2} ",
                  "p0" + ID,
                  "  calling", direction.ToString());
                else if (inElevator) return String.Format("{0} {1} {2}",
                "p0" + ID, " [ NF:", nextFloor + " ]");
                else return String.Format("{0}",
                 "p0" + ID);
            }


            else
            {
                if (inQueue) return String.Format("{0} {1} {2} ",
                 "p" + ID,
                 "  calling", direction.ToString());
                else if (inElevator) return String.Format("{0} {1} {2}",
                "p" + ID, " [ NF:", nextFloor + " ]");
                else return String.Format("{0}",
                 "p" + ID);
            } 
        }

        public void Reset()
        {
            finished = true;
            inQueue = false;
        }

        private void runner()
        {
            while (!finished)
            {
                if (nextFloor != floor)
                {
                    inQueue = !inElevator;
                    lifter.callElevator(floor);
                }
                else
                {
                    inQueue = false;
                    if (nextFloor == 0)
                    {
                        finished = true;
                        waitOnFloor = 1;
                    }
                    else
                    {
                        while (nextFloor == floor) nextFloor = getRandomNumber(0, maxFloor + 1);
                        if (floor - nextFloor > 0) direction = Direction.Down;
                        else direction = Direction.Up;
                        waitOnFloor = getRandomNumber(10000, 120000);
                    }
                    Thread.Sleep(waitOnFloor);
                }

                Thread.Sleep(10);
            }
        }
        
        public void setFloor(int floor, bool inElevator)
        {
            this.floor = floor;
            this.inElevator = inElevator;
            inQueue = false;
        }

        private int getRandomNumber(int start, int end)
        {
            int randomInitValue;
            using (RNGCryptoServiceProvider rg = new RNGCryptoServiceProvider())
            {
                byte[] rno = new byte[5];
                rg.GetBytes(rno);
                randomInitValue = BitConverter.ToInt32(rno, 0);
            }
            Random rnd = new Random(randomInitValue);
            return rnd.Next(start, end);
        }
    }
}
