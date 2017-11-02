using System;
using Memory.View;
    
namespace Memory
{
    class Program
    {
        [STAThread]
        public static void Main()
        {
            Game game = new Game(4, 4);
        }
    }
}
