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
    
    public void LoadControls()
    {
        SceneManager.LoadScene("Controls");
    }
    
    public void LoadStory()
    {
        SceneManager.LoadScene("Story");
    }
    
    public void LoadLeaderboard()
    {
        SceneManager.LoadScene("Leaderboard");
    }
    
    public void LoadGame()
    {
        SceneManager.LoadScene("Forest");
    }
}
