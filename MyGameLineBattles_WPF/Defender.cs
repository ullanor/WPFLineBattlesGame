using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace MyGameLineBattles_WPF
{
    public class Defender : Enemy
    {
        public bool GetHitedByArrow { get; private set; }
        public void RemoveArrow()
        {
            GetHitedByArrow = false;
        }
        public int ExperiencePoints { get; protected set; }
        public Defender(string name, int healthPoints, Image myScaryPicture, int myShootingSkill, int ExperiencePoints) : base(name, healthPoints, myScaryPicture, myShootingSkill)
        {
            this.ExperiencePoints = ExperiencePoints;
        }
        public void IncreaseDefenderShootinSkill() //zwiększa umiejętności strzeleckie o 5
        {
            MyShootingSkill += 5;
            if (MyShootingSkill == 30 && this is IAmRecruitDefender)
            {
                string newName = Name.Remove(0,8);
                Name = "Fusilier " + newName;
            }
        }

        public void ChangeDefenderExperiencePoints(int amount) //zwiększa umiejętności strzeleckie o 5
        {
            ExperiencePoints += amount;
        }
        public Image GetMyScaryPicture()
        {
            return myScaryPicture;
        }
        public void SetArrowHitImage()
        {
            int left = (int)myScaryPicture.Margin.Left - 10;
            int top = (int)myScaryPicture.Margin.Top;
            myScaryPicture.Margin = new System.Windows.Thickness(left, top, 0, 0);
        }
        public override void SetMyRealPicture()
        {
            myScaryPicture.Source = new BitmapImage(new Uri(@"Resources\BlueDefenderF.png", UriKind.Relative));
            myScaryPicture.Visibility = Visibility.Collapsed;
        }      

        public void IncreaseDefenderHealthPoints(int amount)
        {
            HealthPoints += amount;
        }
        public void RestoreDefenderHealthPoints(int amount)
        {
            HealthPoints = amount;
        }
        public override bool AssessDamage(bool orksAreEnemy)
        {
            if (!orksAreEnemy)
            {
                int left = (int)myScaryPicture.Margin.Left - 10;
                int top = (int)myScaryPicture.Margin.Top;
                myScaryPicture.Margin = new System.Windows.Thickness(left, top, 0, 0);
                HealthPoints -= 40;
            }
            else
                HealthPoints -= 30;
            if (HealthPoints <= 0)
            {
                if (!orksAreEnemy)
                    myScaryPicture.Source = new BitmapImage(new Uri(@"Resources\BlueDefenderDead.png", UriKind.Relative));
                return true;
            }
            GetHitedByArrow = true;
            return false;
        }


    }
    
    public class RecruitDefender : Defender, IAmRecruitDefender
    {
        public RecruitDefender(string name, int healthPoints, Image myScaryPicture, int myShootingSkill, int ExperiencePoints) : base(name, healthPoints, myScaryPicture, myShootingSkill, ExperiencePoints)
        { }
    }
    public class EliteDefender : Defender, IAmEliteDefender
    { 
        public EliteDefender(string name, int healthPoints, Image myScaryPicture, int myShootingSkill, int ExperiencePoints) : base(name,healthPoints,myScaryPicture,myShootingSkill, ExperiencePoints)
        {}
        public override void SetMyRealPicture()
        {
            myScaryPicture.Source = new BitmapImage(new Uri(@"Resources\BlueDefenderG.png", UriKind.Relative));
            myScaryPicture.Visibility = Visibility.Collapsed;

        }

    }

    public class GeneralDefender : Defender, IAmEliteDefender, IAmGeneralDefender
    {
        public GeneralDefender(string name, int healthPoints, Image myScaryPicture, int myShootingSkill, int ExperiencePoints, int generalWarbandMorale) : base(name, healthPoints, myScaryPicture, myShootingSkill, ExperiencePoints)
        {
            GeneralWarbandMorale = generalWarbandMorale;
        }
        public int GeneralWarbandMorale { get; private set; }
        public bool IncreaseDefenderMorale()
        {
            if (GeneralWarbandMorale == 100)
                return false;
            GeneralWarbandMorale += 5;
            return true;

        }
        public void SetNewGeneralWarbandMorale(int amount)
        {
            GeneralWarbandMorale = amount;
        }
        public override void SetMyRealPicture()
        {
            myScaryPicture.Source = new BitmapImage(new Uri(@"Resources\BlueDefenderLeader.png", UriKind.Relative));
            myScaryPicture.Visibility = Visibility.Collapsed;

        }

    }
}
