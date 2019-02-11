using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MapGenerator
{
    public class MapDisplay
    {
        public DataGridView displayGrid;
        public int NumberOfFloors { get; set; }
        public int CurrentFloor { get; set; }
        public int[] CurrentDoor{ get; private set; }

        public MapDisplay(DataGridView newGrid)
        {
            displayGrid = newGrid;
            NumberOfFloors = 0;
            CurrentFloor = 1;
            CurrentDoor = new int[2];
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

        public void PrintMapWindow(Map map)
        {
            for (int x = 0; x < map.Height; x++)
            {
                for (int y = 0; y < map.Width; y++)
                {
                    SetCellColor(map, x, y);
                }
            }

            ShowMap();
        }

        private void ShowMap()
        {
            if (FogOfWar.fogOfWarOn)
            {
                ClearDisplayGrid();
                FogOfWar.UpdateFogOfWar(this);
            }

            displayGrid.Visible = true;

        }

        public void ClearDisplayGrid()
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

        public void SwitchMap(int floor)
        {
            FogOfWar.SaveFogInternal(this);
            ClearDisplayGrid();
            GetStairCoordinates(floor);

            if (floor == 1)
            {
                PrintMapWindow(Program.map1);
                SetCellColor(Program.map1, CurrentDoor[0], CurrentDoor[1]);
            }
            else if (floor == 2)
            {
                PrintMapWindow(Program.map2);
                SetCellColor(Program.map2, CurrentDoor[0], CurrentDoor[1]);
            }
            else if (floor == 3)
            {

                PrintMapWindow(Program.map3);
                SetCellColor(Program.map3, CurrentDoor[0], CurrentDoor[1]);
            }

            FogOfWar.LoadFogInternal(this);
        }

        private void GetStairCoordinates(int newFloor)
        {
            int previousFloor = CurrentFloor;
            CurrentFloor = newFloor;
            Map map;

            if (CurrentFloor == 1)
            {
                map = Program.map1;
            }
            else if (CurrentFloor == 2)
            {
                map = Program.map2;
            }
            else
            {
                map = Program.map3;
            }

            for (int x = 0; x < map.Height; x++)
            {
                for (int y = 0; y < map.Width; y++)
                {
                    if (CurrentFloor > previousFloor && map.Contents[x, y] == 'L')
                    {
                        CurrentDoor[0] = x;
                        CurrentDoor[1] = y;
                        return;
                    }
                    else if (CurrentFloor < previousFloor && map.Contents[x, y] == 'H')
                    {
                        CurrentDoor[0] = x;
                        CurrentDoor[1] = y;
                        return;
                    }
                }
            }
        }

        public void SetCurrentDoor(int x, int y)
        {
            CurrentDoor[0] = x;
            CurrentDoor[1] = y;
            SetCellRed(x, y);
        }

        public void SetCellColor(Map map, int x, int y)
        {
            switch (map.Contents[x, y])
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

        public void ActivateDoor()
        {
            int newDoorRow = displayGrid.CurrentCell.OwningRow.Index;
            int newDoorColumn = displayGrid.CurrentCell.OwningColumn.Index;

            int[] previousDoor = new int[2];
            previousDoor[0] = CurrentDoor[0];
            previousDoor[1] = CurrentDoor[1];

            if (CurrentFloor != 1 || Program.map1.Contents[newDoorRow, newDoorColumn] != 'D')
            {
                SetCurrentDoor(newDoorRow, newDoorColumn);
                if (displayGrid.Rows[previousDoor[0]].Cells[previousDoor[1]].Style.BackColor == Color.Red)
                {
                    SetCellBlue(previousDoor[0], previousDoor[1]);
                }
                FogOfWar.UpdateFogOfWar(this);
            }
        }
    }
}
