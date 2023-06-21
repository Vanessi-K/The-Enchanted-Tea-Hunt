using System;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CollectionStateManager))]
public class Collectible : MonoBehaviour
{
    [SerializeField] public float Weight;
    [SerializeField] public CollectibleType Type;
    [SerializeField] public GameObject TableRepresentation;
    private Backpack _backpack;

    private void Start()
    {
        _backpack = GameStats.Instance.Backpack;
        TableRepresentation.SetActive(false);


        if (gameObject.activeSelf)
        {
            AkSoundEngine.PostEvent("Play_shimmer", gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (_backpack.BackpackHasSpace)
            {
                _backpack.AddCollectible(this);
                AkSoundEngine.PostEvent("Play_pickUp", gameObject);
                gameObject.SetActive(false);
            }
        }
    }

    private void OnEnable()
    {
        AkSoundEngine.PostEvent("Play_shimmer", gameObject);
    }

    private void OnDisable()
    {
        AkSoundEngine.PostEvent("Stop_shimmer", gameObject);
        AkSoundEngine.PostEvent("Stop_steps", gameObject);
        AkSoundEngine.PostEvent("Stop_shouts", gameObject);
    }
}