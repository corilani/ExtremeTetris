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

        public void createTable(int width, int height, int spacing)
        {
            tableContains = new bool[height, width];
            setCol(width);
            setRow(height);
            setSpacing(spacing);
            setCenter(width);
        }

        /// <summary>
        /// Cleans the table appearance
        /// </summary>
        public void clear()
        {
            for (int y = 0; y < getRow(); y++)
                for (int x = 0; x < getCol(); x++)
                {
                    tableContains[y, x] = false;
                }
        }

        private void setCol(int value)
        {
            col = value;
        }

        private void setRow(int value)
        {
            row = value;
        }

        private void setSpacing(int value)
        {
            spacing = value;
        }

        private void setCenter(int value)
        {
            center = value / 2;
        }

        /// <summary>
        /// Get amount of colomns
        /// </summary>
        /// <returns>Colomns of int type</returns>
        public int getCol()
        {
            return col;
        }

        /// <summary>
        /// Get amount of rows
        /// </summary>
        /// <returns>Rows of int type</returns>
        public int getRow()
        {
            return row;
        }

        /// <summary>
        /// Get spacing
        /// </summary>
        /// <returns>Spacing of int type</returns>
        public int getSpacing()
        {
            return spacing;
        }

        /// <summary>
        /// Get center of table
        /// </summary>
        /// <returns>Center of int type</returns>
        public int getCenter()
        {
            return center;
        }
    }
}
