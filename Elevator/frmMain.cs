using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Elevator
{
    public partial class frmMain : Form
    {
        List<Person> pList = new List<Person>();
        Lifter lifter;
        private int floor = 0;
        Building building;
        public frmMain()
        {
            InitializeComponent();
            lifter  = new Lifter(this);
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            building = new Building(this);
            //for(int i = 0; i < 150; i++)
            //{
            //    pList.Add(new Person(6, i + 1, lifter));
            //}
            //lsbElevator.BeginUpdate();
            //lsbElevator.DataSource = null;
            //lsbElevator.DataSource = pList;
            //lsbElevator.EndUpdate();

        }

        public void moveElevator(int floor)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<int>(moveElevator), new object[] { floor });
                return;
            }

            switch (floor)
            {
                case 0:
                    lsbElevator.Location = new Point(237, 460);
                    break;
                case 1:
                    lsbElevator.Location = new Point(237, 385);
                    break;
                case 2:
                    lsbElevator.Location = new Point(237, 310);
                    break;
                case 3:
                    lsbElevator.Location = new Point(237, 235);
                    break;
                case 4:
                    lsbElevator.Location = new Point(237, 160);
                    break;
                case 5:
                    lsbElevator.Location = new Point(237, 85);
                    break;
                case 6:
                    lsbElevator.Location = new Point(237, 10);
                    break;
                default:
                    break;
            }
        }

        public void updateElevator(List<Person> pList)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<List<Person>>(updateElevator), new object[] { pList });
                return;
            }
            lsbElevator.BeginUpdate();
            lsbElevator.DataSource = null;
            lsbElevator.DataSource = pList;
            lsbElevator.EndUpdate();
        }

        public void updateFloors(List<Person> pList , int floor)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<List<Person>, int>(updateFloors), new object[] { pList , floor});
                return;
            }

            switch (floor)
            {
                case 0:
                    lsb0Floor.BeginUpdate();
                    lsb0Floor.DataSource = null;
                    lsb0Floor.DataSource = pList;
                    lsb0Floor.EndUpdate();
                    break;
                case 1:
                    lsb1Floor.BeginUpdate();
                    lsb1Floor.DataSource = null;
                    lsb1Floor.DataSource = pList;
                    lsb1Floor.EndUpdate();
                    break;
                case 2:
                    lsb2Floor.BeginUpdate();
                    lsb2Floor.DataSource = null;
                    lsb2Floor.DataSource = pList;
                    lsb2Floor.EndUpdate();
                    break;
                case 3:
                    lsb3Floor.BeginUpdate();
                    lsb3Floor.DataSource = null;
                    lsb3Floor.DataSource = pList;
                    lsb3Floor.EndUpdate();
                    break;
                case 4:
                    lsb4Floor.BeginUpdate();
                    lsb4Floor.DataSource = null;
                    lsb4Floor.DataSource = pList;
                    lsb4Floor.EndUpdate();
                    break;
                case 5:
                    lsb5Floor.BeginUpdate();
                    lsb5Floor.DataSource = null;
                    lsb5Floor.DataSource = pList;
                    lsb5Floor.EndUpdate();
                    break;
                case 6:
                    lsb6Floor.BeginUpdate();
                    lsb6Floor.DataSource = null;
                    lsb6Floor.DataSource = pList;
                    lsb6Floor.EndUpdate();
                    break;
                default:
                    break;
            }

        }
        
        private void btnUp_Click(object sender, EventArgs e)
        {
            floor = (floor >= 6) ? 6 : floor + 1;
            lblFloor.Text = floor.ToString();
            moveElevator(floor);
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            floor = (floor <= 0) ? 0 : floor - 1;
            lblFloor.Text = floor.ToString();
            moveElevator(floor);
        }
    }
}
