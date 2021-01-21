using System;
using System.Linq;

namespace BrickWork
{
    /// <summary>
    /// The main class of the program.
    /// </summary>
    class Program
    {

        /// <summary>
        /// The entry point of the program.
        /// </summary>
        /// <param name="args">Command line arguments</param>
        static void Main(string[] args)
        {

            //The user input for rows and columns of the area (N and M).
            int[] inputFirstRow = Console.ReadLine()
             .Split(' ', StringSplitOptions.RemoveEmptyEntries)
             .Select(int.Parse)
             .ToArray();

            //Takes N and M from the user input.
            int n = inputFirstRow[0];
            int m = inputFirstRow[1];

            //The rectangular area, which builders must cover.
            //Creates the first layer of the construction
            BrickWork construction = new BrickWork(n, m);

            //Specifies whether is possible to create second layer of the construction.
            //If it is possible - create it and return true, otherwise return false.
            bool isSucceedOperation = construction.CreateSecondLayer();

            //If the creation have been succeeded print the second layer of the construction,
            //otherwise print that there is no way to create a second layer.
            if (isSucceedOperation)
            {
                construction.PrintSecondLayer();
                //construction.PrintSecondLayerWithSurrounding(); <- Unmark to print the second layer with surrounding.
            }
            else
            {
                Console.WriteLine(-1);
                Console.WriteLine("There is no solution for this context!");
            }



        }
    }
}
