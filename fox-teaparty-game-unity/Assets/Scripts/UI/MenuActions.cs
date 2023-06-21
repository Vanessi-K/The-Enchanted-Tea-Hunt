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
        SceneManager.LoadScene("End");
    }

    public void LoadStartDiary()
    {
        PlayClickSound();
        SceneManager.LoadScene("FelixDiaryStart");
    }
    
    public void LoadEndDiary()
    {
        AkSoundEngine.StopAll();
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
        AkSoundEngine.StopAll();
        SceneManager.LoadScene("Forest");
    }
    
    public void PlayClickSound()
    {
        AkSoundEngine.PostEvent("Play_click", gameObject);
    }
}
