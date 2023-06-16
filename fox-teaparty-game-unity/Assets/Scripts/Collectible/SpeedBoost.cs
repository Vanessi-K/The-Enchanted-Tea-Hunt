using UnityEngine;

[RequireComponent(typeof(CollectionStateManager))]
public class SpeedBoost : MonoBehaviour
{
    [SerializeField] private float speedBoostPower;
    [SerializeField] private float speedBoostDuration;

    public float SpeedBoostPower
    {
        get
        {
            return speedBoostPower;
        }
    }
    
    public float SpeedBoostDuration
    {
        get
        {
            return speedBoostDuration;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(GameStats.Instance.Backpack.HasSpeedBoost)
                return;

            GameStats.Instance.Backpack.AddSpeedBoost(this);
            gameObject.SetActive(false);
        }
    }
}