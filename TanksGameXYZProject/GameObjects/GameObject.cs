using TanksGame.Struct;
using TanksGameXYZProject.Interfaces;
using TanksGameXYZProject.Struct;

namespace TanksGameXYZProject.GameObjects
{
    public abstract class GameObject
    {
        protected int _hp;
        protected bool CannotTakeDamage;
        byte _collor;
        protected MajorCell _coords;
        bool _destroy = false;
        protected int _currentSpriteNumbor = 0;
        private static List<GameObject> _listOfGameObject = new();
        protected abstract Sprites[] GetArroyOfSprites();
        protected string _name = "UnnamedObject";
        protected List<string> _tags = new();
        bool _anySpriteChange = true;
        public bool NameIs(string name) => _name == name;
        public void AddTag(string tag) => _tags.Add(tag); 
        public bool HasTag(string tag) => _tags.Contains(tag); 
        public void RenameIt(string name ) => _name = name;
        public virtual void ReceiveDamage(int damage)
        {
            if (CannotTakeDamage) return;
            _hp -= damage;
            if (_hp == 0) 
            {
                Destroy(); 
                return; 
            }
        }
        
        protected GameObject(byte collor, Cell cell,string objectName, int hp = -1)
        {
            _name = objectName;
            _coords = new MajorCell(cell);
            _collor = collor;
            _hp= hp;
            if (hp == -1) CannotTakeDamage = true;
            _listOfGameObject.Add(this);
        }
        public static void DestroyAll()
        {
            _listOfGameObject.Clear();
            MovebleGameObject.DestroyAllMoveble();
        }
        public virtual void Destroy()
        {
            _currentSpriteNumbor = -1;
            _destroy = true;
            _listOfGameObject.Remove(this);

        }
        protected bool TouchOtherGO(Cell virtyalCell)
        {
            foreach (var go in _listOfGameObject)
            {
                if (go.GetVirtualCoords().Equals(virtyalCell) && !go.NameIs("Error")) 
                    return true;
            }
            return false;
        }
        public static GameObject? FindGameObjectByCoords(Cell virtyalCell,GameObject except = null)
        {
            foreach (var go in _listOfGameObject)
            {
                if (go.GetVirtualCoords().Equals(virtyalCell)&& except != go && !go.NameIs("Error"))
                    return go;
            }
            return null;
        }
        public static GameObject[] GetAllGameObject()=> _listOfGameObject.ToArray();
        protected virtual void ChangeSprite(int sprite)
        {
            if (GetArroyOfSprites().Count() <= sprite)
            {
                _currentSpriteNumbor = -1;
                return;
            }

            _currentSpriteNumbor = sprite;
            _anySpriteChange = true;
        }
        bool AnySpriteChangeSinceLastTime()
        {
            var result = _anySpriteChange;
            _anySpriteChange = false;
            return result;
        }
        public Cell GetVirtualCoords()
        {
            return _coords.VirtualCell;
        }
        public MajorCell GetMajorCoords(out bool anyChange2)
        {
            anyChange2 = _coords.AnyChangeSinceLastTime();
            return _coords;
        }
        public byte GetColor() => _collor;
        public Sprites GetCurrentSprites(out bool anyChange1) 
        {
            anyChange1 = AnySpriteChangeSinceLastTime();
            return _currentSpriteNumbor == -1 ? Sprites.EmptySprite : GetArroyOfSprites()[_currentSpriteNumbor]; 
        }
        public bool ThisObjectNotExists() => _destroy;
    }
}
