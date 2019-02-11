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
    public partial class UI : Form
    {
        public static MapDisplay displayMap;
        public static bool successfulLoad = false;

        public UI()
        {
            InitializeComponent();
            displayMap = new MapDisplay(displayGrid);
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
                displayMap.NumberOfFloors = 1;
            }
            else if (newFloor == "two")
            {
                displayMap.NumberOfFloors = 2;
            }
            else if (newFloor == "three")
            {
                displayMap.NumberOfFloors = 3;
            }
            else if (newFloor == "random")
            {
                displayMap.NumberOfFloors = 0;
            }
        }

        private void FogOfWarDropdownSelected(object sender, EventArgs e)
        {

            FogOfWar.SetFogOfWar(sender as ComboBox);
        }

        private void DoneButton_Click(object sender, EventArgs e)
        {
            int width = GetSizeInput(widthInput);
            int height = GetSizeInput(heightInput);

            if (height != 0 && width != 0) //0 means invalid input
            {
                Random rng = new Random();

                HideStartingUI();
                displayMap.PrepTable();

                if (displayMap.NumberOfFloors == 0)
                {
                    displayMap.NumberOfFloors = rng.Next(1, 4);
                }
                floorMaxLabel.Text = "of " + displayMap.NumberOfFloors;
                if (height == 1) //1 means it was left blank
                {
                    height = rng.Next(10, 60);
                }

                if (width == 1) //1 means it was left blank
                {
                    width = rng.Next(10, 60);
                }

                GenerateMaps(height, width);
                displayMap.PrintMapWindow(Program.map1);
                ShowSecondaryUI();
            }
        }

        private int GetSizeInput(TextBox sizeInput)
        {
            int size = 1;

            if (sizeInput.Text != "")
            {
                try
                {
                    size = int.Parse(sizeInput.Text);
                }
                catch
                {
                    size = 0;
                }
                finally
                {
                    if(size < 10 || size > 60)
                    {
                        size = 0;
                        workingLabel.Text = "Please input a whole number from 10 to 60 for height and width.";
                        workingLabel.ForeColor = Color.Red;
                        workingLabel.Visible = true;
                    }
                }
            }

            return size;
        }

        private void HideStartingUI()
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

        private void GenerateMaps(int height, int width)
        {
            Program.GenerateMap(height, width, this, 1);

            if (displayMap.NumberOfFloors > 1)
            {
                Program.GenerateMap(height, width, this, 2);
            }
            if (displayMap.NumberOfFloors > 2)
            {
                Program.GenerateMap(height, width, this, 3);
            }
        }

        private void ShowSecondaryUI()
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

        private void CellClicked(object sender, DataGridViewCellEventArgs e)
        {
            if (displayGrid.CurrentCell.Style.BackColor == Color.Blue)
            {
                displayMap.ActivateDoor();
            }
            else if (displayGrid.CurrentCell.Style.BackColor == Color.Green)
            {
                displayMap.SwitchMap(displayMap.CurrentFloor + 1);
                floorMessage.Text = "Floor " + displayMap.CurrentFloor;
            }
            else if (displayGrid.CurrentCell.Style.BackColor == Color.Purple)
            {
                displayMap.SwitchMap(displayMap.CurrentFloor - 1);
                floorMessage.Text = "Floor " + displayMap.CurrentFloor;
            }
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            LoadForm LoadDialogue = new LoadForm();
            LoadDialogue.ShowDialog();

            if (successfulLoad)
            {
                HideStartingUI();
                displayMap.PrepTable();
                if (FogOfWar.fogOfWarOn && !FogOfWar.fogOfWarHarsh)
                {
                    FogOfWar.LoadFogInternal(displayMap);
                    ShowSecondaryUI();
                }
                else
                {
                    displayMap.SwitchMap(displayMap.CurrentFloor);
                }

                floorMessage.Text = "Floor " + displayMap.CurrentFloor;
                floorMaxLabel.Text = "of " + displayMap.NumberOfFloors;
                displayGrid.Visible = true;
                successfulLoad = false;
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            FogOfWar.SaveFogInternal(displayMap);
            SaveForm SaveDialogue = new SaveForm();
            SaveDialogue.ShowDialog();
        }
    }
}