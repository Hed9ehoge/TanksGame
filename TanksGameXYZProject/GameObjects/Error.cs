using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TanksGame.Struct;
using TanksGameXYZProject.Struct;

namespace TanksGameXYZProject.GameObjects
{
    public class Error : GameObject
    {
        public Error(Cell cell) : base(6, cell, "Error", -1)
        {
        }

        protected override Sprites[] GetArroyOfSprites() => new Sprites[1] { new Sprites('?', '?', '?', '?') };
    }
}
