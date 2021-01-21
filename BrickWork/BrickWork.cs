using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrickWork
{
    /// <summary>
    /// Represents the rectangular area, which must be covered.
    /// </summary>
    public class BrickWork
    {
        //Fields which represent the size of the layers.
        private readonly int rows;
        private readonly int cols;

        //Fields which represent the layers in the construction.
        private int[,] firstLayer;
        private int[,] secondLayer;


        /// <summary>
        /// Initializes a new instance of the <see cref="BrickWork"/> class.
        /// </summary>
        /// <param name="rows">The count of the rows</param>
        /// <param name="cols">The count of the columns</param>
        /// <exception cref="ArgumentException">N and M must be smaller than 100 numbers!</exception>
        public BrickWork(int rows, int cols)
        {
            if (!IsValidSizeValue(rows) || !IsValidSizeValue(cols))
            {
                throw new ArgumentException("N and M must be smaller than 100 numbers!");
            }

            //Records the size of the construction and initialize the layers.
            this.rows = rows;
            this.cols = cols;
            this.firstLayer = new int[rows, cols];
            this.FillInputLayer();
            this.secondLayer = new int[rows, cols];

        }

        /// <summary>
        /// Prints the second layer with surrounding on the console.
        /// </summary>
        public void PrintSecondLayerWithSurrounding()
        {
            Console.WriteLine(LayerWithSurrounding(this.secondLayer));
        }

        /// <summary>
        /// Prints the first layer on the console.
        /// </summary>
        public void PrintFirstLayer()
        {
            Console.WriteLine();
            Console.WriteLine(PrintLayer(this.firstLayer));
        }

        /// <summary>
        /// Prints the second layer on the console.
        /// </summary>
        public void PrintSecondLayer()
        {
            Console.WriteLine();
            Console.WriteLine(PrintLayer(this.secondLayer));
        }

        /// <summary>
        /// Tries to create a second layer.
        /// </summary>
        /// <returns><c>true</c> if is possible, otherwise <c>false</c></returns>
        public bool CreateSecondLayer()
        {
            //The initial num, which represent a brick.
            int currentNum = 1;
            // The initial coordinates, where the building of the second layer starts.
            int x = 0;
            int y = 0;

            if (Build(currentNum, x, y))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Specifies whether a number is valid size for row or column.
        /// </summary>
        /// <param name="number">The current number, which must be checked.</param>
        /// <returns><c>true</c> if the number is smaller than 100 and even, otherwise <c>false</c></returns>
        private bool IsValidSizeValue(int number)
        {
            if (number >= 100)
            {
                return false;
            }

            if (number % 2 != 0)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Specifies whether the count of the numbers in the current row is
        /// equal to the count of columns.
        /// </summary>
        /// <param name="numbersCount">The count of the numbers in the current row.</param>
        /// <returns><c>true</c> if the count of the numbers in the current row is
        /// equal to the count of columns, otherwise throws an exception</returns>
        /// <exception cref="ArgumentException">The count of input numbers must be equal to "M".</exception>
        private bool IsValidColumnsInput(int numbersCount)
        {
            if (numbersCount != this.cols)
            {
                throw new ArgumentException("The count of input numbers must be equal to \"M\"");
            }

            return true;
        }

        /// <summary>
        /// Specifies whether the entered first layer is valid.
        /// </summary>
        /// <returns><c>true</c> if there are no
        ///bricks spanning 3 rows/ columns, otherwise <c>false</c></returns>
        private bool HasValidInputLayer()
        {
            if (this.rows == 2 && this.cols == 2)
            {
                return true;
            }
            else if (this.rows == 2)
            {
                for (int i = 0; i < this.rows; i++)
                {
                    for (int j = 0; j < this.cols - 2; j++)
                    {
                        if (this.firstLayer[i, j] == this.firstLayer[i, j + 1] &&
                            this.firstLayer[i, j + 1] == this.firstLayer[i, j + 2])
                        {
                            return false;
                        }
                    }
                }
            }
            else if (this.cols == 2)
            {
                for (int i = 0; i < this.rows - 2; i++)
                {
                    for (int j = 0; j < this.cols; j++)
                    {
                        if (this.firstLayer[i, j] == this.firstLayer[i + 2, j])
                        {
                            return false;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < this.rows - 2; i++)
                {
                    for (int j = 0; j < this.cols - 2; j++)
                    {
                        if (this.firstLayer[i, j] == this.firstLayer[i, j + 2])
                        {
                            return false;
                        }
                        if (this.firstLayer[i, j] == this.firstLayer[i + 2, j])
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Reads data from the console, to fill in the first layer.
        /// </summary>
        private void FillInputLayer()
        {
            for (int i = 0; i < this.rows; i++)
            {
                //Holds the values for a current row in the first layer.
                int[] row = Console.ReadLine()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();

                if (IsValidColumnsInput(row.Length))
                {
                    for (int j = 0; j < this.cols; j++)
                    {
                        //Fills in a current cell (part of a brick) in the first layer.
                        this.firstLayer[i, j] = row[j];
                    }
                }
            }

            if (!this.HasValidInputLayer())
            {
                throw new ArgumentException("There are bricks spanning 3 rows / columns in the input layer!");
            }

        }

        /// <summary>
        /// Tries to create the second layer recursively.
        /// </summary>
        /// <param name="currentNum">The number, which represents the current brick.</param>
        /// <param name="row">Points out the current row.</param>
        /// <param name="col">Points out the current column.</param>
        /// <returns><returns><c>true</c> if succeeded, otherwise <c>false</c></returns>
        private bool Build(int currentNum, int row, int col)
        {
            //Checks whether the second layer is built (base case).
            if (currentNum == firstLayer.GetLength(0) * firstLayer.GetLength(1) / 2 + 1)
            {
                return true;
            }

            //If this row is passed, goes to the other.
            if (col >= firstLayer.GetLength(1))
            {
                col = 0;
                row++;
            }

            //If the last row is passed, the second layer has been built.
            if (row >= firstLayer.GetLength(0))
            {
                return true;
            }

            //If the current cell (part of a brick) is free...
            if (secondLayer[row, col] == 0)
            {
                //...and it can place the current brick horizontally...
                if (CanPlaceRight(row, col))
                {
                    //...it places the brick...
                    secondLayer[row, col] = currentNum;
                    secondLayer[row, col + 1] = currentNum;

                    //and keeps building with the next brick.
                    if (Build(currentNum + 1, row, col + 2))
                    {
                        return true;
                    }
                    //Takes a step back if there is no way to continue.
                    else
                    {
                        secondLayer[row, col] = 0;
                        secondLayer[row, col + 1] = 0;
                    }
                }
                //Tries to place the brick vertically.
                if (CanPlaceDown(row, col))
                {
                    //Places the brick vertically.
                    secondLayer[row, col] = currentNum;
                    secondLayer[row + 1, col] = currentNum;
                    //Keeps building with the next brick.
                    if (Build(currentNum + 1, row, col + 1))
                    {
                        return true;
                    }
                    //Takes a step back if there is no way to continue.
                    else
                    {
                        secondLayer[row, col] = 0;
                        secondLayer[row + 1, col] = 0;
                    }

                }
            }
            //If the current cell is not free (there is a brick on it),
            //keeps building with the next brick.
            else
            {
                if (Build(currentNum, row, col + 1))
                {
                    return true;
                }

            }

            //There is no right path for the second layer.
            return false;
        }

        /// <summary>
        /// Checks whether can place the brick horizontally.
        /// </summary>
        /// <param name="row">Points out the current row, where the <see cref="Build(int, int, int)"/> method
        /// try to put the brick.</param>
        /// <param name="col">Points out the current column, where the <see cref="Build(int, int, int)"/> method
        /// try to put the brick.</param>
        /// <returns><c>true</c> if the brick doesn't go beyond the outlines of the layer,
        /// the brick is not placed in the same way in the first layer and 
        /// there is no other brick on that cell of the second layer, otherwise <c>false</c></returns>
        private bool CanPlaceRight(int row, int col)
        {
            if (col + 1 >= this.firstLayer.GetLength(1))
            {
                return false;
            }

            if (this.firstLayer[row, col] == this.firstLayer[row, col + 1])
            {
                return false;
            }

            if (this.secondLayer[row, col + 1] != 0)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Checks whether can place the brick vertically.
        /// </summary>
        /// <param name="row">Points out the current row, where the <see cref="Build(int, int, int)"/> method
        /// try to put the brick.</param>
        /// <param name="col">Points out the current column, where the <see cref="Build(int, int, int)"/> method
        /// try to put the brick.</param>
        /// <returns><c>true</c> if the brick doesn't go beyond the outlines of the layer,
        /// the brick is not placed in the same way in the first layer and 
        /// there is no other brick on that cell of the second layer, otherwise <c>false</c></returns>
        private bool CanPlaceDown(int row, int col)
        {
            if (row + 1 >= this.firstLayer.GetLength(0))
            {
                return false;
            }

            if (this.firstLayer[row, col] == this.firstLayer[row + 1, col])
            {
                return false;
            }

            if (this.secondLayer[row + 1, col] != 0)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Creates a string, which represent the given layer.
        /// </summary>
        /// <param name="layer">The layer which must be represented as a string.</param>
        /// <returns>The string representation of the given layer.</returns>
        private string PrintLayer(int[,] layer)
        {
            //Holds the string representation.
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < layer.GetLength(0); i++)
            {
                for (int j = 0; j < layer.GetLength(1); j++)
                {
                    sb.Append(layer[i, j] + " ");
                }
                sb.AppendLine();
            }

            return sb.ToString();
        }

        /// <summary>
        /// Creates a string, which represent the given layer with surroundings.
        /// </summary>
        /// <param name="layer">The layer, which must be represented with surroundings as a string.</param>
        /// <returns>The string representation of the given layer with surroundings</returns>
        private string LayerWithSurrounding(int[,] layer)
        {
            //Holds the string representation.
            StringBuilder sb = new StringBuilder();

            //Checks whether there is one-digit-numbers only.
            bool onlyOneDigitNums = layer.GetLength(0) * layer.GetLength(1) / 2 < 10;

            //Chooses one of the methods depending on upper condition.
            if (onlyOneDigitNums)
            {
                sb.Append(CreateSurroundingForOneDigitNumbersOnly(layer));
            }
            else
            {
                sb.Append(CreateSurroundingForTwoDigitNumbers(layer));
            }

            return sb.ToString();
        }

        /// <summary>
        /// Creates the surroundings for layer with one-digit-numbers only.
        /// </summary>
        /// <param name="layer">The layer, which must be surrounded.</param>
        /// <returns>The string representation of the given layer with surroundings</returns>
        private string CreateSurroundingForOneDigitNumbersOnly(int[,] layer)
        {
            //Holds the string representation.
            StringBuilder sb = new StringBuilder();

            //Adds the upper surrounding of the layer.
            int matrixColumnsSize = layer.GetLength(1) * 2 + 1;
            for (int i = 0; i < matrixColumnsSize; i++)
            {
                sb.Append('*');
            }
            sb.AppendLine();

            
            for (int i = 0; i < layer.GetLength(0); i++)
            {
                //Adds the left surrounding.
                sb.Append('*');

                //Adds surroundings between the columns of the layer.
                for (int j = 0; j < layer.GetLength(1); j++)
                {
                    //Adds current part of a brick.
                    sb.Append(layer[i, j]);

                    //Adds "-" if surrounds a brick.
                    if (j + 1 < layer.GetLength(1) && layer[i, j + 1] == layer[i, j])
                    {
                        sb.Append('-');
                    }
                    //Adds "*" if surrounds two different bricks or that is the end of the layer.
                    else
                    {
                        sb.Append('*');
                    }

                }

                sb.AppendLine();
                //Adds the left one.
                sb.Append('*');
                //Starts surrounding the rows.
                for (int j = 0; j < layer.GetLength(1); j++)
                {
                    //Adds "-*" if surrounds a brick.
                    if (i + 1 < layer.GetLength(0) && layer[i + 1, j] == layer[i, j])
                    {
                        sb.Append("-*");
                    }
                    //Adds "**" if surrounds two different bricks or that is the end of the layer.
                    else
                    {
                        sb.Append("**");
                    }
                }


                sb.AppendLine();
            }

            return sb.ToString();
        }

        /// <summary>
        /// Creates the surroundings for layer with one and two digits.
        /// </summary>
        /// <param name="layer">The layer, which must be surrounded.</param>
        /// <returns>The string representation of the given layer with surroundings</returns>
        private string CreateSurroundingForTwoDigitNumbers(int[,] layer)
        {
            //Holds the string representation of the layer.
            StringBuilder sb = new StringBuilder();

            //Adds the upper surrounding of the layer.
            int matrixColumnsSize = layer.GetLength(1) * 3 + 1;
            for (int i = 0; i < matrixColumnsSize; i++)
            {
                sb.Append('*');
            }
            sb.AppendLine();


            for (int i = 0; i < layer.GetLength(0); i++)
            {
                //Adds the left surrounding.
                sb.Append('*');
                for (int j = 0; j < layer.GetLength(1); j++)
                {
                    //If the current number is one digit, adds a space after it.
                    if (layer[i, j] < 10)
                    {
                        sb.Append(layer[i, j] + " ");
                    }
                    else
                    {
                        sb.Append(layer[i, j]);
                    }

                    //Adds surroundings between the columns of the layer.
                    if (j + 1 < layer.GetLength(1) && layer[i, j + 1] == layer[i, j])
                    {
                        sb.Append('-');
                    }
                    else
                    {
                        sb.Append('*');
                    }

                }

                //Adds the left surrounding
                sb.AppendLine();
                sb.Append('*');

                //Adds surroundings between the rows of the layer.
                for (int j = 0; j < layer.GetLength(1); j++)
                {
                    if (i + 1 < layer.GetLength(0) && layer[i + 1, j] == layer[i, j])
                    {
                        sb.Append("--*");
                    }
                    else
                    {
                        sb.Append("***");
                    }
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}
