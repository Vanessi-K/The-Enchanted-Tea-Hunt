using System;
using UnityEngine;
public class Collectible : MonoBehaviour
{
    [SerializeField] public float Weight;
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
