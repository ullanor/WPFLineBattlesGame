using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MyGameLineBattles_WPF
{
    class OperationsOnDefenders
    {
        public List<Defender> PromoteDefender(List<Defender> defenders, int selectedDefender, List<Image> defendersPictures)
        {
            MessageBox.Show(defenders[selectedDefender].Name + " Promoted to Grenadier!");
            string defenderTrueName = defenders[selectedDefender].Name.Remove(0, 9); //dlugosc nazwy strzelec
            int defenderTrueShootingSkill = defenders[selectedDefender].MyShootingSkill;
            int defenderTrueExperience = defenders[selectedDefender].ExperiencePoints;
            defenders.RemoveAt(selectedDefender);
            defenders.Add(new EliteDefender("Grenadier " + defenderTrueName, 50, defendersPictures[defenders.Count], defenderTrueShootingSkill, defenderTrueExperience));
            //defenders[defenders.Count - 1].ChangeDefenderExperiencePoints(defenderTrueExperience);           

            return defenders;
        }
    }
}
