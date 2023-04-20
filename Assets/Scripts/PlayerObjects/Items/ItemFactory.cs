using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ABOGGUS.PlayerObjects.Items
{
    internal class ItemFactory
    {
        public static IItem CreateItem(string name)
        {
            switch (name) {
                case ItemLookup.KeyName: return new Key();
                case ItemLookup.WrenchName: return new Wrench();
                case ItemLookup.GasName: return new Gas();
                case ItemLookup.WheelName: return new Wheel();
                case ItemLookup.TractorKeyName: return new TractorKey();
                case ItemLookup.GrimoireName: return new Grimore();
                case ItemLookup.HammerName: return new Hammer();
                case ItemLookup.BucketName: return new Bucket();
            }

            return null;
        }
    }
}
