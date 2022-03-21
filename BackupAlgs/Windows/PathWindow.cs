using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackupAlgs.Tools;

namespace BackupAlgs.Windows
{
    public class PathWindow : Window
    {
        public Application Parent { get; set; }
        public string Header { get; set; }
        public string[] PathNames = { "Source", "Destination" };
        public string[] NewPaths { get; set; }
        public int index = 0;
        public PathWindow(Application parent, string header)
        {
            NewPaths = Paths.AllPaths;
            Console.Clear();
            Parent = parent;
            Header = header;
        }

        public override void Draw()
        {
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < Console.WindowHeight; i++)
            {
                Console.WriteLine(" ".PadRight(Console.WindowWidth));
            }
            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(Header);
            Console.WriteLine("-".PadRight(Header.Length, '-'));

            for (int i = 1; i < Paths.AllPaths.Length; i++)
            {
                if (index == i - 1)
                    Console.ForegroundColor = ConsoleColor.White;
                else
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine(PathNames[i - 1] + "Path: " + Paths.AllPaths[i]);
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("-".PadRight(Header.Length, '-'));
            }
        }

        public void AddNewPaths()
        {
            for (int i = 0; i < NewPaths.Length; i++)
            {
                Paths.AllPaths[i] = NewPaths[i];
            }
        }

        public override void HandleKey(ConsoleKeyInfo info)
        {
            if(info.Key == ConsoleKey.Enter || info.Key == ConsoleKey.Escape)
            {
                AddNewPaths();
                PathTools.PathUpdateFile();
                LogTools.AddNewLog("Path Changes");
                Parent.ActiveWindow = new MainWindow(Parent, Header);
            }
            else if(info.Key == ConsoleKey.DownArrow)
            {
                index++;
                if (index == Paths.AllPaths.Length - 1)
                    index--;
            }
            else if(info.Key == ConsoleKey.UpArrow)
            {
                index--;
                if (index < 0)
                    index++;
            }
            else if(info.Key == ConsoleKey.Backspace)
            {
                if(NewPaths[index + 1] != "")
                    NewPaths[index + 1] = NewPaths[index + 1].Substring(0, NewPaths[index + 1].Length - 1);
            }
            else
            {
                NewPaths[index + 1] = NewPaths[index + 1] + info.KeyChar;
            }
        }
    }
}
