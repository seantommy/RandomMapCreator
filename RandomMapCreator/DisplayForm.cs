﻿using System;
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
        public static string[,] floor1Fog = new string[60, 60];
        public static string[,] floor2Fog = new string[60, 60];
        public static string[,] floor3Fog = new string[60, 60];
        public static bool successfulLoad = false;

        public DisplayForm()
        {
            InitializeComponent();
        }

        private void RoomSizeDropdown_Set(object sender, EventArgs e)
        {
            ComboBox sizeSelectBox = sender as ComboBox;
            string sizeSelected = sizeSelectBox.Text;

            if (sizeSelected == "small")
            {
                Program.roomSizeModifier = .3;
            }
            else if (sizeSelected == "average")
            {
                Program.roomSizeModifier = .2;
            }
            else if (sizeSelected == "large")
            {
                Program.roomSizeModifier = .1;
            }
        }

        private void NumberOfFloorsDropdown_Set(object sender, EventArgs e)
        {
            ComboBox numberOfFloorsBox = sender as ComboBox;
            string newFloor = numberOfFloorsBox.Text;
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

            if (fogOfWarSetting == "No Fog of War")
            {
                fogOfWarOn = false;
                fogOfWarHarsh = false;
            }
            else if (fogOfWarSetting == "All cleared rooms visible")
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
            int width = GetWidthInput();
            int height = GetHeightInput();

            if (height != 0 && width != 0) //0 means invalid input
            {
                Random rng = new Random();

                HideStartingUI();
                PrepTable();

                if (numberOfFloors == 0)
                {
                    numberOfFloors = rng.Next(1, 4);
                }
                floorMaxLabel.Text = "of " + numberOfFloors;
                if (height == 1) //1 means it was left blank
                {
                    height = rng.Next(10, 60);
                }

                if (width == 1) //1 means it was left blank
                {
                    width = rng.Next(10, 60);
                }

                GenerateMaps(height, width);
                PrintMapWindow(Program.map1, Program.map1.GetLength(0), Program.map1.GetLength(1));
            }
        }

        private int GetHeightInput()
        {
            int height = 1;

            if (heightInput.Text != "")
            {
                try
                {
                    height = int.Parse(heightInput.Text);
                    if (height < 10 || height > 60)
                    {
                        height = 0;
                        workingLabel.Text = "Please input a whole number from 10 to 60 for height and width.";
                        workingLabel.ForeColor = Color.Red;
                        workingLabel.Visible = true;
                    }
                }
                catch
                {
                    height = 0;
                    workingLabel.Text = "Please input a whole number from 10 to 60 for height and width.";
                    workingLabel.ForeColor = Color.Red;
                    workingLabel.Visible = true;
                }
            }

            return height;
        }

        private int GetWidthInput()
        {
            int width = 1;

            if (widthInput.Text != "")
            {
                try
                {
                    width = int.Parse(widthInput.Text);
                    if (width < 10 || width > 60)
                    {
                        width = 0;
                        workingLabel.Text = "Please input a whole number from 10 to 60 for width and height.";
                        workingLabel.ForeColor = Color.Red;
                        workingLabel.Visible = true;
                    }
                }
                catch
                {
                    width = 0;
                    workingLabel.Text = "Please input a whole number from 10 to 60 for width and height.";
                    workingLabel.ForeColor = Color.Red;
                    workingLabel.Visible = true;
                }
            }

            return width;
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
            if (displayGrid.Rows.Count < 5)
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

        private void GenerateMaps(int height, int width)
        {
            Program.GenerateMap(height, width, this, 1);

            if (numberOfFloors > 1)
            {
                Program.GenerateMap(height, width, this, 2);
            }
            if (numberOfFloors > 2)
            {
                Program.GenerateMap(height, width, this, 3);
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

        public void SetCellColor(char[,] map, int x, int y)
        {
            switch (map[x, y])
            {
                case 'X':
                    SetCellBlack(x, y);
                    break;
                case 'E':
                    SetCellBlue(x, y);
                    break;
                case 'S':
                    SetCellBlue(x, y);
                    break;
                case 'D':
                    if (displayGrid.Rows[x].Cells[y].Style.BackColor != Color.Blue && displayGrid.Visible == false)
                        SetCurrentDoor(x, y);
                    else
                        SetCellBlue(x, y);
                    break;
                case 'H':
                    SetCellGreen(x, y);
                    break;
                case 'L':
                    SetCellPurple(x, y);
                    break;
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
        }

        public void SwitchMap(int floor)
        {
            if (fogOfWarOn == true && fogOfWarHarsh == false)
            {
                SaveFogInternal();
            }

            ClearDisplayGrid();
            GetStairCoordinates(floor);

            if (floor == 1)
            {
                PrintMapWindow(Program.map1, Program.map1.GetLength(0), Program.map1.GetLength(1));
                SetCellColor(Program.map1, currentDoor[0], currentDoor[1]);
            }
            else if (floor == 2)
            {
                PrintMapWindow(Program.map2, Program.map2.GetLength(0), Program.map2.GetLength(1));
                SetCellColor(Program.map2, currentDoor[0], currentDoor[1]);
            }
            else if (floor == 3)
            {

                PrintMapWindow(Program.map3, Program.map3.GetLength(0), Program.map3.GetLength(1));
                SetCellColor(Program.map3, currentDoor[0], currentDoor[1]);
            }
            floorMessage.Text = "Floor " + floor;

            if (fogOfWarOn == true && fogOfWarHarsh == false)
            {

                if (currentFloor == 1)
                {
                    LoadFogInternal();
                }
                else if (currentFloor == 2 && floor2Fog[0, 0] != null)
                {
                    LoadFogInternal();
                }
                else if (currentFloor == 3 && floor3Fog[0, 0] != null)
                {
                    LoadFogInternal();
                }
            }
        }

        public void SaveFogInternal()
        {
            if (currentFloor == 1)
            {
                for (int x = 0; x < Program.map1.GetLength(0); x++)
                {
                    for (int y = 0; y < Program.map1.GetLength(1); y++)
                    {
                        floor1Fog[x, y] = displayGrid.Rows[x].Cells[y].Style.BackColor.ToString().Substring(7).TrimEnd(']');
                    }
                }
            }
            else if (currentFloor == 2)
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

        private void ClearDisplayGrid()
        {
            for (int x = 0; x < 60; x++)
            {
                for (int y = 0; y < 60; y++)
                {
                    displayGrid.Rows[x].Cells[y].Style.BackColor = SystemColors.Control;
                    displayGrid.Rows[x].Cells[y].Style.SelectionBackColor = SystemColors.Control;
                }
            }
        }

        private void GetStairCoordinates(int newFloor)
        {
            int previousFloor = currentFloor;
            currentFloor = newFloor;
            char[,] map;

            if (currentFloor == 1)
            {
                map = Program.map1;
            }
            else if (currentFloor == 2)
            {
                map = Program.map2;
            }
            else
            {
                map = Program.map3;
            }

            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    if (currentFloor > previousFloor && map[x, y] == 'L')
                    {
                        currentDoor[0] = x;
                        currentDoor[1] = y;
                        return;
                    }
                    else if (currentFloor < previousFloor && map[x, y] == 'H')
                    {
                        currentDoor[0] = x;
                        currentDoor[1] = y;
                        return;
                    }
                }
            }
        }

        public void LoadFogInternal()
        {
            char[,] map;
            string[,] fog;

            ClearDisplayGrid();

            if (currentFloor == 1)
            {
                map = Program.map1;
                fog = floor1Fog;
            }
            else if (currentFloor == 2)
            {
                map = Program.map2;
                fog = floor2Fog;
            }
            else
            {
                map = Program.map3;
                fog = floor3Fog;
            }

            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    displayGrid.Rows[x].Cells[y].Style.BackColor = Color.FromName(fog[x, y]);
                }
            }
        }

        private void CellClicked(object sender, DataGridViewCellEventArgs e)
        {
            if (displayGrid.CurrentCell.Style.BackColor == Color.Blue)
            {
                ActivateDoor();
            }
            else if (displayGrid.CurrentCell.Style.BackColor == Color.Green)
            {
                SwitchMap(currentFloor + 1);
            }
            else if (displayGrid.CurrentCell.Style.BackColor == Color.Purple)
            {
                SwitchMap(currentFloor - 1);
            }
        }

        private void ActivateDoor()
        {
            int newDoorRow = displayGrid.CurrentCell.OwningRow.Index;
            int newDoorColumn = displayGrid.CurrentCell.OwningColumn.Index;

            int[] previousDoor = new int[2];
            previousDoor[0] = currentDoor[0];
            previousDoor[1] = currentDoor[1];

            if (currentFloor != 1 || Program.map1[newDoorRow, newDoorColumn] != 'D')
            {
                SetCurrentDoor(newDoorRow, newDoorColumn);
                if (displayGrid.Rows[previousDoor[0]].Cells[previousDoor[1]].Style.BackColor == Color.Red)
                {
                    SetCellBlue(previousDoor[0], previousDoor[1]);
                }
                UpdateFogOfWar();
            }
        }

        public void UpdateFogOfWar()
        {
            char[,] mapToUpdate;

            if (!fogOfWarOn)
            {
                return;
            }

            if (fogOfWarHarsh)
            {
                ClearDisplayGrid();
            }

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

            if (IsSpaceAccessible(mapToUpdate, currentDoor[0] - 1, currentDoor[1]))
            {
                int currentX = currentDoor[0] - 1;
                int currentY = currentDoor[1];
                bool verticalWallHit = false;

                int[] leftAndRightAdjust = RevealLeftAndRightAtY(mapToUpdate, currentX, currentY);

                while (!verticalWallHit)
                {
                    currentX--;
                    verticalWallHit = RevealLayerByY(mapToUpdate, currentX, currentY, leftAndRightAdjust);
                }
            }

            if (IsSpaceAccessible(mapToUpdate, currentDoor[0] + 1, currentDoor[1]))
            {
                int currentX = currentDoor[0] + 1;
                int currentY = currentDoor[1];
                bool verticalWallHit = false;

                int[] leftAndRightAdjust = RevealLeftAndRightAtY(mapToUpdate, currentX, currentY);

                while (!verticalWallHit)
                {
                    currentX++;
                    verticalWallHit = RevealLayerByY(mapToUpdate, currentX, currentY, leftAndRightAdjust);
                }
            }

            if (IsSpaceAccessible(mapToUpdate, currentDoor[0], currentDoor[1] - 1))
            {
                int currentX = currentDoor[0];
                int currentY = currentDoor[1] - 1;
                bool verticalWallHit = false;

                int[] leftAndRightAdjust = RevealLeftAndRightAtX(mapToUpdate, currentX, currentY);

                while (!verticalWallHit)
                {
                    currentY--;
                    verticalWallHit = RevealLayerByX(mapToUpdate, currentX, currentY, leftAndRightAdjust);
                }
            }

            if (IsSpaceAccessible(mapToUpdate, currentDoor[0], currentDoor[1] + 1))
            {
                int currentX = currentDoor[0];
                int currentY = currentDoor[1] + 1;
                bool verticalWallHit = false;

                int[] leftAndRightAdjust = RevealLeftAndRightAtX(mapToUpdate, currentX, currentY);

                while (!verticalWallHit)
                {
                    currentY++;
                    verticalWallHit = RevealLayerByX(mapToUpdate, currentX, currentY, leftAndRightAdjust);
                }
            }

            SetCellRed(currentDoor[0], currentDoor[1]);
        }

        private bool IsSpaceAccessible(char[,] mapToUpdate, int x, int y)
        {
            if (x < 0 || x >= mapToUpdate.GetLength(0) || y < 0 || y >= mapToUpdate.GetLength(1))
            {
                return false;
            }
            if (mapToUpdate[x, y] == ' ' || mapToUpdate[x, y] == 'H' || mapToUpdate[x, y] == 'L')
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private int[] RevealLeftAndRightAtY(char[,] mapToUpdate, int currentX, int currentY)
        {
            bool leftWallHit = false;
            bool rightWallHit = false;
            int leftAdjust = 0;
            int rightAdjust = 0;

            while (!leftWallHit)
            {
                SetCellColor(mapToUpdate, currentX, currentY);
                leftAdjust++;
                if (mapToUpdate[currentX, currentY - leftAdjust] != ' ' && mapToUpdate[currentX, currentY - leftAdjust] != 'H' && mapToUpdate[currentX, currentY - leftAdjust] != 'L')
                {
                    SetCellColor(mapToUpdate, currentX, currentY - leftAdjust);
                    leftWallHit = true;
                }
            }

            while (!rightWallHit)
            {
                SetCellColor(mapToUpdate, currentX, currentY + rightAdjust);
                rightAdjust++;
                if (mapToUpdate[currentX, currentY + rightAdjust] != ' ' && mapToUpdate[currentX, currentY + rightAdjust] != 'H' && mapToUpdate[currentX, currentY + rightAdjust] != 'L')
                {
                    SetCellColor(mapToUpdate, currentX, currentY + rightAdjust);
                    rightWallHit = true;
                }
            }

            for (int i = 0; i <= leftAdjust; i++)
            {
                SetCellColor(mapToUpdate, currentDoor[0], currentY - i);
            }

            for (int j = 1; j <= rightAdjust; j++)
            {
                SetCellColor(mapToUpdate, currentDoor[0], currentY + j);
            }

            return new int[] { leftAdjust, rightAdjust };
        }

        private int[] RevealLeftAndRightAtX(char[,] mapToUpdate, int currentX, int currentY)
        {
            bool leftWallHit = false;
            bool rightWallHit = false;
            int leftAdjust = 0;
            int rightAdjust = 0;

            while (!leftWallHit)
            {
                SetCellColor(mapToUpdate, currentX, currentY);
                leftAdjust++;
                if (mapToUpdate[currentX - leftAdjust, currentY] != ' ' && mapToUpdate[currentX - leftAdjust, currentY] != 'H' && mapToUpdate[currentX - leftAdjust, currentY] != 'L')
                {
                    SetCellColor(mapToUpdate, currentX - leftAdjust, currentY);
                    leftWallHit = true;
                }
            }

            while (!rightWallHit)
            {
                SetCellColor(mapToUpdate, currentX + rightAdjust, currentY);
                rightAdjust++;
                if (mapToUpdate[currentX + rightAdjust, currentY] != ' ' && mapToUpdate[currentX + rightAdjust, currentY] != 'H' && mapToUpdate[currentX + rightAdjust, currentY] != 'L')
                {
                    SetCellColor(mapToUpdate, currentX + rightAdjust, currentY);
                    rightWallHit = true;
                }
            }

            for (int i = 0; i <= leftAdjust; i++)
            {
                SetCellColor(mapToUpdate, currentX - i, currentDoor[1]);
            }

            for (int j = 1; j <= rightAdjust; j++)
            {
                SetCellColor(mapToUpdate, currentX + j, currentDoor[1]);
            }

            return new int[] { leftAdjust, rightAdjust };
        }

        private bool RevealLayerByY(char[,] mapToUpdate, int currentX, int currentY, int[] leftAndRightAdjust)
        {
            int leftAdjust = leftAndRightAdjust[0];
            int rightAdjust = leftAndRightAdjust[1];

            for (int i = 0; i <= leftAdjust; i++)
            {
                SetCellColor(mapToUpdate, currentX, currentY - i);
            }

            for (int j = 1; j <= rightAdjust; j++)
            {
                SetCellColor(mapToUpdate, currentX, currentY + j);
            }

            if (mapToUpdate[currentX, currentY] != ' ' && mapToUpdate[currentX, currentY] != 'H' && mapToUpdate[currentX, currentY] != 'L')
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool RevealLayerByX(char[,] mapToUpdate, int currentX, int currentY, int[] leftAndRightAdjust)
        {
            int leftAdjust = leftAndRightAdjust[0];
            int rightAdjust = leftAndRightAdjust[1];

            for (int i = 0; i <= leftAdjust; i++)
            {
                SetCellColor(mapToUpdate, currentX - i, currentY);
            }

            for (int j = 1; j <= rightAdjust; j++)
            {
                SetCellColor(mapToUpdate, currentX + j, currentY);
            }

            if (mapToUpdate[currentX, currentY] != ' ' && mapToUpdate[currentX, currentY] != 'H' && mapToUpdate[currentX, currentY] != 'L')
            {
                return true;
            }
            else
            {
                return false;
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
                if (fogOfWarOn && !fogOfWarHarsh)
                {
                    LoadFogInternal();
                    ShowSecondaryUI();
                }
                else
                {
                    SwitchMap(currentFloor);
                }

                floorMessage.Text = "Floor " + currentFloor;
                floorMaxLabel.Text = "of " + numberOfFloors;
                displayGrid.Visible = true;
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
