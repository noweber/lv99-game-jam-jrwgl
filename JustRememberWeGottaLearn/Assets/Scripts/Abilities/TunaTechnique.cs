using Assets.Scripts.HitHurt;
using UnityEngine;

public class TunaTechnique : KungFu
{
    [SerializeField] private int healingAmount = 1;
    [SerializeField] private float healingInterval = 1.0f;
    HurtBox _playerHealth;
    private float _lastHealTimeStamp;
    
    protected override void Start()
    {
        base.Start();
        _stance = Stance.stance.TunaTechnique;
        _playerHealth = Player.Instance.gameObject.GetComponent<HurtBox>();
        _lastHealTimeStamp = Time.time;
    }

    public override void Perform()
    {
        if (Time.time - _lastHealTimeStamp > healingInterval)
        {
            _playerHealth.Heal(healingAmount);
            _lastHealTimeStamp = Time.time;
        }
        
    }
}
