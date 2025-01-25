using Shared;
using TanksGame.Struct;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TanksGameXYZProject;
using System.Reflection.Emit;

namespace TanksGame
{
    public class TanksGameLogic : BaseGameLogic
    {
        public float GetAllTime => time;
        TanksGameplayState gameplayState = new TanksGameplayState();
        bool newGamePending = false;
        int currentLevel;
        ShowTextState showTextState = new(2);
        public void SetOwnSeed(string seed) => gameplayState.SetOwnSeed(seed);
        private void GotoNextLevel()
        {
            currentLevel++;
            newGamePending = false;
            showTextState.text = $"Level {currentLevel}";
            ChangeState(showTextState);
        }
        private void GotoGameOver()
        {
            currentLevel = 0;
            newGamePending = true;
            showTextState.text = "Game Over";
            ChangeState(showTextState);
        }
        public override void OnArrowDown()
        {
            if(currentState!=gameplayState) return;
            gameplayState.FirstPressedKeyReading(EnumOfInputCommands.Down);
        }

        public override void OnShoot()
        {
            if (currentState != gameplayState) return;
            gameplayState.FirstPressedKeyReading(EnumOfInputCommands.Shoot);
        }
        public override void OnArrowLeft()
        {
            if (currentState != gameplayState) return;
            gameplayState.FirstPressedKeyReading(EnumOfInputCommands.Left);
        }

        public override void OnArrowRight()
        {
            if (currentState != gameplayState) return;
            gameplayState.FirstPressedKeyReading(EnumOfInputCommands.Right);

        }
        public override void OnArrowUp()
        {
            if (currentState != gameplayState) return;
            gameplayState.FirstPressedKeyReading(EnumOfInputCommands.Up);
        }
        public override void Update(float deltaTime)
        {
            if(currentState!=null&&!currentState.IsDone())return;


            if (currentState == null || currentState == gameplayState && !gameplayState.gameOver) GotoNextLevel();

            else if (currentState == gameplayState && gameplayState.gameOver) GotoGameOver();

            else if (currentState != gameplayState && newGamePending) GotoNextLevel();

            else if (currentState != gameplayState && !newGamePending) GotoGameplay();
        }
        public void GotoGameplay()
        {
            gameplayState.level = currentLevel;

            gameplayState.GinerateMap(LoadMap(currentLevel));

            gameplayState.fieldWidth = screenWidth;
            gameplayState.fieldHeight = screenHight;
            ChangeState(gameplayState);
            gameplayState.Reset();
        }
        private string[] LoadMap(int level)
        {
            List<string> result = new List<string>();
            var path = $"Data/Level{level}.txt";
            // "C:\Users\boyko\source\repos\TanksGameXYZProject\TanksGameXYZProject\Data\Level1.txt"
            var doc = File.OpenText(path);
            string str;
            while ((str = doc.ReadLine()) != null) 
            {
                result.Add(str);
            }
            return result.ToArray();
        }

        public override ConsoleColor[] CreatePallet()
        {
            return new ConsoleColor[] { ConsoleColor.Gray, ConsoleColor.DarkRed, ConsoleColor.DarkBlue ,ConsoleColor.DarkMagenta, ConsoleColor.White,ConsoleColor.Cyan , ConsoleColor.Red};
        }
    }
}
