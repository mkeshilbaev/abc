﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using static System.Console;

namespace FileManager
{
    class Program
    {
        static int CONSOLE_SIZE = 28;
         static void ShowDirectoryInfo(DirectoryInfo directory, int cursor, int first)
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.Clear();
            Console.CursorVisible = false;
             FileSystemInfo[] fss = directory.GetFileSystemInfos();
            int index = 0;

            foreach (FileSystemInfo f in fss)
            {
                if (index == cursor)
                {
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.ForegroundColor = ConsoleColor.Blue;
                }

                else if (f.GetType() == typeof(DirectoryInfo))
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                }

                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                }

                if (index >= first && index <= first + CONSOLE_SIZE)
                    Console.WriteLine(f.Name);
                index++;
            }
        }

        static void DrawLAT()
        {

            DirectoryInfo dir = new DirectoryInfo(@"C:\");
            FileSystemInfo[] items = dir.GetFileSystemInfos();
            for(int i = 0; i < items.Length; i++)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(60, i);
                Console.WriteLine(items[i].LastAccessTime);
            }
        }

        static void Main(string[] args)
        {
            DirectoryInfo directory = new DirectoryInfo(@"C:\");
            int first = 0;
            int cursor = 0;
            ShowDirectoryInfo(directory, cursor, first);
            int n = directory.GetFileSystemInfos().Length;
            
            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey();

                if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    cursor--;

                    if (cursor < 0)
                    {
                        cursor = n - 1;
                        first = cursor - CONSOLE_SIZE;
                    }

                    if (cursor < first)
                    {
                        first--;
                    }
                }

                if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    cursor++;
                    if (cursor == n)
                    {
                        cursor = 0;
                        first = 0;
                    }
                    if (cursor > first + CONSOLE_SIZE)
                    {
                        first++;
                    }
                }

                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    if (directory.GetFileSystemInfos()[cursor].GetType() == typeof(DirectoryInfo))
                    {
                        directory = (DirectoryInfo)directory.GetFileSystemInfos()[cursor];
                        cursor = 0;
                        n = directory.GetFileSystemInfos().Length;
                        first = 0;
                    }

                    else
                    {
                        StreamReader sr = new StreamReader(directory.GetFileSystemInfos()[cursor].FullName);
                        string s = sr.ReadToEnd();
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Clear();
                        Console.WriteLine(s);
                        Console.ReadKey();
                    }
                }

                if (keyInfo.Key == ConsoleKey.Escape)
                {
                    if (directory.Parent != null)
                    {
                        directory = directory.Parent;
                        cursor = 0;
                        n = directory.GetFileSystemInfos().Length;
                        first = 0;
                    }

                    else
                        break;
                }
                
                ShowDirectoryInfo(directory, cursor, first);
                DrawLAT();
            }
        }
        
    }
}