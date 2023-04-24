using Assets.Scripts.LevelUps;
using System.Reflection;
using UnityEngine;

namespace Assets.Scripts.Collectibles
{
    public class ExperiencePointsCollectible : CollectibleItem
    {
        [SerializeField] int ExperiencePointsValue;

        protected override bool TryToBeCollectedBy(GameObject collectorGameObject)
        {
            Debug.Log(MethodBase.GetCurrentMethod().DeclaringType.Name + "::" + MethodBase.GetCurrentMethod());
            var levelUpController = collectorGameObject.GetComponent<ILevelUpController>();
            if (levelUpController != null)
            {
                levelUpController.AddExperiencePoints(ExperiencePointsValue);
                return true;
            }
            return false;
        }
    }
}
