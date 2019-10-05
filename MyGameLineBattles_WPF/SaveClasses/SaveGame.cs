using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGameLineBattles_WPF
{
    [Serializable]
    public class SaveGame
    {
        public int moneyToSave;
        public List<DefendersArmySaveClass> defendersToSave;
        public List<DefendersArmySaveClass> reserveDefendersToSave;
        public DefendersCannonSaveClass cannonToSave;
        public List<int> MyLocationsToSave;
        public SaveGame()
        {
        }

    }
}
