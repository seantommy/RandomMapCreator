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
            catch
            {

            }
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
            }catch
            { }

            if (fileSelected != "")
            {
                LoadInfo(fileSelected);
                LoadMaps(fileSelected);

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
            string fileText = File.ReadAllText(directoryString + "\\info");

            string[] fileTextItems = fileText.Split(',');

            DisplayForm.fogOfWarOn = bool.Parse(fileTextItems[0]);
            DisplayForm.fogOfWarHarsh = bool.Parse(fileTextItems[1]);
            DisplayForm.numberOfFloors = int.Parse(fileTextItems[2]);
            DisplayForm.currentFloor = int.Parse(fileTextItems[3]);
            DisplayForm.currentDoor[0] = int.Parse(fileTextItems[4]);
            DisplayForm.currentDoor[1] = int.Parse(fileTextItems[5]);
        }

        public void LoadMaps(string fileName)
        {
            string directoryString = Directory.GetCurrentDirectory().Insert(Directory.GetCurrentDirectory().Length, "\\saves\\" + fileName);
            
            string[] fileLines = File.ReadLines(directoryString + "\\map1").ToArray();
            char[] lineItems = fileLines[0].ToCharArray();
            Program.map1 = new char[fileLines.Length, lineItems.Length];

            for(int x = 0; x < fileLines.Length; x++)
            {
                lineItems = fileLines[x].ToCharArray();
                for(int y = 0; y < lineItems.Length; y++)
                {
                    Program.map1[x, y] = lineItems[y];
                }
            }

            if (File.Exists(directoryString + "\\map2"))
            {
                fileLines = File.ReadLines(directoryString + "\\map2").ToArray();
                lineItems = fileLines[0].ToCharArray();
                Program.map2 = new char[fileLines.Length, lineItems.Length];

                for (int x = 0; x < fileLines.Length; x++)
                {
                    lineItems = fileLines[x].ToCharArray();
                    for (int y = 0; y < lineItems.Length; y++)
                    {
                        Program.map2[x, y] = lineItems[y];
                    }
                }
            }

            if (File.Exists(directoryString + "\\map3"))
            {
                fileLines = File.ReadLines(directoryString + "\\map3").ToArray();
                lineItems = fileLines[0].ToCharArray();
                Program.map3 = new char[fileLines.Length, lineItems.Length];

                for (int x = 0; x < fileLines.Length; x++)
                {
                    lineItems = fileLines[x].ToCharArray();
                    for (int y = 0; y < lineItems.Length; y++)
                    {
                        Program.map3[x, y] = lineItems[y];
                    }
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
