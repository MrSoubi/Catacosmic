using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] UIDocument gameUI;
    [SerializeField] UIDocument giftsPopUp;
    [SerializeField] UIDocument fortuneWheelPopUp;
    [SerializeField] UIDocument questsPopUp;

    [Header("Input Events")]
    public RSE_CallFortuneWheelPopUp callFortuneWheelPopUp;
    public RSE_CallGiftPopUp callGiftPopUp;
    public RSE_CallQuestsPopUp callQuestsPopUp;

    private void OnEnable()
    {
        callQuestsPopUp.Fire += OnQuestsPopUpCalled;
        callFortuneWheelPopUp.Fire += OnFortuneWheelPopUpCalled;
        callGiftPopUp.Fire += OnGiftPopUpCalled;
    }

    private void OnDisable()
    {
        callQuestsPopUp.Fire -= OnQuestsPopUpCalled;
        callFortuneWheelPopUp.Fire -= OnFortuneWheelPopUpCalled;
        callGiftPopUp.Fire -= OnGiftPopUpCalled;
    }

    void OnGiftPopUpCalled()
    {
        giftsPopUp.enabled = true;
    }

    void OnFortuneWheelPopUpCalled()
    {
        fortuneWheelPopUp.enabled = true;
    }

    void OnQuestsPopUpCalled()
    {
        questsPopUp.enabled = true;
    }
}