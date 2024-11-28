using System.Collections.Generic;
using UnityEngine;

public class SpawnDisaster : MonoBehaviour
{
    public PlayerTarget playerTarget;

    public List<GameObject> disasterPrefabs = new List<GameObject>();

    private int currentIndex = -1;
    private GameObject currentDisaster;

    /// <summary>
    /// Destroy the Disaster
    /// </summary>
    private void DestroyCurrentDisaster()
    {
        if (currentDisaster != null)
        {
            Destroy(currentDisaster);
            currentDisaster = null;
        }
    }

    /// <summary>
    /// Spawn Disaster
    /// </summary>
    /// <param name="disasterIndex"></param>
    private void ToggleDisaster(int disasterIndex)
    {
        if (currentIndex == disasterIndex)
        {
            currentIndex = -1;
            DestroyCurrentDisaster();
        }
        else
        {
            currentIndex = disasterIndex;
            DestroyCurrentDisaster();
            currentDisaster = Instantiate(disasterPrefabs[disasterIndex], playerTarget.location.position, Quaternion.identity, playerTarget.location);
        }
    }


    /// <summary>
    /// Press Disaster
    /// </summary>
    public void PressDisaster(int disasterIndex)
    {
        ToggleDisaster(disasterIndex);
    }
}
