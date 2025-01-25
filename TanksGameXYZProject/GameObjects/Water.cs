using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TanksGame.Struct;
using TanksGameXYZProject.Struct;

namespace TanksGameXYZProject.GameObjects
{
    public class Water : GameObject
    {
        public Water(Cell cell) : base(2, cell, "Water", -1)
        {
        }

        protected override Sprites[] GetArroyOfSprites()
        {
            return new Sprites[1] { new Sprites('█', '█', '█', '█') };
        }
    }
}
