using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MapGenerator
{
    static class Program
    {
        public static Map map1;
        public static Map map2;
        public static Map map3;
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
            Map map = new Map(mapHeight, mapWidth);

            FillEmpty(map);
            CreateBorder(map);

            while (mapComplete == false)
            {
                mapComplete = CreateRoom(map);
            }

            if (floor == 1)
            {
                CreateExteriorDoor(map);
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

            CreateStairs(floor, map);
        }

        private static void FillEmpty(Map map)
        {
            for (int x = 1; x < map.Height - 1; x++)
            {
                for (int y = 1; y < map.Width - 1; y++)
                {
                    map.Contents[x, y] = 'O';
                }
            }
        }

        private static void CreateBorder(Map map)
        {
            for (int x = 0; x < map.Width; x++)
            {
                map.Contents[0, x] = 'X';
                map.Contents[map.Height - 1, x] = 'X';
            }

            for (int y = 0; y < map.Height; y++)
            {
                map.Contents[y, 0] = 'X';
                map.Contents[y, map.Width - 1] = 'X';
            }
        }

        private static bool CreateRoom(Map map)
        {
            int[] roomStart = new int[2] { 1, 1 };
            roomStart = FindRoomStart(map, roomStart);

            if (roomStart == null)
            {
                return true;
            }
            else
            {
                BuildRoom(map, roomStart);
                return false;
            }
        }

        private static int[] FindRoomStart(Map map, int[] crawler)
        {
            if (map.Contents[crawler[0], crawler[1]] == 'O')
            {
                return crawler;
            }
            else
            {
                if (crawler[1] < map.Width - 1)
                {
                    crawler[1]++;
                    crawler = FindRoomStart(map, crawler);
                }
                else if (crawler[0] < map.Height - 1)
                {
                    crawler[1] = 1;
                    crawler[0]++;
                    crawler = FindRoomStart(map, crawler);
                }
                else
                {
                    crawler = null;
                }
            }

            return crawler;
        }

        private static void BuildRoom(Map map, int[] roomStart)
        {
            int width = 1;
            int height = 1;

            width = BuildRoomEast(map, roomStart);
            height = BuildRoomSouth(map, roomStart, width);
            AddInteriorDoor(map, roomStart, height, width);
        }

        private static int BuildRoomEast(Map map, int[] roomStart)
        {
            int[] crawler = new int[2];
            crawler[0] = roomStart[0];
            crawler[1] = roomStart[1];
            int width = 1;

            map.Contents[crawler[0], crawler[1]] = ' ';
            if (map.Contents[crawler[0] - 1, crawler[1]] == 'O')
            {
                map.Contents[crawler[0] - 1, crawler[1]] = 'X';
            }
            bool isFinishedHorizontal = false;
            while (isFinishedHorizontal == false)
            {
                crawler[1]++;
                if (map.Contents[crawler[0], crawler[1]] == 'O')
                {
                    if (rng.NextDouble() > roomSizeModifier || map.Contents[crawler[0] - 1, crawler[1]] == 'E' || map.Contents[crawler[0] - 1, crawler[1]] == 'S' || map.Contents[crawler[0] - 1, crawler[1]] == 'D')
                    {
                        map.Contents[crawler[0], crawler[1]] = ' ';

                        if (map.Contents[crawler[0] - 1, crawler[1]] == 'O')
                        {
                            map.Contents[crawler[0] - 1, crawler[1]] = 'X';
                        }
                        width++;
                    }
                    else
                    {
                        if (map.Contents[crawler[0], crawler[1] + 1] == 'X')
                        {
                            map.Contents[crawler[0], crawler[1]] = ' ';
                            crawler[1]++;
                            width++;
                        }
                        map.Contents[crawler[0], crawler[1]] = 'X';
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

        private static int BuildRoomSouth(Map map, int[] roomStart, int width)
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
                    if (map.Contents[crawler[0], crawler[1] + x] != 'O')
                    {
                        isValidRow = false;
                        break;
                    }
                }
                if (isValidRow)
                {
                    if (map.Contents[crawler[0], crawler[1] - 1] == 'O' || map.Contents[crawler[0], crawler[1] - 1] == ' ')
                    {
                        map.Contents[crawler[0], crawler[1] - 1] = 'X';
                    }
                    if (rng.NextDouble() > roomSizeModifier || map.Contents[crawler[0], crawler[1] - 1] == 'E' || map.Contents[crawler[0], crawler[1] - 1] == 'S' || map.Contents[crawler[0], crawler[1] - 1] == 'D')
                    {
                        for (int x = 0; x < width; x++)
                        {
                            map.Contents[crawler[0], crawler[1] + x] = ' ';
                        }
                        map.Contents[crawler[0], crawler[1] + width] = 'X';
                    }
                    else
                    {
                        if (map.Contents[crawler[0] + 1, crawler[1]] == 'X')
                        {
                            for (int x = 0; x < width; x++)
                            {
                                map.Contents[crawler[0], crawler[1] + x] = ' ';
                            }
                            map.Contents[crawler[0], crawler[1] + width] = 'X';
                            crawler[0] = crawler[0] + 1;
                            crawler[1] = roomStart[1];
                        }

                        for (int x = 0; x < width; x++)
                        {
                            map.Contents[crawler[0], crawler[1] + x] = 'X';
                        }
                        map.Contents[crawler[0], crawler[1] + width] = 'X';

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

        private static void AddInteriorDoor(Map map, int[] roomStart, int roomHeight, int roomWidth)
        {
            int doorFixedPosition = 0;

            if (roomStart[0] + roomHeight < map.Height - 1 && roomStart[1] + roomWidth < map.Width - 1)
            {
                if (rng.NextDouble() > .5)  //if true, build eastern door, else build southern door
                {
                    doorFixedPosition = roomStart[1] + roomWidth;
                    bool isDoorBuilt = BuildEastDoor(map, roomStart[0], roomHeight, doorFixedPosition);
                    if (!isDoorBuilt)
                    {
                        doorFixedPosition = roomStart[0] + roomHeight - 1;
                        isDoorBuilt = BuildSouthDoor(map, roomStart[1], doorFixedPosition, roomWidth);
                    }
                }
                else
                {
                    doorFixedPosition = roomStart[0] + roomHeight - 1;
                    bool isDoorBuilt = BuildSouthDoor(map, roomStart[1], doorFixedPosition, roomWidth);
                    if (!isDoorBuilt)
                    {
                        doorFixedPosition = roomStart[1] + roomWidth;
                        BuildEastDoor(map, roomStart[0], roomHeight, doorFixedPosition);
                    }
                }
            }
            else if (roomStart[0] + roomHeight == map.Height - 1 && roomStart[1] + roomWidth < map.Width - 1)
            {
                doorFixedPosition = roomStart[1] + roomWidth;
                BuildEastDoor(map, roomStart[0], roomHeight, doorFixedPosition);
            }
            else if (roomStart[1] + roomWidth == map.Width - 1 && roomStart[0] + roomHeight < map.Height - 1)
            {
                doorFixedPosition = roomStart[0] + roomHeight - 1;
                BuildSouthDoor(map, roomStart[1], doorFixedPosition, roomWidth);
            }

        }

        private static bool BuildEastDoor(Map map, int roomStart, int roomHeight, int doorFixedPosition)
        {
            int doorEastPosition = rng.Next(roomStart, roomStart + roomHeight - 1); //-1 to avoid door in corner
            bool validDoor = CheckEastDoorValidity(map, doorEastPosition, doorFixedPosition);
            if (validDoor)
            {
                if (doorFixedPosition == 0)
                {
                    map.Contents[doorEastPosition, doorFixedPosition] = 'D';
                }
                else
                {
                    map.Contents[doorEastPosition, doorFixedPosition] = 'E';
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
                            map.Contents[newPosition, doorFixedPosition] = 'D';
                        }
                        else
                        {
                            map.Contents[newPosition, doorFixedPosition] = 'E';
                        }
                        break;
                    }
                }
            }
            return validDoor;
        }

        private static bool BuildSouthDoor(Map map, int roomStart, int doorFixedPosition, int roomWidth)
        {
            int doorSouthPosition = rng.Next(roomStart, roomStart + roomWidth - 1); //-1 to avoid door in corner

            bool validDoor = CheckSouthDoorValidity(map, doorSouthPosition, doorFixedPosition);
            if (validDoor)      //We only have to check door validity because of exterior doors              
            {
                if (doorFixedPosition == 0)
                {
                    map.Contents[doorFixedPosition, doorSouthPosition] = 'D';
                }
                else
                {
                    map.Contents[doorFixedPosition, doorSouthPosition] = 'S';
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
                            map.Contents[doorFixedPosition, newPosition] = 'D';
                        }
                        else
                        {
                            map.Contents[doorFixedPosition, newPosition] = 'S';
                        }
                        break;
                    }
                }
            }

            return validDoor;
        }

        private static bool BuildWestDoor(Map map, int roomStart, int roomHeight, int doorFixedPosition)
        {
            int doorWestPosition = rng.Next(roomStart, roomStart + roomHeight - 1); //-1 to avoid door in corner
            bool validDoor = CheckWestDoorValidity(map, doorWestPosition, doorFixedPosition);
            if (validDoor)
            {
                if (doorFixedPosition == map.Width - 1)
                {
                    map.Contents[doorWestPosition, doorFixedPosition] = 'D';
                }
                else
                {
                    map.Contents[doorWestPosition, doorFixedPosition] = 'E';
                }
            }
            else //In case our randomly generated door was invalid
            {
                for (int newPosition = roomStart; newPosition < roomStart + roomHeight - 1; newPosition++)
                {
                    validDoor = CheckWestDoorValidity(map, newPosition, doorFixedPosition);
                    if (validDoor)
                    {
                        if (doorFixedPosition == map.Width - 1)
                        {
                            map.Contents[newPosition, doorFixedPosition] = 'D';
                        }
                        else
                        {
                            map.Contents[newPosition, doorFixedPosition] = 'E';
                        }
                        break;
                    }
                }
            }
            return validDoor;
        }

        private static bool BuildNorthDoor(Map map, int roomStart, int doorFixedPosition, int roomWidth)
        {
            int doorNorthPosition = rng.Next(roomStart, roomStart + roomWidth - 1); //-1 to avoid door in corner

            bool validDoor = CheckNorthDoorValidity(map, doorNorthPosition, doorFixedPosition);
            if (validDoor)
            {
                if (doorFixedPosition == map.Height - 1)
                {
                    map.Contents[doorFixedPosition, doorNorthPosition] = 'D';
                }
                else
                {
                    map.Contents[doorFixedPosition, doorNorthPosition] = 'S';
                }
            }
            else //In case our randomly generated door was invalid
            {
                for (int newPosition = roomStart; newPosition < roomStart + roomWidth - 1; newPosition++)
                {
                    validDoor = CheckNorthDoorValidity(map, newPosition, doorFixedPosition);
                    if (validDoor)
                    {
                        if (doorFixedPosition == map.Height - 1)
                        {
                            map.Contents[doorFixedPosition, newPosition] = 'D';
                        }
                        else
                        {
                            map.Contents[doorFixedPosition, newPosition] = 'S';
                        }
                        break;
                    }
                }
            }

            return validDoor;
        }

        private static bool CheckEastDoorValidity(Map map, int doorPosition, int wallPosition)
        {
            bool isValid = false;

            if (map.Contents[doorPosition, wallPosition + 1] == 'X' || map.Contents[doorPosition, wallPosition + 1] == 'S' || map.Contents[doorPosition, wallPosition + 1] == 'E' || map.Contents[doorPosition, wallPosition + 1] == 'D')
            {
                isValid = false;
            }
            else
            {
                isValid = true;
            }

            return isValid;
        }

        private static bool CheckSouthDoorValidity(Map map, int doorPosition, int wallPosition)
        {
            bool isValid = false;

            if (map.Contents[wallPosition + 1, doorPosition] == 'X' || map.Contents[wallPosition + 1, doorPosition] == 'S' || map.Contents[wallPosition + 1, doorPosition] == 'E' || map.Contents[wallPosition + 1, doorPosition] == 'D')
            {
                isValid = false;
            }
            else
            {
                isValid = true;
            }

            return isValid;
        }

        private static bool CheckWestDoorValidity(Map map, int doorPosition, int wallPosition)
        {
            bool isValid = false;

            if (map.Contents[doorPosition, wallPosition - 1] == 'X' || map.Contents[doorPosition, wallPosition - 1] == 'S' || map.Contents[doorPosition, wallPosition - 1] == 'E' || map.Contents[doorPosition, wallPosition - 1] == 'D')
            {
                isValid = false;
            }
            else
            {
                isValid = true;
            }

            return isValid;
        }

        private static bool CheckNorthDoorValidity(Map map, int doorPosition, int wallPosition)
        {
            bool isValid = false;

            if (map.Contents[wallPosition - 1, doorPosition] == 'X' || map.Contents[wallPosition - 1, doorPosition] == 'S' || map.Contents[wallPosition - 1, doorPosition] == 'E' || map.Contents[wallPosition - 1, doorPosition] == 'D')
            {
                isValid = false;
            }
            else
            {
                isValid = true;
            }

            return isValid;
        }

        private static void CreateExteriorDoor(Map map)
        {
            double cardinalDirection = rng.NextDouble();
            bool doorCreated = false;

            if (cardinalDirection > .75)
            {
                doorCreated = BuildSouthDoor(map, 0, 0, map.Width - 1);
            }
            else if (cardinalDirection > .5)
            {
                doorCreated = BuildWestDoor(map, 0, map.Height - 1, map.Width - 1);
            }
            else if (cardinalDirection > .25)
            {
                doorCreated = BuildNorthDoor(map, 0, map.Height - 1, map.Width - 1);
            }
            else
            {
                doorCreated = BuildEastDoor(map, 0, map.Height - 1, 0);
            }
        }

        private static void CreateStairs(int floor, Map map)
        {
            bool validLocation = false;
            int[] stairLocation = new int[2];

            if (floor == 2)
            {
                validLocation = BuildRandomStairs(map1, map2);

                if (!validLocation)
                {
                    validLocation = BuildInteriorStairs(map1, map2);
                }

                if (!validLocation)
                {
                    BuildWallStairs(map1, map2);
                }
            }
            else if (floor == 3)
            {
                validLocation = BuildRandomStairs(map2, map3);

                if (!validLocation)
                {
                    validLocation = BuildInteriorStairs(map2, map3);
                }

                if (!validLocation)
                {
                    BuildWallStairs(map2, map3);
                }
            }
        }

        private static bool BuildRandomStairs(Map bottomFloorMap, Map topFloorMap)
        {
            int[] stairLocation = new int[2];
            int cycles = 1;
            while (cycles < 200)
            {
                stairLocation[0] = rng.Next(1, topFloorMap.Height - 2);
                stairLocation[1] = rng.Next(1, topFloorMap.Width - 2);
                if (bottomFloorMap.Contents[stairLocation[0], stairLocation[1]] == ' ')
                {
                    if (topFloorMap.Contents[stairLocation[0], stairLocation[1]] == ' ')
                    {
                        bottomFloorMap.Contents[stairLocation[0], stairLocation[1]] = 'H';
                        topFloorMap.Contents[stairLocation[0], stairLocation[1]] = 'L';
                        return true;
                    }
                }
                cycles++;
            }

            return false;
        }

        private static bool BuildInteriorStairs(Map bottomFloor, Map topFloor)
        {
            int[] stairLocation = new int[2];

            for (int x = 1; x < topFloor.Height - 1; x++)
            {
                for (int y = 1; y < topFloor.Width - 1; y++)
                {
                    stairLocation[0] = x;
                    stairLocation[1] = y;
                    if (bottomFloor.Contents[stairLocation[0], stairLocation[1]] == ' ')
                    {
                        if (topFloor.Contents[stairLocation[0], stairLocation[1]] == ' ')
                        {
                            bottomFloor.Contents[stairLocation[0], stairLocation[1]] = 'H';
                            topFloor.Contents[stairLocation[0], stairLocation[1]] = 'L';
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private static void BuildWallStairs(Map bottomFloorMap, Map topFloorMap)
        {
            int[] stairLocation = new int[2];

            for (int x = 0; x < topFloorMap.Height; x++)
            {
                stairLocation[0] = x;
                stairLocation[1] = 0;
                if (bottomFloorMap.Contents[stairLocation[0], stairLocation[1]] == 'X')
                {
                    if (topFloorMap.Contents[stairLocation[0], stairLocation[1]] == 'X')
                    {
                        bottomFloorMap.Contents[stairLocation[0], stairLocation[1]] = 'H';
                        topFloorMap.Contents[stairLocation[0], stairLocation[1]] = 'L';
                        return;
                    }
                }
            }
        }
    }
}

