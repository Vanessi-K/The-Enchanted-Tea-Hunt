using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backpack : MonoBehaviour
{
    [SerializeField] private int size;
    private SpeedBoost _speedBoost = null;
    private bool _activeSpeedBoost = false;
    private float _speedBoostTimer = 0;
    private List<Collectible> _collectibles = new List<Collectible>();

    private void Update()
    {
        if (_activeSpeedBoost)
        {
            _speedBoostTimer += Time.deltaTime;
            
            if(_speedBoostTimer >= _speedBoost.SpeedBoostDuration)
            {
                _activeSpeedBoost = false;
                _speedBoostTimer = 0;
                _speedBoost.GetComponent<CollectionStateManager>().State = CollectionState.Returned;
                _speedBoost = null;
            }
            
        }
    }
    
    public float SpeedBoost
    {
        get {
            if (_activeSpeedBoost)
            {
                return _speedBoost.SpeedBoostPower;
            }

            return 1;
        }
    }

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
    
    public bool HasSpeedBoost
    {
        get { return _speedBoost != null; }
    }
    
    public bool AddCollectible(Collectible collectible)
    {
        if (!BackpackHasSpace) return false;
        _collectibles.Add(collectible);
        collectible.GetComponent<CollectionStateManager>().State = CollectionState.InPlayerInventory;
        return true;
    }

    public bool ReturnCollectibles()
    {
        if (_collectibles.Count == 0) return false;
        foreach (Collectible collectible in _collectibles)
        {
            collectible.GetComponent<CollectionStateManager>().State = CollectionState.Returned;
        }
        _collectibles.Clear();
        return true;
    }
    
    public bool AddSpeedBoost(SpeedBoost speedBoost)
    {
        if (HasSpeedBoost) return false;
        _speedBoost = speedBoost;
        _speedBoost.GetComponent<CollectionStateManager>().State = CollectionState.InPlayerInventory;
        return true;
    }
    
    public bool UseSpeedBoost()
    {
        if (!HasSpeedBoost) return false;
        _activeSpeedBoost = true;
        return true;
    }
}
