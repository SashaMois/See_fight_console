using System;
using System.Linq;

namespace See_fight_console
{
    class Program
    {
        static void DeleteCoordinate
        (
            int x, 
            int y, 
            ref char[,] ground
        )
        {
            ground[x, y] = '\0';
            Console.WriteLine("\nCoordinate {1};{0} succesfully delete!", x, y);
        }

        static void WriteGround(char[,] ground)
        {
            Console.Write("     ");
            for (int i = 1; i < ground.GetLength(0) - 1; ++i)
                Console.Write(i - 1 + ". ");
            Console.Write('y');
            Console.WriteLine('\n');

            for (int i = 1; i < ground.GetLength(0) - 1; ++i)
            {
                Console.Write($"{i - 1}.   ");
                for (int j = 1; j < ground.GetLength(1) - 1; ++j)
                    if (ground[i, j] == '\0')
                        Console.Write(Convert.ToInt32(ground[i, j]) + "  ");
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
            int x = -1;
            int y = -1;

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
                    int[] last_x = Enumerable.Repeat(10, ship_length).ToArray();
                    int[] last_y = Enumerable.Repeat(10, ship_length).ToArray();

                    char[,] intermediate_rez = new char[10, 10];

                    int checkIsAsk = 0;

                    if (ship_length == 3) Console.WriteLine($"Write coordinates for next {i + 1} Cruiser(three deck) ship:");
                    else if (ship_length == 2) Console.WriteLine($"Write coordinates for next {i + 1} Destroyer(two deck) ship:");
                    else if (ship_length == 1) Console.WriteLine($"Write coordinates for next {i + 1} Torpedo Boat(one deck ship):");
                    StartOver:
                    for (int j = 0; j < ship_length; ++j)
                    {
                    AgainY:
                        // initialize y
                        if (isAskY)
                        {
                            do
                            {
                                try
                                {
                                    Console.Write("Coordinate y: ");
                                    string readed = Console.ReadLine();
                                    if (readed == "delete coordinate")
                                    {
                                        if (j == 0)
                                        {
                                            Console.WriteLine("\nYou can't delete coordinate because it doesn't exist!");
                                            continue;
                                        }
                                        DeleteCoordinate(last_x[--j], last_y[j], ref intermediate_rez);
                                        last_x[j] = 10;
                                        last_y[j] = 10;
                                        if (j == 0)
                                        {
                                            isAskX = true;
                                            goto StartOver;
                                        }
                                        continue;
                                    }
                                    y = int.Parse(readed);
                                    if (!(y >= 0 && y <= 9))
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

                            if (j != 0 && !(j == 1 && last_y[j - 1] == y))
                            {
                                int k = 0;
                                for (k = 0; k < last_y.Length; ++k)
                                {
                                    if (last_y[k] == 10)
                                        continue;

                                    if (last_y[k] == y)
                                    {
                                        Console.WriteLine("\nThis coordinate already has one of the parts of the ship!\nPlease, try again.");
                                        goto AgainY;
                                    }
                                }
                                for (k = 0; k < last_y.Length; ++k)
                                {
                                    if (last_y[k] == 10)
                                        continue;

                                    if (y - 1 == last_y[k] || y + 1 == last_y[k])
                                        break;
                                }
                                if (k == last_y.Length)
                                {
                                    Console.WriteLine("\nYour input is wrong! Please, try again.");
                                    goto AgainY;
                                }
                            }
                        }
                        last_y[j] = y;

                        // check whether it makes sense to ask the value of one of the variables
                        {
                            if (j == 0) checkIsAsk = y;

                            if (j == 1 && checkIsAsk == y) isAskY = false;
                            else if (j == 1 && checkIsAsk != y) isAskX = false;
                        }

                    AgainX:
                        // initialize x
                        if (isAskX)
                        {
                            do
                            {
                                try
                                {
                                    Console.Write("Coordinate x: ");
                                    string readed = Console.ReadLine();
                                    if (readed == "delete coordinate")
                                    {
                                        if (j == 0)
                                        {
                                            Console.WriteLine("\nYou can't delete coordinate because it doesn't exist!");
                                            continue;
                                        }

                                        last_y[j] = 10;
                                        DeleteCoordinate(last_x[--j], last_y[j], ref intermediate_rez);
                                        last_x[j] = 10;
                                        last_y[j] = 10;

                                        if (j == 0)
                                        {
                                            isAskY = true;
                                            goto StartOver;
                                        }
                                        else if (j == 1)
                                        {
                                            isAskY = true;
                                            x = last_x[j - 1];
                                        }
                                        goto AgainY;
                                    }
                                    x = int.Parse(readed);
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

                            if (j != 0)
                            {
                                int k = 0;
                                for (k = 0; k < last_x.Length; ++k)
                                {
                                    if (last_x[k] == 10)
                                        continue;

                                    if (last_x[k] == x)
                                    {
                                        Console.WriteLine("\nThis coordinate already has one of the parts of the ship!\nPlease, try again.");
                                        if (j == 1)
                                        {
                                            isAskY = true;
                                            last_y[j] = 10;
                                            goto AgainY;
                                        }
                                        goto AgainX;
                                    }
                                }
                                for (k = 0; k < last_x.Length; ++k)
                                {
                                    if (last_x[k] == 10)
                                        continue;

                                    if (x - 1 == last_x[k] || x + 1 == last_x[k])
                                        break;
                                }
                                if (k == last_x.Length)
                                {
                                    Console.WriteLine("\nYour input is wrong! Please, try again.");
                                    goto AgainX;
                                }
                            }
                        }
                        last_x[j] = x;

                        intermediate_rez[x, y] = '+';
                        if (
                            intermediate_rez[x, y] == ground[x + 1, y + 1] ||
                            intermediate_rez[x, y] == ground[x, y] || intermediate_rez[x, y] == ground[x + 2, y + 2] ||
                            intermediate_rez[x, y] == ground[x, y + 2] || intermediate_rez[x, y] == ground[x + 2, y] ||
                            intermediate_rez[x, y] == ground[x, y + 1] || intermediate_rez[x, y] == ground[x + 2, y + 1] ||
                            intermediate_rez[x, y] == ground[x + 1, y] || intermediate_rez[x, y] == ground[x + 1, y + 2] 
                           )
                        {
                            Console.WriteLine("\nMinimum distance not observed between ships!\nPlease, try again.");
                            intermediate_rez[x, y] = '\0';
                            last_x[j] = 10;
                            last_y[j] = 10;
                            if (j == 0)
                            {
                                isAskX = true;
                                isAskY = true;
                                goto StartOver;
                            }

                            if (j == 1)
                            {
                                isAskX = true;
                                isAskY = true;
                            }
                            goto AgainY;
                        }                   
                    }
                    for (int k = 0; k < intermediate_rez.GetLength(0); ++k)
                        for (int j = 0; j < intermediate_rez.GetLength(1); ++j)
                            if (intermediate_rez[k, j] != 0)
                            {
                                ground[k + 1, j + 1] = intermediate_rez[k, j];
                                intermediate_rez[k, j] = '\0';
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
            char[,] ground1 = new char[12, 12];
            char[,] ground2 = new char[12, 12];

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

