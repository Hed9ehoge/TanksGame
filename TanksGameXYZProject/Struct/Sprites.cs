using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TanksGameXYZProject.Struct
{
    public struct Sprites
    {
        public static Sprites EmptySprite => new Sprites(' ');

        char[] spriteChars;
        public char[] SpriteChars => spriteChars;
        public Sprites(char Sprite1, char Sprite2 = ' ', char Sprite3 = ' ', char Sprite4 = ' ')
        {
            spriteChars = new char[4] { Sprite1, Sprite2, Sprite3, Sprite4 };
        }

    }
    
}
