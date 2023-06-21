using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuActions : MonoBehaviour
{
    public void LoadStart()
    {
        PlayClickSound();
        SceneManager.LoadScene("Start");
    }

    public void LoadEnd()
    {
        PlayClickSound();
        SceneManager.LoadScene("End");
    }

    public void LoadStartDiary()
    {
        PlayClickSound();
        SceneManager.LoadScene("FelixDiaryStart");
    }

    public void LoadEndDiary()
    {
        SceneManager.LoadScene("FelixDiaryEnd");
    }

    public void LoadControls()
    {
        PlayClickSound();
        SceneManager.LoadScene("Controls");
    }

    public void LoadGame()
    {
        PlayClickSound();
        StartCoroutine(StopSound());
    }

    public void PlayClickSound()
    {
        AkSoundEngine.PostEvent("Play_click", gameObject);
    }

    private IEnumerator StopSound()
    {
        yield return new WaitForSeconds(0.05f);
        AkSoundEngine.StopAll();
        SceneManager.LoadScene("Forest");
    }
}