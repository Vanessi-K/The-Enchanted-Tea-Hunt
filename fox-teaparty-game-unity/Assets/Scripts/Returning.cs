using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Returning : MonoBehaviour
{
    private Backpack _backpack;
    [SerializeField] private Material inactiveMaterial;
    [SerializeField] private Material activeMaterial;
    [SerializeField] private Material activeAllItemsReturnedMaterial;
    [SerializeField] private Renderer displayArea;
    private bool _playerIsInside;

    private void Start()
    {
        _backpack = GameStats.Instance.Backpack;
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
    }
}
