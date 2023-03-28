using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABOGGUS.PlayerObjects.Items
{
    public class Key : IItem
    {
        public const int QUANTITY = 1;

        public string getDescription()
        {
            return ItemLookup.KeyDescription;
        }

        public string getName()
        {
            return ItemLookup.KeyName;
        }

        public int getQuantity()
        {
            return QUANTITY;
        }

        public bool increaseQuantity()
        {
            throw new NotImplementedException();
        }
    }
}
