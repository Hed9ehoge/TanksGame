using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TanksGame.Struct;
using TanksGameXYZProject.Interfaces;
using TanksGameXYZProject.Struct;

namespace TanksGameXYZProject.GameObjects
{
    public class Tank : MovebleGameObject
    {

        /// <summary>
        /// ╠╣  
        /// ╚╝
        /// ╔╗
        /// ╠╣
        /// ╔╦
        /// ╚╩
        /// ╦╗
        /// ╩╝
        /// </summary>

        

        int _damage = 1;
        byte _bulletColor;
        public int HP => _hp;
        EnumOfInputCommands _currentAction= EnumOfInputCommands.NoN;
        EnumOfInputCommands _lastMove = EnumOfInputCommands.NoN;

        
        protected override Sprites[] GetArroyOfSprites() => new Sprites[4]
        {
            new Sprites('╦', '╗', '╩', '╝'),
            new Sprites('╠', '╣', '╚', '╝'),
            new Sprites('╔', '╦', '╚', '╩'),
            new Sprites('╔', '╗', '╠', '╣') 
        };
        public Tank(byte color, Cell coords, int hp = -1) : base(color, coords, "Tank", hp)
        {
            _bulletColor = color;
            ChangeSpriteByDirection(EnumOfInputCommands.Up);
        }
        public override void ChangeSpriteByDirection(EnumOfInputCommands action)
        {
            if (!InputCommands.IsItCommandForMove(action)) return;
            ChangeSprite((int)action);
            EndOfAction(action);
        }
        void EndOfAction(EnumOfInputCommands currentAction)
        {
            if (currentAction == EnumOfInputCommands.NoN) return;
            _lastMove = currentAction;
            _currentAction = EnumOfInputCommands.NoN;
        }
        public override void DoAction()
        {
            if (EnumOfInputCommands.NoN== _currentAction) return;

            if (!InputCommands.IsItCommandForMove(_currentAction))
            {
                var bullet=new Bullet(_bulletColor,_coords.VirtualCell.Sum(InputCommands.DirToCell(_lastMove)),_damage, _lastMove);
                bullet.Input(_lastMove);
                bullet.AddTag(_name + "Bullet");
                _currentAction = EnumOfInputCommands.NoN;
                return;
            }
            
            var shiftCell = InputCommands.DirToCell(_currentAction);
            var NextCoords = _coords.VirtualCell.Sum(shiftCell);
            ChangeSpriteByDirection(_currentAction);
            if (!TouchOtherGO(NextCoords)) _coords.VirtualCell = NextCoords;
            EndOfAction(_currentAction);

        }
        public override void Input(EnumOfInputCommands action) => _currentAction = action;
        public override EnumOfInputCommands GetMyNextAction() => _currentAction;
        public override EnumOfInputCommands GetMyLastAction() => _lastMove;

        public override EnumOfInputCommands DirectionOfView() => _lastMove;
    }
}