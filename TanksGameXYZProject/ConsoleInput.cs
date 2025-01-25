using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TanksGame.Interfaces;
using TanksGameXYZProject;

namespace TanksGame
{
    //CORRECTED
    public class ConsoleInput 
    { 
        
        List<IInputListener> inputListeners = new List<IInputListener>();
        Dictionary<EnumOfInputCommands, List<ConsoleKey>> DirectionAndButton = new() 
        {
            {EnumOfInputCommands.Left,new List<ConsoleKey>{ConsoleKey.A,ConsoleKey.LeftArrow} },
            {EnumOfInputCommands.Up,new List<ConsoleKey>{ConsoleKey.W,ConsoleKey.UpArrow} },
            {EnumOfInputCommands.Right,new List<ConsoleKey>{ConsoleKey.D,ConsoleKey.RightArrow} },
            {EnumOfInputCommands.Down,new List<ConsoleKey>{ConsoleKey.S,ConsoleKey.DownArrow} },
            {EnumOfInputCommands.Shoot,new List<ConsoleKey>{ConsoleKey.Enter,ConsoleKey.Spacebar} },
        };


        public void Subscribe(IInputListener listener) 
        {
            inputListeners.Add(listener);
        }
        public void Update()
        {
            if (!Console.KeyAvailable)
                return;
            var key = StealthReadKey();
            

            
            EnumOfInputCommands direction = LastPresedKey(key);
            switch (direction)
            {
                case EnumOfInputCommands.Shoot:
                    foreach (var item in inputListeners)
                        item.OnShoot();
                    break;
                case EnumOfInputCommands.Left:
                    foreach (var item in inputListeners)
                        item.OnArrowLeft();
                    break;
                case EnumOfInputCommands.Up:
                    foreach (var item in inputListeners)
                        item.OnArrowUp();
                    break;
                case EnumOfInputCommands.Right:
                    foreach (var item in inputListeners)
                        item.OnArrowRight();
                    break;
                case EnumOfInputCommands.Down:
                    foreach (var item in inputListeners)
                        item.OnArrowDown();
                    break;
            }
        }
        private ConsoleKeyInfo StealthReadKey()
        {
            var curretCursorDir = Console.GetCursorPosition();
            var maxWidth = Console.WindowWidth;
            var maxHeight = Console.WindowHeight;
            Console.SetCursorPosition(maxWidth-1, maxHeight-1);
            var key = Console.ReadKey();
            Console.SetCursorPosition(maxWidth - 1, maxHeight - 1);
            Console.Write(' ');
            Console.SetCursorPosition(curretCursorDir.Left, curretCursorDir.Top);
            return key;
        }

        private EnumOfInputCommands LastPresedKey(ConsoleKeyInfo key)
        {
            foreach (var dir in DirectionAndButton)
            {
                foreach (var item1 in dir.Value)
                {
                    if (item1.ToString() == key.Key.ToString())
                    {
                        return dir.Key;
                    }
                }
            }
            return default;
        }
    }
}
