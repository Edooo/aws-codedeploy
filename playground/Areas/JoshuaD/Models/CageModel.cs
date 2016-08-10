using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Playground.Areas.JoshuaD.Models
{
    /// <summary>
    /// A cage to hold an animal
    /// </summary>
	public class CageModel
	{
        /// <summary>
        /// Is the Cage Currently occupied.
        /// </summary>
        public bool IsOccupied
        {
            get {
                return null != _occupant;
            }
        }

        /// <summary>
        /// The current ocuppant of the cage.
        /// </summary>
        private AnimalModel _occupant = null;

        /// <summary>
        /// Add an animal to the cage.
        /// </summary>
        /// <param name="animal">The Animal to be Added.</param>
        /// <param name="arrivalTime">The datetime the animal is being added to the cage.</param>
        /// <returns><b>true</b> if the animal was added; otherwise false.</returns>
        /// <exception cref="ArgumentNullException">No animal supplied to add to the cage.</exception>
        /// <remarks>The Main Zookeeper is a Vampire so no animals may be added to the Zoo between the hours of 8 A.M. and 8 P.M. daily. </remarks>
        public bool Add(AnimalModel animal, iHourGetter arrivalTime)
        {
            // I know this is bad practice dan... this is only for testing.
            if (default(AnimalModel) == animal)
                throw new ArgumentNullException("No animal supplied to add to the cage.");

            int hour = arrivalTime.GetHour();

            // Vlad is sleeping in his coffin.
            if (8 <= hour && hour <= 20)
                return false;

            if (IsOccupied)
                return false;
            _occupant = animal;
            return true;
        }
	}
}
