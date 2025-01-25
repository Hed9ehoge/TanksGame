using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TanksGame.Struct;
using TanksGameXYZProject.GameObjects;
using TanksGameXYZProject.Struct;

namespace TanksGameXYZProject.Interfaces
{
    public abstract class MovebleGameObject:GameObject
    {
        protected MovebleGameObject(byte collor, Cell cell, string objectName, int hp = -1) : base(collor, cell, objectName, hp)
        {
            _listOfMovebleGameObject.Add(this);
        }

        public override void Destroy()
        {
            base.Destroy();
            _listOfMovebleGameObject.Remove(this);
        }

        private static List<MovebleGameObject> _listOfMovebleGameObject = new();
        public static void DestroyAllMoveble()=>_listOfMovebleGameObject.Clear();
        public static MovebleGameObject[] GetAllMovebleGameObject() => _listOfMovebleGameObject.ToArray();
        public abstract void DoAction();
        public abstract void ChangeSpriteByDirection(EnumOfInputCommands action);
        public abstract EnumOfInputCommands GetMyNextAction();
        public abstract EnumOfInputCommands GetMyLastAction();
        public abstract void Input(EnumOfInputCommands action);

        public abstract EnumOfInputCommands DirectionOfView();
    }
}
