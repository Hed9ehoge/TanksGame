using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TanksGame.Struct;

namespace TanksGameXYZProject.Struct
{
    public struct MajorCell
    {

        Cell _virtualCell;
        /// <summary>
        /// FIX
        /// </summary>
        public Cell VirtualCell{ get=> _virtualCell;
            set
            {
                RebildRealCoords(value.X, value.Y);
                _virtualCell = value;
            }
        }
        bool _anyChange = true;
        public int[]? RealCordsX;
        public int[]? RealCordsY;
        public MajorCell(Cell virtualCoords)
        {
            VirtualCell = virtualCoords;
        }
        public bool AnyChangeSinceLastTime()
        {
            var result = _anyChange;
            _anyChange = false;
            return result;
        }
        private void RebildRealCoords(int x, int y)
        {
            RealCordsX = new int[4]{x*2, (x * 2)+1, (x*2),(x*2)+1};
            RealCordsY = new int[4]{y*2,y*2, (y * 2)+1,(y*2)+1 };
            _anyChange = true;
        }

        public bool SameVirtualCoords(Cell cell)
        {
           return VirtualCell.X== cell.X && VirtualCell.Y== cell.Y;
        }
    }
}
