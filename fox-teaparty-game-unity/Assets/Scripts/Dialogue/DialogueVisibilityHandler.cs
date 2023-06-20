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

        private void Update()
        {
            if (dialogueBox.activeSelf)
            {
                _dialogueTimer += Time.deltaTime;
                if (_dialogueTimer >= dialogueDuration)
                {
                    _dialogueTimer = 0;
                    dialogueBox.SetActive(false);
                }
            }
        }

        public void ShowDialogue(string dialogueTextString)
        {
            dialogueTextField.text = dialogueTextString;
            dialogueBox.SetActive(true);
        }
    }
}