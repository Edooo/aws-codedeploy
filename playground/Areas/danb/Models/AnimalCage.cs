using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;
using Playground.Models;


namespace Playground.Areas.Danb.Models
{
    public class AnimalCage
    {
        private int? RestrictBeginMS = null;
        private int? RestrictEndMS = null;
        private HashSet<Animal> animals = new HashSet<Animal>();

        public bool IsOccupied
        {
            get
            {
                return animals.Count > 0;
            }
        }

        private string CageType
        {
            get
            {
                if (IsOccupied)
                {
                    Animal animal = animals.First();
                    return animal.Type;
                }
                return null;
            }
        }

        public AnimalCage()
        {
        }

        public bool TimeRestrictions(DateTime begin, DateTime end)
        {
            int beginMS = (int)(begin.TimeOfDay.TotalMilliseconds);
            int endMS = (int)(end.TimeOfDay.TotalMilliseconds);
            Debug.Assert(beginMS < endMS);
            if (beginMS < endMS)
            {
                RestrictBeginMS = beginMS;
                RestrictEndMS = endMS;
                return true;
            }
            return false;
        }

        public bool AllowedToAddNow()
        {
            if (RestrictBeginMS != null && RestrictEndMS != null)
            {
                int nowms = (int)(IBDateTime.Now.TimeOfDay.TotalMilliseconds);
                if (nowms >= RestrictBeginMS && nowms <= RestrictEndMS)
                {
                    return false;    // current time is restricted
                }
                return true;        // current time is NOT restricted (is allowed)
            }
            return true;            // no time restrictions
        }

        public bool Add(Animal animal)
        {
            if (AllowedToAddNow())
            {
                string cageType = CageType;
                if (cageType == null || cageType.CompareTo(animal.Type) == 0)
                {
                    animals.Add(animal);
                    return true;
                }
            }
            return false;
        }
    }
}