using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MapGenerator
{
    static class Program
    {
        public static char[,] map1;
        public static char[,] map2;
        public static char[,] map3;
        public static Random rng = new Random();
        public static double roomSizeModifier = .2;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            DisplayForm Display = new DisplayForm();
            Application.Run(Display);
        }

        public static void GenerateMap(int mapHeight, int mapWidth, DisplayForm Display, int floor)
        {
            bool mapComplete = false;
            char[,] map = new char[mapHeight, mapWidth];

            FillEmpty(map, mapHeight, mapWidth);
            CreateBorder(map, mapHeight, mapWidth);

            while (mapComplete == false)
            {
                mapComplete = CreateRoom(map, mapHeight, mapWidth);
            }

            if (floor == 1)
            {
                CreateExteriorDoor(map, mapHeight, mapWidth);
                map1 = map;
            }
            else if (floor == 2)
            {
                map2 = map;
            }
            else if (floor == 3)
            {
                map3 = map;
            }

            CreateStairs(floor, mapHeight, mapWidth);
        }

        public static void FillEmpty(char[,] map, int height, int width)
        {
            for (int x = 1; x < height - 1; x++)
            {
                for (int y = 1; y < width - 1; y++)
                {
                    map[x, y] = 'O';
                }
            }
        }

        public static void CreateBorder(char[,] map, int height, int width)
        {
            for (int x = 0; x < width; x++)
            {
                map[0, x] = 'X';
                map[height - 1, x] = 'X';
            }

            for (int y = 0; y < height; y++)
            {
                map[y, 0] = 'X';
                map[y, width - 1] = 'X';
            }
        }

        public static bool CreateRoom(char[,] map, int mapHeight, int mapWidth)
        {
            int[] roomStart = new int[2] { 1, 1 };
            roomStart = FindRoomStart(map, mapHeight, mapWidth, roomStart);

            if (roomStart == null)
            {
                return true;
            }
            else
            {
                BuildRoom(map, mapHeight, mapWidth, roomStart);
                return false;
            }
        }

        public static int[] FindRoomStart(char[,] map, int mapHeight, int mapWidth, int[] crawler)
        {
            if (map[crawler[0], crawler[1]] == 'O')
            {
                return crawler;
            }
            else
            {
                if (crawler[1] < mapWidth - 1)
                {
                    crawler[1]++;
                    crawler = FindRoomStart(map, mapHeight, mapWidth, crawler);
                }
                else if (crawler[0] < mapHeight - 1)
                {
                    crawler[1] = 1;
                    crawler[0]++;
                    crawler = FindRoomStart(map, mapHeight, mapWidth, crawler);
                }
                else
                {
                    crawler = null;
                }
            }

            return crawler;
        }

        public static void BuildRoom(char[,] map, int mapHeight, int mapWidth, int[] roomStart)
        {
            int width = 1;
            int height = 1;

            width = BuildRoomEast(map, roomStart);
            height = BuildRoomSouth(map, roomStart, width);
            AddInteriorDoor(map, roomStart, height, width, mapHeight, mapWidth);
        }

        private static int BuildRoomEast(char[,] map, int[] roomStart)
        {
            int[] crawler = new int[2];
            crawler[0] = roomStart[0];
            crawler[1] = roomStart[1];
            int width = 1;

            map[crawler[0], crawler[1]] = ' ';
            if (map[crawler[0] - 1, crawler[1]] == 'O')
            {
                map[crawler[0] - 1, crawler[1]] = 'X';
            }
            bool isFinishedHorizontal = false;
            while (isFinishedHorizontal == false)
            {
                crawler[1]++;
                if (map[crawler[0], crawler[1]] == 'O')
                {
                    if (rng.NextDouble() > roomSizeModifier || map[crawler[0] - 1, crawler[1]] == 'E' || map[crawler[0] - 1, crawler[1]] == 'S' || map[crawler[0] - 1, crawler[1]] == 'D')
                    {
                        map[crawler[0], crawler[1]] = ' ';

                        if (map[crawler[0] - 1, crawler[1]] == 'O')
                        {
                            map[crawler[0] - 1, crawler[1]] = 'X';
                        }
                        width++;
                    }
                    else
                    {
                        if (map[crawler[0], crawler[1] + 1] == 'X')
                        {
                            map[crawler[0], crawler[1]] = ' ';
                            crawler[1]++;
                            width++;
                        }
                        map[crawler[0], crawler[1]] = 'X';
                        isFinishedHorizontal = true;
                    }
                }
                else
                {
                    isFinishedHorizontal = true;
                }
            }

            return width;
        }

        private static int BuildRoomSouth(char[,] map, int[] roomStart, int width)
        {
            int height = 1;
            int[] crawler = new int[2];
            crawler[0] = roomStart[0];
            crawler[1] = roomStart[1];
            bool isFinishedVertical = false;
            while (isFinishedVertical == false)
            {
                crawler[0] = crawler[0] + 1;
                crawler[1] = roomStart[1];
                bool isValidRow = true;
                for (int x = 0; x < width; x++)
                {
                    if (map[crawler[0], crawler[1] + x] != 'O')
                    {
                        isValidRow = false;
                        break;
                    }
                }
                if (isValidRow)
                {
                    if (map[crawler[0], crawler[1] - 1] == 'O' || map[crawler[0], crawler[1] - 1] == ' ')
                    {
                        map[crawler[0], crawler[1] - 1] = 'X';
                    }
                    if (rng.NextDouble() > roomSizeModifier || map[crawler[0], crawler[1] - 1] == 'E' || map[crawler[0], crawler[1] - 1] == 'S' || map[crawler[0], crawler[1] - 1] == 'D')
                    {
                        for (int x = 0; x < width; x++)
                        {
                            map[crawler[0], crawler[1] + x] = ' ';
                        }
                        map[crawler[0], crawler[1] + width] = 'X';
                    }
                    else
                    {
                        if (map[crawler[0] + 1, crawler[1]] == 'X')
                        {
                            for (int x = 0; x < width; x++)
                            {
                                map[crawler[0], crawler[1] + x] = ' ';
                            }
                            map[crawler[0], crawler[1] + width] = 'X';
                            crawler[0] = crawler[0] + 1;
                            crawler[1] = roomStart[1];
                        }

                        for (int x = 0; x < width; x++)
                        {
                            map[crawler[0], crawler[1] + x] = 'X';
                        }
                        map[crawler[0], crawler[1] + width] = 'X';

                        isFinishedVertical = true;
                    }
                    height++;
                }
                else
                {
                    isFinishedVertical = true;
                }
            }

            return height;
        }

        public static void AddInteriorDoor(char[,] map, int[] roomStart, int roomHeight, int roomWidth, int mapHeight, int mapWidth)
        {
            int doorFixedPosition = 0;

            if (roomStart[0] + roomHeight < mapHeight - 1 && roomStart[1] + roomWidth < mapWidth - 1)
            {
                if (rng.NextDouble() > .5)  //if true, build eastern door, else build southern door
                {
                    doorFixedPosition = roomStart[1] + roomWidth;
                    bool isDoorBuilt = BuildEastDoor(map, roomStart[0], roomHeight, doorFixedPosition, mapHeight, mapWidth);
                    if (!isDoorBuilt)
                    {
                        doorFixedPosition = roomStart[0] + roomHeight - 1;
                        isDoorBuilt = BuildSouthDoor(map, roomStart[1], doorFixedPosition, roomWidth, mapHeight, mapWidth);
                    }
                }
                else
                {
                    doorFixedPosition = roomStart[0] + roomHeight - 1;
                    bool isDoorBuilt = BuildSouthDoor(map, roomStart[1], doorFixedPosition, roomWidth, mapHeight, mapWidth);
                    if (!isDoorBuilt)
                    {
                        doorFixedPosition = roomStart[1] + roomWidth;
                        BuildEastDoor(map, roomStart[0], roomHeight, doorFixedPosition, mapHeight, mapWidth);
                    }
                }
            }
            else if (roomStart[0] + roomHeight == mapHeight - 1 && roomStart[1] + roomWidth < mapWidth - 1)
            {
                doorFixedPosition = roomStart[1] + roomWidth;
                BuildEastDoor(map, roomStart[0], roomHeight, doorFixedPosition, mapHeight, mapWidth);
            }
            else if (roomStart[1] + roomWidth == mapWidth - 1 && roomStart[0] + roomHeight < mapHeight - 1)
            {
                doorFixedPosition = roomStart[0] + roomHeight - 1;
                BuildSouthDoor(map, roomStart[1], doorFixedPosition, roomWidth, mapHeight, mapWidth);
            }

        }

        public static bool BuildEastDoor(char[,] map, int roomStart, int roomHeight, int doorFixedPosition, int mapHeight, int mapWidth)
        {
            int doorEastPosition = rng.Next(roomStart, roomStart + roomHeight - 1); //-1 to avoid door in corner
            bool validDoor = CheckEastDoorValidity(map, doorEastPosition, doorFixedPosition);
            if (validDoor)
            {
                if (doorFixedPosition == 0)
                {
                    map[doorEastPosition, doorFixedPosition] = 'D';
                }
                else
                {
                    map[doorEastPosition, doorFixedPosition] = 'E';
                }
            }
            else //In case our randomly generated door was invalid
            {
                for (int newPosition = roomStart; newPosition < roomStart + roomHeight - 1; newPosition++)
                {
                    validDoor = CheckEastDoorValidity(map, newPosition, doorFixedPosition);
                    if (validDoor)
                    {
                        if (doorFixedPosition == 0)
                        {
                            map[newPosition, doorFixedPosition] = 'D';
                        }
                        else
                        {
                            map[newPosition, doorFixedPosition] = 'E';
                        }
                        break;
                    }
                }
            }
            return validDoor;
        }

        public static bool BuildSouthDoor(char[,] map, int roomStart, int doorFixedPosition, int roomWidth, int mapHeight, int mapWidth)
        {
            int doorSouthPosition = rng.Next(roomStart, roomStart + roomWidth - 1); //-1 to avoid door in corner

            bool validDoor = CheckSouthDoorValidity(map, doorSouthPosition, doorFixedPosition);
            if (validDoor)      //We only have to check door validity because of exterior doors              
            {
                if (doorFixedPosition == 0)
                {
                    map[doorFixedPosition, doorSouthPosition] = 'D';
                }
                else
                {
                    map[doorFixedPosition, doorSouthPosition] = 'S';
                }

            }
            else //In case our randomly generated door was invalid
            {
                for (int newPosition = roomStart; newPosition < roomStart + roomWidth - 1; newPosition++)
                {
                    validDoor = CheckSouthDoorValidity(map, newPosition, doorFixedPosition);
                    if (validDoor)
                    {
                        if (doorFixedPosition == 0)
                        {
                            map[doorFixedPosition, newPosition] = 'D';
                        }
                        else
                        {
                            map[doorFixedPosition, newPosition] = 'S';
                        }
                        break;
                    }
                }
            }

            return validDoor;
        }

        public static bool BuildWestDoor(char[,] map, int roomStart, int roomHeight, int doorFixedPosition, int mapHeight, int mapWidth)
        {
            int doorWestPosition = rng.Next(roomStart, roomStart + roomHeight - 1); //-1 to avoid door in corner
            bool validDoor = CheckWestDoorValidity(map, doorWestPosition, doorFixedPosition);
            if (validDoor)
            {
                if (doorFixedPosition == mapWidth - 1)
                {
                    map[doorWestPosition, doorFixedPosition] = 'D';
                }
                else
                {
                    map[doorWestPosition, doorFixedPosition] = 'E';
                }
            }
            else //In case our randomly generated door was invalid
            {
                for (int newPosition = roomStart; newPosition < roomStart + roomHeight - 1; newPosition++)
                {
                    validDoor = CheckWestDoorValidity(map, newPosition, doorFixedPosition);
                    if (validDoor)
                    {
                        if (doorFixedPosition == mapWidth - 1)
                        {
                            map[newPosition, doorFixedPosition] = 'D';
                        }
                        else
                        {
                            map[newPosition, doorFixedPosition] = 'E';
                        }
                        break;
                    }
                }
            }
            return validDoor;
        }

        public static bool BuildNorthDoor(char[,] map, int roomStart, int doorFixedPosition, int roomWidth, int mapHeight, int mapWidth)
        {
            int doorNorthPosition = rng.Next(roomStart, roomStart + roomWidth - 1); //-1 to avoid door in corner

            bool validDoor = CheckNorthDoorValidity(map, doorNorthPosition, doorFixedPosition);
            if (validDoor)
            {
                if (doorFixedPosition == mapHeight - 1)
                {
                    map[doorFixedPosition, doorNorthPosition] = 'D';
                }
                else
                {
                    map[doorFixedPosition, doorNorthPosition] = 'S';
                }
            }
            else //In case our randomly generated door was invalid
            {
                for (int newPosition = roomStart; newPosition < roomStart + roomWidth - 1; newPosition++)
                {
                    validDoor = CheckNorthDoorValidity(map, newPosition, doorFixedPosition);
                    if (validDoor)
                    {
                        if (doorFixedPosition == mapHeight - 1)
                        {
                            map[doorFixedPosition, newPosition] = 'D';
                        }
                        else
                        {
                            map[doorFixedPosition, newPosition] = 'S';
                        }
                        break;
                    }
                }
            }

            return validDoor;
        }

        public static bool CheckEastDoorValidity(char[,] map, int doorPosition, int wallPosition)
        {
            bool isValid = false;

            if (map[doorPosition, wallPosition + 1] == 'X' || map[doorPosition, wallPosition + 1] == 'S' || map[doorPosition, wallPosition + 1] == 'E' || map[doorPosition, wallPosition + 1] == 'D')
            {
                isValid = false;
            }
            else
            {
                isValid = true;
            }

            return isValid;
        }

        public static bool CheckSouthDoorValidity(char[,] map, int doorPosition, int wallPosition)
        {
            bool isValid = false;

            if (map[wallPosition + 1, doorPosition] == 'X' || map[wallPosition + 1, doorPosition] == 'S' || map[wallPosition + 1, doorPosition] == 'E' || map[wallPosition + 1, doorPosition] == 'D')
            {
                isValid = false;
            }
            else
            {
                isValid = true;
            }

            return isValid;
        }

        public static bool CheckWestDoorValidity(char[,] map, int doorPosition, int wallPosition)
        {
            bool isValid = false;

            if (map[doorPosition, wallPosition - 1] == 'X' || map[doorPosition, wallPosition - 1] == 'S' || map[doorPosition, wallPosition - 1] == 'E' || map[doorPosition, wallPosition - 1] == 'D')
            {
                isValid = false;
            }
            else
            {
                isValid = true;
            }

            return isValid;
        }

        public static bool CheckNorthDoorValidity(char[,] map, int doorPosition, int wallPosition)
        {
            bool isValid = false;

            if (map[wallPosition - 1, doorPosition] == 'X' || map[wallPosition - 1, doorPosition] == 'S' || map[wallPosition - 1, doorPosition] == 'E' || map[wallPosition - 1, doorPosition] == 'D')
            {
                isValid = false;
            }
            else
            {
                isValid = true;
            }

            return isValid;
        }

        public static void CreateExteriorDoor(char[,] map, int height, int width)
        {
            double cardinalDirection = rng.NextDouble();
            bool doorCreated = false;

            if (cardinalDirection > .75)
            {
                doorCreated = BuildSouthDoor(map, 0, 0, width - 1, height, width);
            }
            else if (cardinalDirection > .5)
            {
                doorCreated = BuildWestDoor(map, 0, height - 1, width - 1, height, width);
            }
            else if (cardinalDirection > .25)
            {
                doorCreated = BuildNorthDoor(map, 0, height - 1, width - 1, height, width);
            }
            else
            {
                doorCreated = BuildEastDoor(map, 0, height - 1, 0, height, width);
            }
        }

        public static void CreateStairs(int floor, int mapHeight, int mapWidth)
        {
            bool validLocation = false;
            int[] stairLocation = new int[2];
            
            if (floor == 2)
            {
                validLocation = BuildRandomStairs(map1, map2, mapHeight, mapWidth);

                if (!validLocation)
                {
                    validLocation = BuildInteriorStairs(map1, map2, mapHeight, mapWidth);
                }

                if (!validLocation)
                {
                    BuildWallStairs(map1, map2, mapHeight, mapWidth);
                }
            }
            else if (floor == 3)
            {
                validLocation = BuildRandomStairs(map2, map3, mapHeight, mapWidth);
                
                if (!validLocation)
                {
                    validLocation = BuildInteriorStairs(map2, map3, mapHeight, mapWidth);
                }

                if (!validLocation)
                {
                    BuildWallStairs(map2, map3, mapHeight, mapWidth);
                }
            }
        }

        private static bool BuildRandomStairs(char[,] bottomFloorMap, char[,] topFloorMap, int mapHeight, int mapWidth)
        {
            int[] stairLocation = new int[2];
            int cycles = 1;
            while (cycles < 200)
            {
                stairLocation[0] = rng.Next(1, mapHeight - 2);
                stairLocation[1] = rng.Next(1, mapWidth - 2);
                if (bottomFloorMap[stairLocation[0], stairLocation[1]] == ' ')
                {
                    if (topFloorMap[stairLocation[0], stairLocation[1]] == ' ')
                    {
                        bottomFloorMap[stairLocation[0], stairLocation[1]] = 'H';
                        topFloorMap[stairLocation[0], stairLocation[1]] = 'L';
                        return true;
                    }
                }
                cycles++;
            }

            return false;
        }

        private static bool BuildInteriorStairs(char[,] bottomFloor, char[,] topFloor, int mapHeight, int mapWidth)
        {
            int[] stairLocation = new int[2];

            for (int x = 1; x < mapHeight - 1; x++)
            {
                for (int y = 1; y < mapWidth - 1; y++)
                {
                    stairLocation[0] = x;
                    stairLocation[1] = y;
                    if (bottomFloor[stairLocation[0], stairLocation[1]] == ' ')
                    {
                        if (topFloor[stairLocation[0], stairLocation[1]] == ' ')
                        {
                            bottomFloor[stairLocation[0], stairLocation[1]] = 'H';
                            topFloor[stairLocation[0], stairLocation[1]] = 'L';
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private static void BuildWallStairs(char[,] bottomFloorMap, char[,] topFloorMap, int mapHeight, int mapWidth)
        {
            int[] stairLocation = new int[2];

            for (int x = 0; x < mapHeight; x++)
            {
                stairLocation[0] = x;
                stairLocation[1] = 0;
                if (bottomFloorMap[stairLocation[0], stairLocation[1]] == 'X')
                {
                    if (topFloorMap[stairLocation[0], stairLocation[1]] == 'X')
                    {
                        bottomFloorMap[stairLocation[0], stairLocation[1]] = 'H';
                        topFloorMap[stairLocation[0], stairLocation[1]] = 'L';
                        return;
                    }
                }
            }
        }
    }
}

