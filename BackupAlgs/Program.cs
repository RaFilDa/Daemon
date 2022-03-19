using System;

namespace BackupAlgs
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string version = "RaFilDa Backup - v1.2";

            Console.CursorVisible = false;
            Console.Title = version;

            Application panel = new Application(version);

            while (panel.IsOn)
            {
                panel.ActiveWindow.Draw();
                panel.ActiveWindow.HandleKey(Console.ReadKey(true));
            }
        }
    }
}
