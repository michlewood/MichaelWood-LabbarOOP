﻿using Labb7.DataStores;
using Labb7.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb7.Manager
{
    class MenusManager
    {
        public static int MainMenuChoice { get; set; }
        public static int CurrentMenuChoice { get; set; }
        public static int MenuChoice { get; set; }

        public void FiltersMenu(ConsoleKey input, MyLists myLists)
        {
            switch (input)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    Graphics.ElecronicsOn = !Graphics.ElecronicsOn;
                    break;
                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    Graphics.ToysOn = !Graphics.ToysOn;
                    break;
                case ConsoleKey.D3:
                case ConsoleKey.NumPad3:
                    Graphics.VideoGamesOn = !Graphics.VideoGamesOn;
                    break;
                default:
                    break;
            }
            myLists.UpdateCurrentList();
        }

        public bool MainMenuChooser(ProductManager productManager)
        {
            bool loop = true;
            Console.CursorVisible = false;
            Graphics.PrintMainMenu();
            var input = Console.ReadKey(true).Key;
            FiltersMenu(input, productManager.lists);
            MainMenuChoice = MenuChooser(MainMenuChoice, 4, input);
            if (input == ConsoleKey.Enter)
            {
                switch (MainMenuChoice)
                {
                    case 0:
                        Console.Clear();
                        productManager.TypeOfProductToAdd();
                        MenuChoice = 0;
                        break;
                    case 1:
                        Console.Clear();
                        RemoveMenu(productManager);
                        MenuChoice = 0;
                        break;
                    case 2:
                        CartMenu(productManager);
                        break;
                    case 3:
                        loop = false;
                        break;
                    default:
                        break;
                }
            }
            return loop;
        }

        private void CartMenu(ProductManager productManager)
        {
            while (true)
            {
                Graphics.ShowCurrentMenu(productManager.lists.CartList);
                var input = Console.ReadKey(true).Key;
                if (input == ConsoleKey.Escape)
                {
                    return;
                }
            }
        }

        private void RemoveMenu(ProductManager productManager)
        {
            CurrentMenuChoice = 0;
            while (true)
            {
                Graphics.ShowCurrentMenu(productManager.lists.CurrentProducts, true);
                var input = Console.ReadKey(true).Key;
                CurrentMenuChoice = MenuChooser(CurrentMenuChoice, productManager.lists.CurrentProducts.Count, input);
                if (input == ConsoleKey.Enter)
                {
                    Product productToRemove = productManager.lists.CurrentProducts[CurrentMenuChoice];
                    productManager.lists.Products.Remove(productToRemove);
                    productManager.lists.UpdateCurrentList();
                    return;
                }
            }
        }

        private int MenuChooser(int menuChoice, int sizeOfMenu, ConsoleKey input)
        {
            switch (input)
            {
                case ConsoleKey.UpArrow:
                    menuChoice--;
                    if (menuChoice < 0) menuChoice = sizeOfMenu - 1;
                    break;
                case ConsoleKey.DownArrow:
                    menuChoice++;
                    if (menuChoice == sizeOfMenu) menuChoice = 0;
                    break;
                default:
                    break;
            }

            return menuChoice;
        }
    }
}