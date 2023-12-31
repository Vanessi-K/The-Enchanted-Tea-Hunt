using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inactivate : MonoBehaviour
{
    private void OnClose(InputValue inputValue)
    {
        gameObject.SetActive(false);
        GameStats.Instance.IsPaused = false;
        AkSoundEngine.PostEvent("Play_diary_close", gameObject);
    }
}
