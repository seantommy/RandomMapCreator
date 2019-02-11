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
    public partial class LoadForm : Form
    {
        public LoadForm()
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
                    loadListBox.Items.Add(saveNames[x].Remove(0, directoryString.Length));
                }
            }
            catch { }
        }

        private void DeleteButtonClicekd(object sender, EventArgs e)
        {
            string selectedText = loadListBox.Text;
            string directoryString = Directory.GetCurrentDirectory().Insert(Directory.GetCurrentDirectory().Length, "\\saves\\" + selectedText);

            if (selectedText != null && Directory.Exists(directoryString))
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
                loadListBox.Items.Remove(selectedText);
            }
        }

        private void LoadButtonClicked(object sender, EventArgs e)
        {
            string fileSelected = "";
            try
            {
                fileSelected = loadListBox.Text;
            }
            catch { }

            if (fileSelected != "")
            {
                LoadInfo(fileSelected);
                LoadAllMaps(fileSelected);

                if (FogOfWar.fogOfWarOn && !FogOfWar.fogOfWarHarsh)
                {
                    LoadAllFog(fileSelected);
                }

                Application.OpenForms["DisplayForm"].BringToFront();
                UI.successfulLoad = true;
                this.Hide();
                this.Dispose();
            }
        }

        private void LoadInfo(string fileName)
        {
            string directoryString = Directory.GetCurrentDirectory().Insert(Directory.GetCurrentDirectory().Length, "\\saves\\" + fileName);
            string[] fileTextItems = File.ReadAllText(directoryString + "\\info").Split(',');

            FogOfWar.fogOfWarOn = bool.Parse(fileTextItems[0]);
            FogOfWar.fogOfWarHarsh = bool.Parse(fileTextItems[1]);
            UI.displayMap.NumberOfFloors = int.Parse(fileTextItems[2]);
            UI.displayMap.CurrentFloor = int.Parse(fileTextItems[3]);
            UI.displayMap.CurrentDoor[0] = int.Parse(fileTextItems[4]);
            UI.displayMap.CurrentDoor[1] = int.Parse(fileTextItems[5]);
        }

        private void LoadAllMaps(string fileName)
        {
            string directoryString = Directory.GetCurrentDirectory().Insert(Directory.GetCurrentDirectory().Length, "\\saves\\" + fileName);

            LoadOneMap(directoryString + "\\map1", 1);

            if (File.Exists(directoryString + "\\map2"))
            {
                LoadOneMap(directoryString + "\\map2", 2);
            }

            if (File.Exists(directoryString + "\\map3"))
            {
                LoadOneMap(directoryString + "\\map3", 3);
            }
        }

        private void LoadOneMap(string directoryString, int mapNumber)
        {
            string[] fileLines = File.ReadLines(directoryString).ToArray();
            char[] lineItems = fileLines[0].ToCharArray();
            Map map;

            if (mapNumber == 1)
            {
                Program.map1 = new Map(fileLines.Length, lineItems.Length);
                map = Program.map1;
            }
            else if (mapNumber == 2)
            {
                Program.map2 = new Map(fileLines.Length, lineItems.Length);
                map = Program.map2;
            }
            else
            {
                Program.map3 = new Map(fileLines.Length, lineItems.Length);
                map = Program.map3;
            }

            for (int x = 0; x < fileLines.Length; x++)
            {
                lineItems = fileLines[x].ToCharArray();
                for (int y = 0; y < lineItems.Length; y++)
                {
                    map.Contents[x, y] = lineItems[y];
                }
            }
        }

        private void LoadAllFog(string fileName)
        {
            try
            {
                string directoryString = Directory.GetCurrentDirectory().Insert(Directory.GetCurrentDirectory().Length, "\\saves\\" + fileName);
                LoadOneFloorFog(directoryString + "\\floor1Fog", 1);

                if (File.Exists(directoryString + "\\floor2Fog"))
                {
                    LoadOneFloorFog(directoryString + "\\floor2Fog", 2);
                }
                if (File.Exists(directoryString + "\\floor3Fog"))
                {
                    LoadOneFloorFog(directoryString + "\\floor3Fog", 3);
                }
            }
            catch //This is fine. It just means there's no fog of war for one of the floors yet.
            {
                Application.OpenForms["DisplayForm"].BringToFront();
            }
        }

        private void LoadOneFloorFog(string directoryString, int mapNumber)
        {
            string[] fileLines = File.ReadLines(directoryString).ToArray();
            string[] lineItems = fileLines[0].Split(',');

            for (int x = 0; x < 60; x++)
            {
                if (x < fileLines.Length)
                {
                    lineItems = fileLines[x].Split(',');
                    for (int y = 0; y < 60; y++)
                    {
                        if (y < lineItems.Length)
                        {
                            SetFogAtCell(x, y, lineItems[y], mapNumber);
                        }
                        else
                        {
                            SetFogAtCell(x, y, "Control", mapNumber);
                        }
                    }
                }
                else
                {
                    for (int y = 0; y < 60; y++)
                    {
                        SetFogAtCell(x, y, "Control", mapNumber);
                    }
                }
            }
        }

        private void SetFogAtCell(int x, int y, string fogValue, int mapNumber)
        {
            if (mapNumber == 1)
            {
                FogOfWar.floor1Fog[x, y] = fogValue;
            }
            else if (mapNumber == 2)
            {
                FogOfWar.floor2Fog[x, y] = fogValue;
            }
            else
            {
                FogOfWar.floor3Fog[x, y] = fogValue;
            }
        }
    }
}

