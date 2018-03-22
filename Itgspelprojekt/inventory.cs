using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itgspelprojekt
{
    public class inventorySystem
    {
        //Items klassen är för saker som ska vara inventoryn och så att de inte stackas.
       public abstract class Items
        {
            public Guid ID { get; set; }
            public string Name { get; set; }
            public int MaximumStacks { get; set; }

            protected Items()
            {
                MaximumStacks = 1;
            }
            public class Potion : Items
            {
                  
            }
        }
        public class inventory
       {
            private const int Inventory_Slots = 8;
            public static List<Items> inventoryRecord;
            //ADD
            

       }


        
    }
}
