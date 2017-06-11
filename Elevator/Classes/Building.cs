using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Elevator
{
    class Building
    {


        public List<Person> floor0List = new List<Person>();

        public List<Person> floor1List = new List<Person>();

        public List<Person> floor2List = new List<Person>();

        public List<Person> floor3List = new List<Person>();

        public List<Person> floor4List = new List<Person>();

        public List<Person> floor5List = new List<Person>();

        public List<Person> floor6List = new List<Person>();

        private int _maxFloor = 6;

        public int maxFloor
        {
            get
            {
                return _maxFloor;
            }
            private set
            {
                //_maxFloor = value;
                _maxFloor = 6;
            }
        }
        public bool stop = false;
        private Thread Runner;
        private int maxPeople = 6;
        public Lifter lifter;
        private frmMain parentForm;

        public Building(frmMain Form)
        {
            this.parentForm = Form;
            lifter = new Lifter(parentForm);
            for (int i = 0; i < maxPeople; i++) floor0List.Add(new Person(maxFloor, i + 1, lifter));
            Runner = new Thread(new ThreadStart(update));
            Runner.IsBackground = true;
            Runner.Start();

        }
        public void run()
        {
            Runner = new Thread(new ThreadStart(update));
            Runner.IsBackground = true;
            Runner.Start();
        }

        private void update()
        {
            while (!stop)
            {
                if(lifter.direction == Direction.Stop)
                switch (lifter.floor)
                {
                    case 0:
                        if (lifter.lifterList.Count > 0)
                        {
                            for(int i = 0; i < lifter.lifterList.Count; i++)
                            {
                                lifter.lifterList[i].finished = true;
                                lifter.lifterList.RemoveAt(i);
                            }

                        }
                        for(int i = 0; i < floor0List.Count; i++)
                        {
                            if (lifter.lifterList.Count < 12 && floor0List[i].inQueue)
                            {
                                lifter.addPerson(floor0List[i]);
                                
                                floor0List.RemoveAt(i);
                            }
                        }
                        lifter.doorClosed = true;
                        break;

                    case 1:
                        for(int i = 0; i < lifter.lifterList.Count; i++)
                        {
                            if(lifter.lifterList[i].nextFloor == lifter.floor)
                            {
                                lifter.lifterList[i].floor = lifter.floor;
                                floor1List.Add(lifter.lifterList[i]);
                                lifter.lifterList.RemoveAt(i);
                            }    
                        }
                        for(int i =0; i < floor1List.Count; i++)
                        {
                            if(floor1List[i].inQueue && lifter.lifterList.Count < 12 
                                    && floor1List[i].direction == lifter.nextDirection)
                            {
                                lifter.addPerson(floor1List[i]);
                                floor1List.RemoveAt(i);
                            }
                        }
                        lifter.doorClosed = true;
                        break;

                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        break;
                    default:
                        break;
                }
                Thread.Sleep(10);
                
            }
        }
    }
}
