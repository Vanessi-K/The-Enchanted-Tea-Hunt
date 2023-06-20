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
                Debug.Log("Delta: " + Time.deltaTime);
                Debug.Log("Timer: " + _dialogueTimer);
                _dialogueTimer += Time.deltaTime;
                if (_dialogueTimer >= dialogueDuration)
                {
                    Debug.Log("Set inactive");
                    _dialogueTimer = 0;
                    Debug.Log("Timer in if: " + _dialogueTimer);
                    dialogueBox.SetActive(false);
                }
            }
        }

        public void ShowDialogue(string dialogueTextString)
        {
            Debug.Log("Show dialogue");
            dialogueTextField.text = dialogueTextString;
            dialogueBox.SetActive(true);
            _dialogueTimer = 0;
        }
    }
}