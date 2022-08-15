using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Analytics;

public class analytics : MonoBehaviour
{
    async void Start()
    {
        try
        {
            await UnityServices.InitializeAsync();
            List<string> consentIdentifiers = await AnalyticsService.Instance.CheckForRequiredConsents();
            Debug.Log("cargoAnalitycs");
        }
        catch (ConsentCheckException e)
        {
            Debug.LogError(e.Message);
            // Something went wrong when checking the GeoIP, check the e.Reason and handle appropriately
        }
    }
}
