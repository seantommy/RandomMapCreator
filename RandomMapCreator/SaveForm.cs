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
            }catch
            {

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
                catch{ }

                Directory.Delete(directoryString);
                saveListBox.Items.Remove(nameTextbox.Text);
            }
        }
        
        private void SaveButtonClicked(object sender, EventArgs e)
        {
            if(nameTextbox.Text != null)
            {
                SaveInfo(nameTextbox.Text);
                SaveMaps(nameTextbox.Text);
                
                if(DisplayForm.fogOfWarOn && !DisplayForm.fogOfWarHarsh)
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

        public void SaveMaps(string fileName)
        {
            string directoryString = Directory.GetCurrentDirectory().Insert(Directory.GetCurrentDirectory().Length, "\\saves\\" + fileName);
            string fileText = "";

            for (int x = 0; x < Program.map1.GetLength(0); x++)
            {
                for (int y = 0; y < Program.map1.GetLength(1); y++)
                {
                    fileText += Program.map1[x,y];
                }
                fileText += "\n";
            }
            File.WriteAllText((directoryString + "\\map1"), fileText);

            if (DisplayForm.numberOfFloors > 1)
            {
                fileText = "";
                for (int x = 0; x < Program.map2.GetLength(0); x++)
                {
                    for (int y = 0; y < Program.map2.GetLength(1); y++)
                    {
                        fileText += Program.map2[x, y];
                    }
                    fileText += "\n";
                }
                File.WriteAllText((directoryString + "\\map2"), fileText);
            }

            if (DisplayForm.numberOfFloors > 2)
            {
                fileText = "";
                for (int x = 0; x < Program.map3.GetLength(0); x++)
                {
                    for (int y = 0; y < Program.map3.GetLength(1); y++)
                    {
                        fileText += Program.map3[x, y];
                    }
                    fileText += "\n";
                }
                File.WriteAllText((directoryString + "\\map3"), fileText);
            }
        }

        public void SaveFog(string fileName)
        {
            string directoryString = Directory.GetCurrentDirectory().Insert(Directory.GetCurrentDirectory().Length, "\\saves\\" + fileName);
            string fileText = "";

            for (int x = 0; x < Program.map1.GetLength(0); x++)
            {
                for (int y = 0; y < Program.map1.GetLength(1); y++)
                {
                    fileText += DisplayForm.floor1Fog[x, y];
                    if (y < Program.map1.GetLength(1) - 1)
                    {
                        fileText += ",";
                    }
                }
                fileText += "\n";
            }
            File.WriteAllText((directoryString + "\\floor1Fog"), fileText);

            if (DisplayForm.numberOfFloors > 1 && DisplayForm.floor2Fog[0,0] != null)
            {
                fileText = "";
                for (int x = 0; x < Program.map2.GetLength(0); x++)
                {
                    for (int y = 0; y < Program.map2.GetLength(1); y++)
                    {
                        fileText += DisplayForm.floor2Fog[x, y];
                        if (y < Program.map2.GetLength(1) - 1)
                        {
                            fileText += ",";
                        }
                    }
                    fileText += "\n";
                }
                File.WriteAllText((directoryString + "\\floor2Fog"), fileText);
            }

            if (DisplayForm.numberOfFloors > 2 && DisplayForm.floor3Fog[0,0] != null)
            {
                fileText = "";
                for (int x = 0; x < Program.map3.GetLength(0); x++)
                {
                    for (int y = 0; y < Program.map3.GetLength(1); y++)
                    {
                        fileText += DisplayForm.floor3Fog[x, y];
                        if (y < Program.map3.GetLength(1) - 1)
                        {
                            fileText += ",";
                        }
                    }
                    fileText += "\n";
                }
                File.WriteAllText((directoryString + "\\floor3Fog"), fileText);
            }
        }
    }
}
