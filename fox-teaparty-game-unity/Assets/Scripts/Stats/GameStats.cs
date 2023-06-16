using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum CollectibleType
{
    Teapot,
    Cupcake,
    Teacup,
    Spoon,
}

public enum CollectionState 
{
    NotCollected,
    InPlayerInventory,
    Returned
}

public class GameStats : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    public static GameStats Instance;
    private Backpack _backpack;
    private float _timer;

    public Backpack Backpack
    {
        get { return _backpack; }
        private set { _backpack = value; }
    }
    
    private void Awake()
    {
        if (Instance == null)
        {
            GameStats.Instance = this;
            _backpack = GetComponent<Backpack>();
            _timer = 0;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void Update()
    {
        _timer += Time.deltaTime;
        int minutes = Mathf.FloorToInt(_timer / 60);
        int seconds = Mathf.FloorToInt( _timer - minutes * 60);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }

    public void LoadSceneCollectibles(string sceneLayer)
    {
        List<GameObject> collectibles = GetObjectsInLayer(gameObject, LayerMask.NameToLayer(sceneLayer));
        foreach (GameObject collectible in collectibles)
        {
            if (collectible.GetComponent<CollectionStateManager>() != null)
            {
                if(collectible.GetComponent<CollectionStateManager>().State == CollectionState.NotCollected)
                    collectible.SetActive(true);
            } 
        }
    }
    
    public void UnloadSceneCollectibles(string sceneLayer)
    {
        List<GameObject> collectibles = GetObjectsInLayer(gameObject, LayerMask.NameToLayer(sceneLayer));
        foreach (GameObject collectible in collectibles)
        {
            collectible.SetActive(false);
        }
    }
    
    private List<GameObject> GetObjectsInLayer(GameObject root, int layer)
    {
        var objectsInLayer = new List<GameObject>();
        foreach (Transform t in root.GetComponentsInChildren<Transform>(true) )
        {
            if (t.gameObject.layer == layer)
            {
                objectsInLayer.Add(t.gameObject);
            }
        }
        return objectsInLayer;        
    }

    public List<GameObject> Collectibles()
    {
        List<GameObject> collectibles = new List<GameObject>();
        foreach (Collectible collectible in this.GetComponentsInChildren<Collectible>(true))
        {  
            collectibles.Add(collectible.gameObject);
            
        }
        return collectibles;
    }
    
    public List<GameObject> Collectibles(CollectibleType collectibleType)
    {
        List<GameObject> collectibles = new List<GameObject>();
        foreach (Collectible collectible in this.GetComponentsInChildren<Collectible>(true))
        {
            if (collectible.Type == collectibleType)
            {
                collectibles.Add(collectible.gameObject);
            }
        }
        return collectibles;
    }
    
    public int NumberOfCollectibles()
    {
        return Collectibles().Count;
    }
    
    public int NumberOfCollectibles(CollectibleType collectibleType)
    {
        return Collectibles(collectibleType).Count;
    }
    
    public int NumberOfCollectibles(CollectibleType collectibleType, CollectionState[] states)
    {
        return Collectibles(collectibleType).Count(collectible => states.Contains(collectible.GetComponent<CollectionStateManager>().State));
    }
    
    public int NumberOfCollectibles(CollectionState[] states)
    {
        return Collectibles().Count(collectible => states.Contains(collectible.GetComponent<CollectionStateManager>().State));
    }
}