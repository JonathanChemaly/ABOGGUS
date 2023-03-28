using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABOGGUS.PlayerObjects.Items
{
    public class Wrench : IItem
    {
        public const int QUANTITY = 1;

        public string getDescription()
        {
            return ItemLookup.WrenchDescription;
        }

        public string getName()
        {
            return ItemLookup.WrenchName;
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
