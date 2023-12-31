using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    [SerializeField] private string scene;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AkSoundEngine.PostEvent("Play_door", gameObject);
            GameStats.Instance.UnloadSceneCollectibles(SceneManager.GetActiveScene().name);
            SceneManager.LoadScene(scene);
            GameStats.Instance.LoadSceneCollectibles(scene);
            AkSoundEngine.SetState("music", scene);
            AkSoundEngine.SetState("steps", scene);
        }
    }
}
