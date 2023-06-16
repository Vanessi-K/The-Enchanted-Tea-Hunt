using System;
using UnityEngine;

[RequireComponent(typeof(CollectionStateManager))]
public class Collectible : MonoBehaviour
{
    [SerializeField] public float Weight;
    [SerializeField] public CollectibleType Type;
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
            }
        }
    }
}