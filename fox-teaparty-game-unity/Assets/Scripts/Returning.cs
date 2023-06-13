using UnityEngine;
using UnityEngine.InputSystem;

public class Returning : MonoBehaviour
{
    [SerializeField] private Backpack backpack;
    [SerializeField] private Material inactiveMaterial;
    [SerializeField] private Material activeMaterial;
    [SerializeField] private Material activeAllItemsReturnedMaterial;
    [SerializeField] private Renderer displayArea;
    private bool _playerIsInside;

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
        backpack.ReturnCollectibles();
        displayArea.material = activeAllItemsReturnedMaterial;
    }
}
