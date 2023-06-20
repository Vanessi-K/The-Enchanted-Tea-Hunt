using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwlActions : MonoBehaviour
{
    public void OwlSounds()
    {
        AkSoundEngine.PostEvent("Play_owl", gameObject);
    }

}
