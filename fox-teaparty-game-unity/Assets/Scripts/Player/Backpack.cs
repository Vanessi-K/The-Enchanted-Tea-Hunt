using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backpack : MonoBehaviour
{
    [SerializeField] private int size;
    private List<Collectible> _collectibles = new List<Collectible>();

    public bool BackpackHasSpace
    {
        get { return _collectibles.Count < size; }
    }
    
    public bool AddCollectible(Collectible collectible)
    {
        if (!BackpackHasSpace) return false;
        _collectibles.Add(collectible);
        return true;
    }
    
    public bool RemoveCollectible(Collectible collectible)
    {
        if (!_collectibles.Contains(collectible)) return false;
        _collectibles.Remove(collectible);
        return true;
    }
    
    public bool RemoveAllCollectibles()
    {
        if (_collectibles.Count == 0) return false;
        _collectibles.Clear();
        return true;
    }
}
