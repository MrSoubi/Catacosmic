using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string levelName;

    public void PressButton()
    {
        SceneManager.LoadScene(levelName);
    }
}
