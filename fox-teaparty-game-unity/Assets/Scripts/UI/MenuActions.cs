using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuActions : MonoBehaviour
{
    public void LoadStart()
    {
        SceneManager.LoadScene("Start");
    }
    
    public void LoadEnd()
    {
        SceneManager.LoadScene("End");
    }

    public void LoadStartDiary()
    {
        SceneManager.LoadScene("FelixDiaryStart");
    }
    
    public void LoadEndDiary()
    {
        SceneManager.LoadScene("FelixDiaryEnd");
    }
    
    public void LoadControls()
    {
        SceneManager.LoadScene("Controls");
    }
    
    public void LoadGame()
    {
        SceneManager.LoadScene("Forest");
    }
}
