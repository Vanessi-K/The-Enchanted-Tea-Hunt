using TMPro;
using UnityEngine;

namespace Dialogue
{
    public class DialogueVisibilityHandler : MonoBehaviour
    {
        [SerializeField] private GameObject dialogueBox;
        [SerializeField] private TMP_Text dialogueTextField;
        [SerializeField] private float dialogueDuration;
        private float _dialogueTimer = 0;
        private static DialogueVisibilityHandler OPEN_DIALOGUE = null;

        private void Update()
        {
            if (dialogueBox.activeSelf)
            {
                _dialogueTimer += Time.deltaTime;
                if (_dialogueTimer >= dialogueDuration)
                {
                    _dialogueTimer = 0;
                    OPEN_DIALOGUE = null;
                    dialogueBox.SetActive(false);
                }
            }
        }

        public void ShowDialogue(string dialogueTextString)
        {
            if (OPEN_DIALOGUE != null)
            {
                OPEN_DIALOGUE.CloseDialogue();
                OPEN_DIALOGUE = null;
            }
                
            OPEN_DIALOGUE = this;
            dialogueTextField.text = dialogueTextString;
            dialogueBox.SetActive(true);
        }
        
        public void CloseDialogue()
        {
            _dialogueTimer = 0;
            dialogueBox.SetActive(false);
        }
    }
}