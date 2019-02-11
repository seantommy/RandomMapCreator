using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MapGenerator
{
    class FogOfWar
    {
        public static bool fogOfWarOn = false;
        public static bool fogOfWarHarsh = false;
        public static string[,] floor1Fog = new string[60, 60];
        public static string[,] floor2Fog = new string[60, 60];
        public static string[,] floor3Fog = new string[60, 60];

        public static void SetFogOfWar(ComboBox fogOfWarSettingBox)
        {
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

        public static void SaveFogInternal(MapDisplay display)
        {
            if (fogOfWarOn == false || fogOfWarHarsh == true)
            {
                return;
            }

            if (display.CurrentFloor == 1)
            {
                for (int x = 0; x < MapGeneration.map1.Height; x++)
                {
                    for (int y = 0; y < MapGeneration.map1.Width; y++)
                    {
                        floor1Fog[x, y] = display.displayGrid.Rows[x].Cells[y].Style.BackColor.ToString().Substring(7).TrimEnd(']');
                    }
                }
            }
            else if (display.CurrentFloor == 2)
            {
                for (int x = 0; x < MapGeneration.map2.Height; x++)
                {
                    for (int y = 0; y < MapGeneration.map2.Width; y++)
                    {
                        floor2Fog[x, y] = display.displayGrid.Rows[x].Cells[y].Style.BackColor.ToString().Substring(7).TrimEnd(']');
                    }
                }
            }
            else
            {
                for (int x = 0; x < MapGeneration.map3.Height; x++)
                {
                    for (int y = 0; y < MapGeneration.map3.Width; y++)
                    {
                        floor3Fog[x, y] = display.displayGrid.Rows[x].Cells[y].Style.BackColor.ToString().Substring(7).TrimEnd(']');
                    }
                }
            }
        }

        public static void LoadFogInternal(MapDisplay display)
        {
            Map map;
            string[,] fog;

            if (!OkayToLoadFog(display.CurrentFloor))
            {
                return;
            }
            
            display.ClearDisplayGrid();

            if (display.CurrentFloor == 1)
            {
                map = MapGeneration.map1;
                fog = floor1Fog;
            }
            else if (display.CurrentFloor == 2)
            {
                map = MapGeneration.map2;
                fog = floor2Fog;
            }
            else
            {
                map = MapGeneration.map3;
                fog = floor3Fog;
            }

            for (int x = 0; x < map.Height; x++)
            {
                for (int y = 0; y < map.Width; y++)
                {
                    display.displayGrid.Rows[x].Cells[y].Style.BackColor = Color.FromName(fog[x, y]);
                    display.displayGrid.Rows[x].Cells[y].Style.SelectionBackColor = Color.FromName(fog[x, y]);
                }
            }
        }

        private static bool OkayToLoadFog(int currentFloor)
        {
            bool loadFog = false;

            if (fogOfWarOn == true && fogOfWarHarsh == false)
            {

                if (currentFloor == 1)
                {
                    loadFog = true;
                }
                else if (currentFloor == 2 && floor2Fog[0, 0] != null)
                {
                    loadFog = true;
                }
                else if (currentFloor == 3 && floor3Fog[0, 0] != null)
                {
                    loadFog = true;
                }
            }

            return loadFog;
        }

        public static void UpdateFogOfWar(MapDisplay display)
        {
            Map mapToUpdate;

            if (!fogOfWarOn)
            {
                return;
            }

            if (fogOfWarHarsh)
            {
                display.ClearDisplayGrid();
            }

            if (display.CurrentFloor == 1)
            {
                mapToUpdate = MapGeneration.map1;
            }
            else if (display.CurrentFloor == 2)
            {
                mapToUpdate = MapGeneration.map2;
            }
            else
            {
                mapToUpdate = MapGeneration.map3;
            }

            if (IsSpaceAccessible(mapToUpdate, display.CurrentDoor[0] - 1, display.CurrentDoor[1]))
            {
                int currentX = display.CurrentDoor[0] - 1;
                int currentY = display.CurrentDoor[1];
                bool verticalWallHit = false;

                int[] leftAndRightAdjust = RevealLeftAndRightAtY(display, mapToUpdate, currentX, currentY);

                while (!verticalWallHit)
                {
                    currentX--;
                    verticalWallHit = RevealLayerByY(display, mapToUpdate, currentX, currentY, leftAndRightAdjust);
                }
            }

            if (IsSpaceAccessible(mapToUpdate, display.CurrentDoor[0] + 1, display.CurrentDoor[1]))
            {
                int currentX = display.CurrentDoor[0] + 1;
                int currentY = display.CurrentDoor[1];
                bool verticalWallHit = false;

                int[] leftAndRightAdjust = RevealLeftAndRightAtY(display, mapToUpdate, currentX, currentY);

                while (!verticalWallHit)
                {
                    currentX++;
                    verticalWallHit = RevealLayerByY(display, mapToUpdate, currentX, currentY, leftAndRightAdjust);
                }
            }

            if (IsSpaceAccessible(mapToUpdate, display.CurrentDoor[0], display.CurrentDoor[1] - 1))
            {
                int currentX = display.CurrentDoor[0];
                int currentY = display.CurrentDoor[1] - 1;
                bool verticalWallHit = false;

                int[] leftAndRightAdjust = RevealLeftAndRightAtX(display, mapToUpdate, currentX, currentY);

                while (!verticalWallHit)
                {
                    currentY--;
                    verticalWallHit = RevealLayerByX(display, mapToUpdate, currentX, currentY, leftAndRightAdjust);
                }
            }

            if (IsSpaceAccessible(mapToUpdate, display.CurrentDoor[0], display.CurrentDoor[1] + 1))
            {
                int currentX = display.CurrentDoor[0];
                int currentY = display.CurrentDoor[1] + 1;
                bool verticalWallHit = false;

                int[] leftAndRightAdjust = RevealLeftAndRightAtX(display, mapToUpdate, currentX, currentY);

                while (!verticalWallHit)
                {
                    currentY++;
                    verticalWallHit = RevealLayerByX(display, mapToUpdate, currentX, currentY, leftAndRightAdjust);
                }
            }

            display.SetCellRed(display.CurrentDoor[0], display.CurrentDoor[1]);
        }

        private static bool IsSpaceAccessible(Map mapToUpdate, int x, int y)
        {
            if (x < 0 || x >= mapToUpdate.Height || y < 0 || y >= mapToUpdate.Width)
            {
                return false;
            }
            if (mapToUpdate.Contents[x, y] == ' ' || mapToUpdate.Contents[x, y] == 'H' || mapToUpdate.Contents[x, y] == 'L')
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static int[] RevealLeftAndRightAtY(MapDisplay display, Map mapToUpdate, int currentX, int currentY)
        {
            bool leftWallHit = false;
            bool rightWallHit = false;
            int leftAdjust = 0;
            int rightAdjust = 0;

            while (!leftWallHit)
            {
                display.SetCellColor(mapToUpdate, currentX, currentY);
                leftAdjust++;
                if (mapToUpdate.Contents[currentX, currentY - leftAdjust] != ' ' && mapToUpdate.Contents[currentX, currentY - leftAdjust] != 'H' && mapToUpdate.Contents[currentX, currentY - leftAdjust] != 'L')
                {
                    display.SetCellColor(mapToUpdate, currentX, currentY - leftAdjust);
                    leftWallHit = true;
                }
            }

            while (!rightWallHit)
            {
                display.SetCellColor(mapToUpdate, currentX, currentY + rightAdjust);
                rightAdjust++;
                if (mapToUpdate.Contents[currentX, currentY + rightAdjust] != ' ' && mapToUpdate.Contents[currentX, currentY + rightAdjust] != 'H' && mapToUpdate.Contents[currentX, currentY + rightAdjust] != 'L')
                {
                    display.SetCellColor(mapToUpdate, currentX, currentY + rightAdjust);
                    rightWallHit = true;
                }
            }

            for (int i = 0; i <= leftAdjust; i++)
            {
                display.SetCellColor(mapToUpdate, display.CurrentDoor[0], currentY - i);
            }

            for (int j = 1; j <= rightAdjust; j++)
            {
                display.SetCellColor(mapToUpdate, display.CurrentDoor[0], currentY + j);
            }

            return new int[] { leftAdjust, rightAdjust };
        }

        private static int[] RevealLeftAndRightAtX(MapDisplay display, Map mapToUpdate, int currentX, int currentY)
        {
            bool leftWallHit = false;
            bool rightWallHit = false;
            int leftAdjust = 0;
            int rightAdjust = 0;

            while (!leftWallHit)
            {
                display.SetCellColor(mapToUpdate, currentX, currentY);
                leftAdjust++;
                if (mapToUpdate.Contents[currentX - leftAdjust, currentY] != ' ' && mapToUpdate.Contents[currentX - leftAdjust, currentY] != 'H' && mapToUpdate.Contents[currentX - leftAdjust, currentY] != 'L')
                {
                    display.SetCellColor(mapToUpdate, currentX - leftAdjust, currentY);
                    leftWallHit = true;
                }
            }

            while (!rightWallHit)
            {
                display.SetCellColor(mapToUpdate, currentX + rightAdjust, currentY);
                rightAdjust++;
                if (mapToUpdate.Contents[currentX + rightAdjust, currentY] != ' ' && mapToUpdate.Contents[currentX + rightAdjust, currentY] != 'H' && mapToUpdate.Contents[currentX + rightAdjust, currentY] != 'L')
                {
                    display.SetCellColor(mapToUpdate, currentX + rightAdjust, currentY);
                    rightWallHit = true;
                }
            }

            for (int i = 0; i <= leftAdjust; i++)
            {
                display.SetCellColor(mapToUpdate, currentX - i, display.CurrentDoor[1]);
            }

            for (int j = 1; j <= rightAdjust; j++)
            {
                display.SetCellColor(mapToUpdate, currentX + j, display.CurrentDoor[1]);
            }

            return new int[] { leftAdjust, rightAdjust };
        }

        private static bool RevealLayerByY(MapDisplay display, Map mapToUpdate, int currentX, int currentY, int[] leftAndRightAdjust)
        {
            int leftAdjust = leftAndRightAdjust[0];
            int rightAdjust = leftAndRightAdjust[1];

            for (int i = 0; i <= leftAdjust; i++)
            {
                display.SetCellColor(mapToUpdate, currentX, currentY - i);
            }

            for (int j = 1; j <= rightAdjust; j++)
            {
                display.SetCellColor(mapToUpdate, currentX, currentY + j);
            }

            if (mapToUpdate.Contents[currentX, currentY] != ' ' && mapToUpdate.Contents[currentX, currentY] != 'H' && mapToUpdate.Contents[currentX, currentY] != 'L')
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool RevealLayerByX(MapDisplay display, Map mapToUpdate, int currentX, int currentY, int[] leftAndRightAdjust)
        {
            int leftAdjust = leftAndRightAdjust[0];
            int rightAdjust = leftAndRightAdjust[1];

            for (int i = 0; i <= leftAdjust; i++)
            {
                display.SetCellColor(mapToUpdate, currentX - i, currentY);
            }

            for (int j = 1; j <= rightAdjust; j++)
            {
                display.SetCellColor(mapToUpdate, currentX + j, currentY);
            }

            if (mapToUpdate.Contents[currentX, currentY] != ' ' && mapToUpdate.Contents[currentX, currentY] != 'H' && mapToUpdate.Contents[currentX, currentY] != 'L')
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
