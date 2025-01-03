using UnityEngine;
using UnityEngine.UIElements;
using Sirenix.OdinInspector;
using UnityEngine.SceneManagement;
using System.Collections;

public class RUI_Challenge : MonoBehaviour
{
    private Button buttonPlanet1;
    private Button buttonPlanet2;
    private Button buttonPlanet3;
    private Button buttonPlanet4;
    private Button buttonPlanet5;

    private AsyncOperation ao;

    private void OnEnable()
    {
        var uiDocument = GetComponent<UIDocument>();

        buttonPlanet1 = uiDocument.rootVisualElement.Q("Button_Planet1") as Button;
        buttonPlanet2 = uiDocument.rootVisualElement.Q("Button_Planet2") as Button;
        buttonPlanet3 = uiDocument.rootVisualElement.Q("Button_Planet3") as Button;
        buttonPlanet4 = uiDocument.rootVisualElement.Q("Button_Planet4") as Button;
        buttonPlanet5 = uiDocument.rootVisualElement.Q("Button_Planet5") as Button;

        buttonPlanet1.RegisterCallback<ClickEvent>(CallPlanet1);
        buttonPlanet2.RegisterCallback<ClickEvent>(CallPlanet2);
        buttonPlanet3.RegisterCallback<ClickEvent>(CallPlanet3);
        buttonPlanet4.RegisterCallback<ClickEvent>(CallPlanet4);
        buttonPlanet5.RegisterCallback<ClickEvent>(CallPlanet5);
    }

    private void OnDisable()
    {
        buttonPlanet1.UnregisterCallback<ClickEvent>(CallPlanet1);
        buttonPlanet2.UnregisterCallback<ClickEvent>(CallPlanet2);
        buttonPlanet3.UnregisterCallback<ClickEvent>(CallPlanet3);
        buttonPlanet4.UnregisterCallback<ClickEvent>(CallPlanet4);
        buttonPlanet5.UnregisterCallback<ClickEvent>(CallPlanet5);
    }

    private IEnumerator ChangeLevel()
    {
        yield return new WaitForSecondsRealtime(0.2f);

        ao.allowSceneActivation = true;
    }

    private void CallPlanet1(ClickEvent clickEvent)
    {
        ao = SceneManager.LoadSceneAsync("Killian_FreePress");
        ao.allowSceneActivation = false;

        StartCoroutine(ChangeLevel());
    }

    private void CallPlanet2(ClickEvent clickEvent)
    {
        ao = SceneManager.LoadSceneAsync("Killian_FreePress");
        ao.allowSceneActivation = false;

        StartCoroutine(ChangeLevel());
    }

    private void CallPlanet3(ClickEvent clickEvent)
    {
        ao = SceneManager.LoadSceneAsync("Killian_FreePress");
        ao.allowSceneActivation = false;

        StartCoroutine(ChangeLevel());
    }

    private void CallPlanet4(ClickEvent clickEvent)
    {
        ao = SceneManager.LoadSceneAsync("Killian_FreePress");
        ao.allowSceneActivation = false;

        StartCoroutine(ChangeLevel());
    }

    private void CallPlanet5(ClickEvent clickEvent)
    {
        ao = SceneManager.LoadSceneAsync("Killian_FreePress");
        ao.allowSceneActivation = false;

        StartCoroutine(ChangeLevel());
    }
}
