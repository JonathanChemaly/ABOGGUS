using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABOGGUS.PlayerObjects.Items
{
    public class Hammer : IItem
    {
        public const int QUANTITY = 1;

        public string GetDescription()
        {
            return ItemLookup.HammerDescription;
        }

        public string GetName()
        {
            return ItemLookup.HammerName;
        }

        public int GetQuantity()
        {
            return QUANTITY;
        }

        public bool IncreaseQuantity()
        {
            throw new NotImplementedException();
        }

        public int CompareTo(IItem item)
        {
            return this.GetID().CompareTo(item.GetID());
        }

        public int GetID()
        {
            return ItemLookup.HammerID;
        }
    }
}
