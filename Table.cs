using System;
using System.Collections.Generic;
using System.Text;

namespace ExtremeTetris
{
    class Table
    {
        public bool[,] tableContains;
        public int col;
        public int row;
        public int spacing;
        public int center;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="spacing"></param>
        public void CreateTable(int width, int height, int spacing)
        {
            tableContains = new bool[height, width];
            SetCol(width);
            SetRow(height);
            SetSpacing(spacing);
            SetCenter(width);
        }

        /// <summary>
        /// Cleans the table contains
        /// </summary>
        public void Clear()
        {
            for (int y = 0; y < GetRow(); y++)
                for (int x = 0; x < GetCol(); x++)
                {
                    tableContains[y, x] = false;
                }
        }

        /// <summary>
        /// Changes Column variable
        /// </summary>
        /// <param name="value"></param>
        private void SetCol(int value)
        {
            col = value;
        }

        /// <summary>
        /// Changes Row variable
        /// </summary>
        /// <param name="value"></param>
        private void SetRow(int value)
        {
            row = value;
        }

        /// <summary>
        /// Changes Table Spacing
        /// </summary>
        /// <param name="value"></param>
        private void SetSpacing(int value)
        {
            spacing = value;
        }

        /// <summary>
        /// Changes Table center
        /// </summary>
        /// <param name="value"></param>
        private void SetCenter(int value)
        {
            center = value / 2;
        }

        /// <summary>
        /// Get Colomn variable (amount of columns)
        /// </summary>
        /// <returns>Colomns of int type</returns>
        public int GetCol()
        {
            return col;
        }

        /// <summary>
        /// Get Row variable (amount of rows)
        /// </summary>
        /// <returns>Rows of int type</returns>
        public int GetRow()
        {
            return row;
        }

        /// <summary>
        /// Get Spacing variable
        /// </summary>
        /// <returns>Spacing of int type</returns>
        public int GetSpacing()
        {
            return spacing;
        }

        /// <summary>
        /// Get Center variable (Table Center)
        /// </summary>
        /// <returns>Center of int type</returns>
        public int GetCenter()
        {
            return center;
        }
    }
}
