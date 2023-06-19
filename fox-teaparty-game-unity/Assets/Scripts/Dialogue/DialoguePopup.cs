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
    [SerializeField] private bool startOnCollision;
    private float _dialogueTimer = 0;

    private void Update()
    {
        Debug.Log(_dialogueTimer + " " + dialogueBox.activeSelf);
        
        if (dialogueBox.activeInHierarchy)
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
        if (startOnCollision)
        {
            if (other.CompareTag("Player"))
            {
                showDialogue();
            }
        }
    }
    
    public void showDialogue()
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
