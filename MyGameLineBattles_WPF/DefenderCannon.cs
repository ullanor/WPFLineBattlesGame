using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace MyGameLineBattles_WPF
{
    public class DefenderCannon
    {
        protected Image myScaryPicture = new Image();
        public string Name { get; protected set; }
        public int HealthPoints { get; protected set; }
        public int MyShootingSkill { get; protected set; }
        public int CannonPosition { get; private set; }
        public bool CannonCanShoot { get; private set; }
        public int MyLeftMargin { get; protected set; }
        public int MyTopMargin { get; protected set; }
        public int ExperiencePoints { get; protected set; }
        
        public void TrainCannonBS()
        {
            MyShootingSkill += 5;
        }
        public void RepairCannonHP()
        {
            HealthPoints = 50;
        }
        public void ChangeDefenderCannonExperiencePoints(int amount) 
        {
            ExperiencePoints += amount;
        }
        public virtual bool AssessDamage()
        {
            int left = (int)myScaryPicture.Margin.Left - 10;
            int top = (int)myScaryPicture.Margin.Top;
            myScaryPicture.Margin = new System.Windows.Thickness(left, top, 0, 0);
            HealthPoints -= 30;
            if (HealthPoints <= 0)
                return true;
            return false;
        }

        public DefenderCannon(string name, int healthPoints, int myShootingSkill, Image myScaryImage)
        {
            Name = name;
            HealthPoints = healthPoints;
            MyShootingSkill = myShootingSkill;
            CannonPosition = 2;
            CannonCanShoot = true;
            myScaryPicture = myScaryImage;
            MyLeftMargin = (int)myScaryPicture.Margin.Left;
            MyTopMargin = (int)myScaryPicture.Margin.Top;
            myScaryPicture.Source = new BitmapImage(new Uri(@"Resources\BlueDefenderCannon.png", UriKind.Relative));
        }
        //restoring cannon from save constructor !----------------------------
        public DefenderCannon(string name, int healthPoints, int myShootingSkill, int experiencePoints, Image myScaryImage) : this(name,healthPoints,myShootingSkill,myScaryImage)
        {
            ExperiencePoints = experiencePoints;
        }
        public void ChangeImageToDestroyed()
        {
            // MessageBox.Show("here");
            myScaryPicture.Source = new BitmapImage(new Uri(@"Resources\CannonDestroyed.png", UriKind.Relative));
        }
        public void SetCannonShoot(bool shoot)
        {
            CannonCanShoot = shoot;
        }
        public void RestoreDefaultImagePosition()
        {

            myScaryPicture.Margin = new System.Windows.Thickness(MyLeftMargin, MyTopMargin, 0, 0);

        }
        public virtual void ShootingRollBack()
        {

            int left = (int)myScaryPicture.Margin.Left - 10;
            int top = (int)myScaryPicture.Margin.Top;
            myScaryPicture.Margin = new System.Windows.Thickness(left, top, 0, 0);

        }
        public void ChangeCannonPosition()
        {
            if (CannonPosition == 0)
                CannonPosition = 1;
            else if (CannonPosition == 1)
                CannonPosition = 2;
            else
                CannonPosition = 0;
        }
    }

    public class EnemyCannon : DefenderCannon
    {
        public EnemyCannon(Image myScaryImage) : base("Cannon", 50,50,myScaryImage)
        {
            
            myScaryPicture.Source = new BitmapImage(new Uri(@"Resources\RedEnemyCannon.png", UriKind.Relative));
        }
        public void ChangeEnemyCannonStats(string newName,int healthPoints,int shootingSkill)
        {
            this.Name = newName;
            this.HealthPoints = healthPoints;
            this.MyShootingSkill = shootingSkill;
        }
        public void ChangeImageToDestroyed(bool demonAreEnemy)
        {
            if (demonAreEnemy)
                myScaryPicture.Source = new BitmapImage(new Uri(@"Resources\bombaDemonDead.png", UriKind.Relative));
            else
                myScaryPicture.Source = new BitmapImage(new Uri(@"Resources\CannonDestroyed.png", UriKind.Relative));
        }
        public override void ShootingRollBack()
        {

            int left = (int)myScaryPicture.Margin.Left + 10;
            int top = (int)myScaryPicture.Margin.Top;
            myScaryPicture.Margin = new System.Windows.Thickness(left, top, 0, 0);

        }
        public override bool AssessDamage()
        {
            int left = (int)myScaryPicture.Margin.Left + 10;
            int top = (int)myScaryPicture.Margin.Top;
            myScaryPicture.Margin = new System.Windows.Thickness(left, top, 0, 0);
            HealthPoints -= 30;
            if (HealthPoints <= 0)
                return true;
            return false;
        }
    }
  
}
