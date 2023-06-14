using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

public class GameStats : MonoBehaviour
{
    public static GameStats Instance;
    private Backpack _backpack;
    public Backpack Backpack
    {
        get { return _backpack; }
        private set { _backpack = value; }
    }
    
    void Awake()
    {
        if (Instance == null)
        {
            GameStats.Instance = this;
            _backpack = GetComponent<Backpack>();
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadSceneCollectibles(string sceneLayer)
    {
        List<GameObject> collectibles = GetObjectsInLayer(gameObject, LayerMask.NameToLayer(sceneLayer));
        foreach (GameObject collectible in collectibles)
        {
            if(collectible.GetComponent<Collectible>().CollectionState == CollectionState.NotCollected)
                collectible.SetActive(true);
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
}