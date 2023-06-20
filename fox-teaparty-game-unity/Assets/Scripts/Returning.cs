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
    private PlayerMovement _playerMovement;
    public static bool FIRST_TIME = true;
    
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
            _playerMovement = other.GetComponent<PlayerMovement>();
            displayArea.material = activeMaterial;

            if (FIRST_TIME)
            {
                gameObject.GetComponent<DialoguePopup>().ShowDialogue();
                FIRST_TIME = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerIsInside = false;
            displayArea.material = inactiveMaterial;
            _playerMovement = null;
        }
    }
    
    private void OnReturn(InputValue value)
    {
        if (!_playerIsInside) return;
        
        if (_backpack.ReturnCollectibles())
        {
            displayArea.material = activeAllItemsReturnedMaterial;
        }
        

        if (_totalCollectibles == GameStats.Instance.NumberOfCollectibles(_collectibleStates))
        {
            _playerMovement.Celebrate();
            _playerMovement.enabled = false;
            GameStats.Instance.IsPaused = true;
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
