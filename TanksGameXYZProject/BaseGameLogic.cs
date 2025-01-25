using Shared;
using TanksGame.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TanksGame
{
    public abstract class BaseGameLogic : IInputListener
    {
        protected BaseGameState? currentState;
        protected float time;
        protected int screenWidth;
        protected int screenHight;
        public virtual void OnArrowDown() { }
        public virtual void OnArrowLeft() { }
        public virtual void OnArrowRight() { }
        public virtual void OnArrowUp() { }
        public virtual void OnShoot() { }
        protected virtual void ChangeState(BaseGameState state) 
        {
            currentState?.Reset();
            currentState = state;
        }
        public void InitializeInput(ConsoleInput input)
        {
            input.Subscribe(this);
        }
        public abstract void Update(float deltaTime);
        public abstract ConsoleColor[] CreatePallet();
        public void DrawNewState(float deltaTime, ConsoleRenderer renderer)
        {
            time += deltaTime;
            screenHight = renderer.height;
            screenWidth = renderer.width;

            currentState?.Update(deltaTime);
            currentState?.Draw(renderer);

            Update(deltaTime);
        }

    }
}
