using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABOGGUS.PlayerObjects.Items
{
    public interface IItem : IComparable<IItem>
    {
        public string GetName();

        public string GetDescription();

        public int GetQuantity();

        public bool IncreaseQuantity();

        public int GetID();
    }
}
