using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Playground.Areas.JoshuaD.Models
{
    /// <summary>
    /// Animal Class.
    /// </summary>
    public class AnimalModel
    {
        /// <summary>
        /// The name of the animal.
        /// </summary>
        public string Name
        {
            get;
            private set;
        }
        /// <summary>
        /// Custom constructor. Assign animal name.
        /// </summary>
        /// <param name="name">The name of the animal.</param>
        public AnimalModel(string name)
        {
            Name = name;
        }
    }
}