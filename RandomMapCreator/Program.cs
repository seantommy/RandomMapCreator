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
        public static int[] stairLocation = new int[2];
        public static double roomSizeModifier = .2;

        //This line for testing purposes
        //This line also for testing purposes

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
            char[,] map;

            map = new char[mapHeight, mapWidth];
            Console.WriteLine("Map size: " + mapHeight + "," + mapWidth);


            FillEmpty(map, mapHeight, mapWidth);
            CreateBorder(map, mapHeight, mapWidth);

            while (mapComplete == false)
            {
                mapComplete = CreateRoom(map, mapHeight, mapWidth);
            }

            if (floor == 1)
            {
                CreateExteriorDoor(map, mapHeight, mapWidth);
            }

            if (floor == 1)
            {
                map1 = new char[mapHeight, mapWidth];

                for(int x = 0; x < mapHeight; x++)
                {
                    for(int y = 0; y < mapWidth; y++)
                    {
                        map1[x, y] = map[x, y];
                    }
                }
            }else if(floor == 2)
            {
                map2 = new char[mapHeight, mapWidth];

                for (int x = 0; x < mapHeight; x++)
                {
                    for (int y = 0; y < mapWidth; y++)
                    {
                        map2[x, y] = map[x, y];
                    }
                }
            }
            else if(floor == 3)
            {
                map3 = new char[mapHeight, mapWidth];

                for (int x = 0; x < mapHeight; x++)
                {
                    for (int y = 0; y < mapWidth; y++)
                    {
                        map3[x, y] = map[x, y];
                    }
                }
            }

            CreateStairs(map, floor, mapHeight, mapWidth);
            
            PrintMapConsole(map, mapHeight, mapWidth);
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
            int[] roomStart = new int[2];
            roomStart[0] = 1;
            roomStart[1] = 1;
            roomStart = FindRoomStart(map, mapHeight, mapWidth, roomStart);
            
            if (roomStart != null)
            {
                BuildRoom(map, mapHeight, mapWidth, roomStart);
                return false;
            }
            else
            {
                return true;
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

        public static void BuildRoom(char[,] map, int mapHeight, int mapWidth, int[] crawler)
        {

            int[] roomStart = new int[2];
            roomStart[0] = crawler[0];
            roomStart[1] = crawler[1];
            int width = 1;
            int height = 1;

            map[crawler[0], crawler[1]] = ' ';          //Building to the East
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


            crawler[0] = roomStart[0];
            crawler[1] = roomStart[1];
            bool isFinishedVertical = false;
            while (isFinishedVertical == false)  //Building to the South
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

            AddInteriorDoor(map, roomStart, height, width, mapHeight, mapWidth);
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
                        isDoorBuilt = BuildEastDoor(map, roomStart[0], roomHeight, doorFixedPosition, mapHeight, mapWidth);
                    }
                }
            }
            else if (roomStart[0] + roomHeight == mapHeight - 1 && roomStart[1] + roomWidth < mapWidth - 1)
            {
                doorFixedPosition = roomStart[1] + roomWidth;
                bool isDoorBuilt = BuildEastDoor(map, roomStart[0], roomHeight, doorFixedPosition, mapHeight, mapWidth);
            }
            else if (roomStart[1] + roomWidth == mapWidth - 1 && roomStart[0] + roomHeight < mapHeight - 1)
            {
                doorFixedPosition = roomStart[0] + roomHeight - 1;
                bool isDoorBuilt = BuildSouthDoor(map, roomStart[1], doorFixedPosition, roomWidth, mapHeight, mapWidth);
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
            else
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
            else
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
                if (doorFixedPosition == mapWidth -1)
                {
                    map[doorWestPosition, doorFixedPosition] = 'D';
                }
                else
                {
                    map[doorWestPosition, doorFixedPosition] = 'E';
                }
            }
            else
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
            else
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

            if (map[wallPosition+1, doorPosition] == 'X' || map[wallPosition + 1, doorPosition] == 'S' || map[wallPosition + 1, doorPosition] == 'E' || map[wallPosition + 1, doorPosition] == 'D')
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
                doorCreated = BuildSouthDoor(map, 0, 0, width-1, height, width);
            }
            else if(cardinalDirection > .5)
            {
                doorCreated = BuildWestDoor(map, 0, height-1, width-1, height, width);
            }
            else if(cardinalDirection > .25)
            {
                doorCreated = BuildNorthDoor(map, 0, height-1, width-1, height, width);
            }
            else
            {
                doorCreated = BuildEastDoor(map, 0, height-1, 0, height, width);
            }
        }

        public static void CreateStairs(char[,] map, int floor, int mapHeight, int mapWidth)
        {
            bool validLocation = false;
            int cycles = 0;

            if (floor == 2)
            {
                while (!validLocation && cycles < 200)
                {
                    stairLocation[0] = rng.Next(1, mapHeight - 2);
                    stairLocation[1] = rng.Next(1, mapWidth - 2);
                    if (map1[stairLocation[0], stairLocation[1]] == ' ')
                    {
                        if (map2[stairLocation[0], stairLocation[1]] == ' ')
                        {
                            validLocation = true;
                            map1[stairLocation[0], stairLocation[1]] = 'H';
                            map2[stairLocation[0], stairLocation[1]] = 'L';
                        }
                    }
                    cycles++;
                }

                if (!validLocation)
                {
                    for (int x = 1; x < mapHeight - 1; x++)
                    {
                        for (int y = 1; y < mapWidth - 1; y++)
                        {
                            stairLocation[0] = x;
                            stairLocation[1] = y;
                            if (map1[stairLocation[0], stairLocation[1]] == ' ')
                            {
                                if (map2[stairLocation[0], stairLocation[1]] == ' ')
                                {
                                    validLocation = true;
                                    map1[stairLocation[0], stairLocation[1]] = 'H';
                                    map2[stairLocation[0], stairLocation[1]] = 'L';
                                    break;
                                }
                            }
                        }
                        if (validLocation)
                        {
                            break;
                        }
                    }
                }

                if (!validLocation)
                {
                    for (int x = 0; x < mapHeight; x++)
                    {
                        stairLocation[0] = x;
                        stairLocation[1] = 0;
                        if (map1[stairLocation[0], stairLocation[1]] == 'X')
                        {
                            if (map2[stairLocation[0], stairLocation[1]] == 'X')
                            {
                                validLocation = true;
                                map1[stairLocation[0], stairLocation[1]] = 'H';
                                map2[stairLocation[0], stairLocation[1]] = 'L';
                                break;
                            }

                        }
                    }
                }

                validLocation = false;
                cycles = 0;
            }

                if (floor == 3)
                {
                    while (!validLocation && cycles < 200)
                    {
                        stairLocation[0] = rng.Next(1, mapHeight - 2);
                        stairLocation[1] = rng.Next(1, mapWidth - 2);
                        if (map2[stairLocation[0], stairLocation[1]] == ' ')
                        {
                            if (map3[stairLocation[0], stairLocation[1]] == ' ')
                            {
                                validLocation = true;
                                map2[stairLocation[0], stairLocation[1]] = 'H';
                                map3[stairLocation[0], stairLocation[1]] = 'L';
                            }
                        }
                        cycles++;
                    }

                    if (!validLocation)
                    {
                        for (int x = 1; x < mapHeight - 1; x++)
                        {
                            for (int y = 1; y < mapWidth - 1; y++)
                            {
                                stairLocation[0] = x;
                                stairLocation[1] = y;
                                if (map2[stairLocation[0], stairLocation[1]] == ' ')
                                {
                                    if (map3[stairLocation[0], stairLocation[1]] == ' ')
                                    {
                                        validLocation = true;
                                        map2[stairLocation[0], stairLocation[1]] = 'H';
                                        map3[stairLocation[0], stairLocation[1]] = 'L';
                                        break;
                                    }
                                }
                            }
                            if (validLocation)
                            {
                                break;
                            }
                        }
                    }

                if (!validLocation)
                {
                    for (int x = 0; x < mapHeight; x++)
                    {
                        stairLocation[0] = x;
                        stairLocation[1] = 0;
                        if (map2[stairLocation[0], stairLocation[1]] == 'X')
                        {
                            if (map3[stairLocation[0], stairLocation[1]] == 'X')
                            {
                                validLocation = true;
                                map2[stairLocation[0], stairLocation[1]] = 'H';
                                map3[stairLocation[0], stairLocation[1]] = 'L';
                                break;
                            }

                        }
                    }
                }
                
            }
        }

        public static void PrintMapConsole(char[,] map, int height, int width)
        {
            for (int x = 0; x < height; x++)
            {
                for (int y = 0; y < width; y++)
                {
                    Console.Write(map[x, y]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        
    }
}

