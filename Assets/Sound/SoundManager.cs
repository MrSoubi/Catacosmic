using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    public RSE_BuildingDestroyed buildingDestroyed;

    private void OnEnable()
    {
        buildingDestroyed.Fire += PlayBuildingDemolition;
    }

    private void OnDisable()
    {
        buildingDestroyed.Fire -= PlayBuildingDemolition;
    }

    private void PlayBuildingDemolition()
    {
        audioSource.Play();
    }
}
