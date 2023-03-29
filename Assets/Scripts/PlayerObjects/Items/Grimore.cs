using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABOGGUS.PlayerObjects.Items
{
    public class Grimore : IItem
    {
        public const int QUANTITY = 1;

        public string getDescription()
        {
            return ItemLookup.GrimoreDescription;
        }

        public string getName()
        {
            return ItemLookup.GrimoreName;
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