using Assets.Scripts.Audio;
using UnityEngine;

namespace Assets.Scripts.LevelUps
{
    public sealed class LevelUpController : SoundEffectPlayer, ILevelUpController
    {
        public int Level
        {
            get { return CurrentLevel; }
        }

        [SerializeField] private int CurrentLevel;

        [SerializeField] private int BaseLevelUpExperiencePointsCost;

        [SerializeField] private int ExperiencePointsToNextLevel;

        protected override void Awake()
        {
            base.Awake();
        }

        public void AddExperiencePoints(int experiencePoints)
        {
            ExperiencePointsToNextLevel -= experiencePoints;
            if (ExperiencePointsToNextLevel <= 0)
            {
                LevelUp();
            }
        }

        public int GetExperiencePointsToNextLevel()
        {
            return ExperiencePointsToNextLevel;
        }

        private void LevelUp()
        {
            CurrentLevel++;
            ExperiencePointsToNextLevel += (BaseLevelUpExperiencePointsCost * Level);
            PlaySoundEffect();
        }
    }
}
