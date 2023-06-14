using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialoguePopup : MonoBehaviour
{
    [SerializeField] private string[] mockingText;
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private float dialogueDuration;
    private float _dialogueTimer = 0;

    private void Update()
    {
        if (dialogueBox.activeSelf)
        {
            _dialogueTimer += Time.deltaTime;
            if (_dialogueTimer >= dialogueDuration)
            {
                dialogueBox.SetActive(false);
                _dialogueTimer = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int randomValue = UnityEngine.Random.Range(0, 10);
            
            //Adjust how often the owl is mocking the player
            if (randomValue < 4)
            {
                int randomMockingText = UnityEngine.Random.Range(0, mockingText.Length);
                dialogueText.text = mockingText[randomMockingText];
                dialogueBox.SetActive(true);
            }
        }
    }
}
