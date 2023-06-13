using System;
using UnityEngine;

public enum CollectibleType
{
    Teapot,
    Cupcake,
    Teacup
}

public enum CollectionState 
{
    NotCollected,
    InPlayerInventory,
    Returned
}

public class Collectible : MonoBehaviour
{
    [SerializeField] private float weight;
    private bool _isActive;
    public CollectionState CollectionState = CollectionState.NotCollected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Backpack backpack = other.GetComponent<Backpack>();
            if (backpack.BackpackHasSpace)
            {
                backpack.AddCollectible(this);
                gameObject.SetActive(false);
                _isActive = false;
            }
        }
    }
}
