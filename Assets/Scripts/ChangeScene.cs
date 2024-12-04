using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [Header("Scenes Name")]
    [SerializeField, Tooltip("Scene Name 1")] ScenesNameEnum levelName1;
    [SerializeField, Tooltip("Scene Name 2")] ScenesNameEnum levelName2;

    /// <summary>
    /// Scene 1
    /// </summary>
    public void PressButton1()
    {
        SceneManager.LoadScene(levelName1.ToString());
    }

    /// <summary>
    /// Scene 2
    /// </summary>
    public void PressButton2()
    {
        SceneManager.LoadScene(levelName2.ToString());
    }
}
