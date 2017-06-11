using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Elevator
{
    public class Building
    {


        public List<Person> floor0List = new List<Person>();

        public List<Person> exitList = new List<Person>();

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
        private int elevatorCapacity = 5;
        private const int maxPeople = 50;
        private int peopleCounter = 0;
        public Lifter lifter;
        private frmMain parentForm;

        public Building(frmMain Form)
        {
            this.parentForm = Form;
            lifter = new Lifter(parentForm, this);
            // /*for (int i = 0; i < maxPeople; i++) */floor0List.Add(new Person(maxFloor, 1, lifter, parentForm));
            //for (int i = 0; i < elevatorCapacity + 15; i++)
            //    floor0List.Add(new Person(maxFloor,i + 1, lifter, parentForm));

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
                if(peopleCounter< maxPeople && getRandomNumber(0, 101) > 97)
                    floor0List.Add(new Person(maxFloor, ++peopleCounter , lifter, parentForm));

                if (!lifter.doorClosed)
                {
                    
                    switch (lifter.floor)
                    {
                        case 0:
                            if (lifter.lifterList.Count > 0)
                            {
                                for (int i = lifter.lifterList.Count - 1; i >= 0; i--)
                                {
                                    if (lifter.lifterList[i].nextFloor == 0)
                                    {
                                        Thread.Sleep(400);
                                        lifter.lifterList[i].Reset();
                                        exitList.Add(lifter.lifterList[i]);
                                        parentForm.updateExit(exitList);
                                        lifter.lifterList.RemoveAt(i);
                                        parentForm.updateElevator(lifter.lifterList);
                                        lifter.callElevator(lifter.floor);
                                    }
                                }

                            }
                            for (int i = floor0List.Count - 1; i >= 0; i--)
                            {
                                if (lifter.lifterList.Count < elevatorCapacity)
                                {
                                    Thread.Sleep(400);
                                    floor0List[i].setFloor(lifter.floor, true);
                                    lifter.addPerson(floor0List[i]);
                                    parentForm.updateElevator(lifter.lifterList);
                                    floor0List.RemoveAt(i);
                                    parentForm.updateFloors(floor0List, 0);
                                }
                            }
                            lifter.doorClosed = true;
                            break;

                        case 1: floor1List = updateUI(floor1List, false); break;
                        case 2: floor2List = updateUI(floor2List, false); break;
                        case 3: floor3List = updateUI(floor3List, false); break;
                        case 4: floor4List = updateUI(floor4List, false); break;
                        case 5: floor5List = updateUI(floor5List, false); break;
                        case 6: floor6List = updateUI(floor6List, true); break;
                        default: break;
                    }
                }
                
                parentForm.updateExit(exitList);
                parentForm.updateElevator(lifter.lifterList);
                parentForm.updateFloors(floor0List, 0);
                parentForm.updateFloors(floor1List, 1);
                parentForm.updateFloors(floor2List, 2);
                parentForm.updateFloors(floor3List, 3);
                parentForm.updateFloors(floor4List, 4);
                parentForm.updateFloors(floor5List, 5);
                parentForm.updateFloors(floor6List, 6);
                Thread.Sleep(100);
                
            }

        }

        private List<Person> updateUI(List<Person> pList, bool edgeFloor)
        {
            List<Person> personList = pList;
            for (int i = lifter.lifterList.Count - 1; i >= 0; i--)
            {
                if (lifter.lifterList[i].nextFloor == lifter.floor)
                {
                    Thread.Sleep(400);
                    lifter.lifterList[i].setFloor(lifter.floor, false);
                    personList.Add(lifter.lifterList[i]);
                    parentForm.updateFloors(personList, lifter.floor);
                    lifter.lifterList.RemoveAt(i);
                    parentForm.updateElevator(lifter.lifterList);
                    lifter.callElevator(lifter.floor);
                }
            }

            bool empty = (lifter.lifterList.Count == 0);
            for (int i = personList.Count - 1; i >= 0; i--)
            {
                if (personList[i].inQueue && lifter.lifterList.Count < elevatorCapacity
                        && ((personList[i].direction == lifter.previousDirection || empty) || edgeFloor))
                {
                    Thread.Sleep(400);
                    personList[i].setFloor(lifter.floor, true);
                    lifter.addPerson(personList[i]);
                    parentForm.updateElevator(lifter.lifterList);
                    personList.RemoveAt(i);
                    parentForm.updateFloors(personList, lifter.floor);
                }
            }
            lifter.doorClosed = true;
            return personList;
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
