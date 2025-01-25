using System.Collections.ObjectModel;

namespace TanksGame.Interfaces
{
    //CORRECTED
    public interface IInputListener
    {
        public abstract void OnArrowLeft();
        public abstract void OnArrowUp();
        public abstract void OnArrowRight();
        public abstract void OnArrowDown();
        public abstract void OnShoot();

    }
}