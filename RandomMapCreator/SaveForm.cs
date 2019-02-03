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
    public partial class SaveForm : Form
    {
        public SaveForm()
        {
            InitializeComponent();
            DisplaySaves();
        }

        public void DisplaySaves()
        {
            string directoryString = Directory.GetCurrentDirectory().Insert(Directory.GetCurrentDirectory().Length, "\\saves\\");
            Directory.CreateDirectory(directoryString);

            try
            {
                string[] saveNames = Directory.GetDirectories(directoryString);
                for (int x = 0; x < saveNames.Length; x++)
                {
                    saveListBox.Items.Add(saveNames[x].Remove(0, directoryString.Length));
                }
            }
            catch
            {
                //This is fine. It means there are no saves.
            }
        }

        private void SaveListItemSelected(object sender, EventArgs e)
        {
            ListBox listie = sender as ListBox;
            nameTextbox.Text = listie.Text;
        }

        private void DeleteButtonClicekd(object sender, EventArgs e)
        {
            string directoryString = Directory.GetCurrentDirectory().Insert(Directory.GetCurrentDirectory().Length, "\\saves\\" + nameTextbox.Text);

            if (nameTextbox.Text != null && Directory.Exists(directoryString))
            {
                string[] fileArray = Directory.GetFiles(directoryString);

                try
                {
                    for (int x = 0; x < fileArray.Length; x++)
                    {
                        File.Delete(fileArray[x]);
                    }
                }
                catch { }

                Directory.Delete(directoryString);
                saveListBox.Items.Remove(nameTextbox.Text);
            }
        }

        private void SaveButtonClicked(object sender, EventArgs e)
        {
            if (nameTextbox.Text != null)
            {
                SaveInfo(nameTextbox.Text);
                SaveAllMaps(nameTextbox.Text);

                if (DisplayForm.fogOfWarOn && !DisplayForm.fogOfWarHarsh)
                {
                    SaveFog(nameTextbox.Text);
                }

                Application.OpenForms["DisplayForm"].BringToFront();
                this.Hide();
                this.Dispose();
            }
        }

        public void SaveInfo(string fileName)
        {
            string directoryString = Directory.GetCurrentDirectory().Insert(Directory.GetCurrentDirectory().Length, "\\saves\\" + fileName);
            Directory.CreateDirectory(directoryString);
            string fileText = "";

            fileText += DisplayForm.fogOfWarOn.ToString().Insert(DisplayForm.fogOfWarOn.ToString().Length, ",");
            fileText += DisplayForm.fogOfWarHarsh.ToString().Insert(DisplayForm.fogOfWarHarsh.ToString().Length, ",");
            fileText += DisplayForm.numberOfFloors.ToString().Insert(DisplayForm.numberOfFloors.ToString().Length, ",");
            fileText += DisplayForm.currentFloor.ToString().Insert(DisplayForm.currentFloor.ToString().Length, ",");
            fileText += DisplayForm.currentDoor[0].ToString().Insert(DisplayForm.currentDoor[0].ToString().Length, ",");
            fileText += DisplayForm.currentDoor[1].ToString();

            File.WriteAllText((directoryString + "\\info"), fileText);
        }

        public void SaveAllMaps(string fileName)
        {
            string directoryString = Directory.GetCurrentDirectory().Insert(Directory.GetCurrentDirectory().Length, "\\saves\\" + fileName);
            SaveOneMap(directoryString + "\\map1", 1);

            if (DisplayForm.numberOfFloors > 1)
            {
                SaveOneMap(directoryString + "\\map2", 2);
            }
            if (DisplayForm.numberOfFloors > 2)
            {
                SaveOneMap(directoryString + "\\map3", 3);
            }
        }

        private void SaveOneMap(string directoryString, int mapNumber)
        {
            string fileText = "";
            char[,] map;
            if (mapNumber == 1)
            {
                map = Program.map1;
            }
            else if (mapNumber == 2)
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
                    fileText += map[x, y];
                }
                fileText += "\n";
            }
            File.WriteAllText(directoryString, fileText);
        }

        public void SaveFog(string fileName)
        {
            string directoryString = Directory.GetCurrentDirectory().Insert(Directory.GetCurrentDirectory().Length, "\\saves\\" + fileName);

            SaveOneFloorFog(directoryString + "\\floor1Fog", 1);

            if (DisplayForm.numberOfFloors > 1 && DisplayForm.floor2Fog[0, 0] != null)
            {
                SaveOneFloorFog(directoryString + "\\floor2Fog", 2);
            }
            if (DisplayForm.numberOfFloors > 2 && DisplayForm.floor3Fog[0, 0] != null)
            {
                SaveOneFloorFog(directoryString + "\\floor3Fog", 3);
            }
        }

        private void SaveOneFloorFog(string directoryString, int mapNumber)
        {
            string fileText = "";
            char[,] map;
            string[,] fogArray;
            if (mapNumber == 1)
            {
                map = Program.map1;
                fogArray = DisplayForm.floor1Fog;
            }
            else if (mapNumber == 2)
            {
                map = Program.map2;
                fogArray = DisplayForm.floor2Fog;
            }
            else
            {
                map = Program.map3;
                fogArray = DisplayForm.floor3Fog;
            }

            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    fileText += fogArray[x, y];
                    if (y < map.GetLength(1) - 1)
                    {
                        fileText += ",";
                    }
                }
                fileText += "\n";
            }
            File.WriteAllText(directoryString, fileText);
        }
    }
}