using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABOGGUS.PlayerObjects.Items
{
    public class TractorKey : IItem
    {
        public const int QUANTITY = 1;

        public string GetDescription()
        {
            return ItemLookup.TractorKeyDescription;
        }

        public string GetName()
        {
            return ItemLookup.TractorKeyName;
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
            return ItemLookup.TractorKeyID;
        }

        public int CompareTo(IItem item)
        {
            return this.GetID().CompareTo(item.GetID());
        }
    }
}