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

        private void DisplaySaves()
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

                if (FogOfWar.fogOfWarOn && !FogOfWar.fogOfWarHarsh)
                {
                    SaveFog(nameTextbox.Text);
                }

                Application.OpenForms["DisplayForm"].BringToFront();
                this.Hide();
                this.Dispose();
            }
        }

        private void SaveInfo(string fileName)
        {
            string directoryString = Directory.GetCurrentDirectory().Insert(Directory.GetCurrentDirectory().Length, "\\saves\\" + fileName);
            Directory.CreateDirectory(directoryString);
            string fileText = "";

            fileText += FogOfWar.fogOfWarOn.ToString().Insert(FogOfWar.fogOfWarOn.ToString().Length, ",");
            fileText += FogOfWar.fogOfWarHarsh.ToString().Insert(FogOfWar.fogOfWarHarsh.ToString().Length, ",");
            fileText += UI.displayMap.NumberOfFloors.ToString().Insert(UI.displayMap.NumberOfFloors.ToString().Length, ",");
            fileText += UI.displayMap.CurrentFloor.ToString().Insert(UI.displayMap.CurrentFloor.ToString().Length, ",");
            fileText += UI.displayMap.CurrentDoor[0].ToString().Insert(UI.displayMap.CurrentDoor[0].ToString().Length, ",");
            fileText += UI.displayMap.CurrentDoor[1].ToString();

            File.WriteAllText((directoryString + "\\info"), fileText);
        }

        private void SaveAllMaps(string fileName)
        {
            string directoryString = Directory.GetCurrentDirectory().Insert(Directory.GetCurrentDirectory().Length, "\\saves\\" + fileName);
            SaveOneMap(directoryString + "\\map1", 1);

            if (UI.displayMap.NumberOfFloors > 1)
            {
                SaveOneMap(directoryString + "\\map2", 2);
            }
            if (UI.displayMap.NumberOfFloors > 2)
            {
                SaveOneMap(directoryString + "\\map3", 3);
            }
        }

        private void SaveOneMap(string directoryString, int mapNumber)
        {
            string fileText = "";
            Map map;
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

            for (int x = 0; x < map.Height; x++)
            {
                for (int y = 0; y < map.Width; y++)
                {
                    fileText += map.Contents[x, y];
                }
                fileText += "\n";
            }
            File.WriteAllText(directoryString, fileText);
        }

        private void SaveFog(string fileName)
        {
            string directoryString = Directory.GetCurrentDirectory().Insert(Directory.GetCurrentDirectory().Length, "\\saves\\" + fileName);

            SaveOneFloorFog(directoryString + "\\floor1Fog", 1);

            if (UI.displayMap.NumberOfFloors > 1 && FogOfWar.floor2Fog[0, 0] != null)
            {
                SaveOneFloorFog(directoryString + "\\floor2Fog", 2);
            }
            if (UI.displayMap.NumberOfFloors > 2 && FogOfWar.floor3Fog[0, 0] != null)
            {
                SaveOneFloorFog(directoryString + "\\floor3Fog", 3);
            }
        }

        private void SaveOneFloorFog(string directoryString, int mapNumber)
        {
            string fileText = "";
            Map map;
            string[,] fogArray;
            if (mapNumber == 1)
            {
                map = Program.map1;
                fogArray = FogOfWar.floor1Fog;
            }
            else if (mapNumber == 2)
            {
                map = Program.map2;
                fogArray = FogOfWar.floor2Fog;
            }
            else
            {
                map = Program.map3;
                fogArray = FogOfWar.floor3Fog;
            }

            for (int x = 0; x < map.Height; x++)
            {
                for (int y = 0; y < map.Width; y++)
                {
                    fileText += fogArray[x, y];
                    if (y < map.Width - 1)
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