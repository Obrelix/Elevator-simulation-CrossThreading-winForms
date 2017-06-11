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
            // /*for (int i = 0; i < maxPeople; i++) */floor0List.Add(new Person(maxFloor, 1, lifter, parentForm));
            for (int i = 0; i < maxPeople; i++)
                floor0List.Add(new Person(maxFloor,i + 1, lifter, parentForm));

            parentForm.updateFloors(floor0List, 0);

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
                                    parentForm.updateElevator(lifter.lifterList);
                                }

                            }
                            for(int i = 0; i < floor0List.Count; i++)
                            {
                                if (lifter.lifterList.Count < 6 && floor0List[i].inQueue)
                                {
                                    lifter.addPerson(floor0List[i]);
                                    parentForm.updateElevator(lifter.lifterList);
                                    floor0List.RemoveAt(i);
                                    parentForm.updateFloors(floor0List, 0);
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

                                    parentForm.updateFloors(floor1List, lifter.floor);
                                    lifter.lifterList.RemoveAt(i);
                                    parentForm.updateElevator(lifter.lifterList);
                                }    
                            }
                            for(int i =0; i < floor1List.Count; i++)
                            {
                                if(floor1List[i].inQueue && lifter.lifterList.Count < 6 )
                                        //&& floor1List[i].direction == lifter.nextDirection)
                                {
                                    lifter.addPerson(floor1List[i]);
                                    parentForm.updateElevator(lifter.lifterList);
                                    floor1List.RemoveAt(i);
                                    parentForm.updateFloors(floor1List, lifter.floor);
                                }
                            }
                            lifter.doorClosed = true;
                            break;
                    case 2:
                            for (int i = 0; i < lifter.lifterList.Count; i++)
                            {
                                if (lifter.lifterList[i].nextFloor == lifter.floor)
                                {
                                    lifter.lifterList[i].floor = lifter.floor;
                                    floor2List.Add(lifter.lifterList[i]);

                                    parentForm.updateFloors(floor2List, lifter.floor);
                                    lifter.lifterList.RemoveAt(i);
                                    parentForm.updateElevator(lifter.lifterList);
                                }
                            }
                            for (int i = 0; i < floor2List.Count; i++)
                            {
                                if (floor2List[i].inQueue && lifter.lifterList.Count < 6)
                                        //&& floor2List[i].direction == lifter.nextDirection)
                                {
                                    lifter.addPerson(floor2List[i]);
                                    parentForm.updateElevator(lifter.lifterList);
                                    floor2List.RemoveAt(i);
                                    parentForm.updateFloors(floor2List, lifter.floor);
                                }
                            }
                            lifter.doorClosed = true;
                            break;
                    case 3:
                            for (int i = 0; i < lifter.lifterList.Count; i++)
                            {
                                if (lifter.lifterList[i].nextFloor == lifter.floor)
                                {
                                    lifter.lifterList[i].floor = lifter.floor;
                                    floor3List.Add(lifter.lifterList[i]);

                                    parentForm.updateFloors(floor3List, lifter.floor);
                                    lifter.lifterList.RemoveAt(i);
                                    parentForm.updateElevator(lifter.lifterList);
                                }
                            }
                            for (int i = 0; i < floor3List.Count; i++)
                            {
                                if (floor3List[i].inQueue && lifter.lifterList.Count < 6)
                                        //&& floor3List[i].direction == lifter.nextDirection)
                                {
                                    lifter.addPerson(floor3List[i]);
                                    parentForm.updateElevator(lifter.lifterList);
                                    floor3List.RemoveAt(i);
                                    parentForm.updateFloors(floor3List, lifter.floor);
                                }
                            }
                            lifter.doorClosed = true;
                            break;
                    case 4:
                            for (int i = 0; i < lifter.lifterList.Count; i++)
                            {
                                if (lifter.lifterList[i].nextFloor == lifter.floor)
                                {
                                    lifter.lifterList[i].floor = lifter.floor;
                                    floor4List.Add(lifter.lifterList[i]);

                                    parentForm.updateFloors(floor4List, lifter.floor);
                                    lifter.lifterList.RemoveAt(i);
                                    parentForm.updateElevator(lifter.lifterList);
                                }
                            }
                            for (int i = 0; i < floor4List.Count; i++)
                            {
                                if (floor4List[i].inQueue && lifter.lifterList.Count < 6)
                                        //&& floor4List[i].direction == lifter.nextDirection)
                                {
                                    lifter.addPerson(floor4List[i]);
                                    parentForm.updateElevator(lifter.lifterList);
                                    floor4List.RemoveAt(i);
                                    parentForm.updateFloors(floor4List, lifter.floor);
                                }
                            }
                            lifter.doorClosed = true;
                            break;
                    case 5:
                            for (int i = 0; i < lifter.lifterList.Count; i++)
                            {
                                if (lifter.lifterList[i].nextFloor == lifter.floor)
                                {
                                    lifter.lifterList[i].floor = lifter.floor;
                                    floor5List.Add(lifter.lifterList[i]);

                                    parentForm.updateFloors(floor5List, lifter.floor);
                                    lifter.lifterList.RemoveAt(i);
                                    parentForm.updateElevator(lifter.lifterList);
                                }
                            }
                            for (int i = 0; i < floor5List.Count; i++)
                            {
                                if (floor5List[i].inQueue && lifter.lifterList.Count < 6)
                                        //&& floor5List[i].direction == lifter.nextDirection)
                                {
                                    lifter.addPerson(floor5List[i]);
                                    parentForm.updateElevator(lifter.lifterList);
                                    floor5List.RemoveAt(i);
                                    parentForm.updateFloors(floor5List, lifter.floor);
                                }
                            }
                            lifter.doorClosed = true;
                            break;
                    case 6:
                            for (int i = 0; i < lifter.lifterList.Count; i++)
                            {
                                if (lifter.lifterList[i].nextFloor == lifter.floor)
                                {
                                    lifter.lifterList[i].floor = lifter.floor;
                                    floor6List.Add(lifter.lifterList[i]);

                                    parentForm.updateFloors(floor6List, lifter.floor);
                                    lifter.lifterList.RemoveAt(i);
                                    parentForm.updateElevator(lifter.lifterList);
                                }
                            }
                            for (int i = 0; i < floor6List.Count; i++)
                            {
                                if (floor6List[i].inQueue && lifter.lifterList.Count < 6)
                                        //&& floor6List[i].direction == lifter.nextDirection)
                                {
                                    lifter.addPerson(floor6List[i]);
                                    parentForm.updateElevator(lifter.lifterList);
                                    floor6List.RemoveAt(i);
                                    parentForm.updateFloors(floor6List, lifter.floor);
                                }
                            }
                            lifter.doorClosed = true;
                            break;
                    default:
                        break;
                }

                Thread.Sleep(10);
                
            }
        }
    }
}
