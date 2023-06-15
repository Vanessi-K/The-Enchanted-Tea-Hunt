using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class DialoguePopup : MonoBehaviour
{
    [TextAreaAttribute]
    [SerializeField] private string[] dialogueText;
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text dialogueTextField;
    [SerializeField] private float dialogueDuration;
    [SerializeField] private int probabilityOfShowingDialogue; 
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
            int randomValue = UnityEngine.Random.Range(0, 100);
            
            if (randomValue <= probabilityOfShowingDialogue)
            {
                int randomDialogueText = UnityEngine.Random.Range(0, dialogueText.Length);
                dialogueTextField.text = dialogueText[randomDialogueText];
                dialogueBox.SetActive(true);
            }
        }
    }
}
