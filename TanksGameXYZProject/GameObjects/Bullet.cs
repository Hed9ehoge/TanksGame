using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TanksGame.Struct;
using TanksGameXYZProject.Interfaces;
using TanksGameXYZProject.Struct;

namespace TanksGameXYZProject.GameObjects
{
    public class Bullet : MovebleGameObject
    {
        int _damage;
        EnumOfInputCommands _direction;
        Cell _lastMove;
        protected override Sprites[] GetArroyOfSprites() => new Sprites[1] { new Sprites('0', '0', '0', '0') };
        public Bullet(byte collor, Cell cell, int damage, EnumOfInputCommands direction) : base(collor, cell, "Bullet", -1)
        {
            var obj = FindGameObjectByCoords(cell,this);
            if (obj != null)
            {
                if (obj.NameIs(_name))
                {
                    Destroy();
                    return;
                }
            }
            _damage = damage;
            _direction = direction;
            CheckBulletCollision(_coords.VirtualCell);
        }

        private void CheckBulletCollision(Cell virtualCell)
        {
            var go = FindGameObjectByCoords(virtualCell, this);

            var SidesTAg = HasTag("TankBullet") ? "TankBullet" : "PlayerBullet";

            if (go != null && !go.NameIs("Water") && !go.HasTag(SidesTAg))
            {
                if (!go.NameIs("Bullet"))
                {
                    go.ReceiveDamage(_damage);
                    Destroy();
                    return;
                }
                
            }
            _coords.VirtualCell = virtualCell;
        }

        public override void ChangeSpriteByDirection(EnumOfInputCommands action)
        {
            Destroy();
            return;
        }
        public override void Input(EnumOfInputCommands action) => _direction = action;

        public override void DoAction()
        {
            if (!InputCommands.IsItCommandForMove(_direction))
            {
                Destroy();
                return;
            }
            var shiftCell = InputCommands.DirToCell(_direction);
            var NextCoords = _coords.VirtualCell.Sum(shiftCell);
            CheckBulletCollision(NextCoords);
        }

        public override EnumOfInputCommands GetMyNextAction() => _direction;
        public override EnumOfInputCommands GetMyLastAction() => _direction;
        public override EnumOfInputCommands DirectionOfView() => _direction;


    }
}
