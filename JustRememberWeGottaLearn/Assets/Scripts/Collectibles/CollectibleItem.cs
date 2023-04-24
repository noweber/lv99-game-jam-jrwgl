using Assets.Scripts.Audio;
using System;
using UnityEngine;

namespace Assets.Scripts.Collectibles
{
    public abstract class CollectibleItem : SoundEffectPlayer, ICollectible
    {
        [SerializeField] protected string TagCollectibleBy = Tags.Player;

        public event Action OnCollection;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag(TagCollectibleBy))
            {
                PlaySoundEffect();
                if (TryToBeCollectedBy(other.gameObject))
                {
                    if (OnCollection != null)
                    {
                        OnCollection.Invoke();
                    }
                    else
                    {
                        Destroy(gameObject);
                    }
                }
            }
        }

        protected abstract bool TryToBeCollectedBy(GameObject collectorGameObject);
    }
}
