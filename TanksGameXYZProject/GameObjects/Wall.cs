using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TanksGame.Struct;
using TanksGameXYZProject.Struct;

namespace TanksGameXYZProject.GameObjects
{
    public class Wall : GameObject
    {
        
        public Wall(Cell cell) : base(1, cell, "Wall", 2)
        {
        }
        public override void ReceiveDamage(int damage)
        {
            base.ReceiveDamage(damage);
            if (_currentSpriteNumbor != -1)
                ChangeSprite(_currentSpriteNumbor + 1);
        }

        protected override Sprites[] GetArroyOfSprites()=>new Sprites[2] { new Sprites('▓', '▓', '▓', '▓'), new Sprites('/', '/', '/', '/') };
    }
}
