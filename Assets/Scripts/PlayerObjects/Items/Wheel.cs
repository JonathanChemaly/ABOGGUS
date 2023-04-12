using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABOGGUS.PlayerObjects.Items
{
    public class Wheel : IItem
    {
        public const int QUANTITY = 1;

        public string GetDescription()
        {
            return ItemLookup.WheelDescription;
        }

        public string GetName()
        {
            return ItemLookup.WheelName;
        }

        public int GetQuantity()
        {
            return QUANTITY;
        }

        public bool IncreaseQuantity()
        {
            throw new NotImplementedException();
        }
        public int GetID()
        {
            return ItemLookup.WheelID;
        }

        public int CompareTo(IItem item)
        {
            return this.GetID().CompareTo(item.GetID());
        }
    }
}
