using Sirenix.Serialization;
using Unity.Services.Analytics;
using Unity.Services.Core;
using UnityEngine;

public class AnalyticsInitializer : MonoBehaviour
{

    void Start()
    {
        UnityServices.InitializeAsync();
        AnalyticsService.Instance.StartDataCollection();
    }
}
