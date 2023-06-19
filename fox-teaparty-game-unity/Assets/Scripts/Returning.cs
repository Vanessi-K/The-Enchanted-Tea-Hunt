using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Returning : MonoBehaviour
{
    private Backpack _backpack;
    [SerializeField] private Material inactiveMaterial;
    [SerializeField] private Material activeMaterial;
    [SerializeField] private Material activeAllItemsReturnedMaterial;
    [SerializeField] private Renderer displayArea;
    [SerializeField] private GameObject celebrationConfetti;
    private bool _playerIsInside;
    private int _totalCollectibles;
    private CollectionState[] _collectibleStates;
    
    private void Start()
    {
        _totalCollectibles = GameStats.Instance.NumberOfCollectibles();
        _backpack = GameStats.Instance.Backpack;
        _collectibleStates = new CollectionState[1];
        _collectibleStates[0] = CollectionState.Returned;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerIsInside = true;
            displayArea.material = activeMaterial;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerIsInside = false;
            displayArea.material = inactiveMaterial;
        }
    }
    
    private void OnReturn(InputValue value)
    {
        if (!_playerIsInside) return;
        _backpack.ReturnCollectibles();
        displayArea.material = activeAllItemsReturnedMaterial;

        if (_totalCollectibles == GameStats.Instance.NumberOfCollectibles(_collectibleStates))
        {
            celebrationConfetti.SetActive(true);
            StartCoroutine(WaitForEndSceneLoad());
        }
    }
    
    private IEnumerator WaitForEndSceneLoad()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("End");
    }
}
