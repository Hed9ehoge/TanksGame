using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TanksGame.Struct;
using TanksGameXYZProject.GameObjects;
using TanksGameXYZProject.Interfaces;
using TanksGameXYZProject.Struct;

namespace TanksGameXYZProject.Algoritm
{
    public class EnemyBrain
    {
        MovebleGameObject _playerTank;
        Random _random;
        public EnemyBrain(Random random, MovebleGameObject playerTank)
        {
            _random = random;
            _playerTank = playerTank;
        }
        public void CalculationTankAction(MovebleGameObject go)
        {
            if (go == null||!go.NameIs("Tank")) return;

            if (TryTakeAShot(go)) return;

            var allOption = SearchFreeWay(go);

            if (allOption.Length == 0) return;

            if (TryToRepeatMove(go, allOption)) return;

            GenerationNewDirection(go, allOption);
        }

        private void GenerationNewDirection(MovebleGameObject go, Cell[] allOption)
        {
            var choice = _random.Next(allOption.Length);
            go.Input(InputCommands.DirToCell(allOption[choice]));
        }
        private bool TryToRepeatMove(MovebleGameObject go, Cell[] allOption)
        {
            var lastMove = go.GetMyLastAction();
            if(!InputCommands.IsItCommandForMove(lastMove)) return false;

            if(GameObject.FindGameObjectByCoords(go.GetVirtualCoords().Sum(InputCommands.DirToCell(lastMove)))!=null) return false;
            
            if (_random.Next(4)==1 && allOption.Length>2) return false;

            go.Input(lastMove);
            return true;        

        }
        private bool TryTakeAShot(MovebleGameObject go)
        {
            var direction = DirectionToThePlayer(go.GetVirtualCoords());
            if (direction != null)
            {
                var chanse = _random.Next(4);
                if (chanse == 1)
                {
                    go.ChangeSpriteByDirection(InputCommands.DirToCell((Cell)direction));
                    go.Input(EnumOfInputCommands.Shoot);
                }
                    return true;
            }
            return false;
        }

        private Cell[] SearchFreeWay(GameObject go)
        {
            List<Cell> result = new();
            foreach (var item in InputCommands.GetAllCellDirection())
            {
                var way = GameObject.FindGameObjectByCoords(go.GetVirtualCoords().Sum(item));
                if (way == null)
                    result.Add(item);
            }
            return result.ToArray();
        } 

        private Cell? DirectionToThePlayer(Cell coords)
        {
            var playerCoords = _playerTank.GetVirtualCoords();
            if (playerCoords.X==coords.X|| playerCoords.Y == coords.Y)
            {
                var XNum = (playerCoords.X - coords.X);
                var YNum = (playerCoords.Y - coords.Y);
                var FirstNormolizedNum = XNum == 0 ? 0 : XNum / Math.Abs(XNum);

                var SecondNormolizedNum = YNum == 0 ? 0 : YNum/ Math.Abs(YNum);

                return new Cell(FirstNormolizedNum, SecondNormolizedNum);
            }
            return null;
        }
    }
}
