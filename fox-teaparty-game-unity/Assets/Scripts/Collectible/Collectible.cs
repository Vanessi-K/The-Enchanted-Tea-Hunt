using System;
using UnityEngine;
public class Collectible : MonoBehaviour
{
    [SerializeField] public float Weight;
    private bool _isActive;
    public CollectionState CollectionState = CollectionState.NotCollected;
    private Backpack _backpack;

    private void Start()
    {
        _backpack = GameStats.Instance.Backpack;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (_backpack.BackpackHasSpace)
            {
                _backpack.AddCollectible(this);
                gameObject.SetActive(false);
                _isActive = false;
            }
        }
    }
}
