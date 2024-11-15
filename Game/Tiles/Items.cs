using DungeonCrawl.Maps;
using DungeonCrawl.Mechanics;
using SadConsole;
using SadRogue.Primitives;

namespace DungeonCrawl.Tiles
{
    public abstract class Items : GameObject
    {
        private IScreenSurface screenSurface;
        protected Items(ColoredGlyph appearance, Point position, IScreenSurface hostingSurface) : base(appearance, position, hostingSurface)
        {
            screenSurface = hostingSurface;
        }
       
        protected override bool Touched(GameObject source, Map map)
        {
            if (source == map.UserControlledObject)
            {
                map.UserControlledObject.AddNewItemToInventory(this);
                map.RemoveMapObject(this);
                return true;
            }
            return false;
        }
    }

    public class Sword : Items
    {
        public int Damage { get; private set; }
        public Sword(Point position, IScreenSurface hostingSurface) 
            : base(new ColoredGlyph(Color.Cyan, Color.Transparent, 'S'), position, hostingSurface)
        {
            Damage = 10;
        }
    }
    public class RangeBonus : Items
    {
        public RangeBonus(Point position, IScreenSurface hostingSurface) 
            : base(new ColoredGlyph(Color.Cyan, Color.Transparent, 'R'), position, hostingSurface)
        {
        }

        protected override bool Touched(GameObject source, Map map)
        {
            if (source is Player)
            {
                source.Range += 3;
                map.RemoveMapObject(this);
                return true;
            }
            //source.Touching(this);
            return true;
        }
    }

    public class Potion : Items
    {
        public int Amount { get; private set; }
        public Potion(Point position, IScreenSurface hostingSurface) 
            : base(new ColoredGlyph(Color.Red, Color.Transparent, 3), position, hostingSurface)
        {
            Amount = 25;
        }
        protected override bool Touched(GameObject source, Map map)
        {
            if (source is Player )
            {
                map.UserControlledObject.BaseHealth += 25;
                map.RemoveMapObject(this);
                return true;
            }
            //source.Touching(this);
            return true;
        }
        
    }

    public class Shield : Items
    {
        public int Amount { get; private set; }
        public Shield(Point position, IScreenSurface hostingSurface) 
            : base(new ColoredGlyph(Color.Green, Color.Transparent, 'V'), position, hostingSurface)
        {
            Amount = 10;
        }
    }

}