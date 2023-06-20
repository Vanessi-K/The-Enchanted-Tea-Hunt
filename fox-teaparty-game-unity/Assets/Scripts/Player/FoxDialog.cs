using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxDialog : MonoBehaviour
{
    public void FoxSounds()
    {
        AkSoundEngine.PostEvent("Play_fox", gameObject);
    }
}
