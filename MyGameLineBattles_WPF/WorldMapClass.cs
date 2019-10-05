using System;
using System.Collections.Generic;

namespace MyGameLineBattles_WPF
{
    public class WorldMapClass
    {
        //testowe ---------------------------------------
        public List<int> MyLocations = new List<int>();
        public int targetLocation;
        public bool IsThisOrksLocation()
        {
            if (targetLocation >= 4 && targetLocation <= 7)
                return true;
            return false;
        }
        public bool IsThisDemonsLocation()
        {
            if (targetLocation == 11 || targetLocation == 12)
                return true;
            return false;
        }
        public void AddConqueredLocation()
        {
            MyLocations.Add(targetLocation);
        }
        public void SetNewLoadedMyLocations(List<int> loadedLocations)
        {
            MyLocations.Clear();
            MyLocations = loadedLocations;
        }
        public void DeleteDefendedLocation(Random random)
        {
            int location;
            do
            {
                location = MyLocations[random.Next(MyLocations.Count)];
            } while (location == 3 || location == 6 || location == 9 || location == 11);
            MyLocations.Remove(location);
            targetLocation = location;
        }
    }
}
