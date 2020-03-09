﻿using System;

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
            int ship_length = 4;
            uint ship_count = 1;

            // coordinates
            int x = 0;
            int y = 0;

            // arrangement
            while (ship_count <= 4)
            {

                if (ship_length == 4) Console.WriteLine("\nWrite coordinates for Linkor(four deck) ship:");
                else if (ship_length == 3) Console.WriteLine("\nWrite coordinates for Cruisers(three deck) ships:");
                else if (ship_length == 2) Console.WriteLine("\nWrite coordinates for Destroyers(two deck) ships:");
                else if (ship_length == 1) Console.WriteLine("\nWrite coordinates for Torpedo Boats(one deck ship):");

                for (uint i = 0; i < ship_count; ++i)
                {
                    // check whether to request a new value for a variable
                    bool isAskX = true;
                    bool isAskY = true;

                    // for check truth of next value of x or y
                    int last_x = 0;
                    int last_y = 0;

                    int checkIsAsk = 0;

                    if (ship_length == 3) Console.WriteLine($"Write coordinates for next {i + 1} Cruiser(three deck) ship:");
                    else if (ship_length == 2) Console.WriteLine($"Write coordinates for next {i + 1} Destroyer(two deck) ship:");
                    else if (ship_length == 1) Console.WriteLine($"Write coordinates for next {i + 1} Torpedo Boat(one deck ship):");
                    for (int j = 0; j < ship_length; ++j)
                    {
                        // initialize y
                        if (isAskY)
                        {
                            do
                            {
                                try
                                {
                                    Console.Write("Coordinate y: ");
                                    string readed = Console.ReadLine();
                                    int.TryParse(readed, out y);
                                    if (!(y >= 0 && y <= 9))
                                    {
                                        if ((ship_length == 4 || ship_length == 3) && (ship_length - j == 1 || ship_length - j == 2))
                                        {
                                            bool a = (y < 0) ? true : false;

                                            if (a)
                                            {
                                                ++y;
                                                while (ground[x, y] == '+')
                                                    ++y;
                                                if (ship_length - 2 == j)
                                                    ground[x, y++] = '+';
                                            }
                                            else
                                            {
                                                --y;
                                                while (ground[x, y] == '+')
                                                    --y;
                                                if (ship_length - 2 == j)
                                                    ground[x, y--] = '+';
                                            }
                                            j = ship_length;
                                            break;
                                        }
                                        Console.WriteLine("\nThis coordinate doesn't exist! Please, try again.");
                                        continue;
                                    }

                                    break;
                                }
                                catch
                                {
                                    Console.WriteLine("\nYour input is wrong! Please, try again.");
                                }
                            } while (true);

                            if (j != 0 && j != ship_length)
                                if (last_y == y && j != 1)
                                {
                                    Console.WriteLine("\nInvalid coordinate! Please, try again.1");
                                    --j;
                                    continue;
                                }
                                else if (!(last_y - 1 == y || last_y + 1 == y) && j != 1)
                                {
                                    Console.WriteLine("\nInvalid coordinate! Please, try again.2");
                                    --j;
                                    continue;
                                }

                            last_y = y;
                        }

                        // check whether it makes sense to ask the value of one of the variables
                        {
                            if (j == 0) checkIsAsk = y;

                            if (j == 1 && checkIsAsk == y) isAskY = false;
                            else if (j == 1 && checkIsAsk != y) isAskX = false;
                        }

                        // initialize x
                        if (isAskX)
                        {
                            do
                            {
                                try
                                {
                                    Console.Write("Coordinate x: ");
                                    string readed = Console.ReadLine();
                                    int.TryParse(readed, out x);
                                    if (!(x >= 0 && x <= 9))
                                    {
                                        Console.WriteLine("\nThis coordinate doesn't exist! Please, try again.");
                                        continue;
                                    }

                                    break;
                                }
                                catch
                                {
                                    Console.WriteLine("\nYour input is wrong! Please, try again.");
                                }
                            } while (true);

                            if (!(last_x - 1 == x || last_x + 1 == x) && j != 0)
                            {
                                Console.WriteLine("\nInvalid coordinate! Please, try again.");
                                --j;
                                continue;
                            }

                            last_x = x;
                        }

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

            // player 2
            {
                Console.WriteLine('\n' + player2_name + ", start arrangement your ships!");

                Console.WriteLine("Your map:\n");
                WriteGround(ground2);
                ArrangementShips(ref ground2);
            }

        }
    }
    
}

