using TMPro;
using UnityEngine;

public class EndSceneManager : MonoBehaviour
{
    [SerializeField] private TMP_Text endTimer;

    private void Start()
    {
        endTimer.text = GameStats.Instance.GetTime();
    }
}
