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

        public void DisplaySaves()
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
            }catch { }

            if (fileSelected != "")
            {
                LoadInfo(fileSelected);
                LoadAllMaps(fileSelected);

                if (DisplayForm.fogOfWarOn && !DisplayForm.fogOfWarHarsh)
                {
                    LoadFog(fileSelected);
                }

                Application.OpenForms["DisplayForm"].BringToFront();
                DisplayForm.successfulLoad = true;
                this.Hide();
                this.Dispose();
            }
        }

        public void LoadInfo(string fileName)
        {
            string directoryString = Directory.GetCurrentDirectory().Insert(Directory.GetCurrentDirectory().Length, "\\saves\\" + fileName);
            string[] fileTextItems = File.ReadAllText(directoryString + "\\info").Split(',');

            DisplayForm.fogOfWarOn = bool.Parse(fileTextItems[0]);
            DisplayForm.fogOfWarHarsh = bool.Parse(fileTextItems[1]);
            DisplayForm.numberOfFloors = int.Parse(fileTextItems[2]);
            DisplayForm.currentFloor = int.Parse(fileTextItems[3]);
            DisplayForm.currentDoor[0] = int.Parse(fileTextItems[4]);
            DisplayForm.currentDoor[1] = int.Parse(fileTextItems[5]);
        }

        public void LoadAllMaps(string fileName)
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
            char[,] map;

            if (mapNumber == 1)
            {
                Program.map1 = new char[fileLines.Length, lineItems.Length];
                map = Program.map1;
            }
            else if(mapNumber == 2)
            {
                Program.map2 = new char[fileLines.Length, lineItems.Length];
                map = Program.map2;
            }
            else
            {
                Program.map3 = new char[fileLines.Length, lineItems.Length];
                map = Program.map3;
            }
            
            for (int x = 0; x < fileLines.Length; x++)
            {
                lineItems = fileLines[x].ToCharArray();
                for (int y = 0; y < lineItems.Length; y++)
                {
                    map[x, y] = lineItems[y];
                }
            }
        }

        public void LoadFog(string fileName)
        {
            try
            {
                string directoryString = Directory.GetCurrentDirectory().Insert(Directory.GetCurrentDirectory().Length, "\\saves\\" + fileName);

                string[] fileLines = File.ReadLines(directoryString + "\\floor1Fog").ToArray();
                string[] lineItems = fileLines[0].Split(',');
                DisplayForm.floor1Fog = new string[60, 60];

                for (int x = 0; x < 60; x++)
                {
                    if (x < fileLines.Length)
                    {
                        lineItems = fileLines[x].Split(',');
                        for (int y = 0; y < 60; y++)
                        {
                            if (y < lineItems.Length)
                            {
                                DisplayForm.floor1Fog[x, y] = lineItems[y];
                            }
                            else
                            {
                                DisplayForm.floor1Fog[x, y] = "Control";
                            }
                        }
                    }
                    else
                    {
                        for (int y = 0; y < 60; y++)
                        {
                            DisplayForm.floor1Fog[x, y] = "Control";
                        }
                    }
                }

                if (File.Exists(directoryString + "\\map2"))
                {
                    fileLines = File.ReadLines(directoryString + "\\floor2Fog").ToArray();
                    lineItems = fileLines[0].Split(',');
                    DisplayForm.floor2Fog = new string[60, 60];

                    for (int x = 0; x < 60; x++)
                    {
                        if (x < fileLines.Length)
                        {
                            lineItems = fileLines[x].Split(',');
                            for (int y = 0; y < 60; y++)
                            {
                                if (y < lineItems.Length)
                                {
                                    DisplayForm.floor2Fog[x, y] = lineItems[y];
                                }
                                else
                                {
                                    DisplayForm.floor2Fog[x, y] = "Control";
                                }
                            }
                        }
                        else
                        {
                            for (int y = 0; y < 60; y++)
                            {
                                DisplayForm.floor2Fog[x, y] = "Control";
                            }
                        }
                    }
                }

                if (File.Exists(directoryString + "\\map3"))
                {
                    fileLines = File.ReadLines(directoryString + "\\floor3Fog").ToArray();
                    lineItems = fileLines[0].Split(',');
                    DisplayForm.floor3Fog = new string[60, 60];

                    for (int x = 0; x < 60; x++)
                    {
                        if (x < fileLines.Length)
                        {
                            lineItems = fileLines[x].Split(',');
                            for (int y = 0; y < 60; y++)
                            {
                                if (y < lineItems.Length)
                                {
                                    DisplayForm.floor3Fog[x, y] = lineItems[y];
                                }
                                else
                                {
                                    DisplayForm.floor3Fog[x, y] = "Control";
                                }
                            }
                        }
                        else
                        {
                            for (int y = 0; y < 60; y++)
                            {
                                DisplayForm.floor3Fog[x, y] = "Control";
                            }
                        }
                    }
                }
            } catch //This is fine. It just means there's no fog of war for one of the floors yet.
            {
                Application.OpenForms["DisplayForm"].BringToFront();
            }
        }
    }
}
