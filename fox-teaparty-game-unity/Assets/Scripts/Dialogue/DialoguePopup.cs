using System;
using System.Collections;
using System.Collections.Generic;
using Dialogue;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Events;

public class DialoguePopup : MonoBehaviour
{
    [TextAreaAttribute]
    [SerializeField] private string[] dialogueText;
    [SerializeField] private DialogueVisibilityHandler dialogue;
    [SerializeField] private int probabilityOfShowingDialogue; 
    [SerializeField] private bool startOnCollision;
    [SerializeField] private UnityEvent onDialogueShown;
    
    private void OnTriggerEnter(Collider other)
    {
        if (startOnCollision)
        {
            if (other.CompareTag("Player"))
            {
                ShowDialogue();
            }
        }
    }
    
    public void ShowDialogue()
    {
        int randomValue = UnityEngine.Random.Range(0, 100);
            
        if (randomValue <= probabilityOfShowingDialogue)
        {
            int randomDialogueText = UnityEngine.Random.Range(0, dialogueText.Length);
            dialogue.ShowDialogue(dialogueText[randomDialogueText]);
            onDialogueShown?.Invoke();
        }
    }
}
