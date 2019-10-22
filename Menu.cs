using System;
using System.Collections.Generic;
using System.Text;

namespace ExtremeTetris
{
    class Menu
    {
        private int numOfMenus;

        private string[] menus;

        private int selectedMenu = 0;

        public Menu(string[] value)
        {
            numOfMenus = value.Length;
            menus = new string[numOfMenus];
            menus = value;
        }

        /// <summary>
        /// Moves selected menu down
        /// </summary>
        public void IncreaseMenu()
        {
            if (selectedMenu < numOfMenus - 1)
                selectedMenu++; 
        }

        /// <summary>
        /// Moves selected menu up
        /// </summary>
        public void DecreaseMenu()
        {
            if (selectedMenu > 0)
                selectedMenu--;
        }

        /// <summary>
        /// Get number of Menus
        /// </summary>
        /// <returns>Int of Menus</returns>
        public int GetNumberOfMenus => numOfMenus;

        /// <summary>
        /// Get Menus
        /// </summary>
        /// <returns>String[] of Menus</returns>
        public string[] GetMenus => menus;

        /// <summary>
        /// Get index of selected Menus
        /// </summary>
        /// <returns>Int of selected Menus</returns>
        public int getSelectedIndex => selectedMenu;

    }
}
