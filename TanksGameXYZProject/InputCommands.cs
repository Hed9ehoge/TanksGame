using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using TanksGame.Struct;

namespace TanksGameXYZProject
{
    public enum EnumOfInputCommands
    {
        Left,
        Up, 
        Right,
        Down,
        Shoot,
        NoN
    }

    public static class InputCommands
    {
        private static string[] _commandForMove = new string[4] { "Left", "Up", "Right", "Down" };
        public static bool IsItCommandForMove(EnumOfInputCommands command) => _commandForMove.Contains(command.ToString());

        private static Dictionary<Cell, EnumOfInputCommands> CellTodir = new()
        {
            {new Cell(0,1), EnumOfInputCommands.Down},
            {new Cell(0,-1), EnumOfInputCommands.Up},
            {new Cell(-1,0), EnumOfInputCommands.Left},
            {new Cell(1,0), EnumOfInputCommands.Right}
        };
        public static Cell[] GetAllCellDirection() => CellTodir.Select(x => x.Key).ToArray();
        //public static EnumOfInputCommands[] GetAllDirection() => CellTodir.Select(x => x.Value).ToArray();
        public static EnumOfInputCommands DirToCell(Cell cell) => CellTodir.GetValueOrDefault(cell);
        public static Cell DirToCell(EnumOfInputCommands dir)
        {

            foreach (var item in CellTodir)
            {
                if (item.Value == dir)
                    return item.Key;
            }
            return default;
        }
    }
}
