using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxDialog : MonoBehaviour
{
    public void FoxSounds()
    {
        Debug.Log("Fox sounds");
        AkSoundEngine.PostEvent("Play_fox", gameObject);
    }
}
