using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MapGenerator
{
    public partial class DisplayForm : Form
    {

        public static bool fogOfWarOn = false;
        public static bool fogOfWarHarsh = false;
        public static int numberOfFloors = 0;
        public static int currentFloor = 1;
        public static int[] currentDoor = new int[2];
        public static string[,] floor1Fog = new string[60,60];
        public static string[,] floor2Fog = new string[60,60];
        public static string[,] floor3Fog = new string[60,60];
        public static bool successfulLoad = false;

        public DisplayForm()
        {
            InitializeComponent();
        }

        private void RoomSizeDropdown_Set(object sender, EventArgs e)
        {
            ComboBox sizeSelectBox = sender as ComboBox;
            string sizeSelected = sizeSelectBox.Text;

            if(sizeSelected == "small")
            {
                Program.roomSizeModifier = .3;
            }else if(sizeSelected == "average")
            {
                Program.roomSizeModifier = .2;
            }else if(sizeSelected == "large")
            {
                Program.roomSizeModifier = .1;
            }
        }

        private void NumberOfFloorsDropdown_Set(object sender, EventArgs e)
        {
            ComboBox floorSelectBox = sender as ComboBox;
            string newFloor = floorSelectBox.Text;
            if (newFloor == "one")
            {
                numberOfFloors = 1;
            }
            else if (newFloor == "two")
            {
                numberOfFloors = 2;
            }
            else if (newFloor == "three")
            {
                numberOfFloors = 3;
            }
            else if (newFloor == "random")
            {
                numberOfFloors = 0;
            }
        }

        private void FogOfWarDropdownSelected(object sender, EventArgs e)
        {
            ComboBox fogOfWarSettingBox = sender as ComboBox;
            string fogOfWarSetting = fogOfWarSettingBox.Text;

            if(fogOfWarSetting == "No Fog of War")
            {
                fogOfWarOn = false;
                fogOfWarHarsh = false;
            }else if(fogOfWarSetting == "All cleared rooms visible")
            {
                fogOfWarOn = true;
                fogOfWarHarsh = false;
            }
            else
            {
                fogOfWarOn = true;
                fogOfWarHarsh = true;
            }
        }

        private void DoneButton_Click(object sender, EventArgs e)
        {
            bool validInput = true;
            int height = 0;
            int width = 0;

            if (heightInput.Text != "")
            {
                try
                {
                    height = int.Parse(heightInput.Text);
                    if (height >= 10 && height <= 60)
                    {
                        validInput = true;
                    }
                    else
                    {
                        workingLabel.Text = "Please input a whole number from 10 to 60.";
                        workingLabel.ForeColor = Color.Red;
                        workingLabel.Visible = true;
                        validInput = false;
                    }
                }
                catch
                {
                    workingLabel.Text = "Please input a whole number from 10 to 60.";
                    workingLabel.ForeColor = Color.Red;
                    workingLabel.Visible = true;
                    validInput = false;
                }
            }

            if (validInput == true && widthInput.Text != "")
            {
                try
                {
                    width = int.Parse(widthInput.Text);
                    if (width >= 10 && width <= 60)
                    {
                        validInput = true;
                    }
                    else
                    {
                        workingLabel.Text = "Please input a whole number from 10 to 60.";
                        workingLabel.ForeColor = Color.Red;
                        workingLabel.Visible = true;
                        validInput = false;
                    }
                }
                catch
                {
                    workingLabel.Text = "Please input a whole number from 10 to 60.";
                    workingLabel.ForeColor = Color.Red;
                    workingLabel.Visible = true;
                    validInput = false;
                }
            }

            if (validInput == true)
            {
                Random rng = new Random();

                HideStartingUI();
                PrepTable();

                if (numberOfFloors == 0)
                {
                    numberOfFloors = rng.Next(1, 4);
                }
                floorMaxLabel.Text += numberOfFloors;

                if (height == 0)
                {
                    height = rng.Next(10, 60);
                }

                if (width == 0)
                {
                    width = rng.Next(10, 60);
                }


                Program.GenerateMap(height, width, this, 1);
                
                if (numberOfFloors > 1)
                {
                    Program.GenerateMap(height, width, this, 2);
                }
                if(numberOfFloors > 2)
                {
                    Program.GenerateMap(height, width, this, 3);
                }

                PrintMapWindow(Program.map1, Program.map1.GetLength(0), Program.map1.GetLength(1));

            }

        }

        public void HideStartingUI()
        {
            label1.Visible = false;
            label2.Visible = false;
            heightLabel.Visible = false;
            heightInput.Visible = false;
            widthLabel.Visible = false;
            widthInput.Visible = false;
            roomSizeLabel.Visible = false;
            roomSizeDropdown.Visible = false;
            floorsLabel.Visible = false;
            floorsDropdown.Visible = false;
            fogOfWarLabel.Visible = false;
            fogOfWarDropdown.Visible = false;
            doneButton.Visible = false;
            workingLabel.Visible = false;
        }

        public void PrepTable()
        {
            try
            {
                if (displayGrid.Rows[5].Cells[6].Style.BackColor == Color.Blue)
                {

                }
            }
            catch
            {
                for (int x = 0; x < 60; x++)
                {
                    displayGrid.Columns.Add(String.Empty, String.Empty);
                    displayGrid.Columns[x].Width = 11;
                    displayGrid.Rows.Add();
                    displayGrid.Rows[x].Height = 11;
                }
            }
        }

        public void PrintMapWindow(char[,] map, int height, int width)
        {
            for (int x = 0; x < height; x++)
            {
                for (int y = 0; y < width; y++)
                {
                    SetCellColor(map, x, y);
                }

            }
            
            ShowMap();
        }

        private void FloorSelectBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox floorSelectBox = sender as ComboBox;
            string newFloor = floorSelectBox.Text;
            if(newFloor == "one")
            {
                SwitchMap(1);
            }
            else if(newFloor == "two")
            {
                SwitchMap(2);
            }
            else if(newFloor == "three")
            {
                SwitchMap(3);
            }
        }

        public void SetCellColor(char[,] map, int x, int y)
        {
            if (map[x, y] == 'X')
            {
                SetCellBlack(x, y);
            }
            else if (map[x, y] == 'E')
            {
                SetCellBlue(x, y);
            }
            else if (map[x, y] == 'S')
            {
                SetCellBlue(x, y);
            }
            else if (map[x, y] == 'D')
            {
                if (displayGrid.Rows[x].Cells[y].Style.BackColor != Color.Blue && displayGrid.Visible == false)
                {
                    SetCurrentDoor(x, y);
                }
                else
                {
                    SetCellBlue(x, y);
                }
            }else if(map[x, y] == 'H')
            {
                SetCellGreen(x, y);
            }else if(map[x, y] == 'L')
            {
                SetCellPurple(x, y);
            }
        }

        public void SetCurrentDoor(int x, int y)
        {
            currentDoor[0] = x;
            currentDoor[1] = y;
            SetCellRed(x, y);
        }

        public void SetCellRed(int x, int y)
        {
            displayGrid.Rows[x].Cells[y].Style.BackColor = Color.Red;
            displayGrid.Rows[x].Cells[y].Style.SelectionBackColor = Color.Red;
        }

        public void SetCellBlue(int y, int x)
        {
            displayGrid.Rows[y].Cells[x].Style.BackColor = Color.Blue;
            displayGrid.Rows[y].Cells[x].Style.SelectionBackColor = Color.Blue;
        }

        public void SetCellGreen(int y, int x)
        {
            displayGrid.Rows[y].Cells[x].Style.BackColor = Color.Green;
            displayGrid.Rows[y].Cells[x].Style.SelectionBackColor = Color.Green;
        }

        public void SetCellPurple(int y, int x)
        {
            displayGrid.Rows[y].Cells[x].Style.BackColor = Color.Purple;
            displayGrid.Rows[y].Cells[x].Style.SelectionBackColor = Color.Purple;
        }

        public void SetCellBlack(int y, int x)
        {
            displayGrid.Rows[y].Cells[x].Style.BackColor = Color.Black;
            displayGrid.Rows[y].Cells[x].Style.SelectionBackColor = Color.Black;
        }

        public void ShowMap()
        {
            ShowSecondaryUI();
            
            if (fogOfWarOn)
            {
                for (int x = 0; x < 60; x++)
                {
                    for (int y = 0; y < 60; y++)
                    {               //fog of war over everything
                        displayGrid.Rows[x].Cells[y].Style.BackColor = SystemColors.Control;
                        displayGrid.Rows[x].Cells[y].Style.SelectionBackColor = SystemColors.Control;
                    }
                }

                UpdateFogOfWar();
            }

            displayGrid.Visible = true;

        }

        public void ShowSecondaryUI()
        {
            workingLabel.Visible = false;
            legendLabel.Visible = true;
            blackLabel.Visible = true;
            blackTextLabel.Visible = true;
            blueLabel.Visible = true;
            blueTextLabel.Visible = true;
            greenLabel.Visible = true;
            greenTextLabel.Visible = true;
            purpleLabel.Visible = true;
            purpleTextLabel.Visible = true;
            redLabel.Visible = true;
            redTextLabel.Visible = true;
            floorMessage.Visible = true;
            floorMaxLabel.Visible = true;
            saveButton.Visible = true;
            //floorSelectBox.Visible = true;    for troubleshooting
        }


        public void SwitchMap(int floor)
        {

            if(fogOfWarOn == true && fogOfWarHarsh == false)
            {
                SaveFogInternal();
            }

            for (int x = 0; x < 60; x++)
            {
                for (int y = 0; y < 60; y++)
                {
                    displayGrid.Rows[x].Cells[y].Style.BackColor = SystemColors.Control;
                    displayGrid.Rows[x].Cells[y].Style.SelectionBackColor = SystemColors.Control;
                }
            }

            int prevFloor = currentFloor;
            if (floor == 1)
            {
                currentFloor = 1;
                bool foundStairs = false;
                for (int x = 0; x < Program.map1.GetLength(0); x++)
                {
                    for(int y = 0; y < Program.map1.GetLength(1); y++)
                    {
                        if(Program.map1[x, y] == 'H')
                        {
                            currentDoor[0] = x;
                            currentDoor[1] = y;
                            foundStairs = true;
                            break;
                        }    
                    }
                    if (foundStairs)
                    {
                        break;
                    }
                }
                PrintMapWindow(Program.map1, Program.map1.GetLength(0), Program.map1.GetLength(1));
                SetCellColor(Program.map1, currentDoor[0], currentDoor[1]);
                floorMessage.Text = "Floor 1";
            }else if(floor == 2)
            {
                try
                {
                    currentFloor = 2;
                    bool foundStairs = false;
                    for (int x = 0; x < Program.map2.GetLength(0); x++)
                    {
                        for (int y = 0; y < Program.map2.GetLength(1); y++)
                        {
                            if (currentFloor > prevFloor && Program.map2[x, y] == 'L')
                            {
                                currentDoor[0] = x;
                                currentDoor[1] = y;
                                foundStairs = true;
                                break;
                            }else if(currentFloor < prevFloor && Program.map2[x, y] == 'H')
                            {
                                currentDoor[0] = x;
                                currentDoor[1] = y;
                                foundStairs = true;
                                break;
                            }
                        }
                        if (foundStairs)
                        {
                            break;
                        }
                    }
                    PrintMapWindow(Program.map2, Program.map2.GetLength(0), Program.map2.GetLength(1));
                    SetCellColor(Program.map2, currentDoor[0], currentDoor[1]);
                    floorMessage.Text = "Floor 2";
                }
                catch
                {
                    currentFloor = prevFloor;
                }
            }
            else if(floor == 3)
            {
                try
                {
                    currentFloor = 3;
                    bool foundStairs = false;
                    for (int x = 0; x < Program.map3.GetLength(0); x++)
                    {
                        for (int y = 0; y < Program.map3.GetLength(1); y++)
                        {
                            if (Program.map3[x, y] == 'L')
                            {
                                currentDoor[0] = x;
                                currentDoor[1] = y;
                                foundStairs = true;
                                break;
                            }
                        }
                        if (foundStairs)
                        {
                            break;
                        }
                    }

                    PrintMapWindow(Program.map3, Program.map3.GetLength(0), Program.map3.GetLength(1));
                    SetCellColor(Program.map3, currentDoor[0], currentDoor[1]);
                    floorMessage.Text = "Floor 3";
                }
                catch
                {
                    currentFloor = prevFloor;
                    floorMessage.Text = ("Error: returning to floor " + currentFloor);
                }
            }

            if (fogOfWarOn == true && fogOfWarHarsh == false)
            {

                if (currentFloor == 2)
                {
                    if (floor2Fog[0, 0] != null)
                    {
                        LoadFogInternal();
                    }
                }
                else if (currentFloor == 3)
                {
                    if (floor3Fog[0, 0] != null)
                    {
                        LoadFogInternal();
                    }
                }
                else
                {
                    LoadFogInternal();
                }
            }
        }

        public void SaveFogInternal()
        {
            if(currentFloor == 1)
            {
                for(int x = 0; x < Program.map1.GetLength(0); x++)
                {
                    for(int y = 0; y < Program.map1.GetLength(1); y++)
                    {
                        floor1Fog[x,y] = displayGrid.Rows[x].Cells[y].Style.BackColor.ToString().Substring(7).TrimEnd(']');
                    }
                }
            }else if(currentFloor == 2)
            {
                for (int x = 0; x < Program.map2.GetLength(0); x++)
                {
                    for (int y = 0; y < Program.map2.GetLength(1); y++)
                    {
                        floor2Fog[x, y] = displayGrid.Rows[x].Cells[y].Style.BackColor.ToString().Substring(7).TrimEnd(']');
                    }
                }
            }
            else
            {
                for (int x = 0; x < Program.map3.GetLength(0); x++)
                {
                    for (int y = 0; y < Program.map3.GetLength(1); y++)
                    {
                        floor3Fog[x, y] = displayGrid.Rows[x].Cells[y].Style.BackColor.ToString().Substring(7).TrimEnd(']');
                    }
                }
            }
        }

        public void LoadFogInternal()
        {
            for (int x = 0; x < 60; x++)
            {
                for (int y = 0; y < 60; y++)
                {
                    displayGrid.Rows[x].Cells[y].Style.BackColor = SystemColors.Control;
                    displayGrid.Rows[x].Cells[y].Style.SelectionBackColor = SystemColors.Control;
                }
            }

            if (currentFloor == 1)
            {
                for (int x = 0; x < Program.map1.GetLength(0); x++)
                {
                    for (int y = 0; y < Program.map1.GetLength(1); y++)
                    {
                        displayGrid.Rows[x].Cells[y].Style.BackColor = Color.FromName(floor1Fog[x, y]);
                    }
                }
            }
            else if (currentFloor == 2)
            {
                for (int x = 0; x < Program.map2.GetLength(0); x++)
                {
                    for (int y = 0; y < Program.map2.GetLength(1); y++)
                    {
                        displayGrid.Rows[x].Cells[y].Style.BackColor = Color.FromName(floor2Fog[x, y]);
                    }
                }
            }
            else
            {
                for (int x = 0; x < Program.map3.GetLength(0); x++)
                {
                    for (int y = 0; y < Program.map3.GetLength(1); y++)
                    {
                        displayGrid.Rows[x].Cells[y].Style.BackColor = Color.FromName(floor3Fog[x, y]);
                    }
                }
            }
        }

        private void CellClicked(object sender, DataGridViewCellEventArgs e)
        {
            if(displayGrid.CurrentCell.Style.BackColor == Color.Blue)
            {
                int newDoorRow = displayGrid.CurrentCell.OwningRow.Index;
                int newDoorColumn = displayGrid.CurrentCell.OwningColumn.Index;

                int[] previousDoor = new int[2];
                previousDoor[0] = currentDoor[0];
                previousDoor[1] = currentDoor[1];

                bool exteriorDoor = false;
                if(currentFloor == 1)
                {
                    if(Program.map1[newDoorRow,newDoorColumn] == 'D')
                    {
                        exteriorDoor = true;
                    }
                }

                if (!exteriorDoor)
                {
                    SetCurrentDoor(newDoorRow, newDoorColumn);
                    if (displayGrid.Rows[previousDoor[0]].Cells[previousDoor[1]].Style.BackColor == Color.Red)
                    {
                        SetCellBlue(previousDoor[0], previousDoor[1]);
                    }
                    UpdateFogOfWar();
                }

            }else if(displayGrid.CurrentCell.Style.BackColor == Color.Green)
            {
                SwitchMap(currentFloor + 1);
            }else if(displayGrid.CurrentCell.Style.BackColor == Color.Purple)
            {
                SwitchMap(currentFloor - 1);
            }
        }

       public void UpdateFogOfWar()
        {
            if (fogOfWarOn)
            {
                char[,] mapToUpdate;
                if (currentFloor == 1)
                {
                    mapToUpdate = Program.map1;
                }
                else if (currentFloor == 2)
                {
                    mapToUpdate = Program.map2;
                }
                else
                {
                    mapToUpdate = Program.map3;
                }

                if (fogOfWarHarsh)
                {
                    for(int x = 0; x < 60; x++)
                    {
                        for(int y = 0; y < 60; y++)
                        {
                            displayGrid.Rows[x].Cells[y].Style.BackColor = SystemColors.Control;
                            displayGrid.Rows[x].Cells[y].Style.SelectionBackColor = SystemColors.Control;
                        }
                    }
                    
                    
                }

                try
                {
                    if (mapToUpdate[currentDoor[0] - 1, currentDoor[1]] == ' ' || mapToUpdate[currentDoor[0] - 1, currentDoor[1]] == 'H' || mapToUpdate[currentDoor[0] - 1, currentDoor[1]] == 'L')
                    {
                        int[] currentSpace = new int[2];
                        currentSpace[0] = currentDoor[0] - 1;
                        currentSpace[1] = currentDoor[1];
                        bool verticalWallHit = false;
                        bool leftWallHit = false;
                        bool rightWallHit = false;
                        int leftAdjust = 0;
                        int rightAdjust = 0;

                        while (!leftWallHit)
                        {
                            SetCellColor(mapToUpdate, currentSpace[0], currentSpace[1]);
                            leftAdjust++;
                            if (mapToUpdate[currentSpace[0], currentSpace[1] - leftAdjust] != ' ' && mapToUpdate[currentSpace[0], currentSpace[1] - leftAdjust] != 'H' && mapToUpdate[currentSpace[0], currentSpace[1] - leftAdjust] != 'L')
                            {
                                SetCellColor(mapToUpdate, currentSpace[0], currentSpace[1] - leftAdjust);
                                leftWallHit = true;
                            }
                        }
                        
                        while (!rightWallHit)
                        {
                            SetCellColor(mapToUpdate, currentSpace[0], currentSpace[1] + rightAdjust);
                            rightAdjust++;
                            if (mapToUpdate[currentSpace[0], currentSpace[1] + rightAdjust] != ' ' && mapToUpdate[currentSpace[0], currentSpace[1] + rightAdjust] != 'H' && mapToUpdate[currentSpace[0], currentSpace[1] + rightAdjust] != 'L')
                            {
                                SetCellColor(mapToUpdate, currentSpace[0], currentSpace[1] + rightAdjust);
                                rightWallHit = true;
                            }
                        }

                        for (int x = 0; x <= leftAdjust; x++)
                        {
                            SetCellColor(mapToUpdate, currentSpace[0] + 1, currentSpace[1] - x);
                        }

                        for (int y = 1; y <= rightAdjust; y++)
                        {
                            SetCellColor(mapToUpdate, currentSpace[0] + 1, currentSpace[1] + y);
                        }

                        while (!verticalWallHit)
                        {
                            currentSpace[0]--;
                            if(mapToUpdate[currentSpace[0], currentSpace[1]] != ' ' && mapToUpdate[currentSpace[0], currentSpace[1]] != 'H' && mapToUpdate[currentSpace[0], currentSpace[1]] != 'L')
                            {
                                verticalWallHit = true;
                            }
                            for (int x = 0; x <= leftAdjust; x++)
                            {
                                SetCellColor(mapToUpdate, currentSpace[0], currentSpace[1] - x);
                            }

                            for(int y = 1; y <= rightAdjust; y++)
                            {
                                SetCellColor(mapToUpdate, currentSpace[0], currentSpace[1] + y);
                            }
                        }

                    }
                }
                catch (NullReferenceException e)
                {
                    //Console.WriteLine("{0}: {1}", e.GetType().Name, e.Message);
                    //Console.WriteLine(e.Source + "||||" + e.StackTrace);
                }
                catch
                {
                    //Console.WriteLine("Invalid space type 1 at " + currentDoor[0] + "," + currentDoor[1]);
                }

                try
                {
                    if (mapToUpdate[currentDoor[0] + 1, currentDoor[1]] == ' ' || mapToUpdate[currentDoor[0] + 1, currentDoor[1]] == 'H' || mapToUpdate[currentDoor[0] + 1, currentDoor[1]] == 'L')
                    {
                        int[] currentSpace = new int[2];
                        currentSpace[0] = currentDoor[0] + 1;
                        currentSpace[1] = currentDoor[1];
                        bool verticalWallHit = false;
                        bool leftWallHit = false;
                        bool rightWallHit = false;
                        int leftAdjust = 0;
                        int rightAdjust = 0;

                        while (!leftWallHit)
                        {
                            SetCellColor(mapToUpdate, currentSpace[0], currentSpace[1] - leftAdjust);
                            leftAdjust++;
                            if (mapToUpdate[currentSpace[0], currentSpace[1] - leftAdjust] != ' ' && mapToUpdate[currentSpace[0], currentSpace[1] - leftAdjust] != 'H' && mapToUpdate[currentSpace[0], currentSpace[1] - leftAdjust] != 'L')
                            {
                                SetCellColor(mapToUpdate, currentSpace[0], currentSpace[1] - leftAdjust);
                                leftWallHit = true;
                            }
                        }

                        while (!rightWallHit)
                        {
                            SetCellColor(mapToUpdate, currentSpace[0], currentSpace[1] + rightAdjust);
                            rightAdjust++;
                            if (mapToUpdate[currentSpace[0], currentSpace[1] + rightAdjust] != ' ' && mapToUpdate[currentSpace[0], currentSpace[1] + rightAdjust] != 'H' && mapToUpdate[currentSpace[0], currentSpace[1] + rightAdjust] != 'L')
                            {
                                SetCellColor(mapToUpdate, currentSpace[0], currentSpace[1] + rightAdjust);
                                rightWallHit = true;
                            }
                        }

                        for (int x = 0; x <= leftAdjust; x++)
                        {
                            SetCellColor(mapToUpdate, currentSpace[0] - 1, currentSpace[1] - x);
                        }

                        for (int y = 1; y <= rightAdjust; y++)
                        {
                            SetCellColor(mapToUpdate, currentSpace[0] - 1, currentSpace[1] + y);
                        }

                        while (!verticalWallHit)
                        {
                            currentSpace[0]++;
                            if (mapToUpdate[currentSpace[0], currentSpace[1]] != ' ' && mapToUpdate[currentSpace[0], currentSpace[1]] != 'H' && mapToUpdate[currentSpace[0], currentSpace[1]] != 'L')
                            {
                                verticalWallHit = true;
                            }
                            for (int x = 0; x <= leftAdjust; x++)
                            {
                                SetCellColor(mapToUpdate, currentSpace[0], currentSpace[1] - x);
                            }

                            for (int y = 1; y <= rightAdjust; y++)
                            {
                                SetCellColor(mapToUpdate, currentSpace[0], currentSpace[1] + y);
                            }
                        }
                    }
                }
                catch (NullReferenceException e)
                {
                    //Console.WriteLine("{0}: {1}", e.GetType().Name, e.Message);
                    //Console.WriteLine(e.Source + "||||" + e.StackTrace);
                }
                catch
                {
                    //Console.WriteLine("Invalid space type 2 at " + currentDoor[0] + "," + currentDoor[1]);
                }

                try
                {
                    if (mapToUpdate[currentDoor[0], currentDoor[1] - 1] == ' ' || mapToUpdate[currentDoor[0], currentDoor[1] - 1] == 'H' || mapToUpdate[currentDoor[0], currentDoor[1] - 1] == 'L')
                    {
                        int[] currentSpace = new int[2];
                        currentSpace[0] = currentDoor[0];
                        currentSpace[1] = currentDoor[1] - 1;
                        bool verticalWallHit = false;
                        bool leftWallHit = false;
                        bool rightWallHit = false;
                        int leftAdjust = 0;
                        int rightAdjust = 0;

                        while (!leftWallHit)
                        {
                            SetCellColor(mapToUpdate, currentSpace[0] - leftAdjust, currentSpace[1]);
                            leftAdjust++;
                            if (mapToUpdate[currentSpace[0] - leftAdjust, currentSpace[1]] != ' ' && mapToUpdate[currentSpace[0] - leftAdjust, currentSpace[1]] != 'H' && mapToUpdate[currentSpace[0] - leftAdjust, currentSpace[1]] != 'L')
                            {
                                SetCellColor(mapToUpdate, currentSpace[0] - leftAdjust, currentSpace[1]);
                                leftWallHit = true;
                            }
                        }

                        while (!rightWallHit)
                        {
                            SetCellColor(mapToUpdate, currentSpace[0] + rightAdjust, currentSpace[1]);
                            rightAdjust++;
                            if (mapToUpdate[currentSpace[0] + rightAdjust, currentSpace[1]] != ' ' && mapToUpdate[currentSpace[0] + rightAdjust, currentSpace[1]] != 'H' && mapToUpdate[currentSpace[0] + rightAdjust, currentSpace[1]] != 'L')
                            {
                                SetCellColor(mapToUpdate, currentSpace[0] + rightAdjust, currentSpace[1]);
                                rightWallHit = true;
                            }
                        }

                        for (int x = 0; x <= leftAdjust; x++)
                        {
                            SetCellColor(mapToUpdate, currentSpace[0] - x, currentSpace[1] + 1);
                        }

                        for (int y = 1; y <= rightAdjust; y++)
                        {
                            SetCellColor(mapToUpdate, currentSpace[0] + y, currentSpace[1] + 1);
                        }

                        while (!verticalWallHit)
                        {
                            currentSpace[1]--;
                            if (mapToUpdate[currentSpace[0], currentSpace[1]] != ' ' && mapToUpdate[currentSpace[0], currentSpace[1]] != 'H' && mapToUpdate[currentSpace[0], currentSpace[1]] != 'L')
                            {
                                verticalWallHit = true;
                            }
                            for (int x = 0; x <= leftAdjust; x++)
                            {
                                SetCellColor(mapToUpdate, currentSpace[0] - x, currentSpace[1]);
                            }

                            for (int y = 1; y <= rightAdjust; y++)
                            {
                                SetCellColor(mapToUpdate, currentSpace[0] + y, currentSpace[1]);
                            }
                        }
                    }
                }
                catch(NullReferenceException e)
                {
                    //Console.WriteLine("{0}: {1}", e.GetType().Name, e.Message);
                    //Console.WriteLine(e.Source + "||||" + e.StackTrace);
                }
                catch
                {
                    //Console.WriteLine("Invalid space type 3 at " + currentDoor[0] + "," + currentDoor[1]);
                }

                try
                {
                    if (mapToUpdate[currentDoor[0], currentDoor[1] + 1] == ' ' || mapToUpdate[currentDoor[0], currentDoor[1] + 1] == 'H' || mapToUpdate[currentDoor[0], currentDoor[1] + 1] == 'L')
                    {
                        int[] currentSpace = new int[2];
                        currentSpace[0] = currentDoor[0];
                        currentSpace[1] = currentDoor[1] + 1;
                        bool verticalWallHit = false;
                        bool leftWallHit = false;
                        bool rightWallHit = false;
                        int leftAdjust = 0;
                        int rightAdjust = 0;

                        while (!leftWallHit)
                        {
                            SetCellColor(mapToUpdate, currentSpace[0] - leftAdjust, currentSpace[1]);
                            leftAdjust++;
                            if (mapToUpdate[currentSpace[0] - leftAdjust, currentSpace[1]] != ' ' && mapToUpdate[currentSpace[0] - leftAdjust, currentSpace[1]] != 'H' && mapToUpdate[currentSpace[0] - leftAdjust, currentSpace[1]] != 'L')
                            {
                                SetCellColor(mapToUpdate, currentSpace[0] - leftAdjust, currentSpace[1]);
                                leftWallHit = true;
                            }
                        }

                        while (!rightWallHit)
                        {
                            SetCellColor(mapToUpdate, currentSpace[0] + rightAdjust, currentSpace[1]);
                            rightAdjust++;
                            if (mapToUpdate[currentSpace[0] + rightAdjust, currentSpace[1]] != ' ' && mapToUpdate[currentSpace[0] + rightAdjust, currentSpace[1]] != 'H' && mapToUpdate[currentSpace[0] + rightAdjust, currentSpace[1]] != 'L')
                            {
                                SetCellColor(mapToUpdate, currentSpace[0] + rightAdjust, currentSpace[1]);
                                rightWallHit = true;
                            }
                        }

                        for (int x = 0; x <= leftAdjust; x++)
                        {
                            SetCellColor(mapToUpdate, currentSpace[0] - x, currentSpace[1] - 1);
                        }

                        for (int y = 1; y <= rightAdjust; y++)
                        {
                            SetCellColor(mapToUpdate, currentSpace[0] + y, currentSpace[1] - 1);
                        }

                        while (!verticalWallHit)
                        {
                            currentSpace[1]++;
                            if (mapToUpdate[currentSpace[0], currentSpace[1]] != ' ' && mapToUpdate[currentSpace[0], currentSpace[1]] != 'H' && mapToUpdate[currentSpace[0], currentSpace[1]] != 'L')
                            {
                                verticalWallHit = true;
                            }
                            for (int x = 0; x <= leftAdjust; x++)
                            {
                                SetCellColor(mapToUpdate, currentSpace[0] - x, currentSpace[1]);
                            }

                            for (int y = 1; y <= rightAdjust; y++)
                            {
                                SetCellColor(mapToUpdate, currentSpace[0] + y, currentSpace[1]);
                            }
                        }
                    }
                }
                catch (NullReferenceException e)
                {
                    //Console.WriteLine("{0}: {1}", e.GetType().Name, e.Message);
                    //Console.WriteLine(e.Source + "||||" + e.StackTrace);
                }
                catch
                {
                    //Console.WriteLine("Invalid space type 4 at " + currentDoor[0] + "," + currentDoor[1]);
                }

                SetCellRed(currentDoor[0], currentDoor[1]);
            }
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            LoadForm LoadDialogue = new LoadForm();
            LoadDialogue.ShowDialog();

            if (successfulLoad)
            {
                HideStartingUI();
                PrepTable();
                if(fogOfWarOn && !fogOfWarHarsh)
                {
                    LoadFogInternal();
                    ShowSecondaryUI();
                }
                else
                {
                    SwitchMap(currentFloor);
                }
                displayGrid.Visible = true;

                if (currentFloor == 1)
                {
                    floorMessage.Text = "Floor 1";
                }else if(currentFloor == 2)
                {
                    floorMessage.Text = "Floor 2";
                }
                else
                {
                    floorMessage.Text = "Floor 3";
                }

                if (numberOfFloors == 1)
                {
                    floorMaxLabel.Text = "of 1";
                } else if (numberOfFloors == 2)
                {
                    floorMaxLabel.Text = "of 2";
                }
                else
                {
                    floorMaxLabel.Text = "of 3";
                }

                successfulLoad = false;
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            SaveFogInternal();
            SaveForm SaveDialogue = new SaveForm();
            SaveDialogue.ShowDialog();
        }
    }
}
