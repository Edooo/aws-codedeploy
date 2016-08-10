using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Playground.Areas.Danb.Models
{
    public class Animal
    {
        public string Type
        {
            get;
            private set;
        }
        public float Weight;

        public Animal(string type, float weight)
        {
            Type = type;
            Weight = weight;
        }

        public static Animal MakeTiger(float weight)
        {
            return new Animal("tiger", weight);
        }

        public static Animal MakeMonkey(float weight)
        {
            return new Animal("monkey", weight);
        }

        public static Animal MakeZebra(float weight)
        {
            return new Animal("zebra", weight);
        }
    }
}