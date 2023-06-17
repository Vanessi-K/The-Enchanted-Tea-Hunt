using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class IconToCollectibleType
{
    [SerializeField] private Sprite sprites;
    [SerializeField] private CollectibleType types;

    public Sprite Sprites
    {
        get => sprites;
        private set => sprites = value;
    }
    
    public CollectibleType Types
    {
        get => types;
        private set => types = value;
    }
}

public class Backpack : MonoBehaviour
{
    [SerializeField] private int size;
    [SerializeField] private GameObject slot;
    [SerializeField] private GameObject panel;
    [SerializeField] private Image speedBoostImage;
    [SerializeField] private IconToCollectibleType[] iconToCollectibleTypes;
    private SpeedBoost _speedBoost = null;
    private bool _activeSpeedBoost = false;
    private float _speedBoostTimer = 0;
    private List<Collectible> _collectibles = new List<Collectible>();
    private GameObject[] _slots;
    
    private void Awake()
    {
        _slots = new GameObject[size];
        for(int i = 0; i < size; i++)
        {
            GameObject slotInstance = Instantiate(slot, panel.transform);
            slotInstance.SetActive(true);
            _slots[i] = slotInstance;
        }
    }

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
                speedBoostImage.gameObject.SetActive(false);
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
        _slots[_collectibles.Count - 1].GetComponentInChildren<RectTransform>().GetChild(0).gameObject.SetActive(true);
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

        foreach (GameObject item in _slots)
        {
            item.GetComponentInChildren<RectTransform>().GetChild(0).gameObject.SetActive(false);
        }
        
        return true;
    }
    
    public bool AddSpeedBoost(SpeedBoost speedBoost)
    {
        if (HasSpeedBoost) return false;
        _speedBoost = speedBoost;
        _speedBoost.GetComponent<CollectionStateManager>().State = CollectionState.InPlayerInventory;
        speedBoostImage.gameObject.SetActive(true);
        return true;
    }
    
    public bool UseSpeedBoost()
    {
        if (!HasSpeedBoost) return false;
        _activeSpeedBoost = true;
        return true;
    }
}
