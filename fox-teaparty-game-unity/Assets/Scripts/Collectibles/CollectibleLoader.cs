using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleLoader : MonoBehaviour
{
    [SerializeField] private string sceneLayer;
    
    void Start()
    { 
        GameStats.Instance.LoadSceneCollectibles(sceneLayer);
    }
}
