using System;

namespace See_fight_console
{
    class Program
    {

        static void WriteGround(char[,] ground)
        {
            Console.Write("     ");
            for (int i = 0; i < ground.GetLength(0); ++i)
                Console.Write(i + ". ");
            Console.Write('y');
            Console.WriteLine('\n');

            for (int i = 0; i < ground.GetLength(0); ++i)
            {
                Console.Write($"{i}.   ");
                for (int j = 0; j < ground.GetLength(1); ++j)
                    if (ground[i, j] == '\0')
                        Console.Write(Convert.ToInt32((ground[i, j])) + "  ");
                    else
                        Console.Write(ground[i, j] + "  ");
                Console.WriteLine();
            }
            Console.WriteLine("x\n");
        }

        static void ArrangementShips(ref char[,] ground)
        {
           /* 
            * Variables for arrangement ships. 
            * These values are inversely proportional.
            */
            uint ship_length = 4;
            uint ship_count = 1;

            // coordinates
            uint x = 0;
            uint y = 0;

            // arrangement
            while(ship_count <= 4)
            {

                if (ship_length == 4) Console.WriteLine("\nWrite coordinates for Linkor(four deck) ship:");
                else if (ship_length == 3) Console.WriteLine("\nWrite coordinates for Cruisers(three deck) ships:");
                else if (ship_length == 2) Console.WriteLine("\nWrite coordinates for Destroyers(two deck) ships:");
                else if (ship_length == 1) Console.WriteLine("\nWrite coordinates for Torpedo Boats(one deck ship):");

                for (uint i = 0; i < ship_count; ++i)
                {
                    bool isAskX = true;
                    bool isAskY = true;

                    uint last_y = 0;

                    if (ship_length == 3) Console.WriteLine($"Write coordinates for next {i + 1} Cruiser(three deck) ship:");
                    else if (ship_length == 2) Console.WriteLine($"Write coordinates for next {i + 1} Destroyer(two deck) ship:");
                    else if (ship_length == 1) Console.WriteLine($"Write coordinates for next {i + 1} Torpedo Boat(one deck ship):");
                    for (uint j = 0; j < ship_length; ++j)
                    {
                        // initialize y
                        if (isAskY)
                            do
                            {
                                try
                                {
                                    Console.Write("Coordinate y: ");
                                    string readed = Console.ReadLine();
                                    uint.TryParse(readed, out y);
                                    if (!(y >= 0 && y <= 10))
                                    {
                                        Console.WriteLine("\nThis coordinate doesn't exist! Please, try again.");
                                        continue;
                                    }


                                    break;
                                }
                                catch
                                {
                                    Console.WriteLine("Your input is wrong! Please, try again.");
                                }
                            } while (true);
                    }
                        {
                            if (j == 0) last_y = y;

                            if (j == 1 && last_y == y) isAskY = false;
                            else if (j == 1 && last_y != y) isAskX = false;
                        }

                        // initialize x
                        if (isAskX)
                            do
                            {
                                try
                                {
                                    Console.Write("Coordinate x: ");
                                    string readed = Console.ReadLine();
                                    uint.TryParse(readed, out x);
                                    if (!(y >= 0 && y <= 10))
                                    {
                                        Console.WriteLine("\nThis coordinate doesn't exist! Please, try again.");
                                        continue;
                                    }

                                    break;
                                }
                                catch
                                {
                                    Console.WriteLine("Your input is wrong! Please, try again.");
                                }
                            } while (true);

                        ground[x, y] = '+';
                    }
                    Console.WriteLine();
                    WriteGround(ground);
                }
                ++ship_count;
                --ship_length;
            }

        }

        static void Main(string[] args)
        {
            string player1_name; 
            string player2_name;
            char[,] ground1 = new char[10, 10];
            char[,] ground2 = new char[10, 10];

            // greeting and initializing names
            {
                Console.WriteLine("Hello, Players! What are your names?\n");

                Console.Write("Player 1 name: ");
                player1_name = Console.ReadLine();

                Console.Write("Player 2 name: ");
                player2_name = Console.ReadLine();

                Console.WriteLine();
            }

            // ship arrangement
            // player 1
            {
                Console.WriteLine(player1_name + ", start arrangement your ships!");

                Console.WriteLine("Your map:\n");
                WriteGround(ground1);
                ArrangementShips(ref ground1);
            }
        }

    }
}
