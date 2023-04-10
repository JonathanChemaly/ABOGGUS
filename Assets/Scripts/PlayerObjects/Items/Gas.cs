using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABOGGUS.PlayerObjects.Items
{
    public class Gas : IItem
    {
        public const int QUANTITY = 1;

        public string GetDescription()
        {
            return ItemLookup.GasDescription;
        }

        public string GetName()
        {
            return ItemLookup.GasName;
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
            return ItemLookup.GasID;

        }
        public int CompareTo(IItem item)
        {
            return this.GetID().CompareTo(item.GetID());
        }
    }
}