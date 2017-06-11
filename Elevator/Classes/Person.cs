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
        private static bool first = true;
        private Lifter lifter;
        private frmMain parentForm;

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
            return String.Format("{0} {1} {2} {3} {4} {5} {6} {7}",
                "ID:", ID,
                " NF:", nextFloor,
                " CF:", floor,
                " Q:", inQueue);


        }

        private void runner()
        {
            while (!finished)
            {
                if (nextFloor != floor)
                {
                    inQueue = true;
                    Thread.Sleep(10);
                }
                else
                {
                    inQueue = false;
                    if (nextFloor == 0)
                    {
                        finished = true;
                        waitOnFloor = 10;
                    }
                    else
                    {
                        while (nextFloor == floor) nextFloor = getRandomNumber(0, maxFloor + 1);
                        if (floor - nextFloor > 0) direction = Direction.Down;
                        else direction = Direction.Up;
                        waitOnFloor = getRandomNumber(1000, 10000);
                    }
                    Thread.Sleep(waitOnFloor);
                    lifter.callElevator(floor);
                }
            }
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
