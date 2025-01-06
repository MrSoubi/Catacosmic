using UnityEngine;
using UnityEngine.UIElements;
using Sirenix.OdinInspector;
using System.Collections;

public class RUI_Wheel : MonoBehaviour
{
    [Title("Output Events")]
    public RSE_ShutFortuneWheelPopUp shutFortuneWheelPopUp;

    [Title("Parameters")]
    [SerializeField] private int numberTurn;
    [SerializeField, SuffixLabel("degres/s")] private float rotationSpeed;

    private VisualElement wheel;

    private Button buttonBack;
    private Button buttonLaunch;

    private float rotationAngle;

    private void OnEnable()
    {
        var uiDocument = GetComponent<UIDocument>();

        buttonBack = uiDocument.rootVisualElement.Q("Button_Back") as Button;
        buttonLaunch = uiDocument.rootVisualElement.Q("Button_Launch") as Button;
        wheel = uiDocument.rootVisualElement.Q("Wheel") as VisualElement;

        buttonBack.RegisterCallback<ClickEvent>(CallButtonShut);
        buttonLaunch.RegisterCallback<ClickEvent>(CallButtonLaunch);
    }

    private void OnDisable()
    {
        buttonBack.UnregisterCallback<ClickEvent>(CallButtonShut);
        buttonLaunch.UnregisterCallback<ClickEvent>(CallButtonLaunch);
    }

    private void CallButtonShut(ClickEvent clickEvent)
    {
        shutFortuneWheelPopUp.Fire?.Invoke();
    }
    private void CallButtonLaunch(ClickEvent clickEvent)
    {
        StartCoroutine(RotateWheel());
    }

    private IEnumerator RotateWheel()
    {
        int step = 15;
        int maxSteps = 360 / step;
        int randomStep = Random.Range(0, maxSteps + 1);
        int randomNumber = randomStep * step;

        float totalRotation = rotationAngle + 360f * numberTurn + randomNumber;

        wheel.transform.rotation = Quaternion.Euler(0, 0, 0);

        while (rotationAngle < totalRotation)
        {
            float deltaRotation = rotationSpeed * Time.deltaTime;

            if (rotationAngle + deltaRotation > totalRotation)
            {
                deltaRotation = totalRotation - rotationAngle;
            }

            rotationAngle += deltaRotation;
            wheel.transform.rotation = Quaternion.Euler(0, 0, rotationAngle);

            yield return null;
        }
    }
}
