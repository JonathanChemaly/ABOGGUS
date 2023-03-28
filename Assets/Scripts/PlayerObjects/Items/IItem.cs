using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABOGGUS.PlayerObjects.Items
{
    public interface IItem
    {
        public string getName();

        public string getDescription();

        public int getQuantity();

        public bool increaseQuantity();
    }
}
