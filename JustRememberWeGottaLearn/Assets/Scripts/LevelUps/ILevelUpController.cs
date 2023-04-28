namespace Assets.Scripts.LevelUps
{
    public interface ILevelUpController
    {
        int Level { get; }

        void AddExperiencePoints(int experiencePoints);

        int GetExperiencePointsToNextLevel();
    }
}
