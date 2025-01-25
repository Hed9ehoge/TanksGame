using Shared;
using TanksGame;
using System    ;


namespace MyApp
{
    internal class Program
    {
        const float targetFrameTime = 1 / 60;
        static void Main(string[] args)
        {
            TanksGameLogic gameLogic = new TanksGameLogic();
            ConsoleInput Input = new ConsoleInput();
            gameLogic.SetOwnSeed("Этот сид крут");

            var pallete = gameLogic.CreatePallet();

            ConsoleRenderer renderer0 = new ConsoleRenderer(pallete);
            ConsoleRenderer renderer1 = new ConsoleRenderer(pallete);

            gameLogic.InitializeInput(Input);

            var prevRenderer = renderer0;
            var currRenderer = renderer1;
            var lastFrameTime = DateTime.Now; 
            while (true) 
            {
                
                var frameStartTime = DateTime.Now;
                var deltaTime = (float)(frameStartTime - lastFrameTime).TotalSeconds;
                Input.Update();

                gameLogic.DrawNewState(deltaTime, currRenderer);
                lastFrameTime = frameStartTime;

                if (!currRenderer.Equals(prevRenderer)) currRenderer.Render();

                var tmp = prevRenderer;
                prevRenderer = currRenderer;
                currRenderer = tmp;
                currRenderer.Clear();

                var nextFrameTime = frameStartTime + TimeSpan.FromSeconds(targetFrameTime);
                var endFrameTime = DateTime.Now;
                if (nextFrameTime > endFrameTime)
                    Thread.Sleep((int)(nextFrameTime - endFrameTime).TotalMilliseconds);
            }
        }
    }
}