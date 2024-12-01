using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string levelName1;
    public string levelName2;

    /// <summary>
    /// Scene 1
    /// </summary>
    public void PressButton1()
    {
        SceneManager.LoadScene(levelName1);
    }

    /// <summary>
    /// Scene 2
    /// </summary>
    public void PressButton2()
    {
        SceneManager.LoadScene(levelName2);
    }
}
