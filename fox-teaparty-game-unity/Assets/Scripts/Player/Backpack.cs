using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backpack : MonoBehaviour
{
    [SerializeField] private int size;
    private List<Collectible> _collectibles = new List<Collectible>();
    
    public float TotalWeight
    {
        get
        {
            float totalWeight = 0;
            foreach (Collectible collectible in _collectibles)
            {
                totalWeight += collectible.Weight;
            }
            return totalWeight;
        }
    }

    public bool BackpackHasSpace
    {
        get { return _collectibles.Count < size; }
    }
    
    public bool AddCollectible(Collectible collectible)
    {
        if (!BackpackHasSpace) return false;
        _collectibles.Add(collectible);
        collectible.CollectionState = CollectionState.InPlayerInventory;
        return true;
    }
    
    
    public bool ReturnCollectibles()
    {
        if (_collectibles.Count == 0) return false;
        foreach (Collectible collectible in _collectibles)
        {
            collectible.CollectionState = CollectionState.Returned;
        }
        _collectibles.Clear();
        return true;
    }
}
