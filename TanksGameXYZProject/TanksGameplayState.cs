using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Shared;
using TanksGame.Struct;
using TanksGameXYZProject;
using TanksGameXYZProject.Algoritm;
using TanksGameXYZProject.GameObjects;
using TanksGameXYZProject.Interfaces;
using static System.Net.Mime.MediaTypeNames;

namespace TanksGame
{
    public class TanksGameplayState : BaseGameState
    {
        public Cell CurrentCoords;
        protected Cell BaseCoords = new Cell(5, 5);
        private int _playerHP = 5;
        private int _enemyHP = 4;
        private Tank? _playerTank;

        private float timeToMove;
        public int fieldWidth;
        public int fieldHeight;
        public int gameMapWidth;
        public int gameMapHeight;
        Random _random = new();
        public bool gameOver;
        public bool hasWon;
        public int level;
        EnemyBrain _enemyBrain;
        List<MovebleGameObject> EnemyList = new();
        
        private string _seed;
        private int tmp;
        public void SetOwnSeed(string seed) {_seed = seed; _random = new (GetHash(seed)); tmp = GetHash(seed); }
        private int GetHash(string txt)
        {
            int resul=0;
            foreach (var letter in txt)
            {

                resul+=(int)Char.GetNumericValue(letter);
            }
            return resul;
        }

        public const float FPS = 1f/4;
        public TanksGameplayState()
        {
            if (string.IsNullOrEmpty(_seed))
            {
                _seed = (_random.Next(int.MinValue, int.MaxValue)).ToString();
                _random = new(GetHash(_seed));
            }
        }
        public void FirstPressedKeyReading(EnumOfInputCommands action)
        {
            if (_playerTank.GetMyNextAction() != EnumOfInputCommands.NoN) return;
            _playerTank.Input(action);
        }
        public void ActionUpdate()
        {
            var arrey = MovebleGameObject.GetAllMovebleGameObject();
            foreach (var go in arrey)
            {
                Cell nextCell;

                if (go.NameIs("Tank")) _enemyBrain.CalculationTankAction(go);

                var action = go.GetMyNextAction();
                if (action == EnumOfInputCommands.NoN) continue;


                if (InputCommands.IsItCommandForMove(action))
                    nextCell = InputCommands.DirToCell(action);
                else
                    nextCell = InputCommands.DirToCell(go.DirectionOfView());

                nextCell = nextCell.Sum(go.GetVirtualCoords());

                if ((nextCell.X < 0 || nextCell.Y < 0 || nextCell.Y > gameMapHeight || nextCell.X > gameMapWidth))
                {
                    go.ChangeSpriteByDirection(action);
                    continue;
                }              
                go.DoAction();
            }

        }
        public override void Reset()
        {
            CurrentCoords = BaseCoords;
            gameOver = false;
            hasWon = false;
            var middleY = fieldHeight/2;
            var middleX = fieldWidth/2;
            timeToMove = 0;
            _playerTank.RenameIt("Player");
            _enemyBrain = new EnemyBrain(_random,_playerTank);
        }

        public override void Update(float deltaTime)
        {
            timeToMove -= deltaTime;

            if (timeToMove > 0 || gameOver) return;

            timeToMove = FPS;
            
            ActionUpdate();
            AmIWin();

            if (_playerTank!=null && _playerTank.ThisObjectNotExists())
            {
                gameOver = true;
                return;
            }

        }

        private void AmIWin()
        {
            foreach (var item in EnemyList)
            {
                if (!item.ThisObjectNotExists())
                    return;
            }
            hasWon = true;
        }

        public override void Draw(ConsoleRenderer renderer)
        {
            var textX = (gameMapWidth + 2) * 2;
            renderer.DrawString($"Level {level}", textX, 4,ConsoleColor.Cyan);
            renderer.DrawString($"Player HP {_playerTank.HP}", textX, 6,ConsoleColor.Cyan);
            renderer.DrawString($"Seed {_seed}", textX, 8,ConsoleColor.Cyan);
            renderer.DrawString($"Seed {tmp}", textX, 10,ConsoleColor.Cyan);
            foreach (var go in GameObject.GetAllGameObject())
            {
                if (go.ThisObjectNotExists()) continue;
                bool anyChange1;
                bool anyChange2;
                var sprites = go.GetCurrentSprites(out anyChange1);
                var cords = go.GetMajorCoords(out anyChange2);
                var color = go.GetColor();
                //if (!anyChange1 && !anyChange2) continue;
                for (var i = 0; i != 4; i++)
                {
                    renderer.SetPixel(cords.RealCordsX[i], cords.RealCordsY[i], sprites.SpriteChars[i],color);
                }
            }
        }

        public override bool IsDone()
        {
            return gameOver|| hasWon;
        }

        public void GinerateMap(string[] textMap)
        {
            GameObject.DestroyAll();
            _playerTank = null;
            gameMapWidth = textMap[0].Length-1;
            gameMapHeight = textMap.Length-1;
            for (var y = 0; y < textMap.Length; y++)
            {
                for (var x = 0; x < textMap[0].Length; x++)
                {
                    var cell = new Cell(x, y);
                    var letter = textMap[y][x];
                    switch (letter)
                    {
                        case ' ':
                            break;
                        case 'P':
                            if (_playerTank == null)
                                _playerTank = new Tank(0, cell, _playerHP);
                            else
                                new Error(cell);
                            break;
                        case 'E':
                            EnemyList.Add(new Tank(3, cell, _enemyHP));
                            break;
                        case '#':
                            new Wall(cell);
                            break;
                        case 'W':
                            new Water(cell);
                            break;
                        default:
                            new Error(cell);
                            break;
                    }
                }
            }           
            }
        }
    }


