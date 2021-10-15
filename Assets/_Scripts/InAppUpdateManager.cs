using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Google.Play.AppUpdate;
using Google.Play.Common;

public class InAppUpdateManager : MonoBehaviour
{

    // bundle version code in build is important

    [SerializeField] private Text inAppStatus;

    AppUpdateManager appUpdateManager;

    // Start is called before the first frame update
    void Start()
    {
        appUpdateManager = new AppUpdateManager();
        StartCoroutine(CheckForUpdate());
    }


    private IEnumerator CheckForUpdate()
    {
        PlayAsyncOperation<AppUpdateInfo, AppUpdateErrorCode> appUpdateInfoOperation = appUpdateManager.GetAppUpdateInfo();

        // until it the async operation completes
        yield return appUpdateInfoOperation;

        if (appUpdateInfoOperation.IsSuccessful)
        {
            var appUpdateInfoResult = appUpdateInfoOperation.GetResult();
            // check appupdateinfo's UpdateAvailability, UpdatePriority,
            // IsUpdateTypeAllowed() etc, and decide whether to ask the user 
            // to start an in-app update

            // display if there is an update or not
            if (appUpdateInfoResult.UpdateAvailability == UpdateAvailability.UpdateAvailable)
            {
                inAppStatus.text = UpdateAvailability.UpdateAvailable.ToString();
            }
            else
            {
                inAppStatus.text = "No Update Available";
            }

            // create an AppUpdateOptions defining an immediate in-app
            // update flow and its parameters.

            var appUpdateOptions = AppUpdateOptions.ImmediateAppUpdateOptions();
            StartCoroutine(StartImmediateUpdate(appUpdateInfoResult, appUpdateOptions));
        }
    }


    private IEnumerator StartImmediateUpdate(AppUpdateInfo appupdateInfoOp_i, AppUpdateOptions appUpdateOptions_i)
    {
        // Creates an AppUpdateRequest that can be used to monitor the 
        // requested in-app update flow
        var startUpdateRequest = appUpdateManager.StartUpdate(
            // the result returned by PlayAsyncOperation.GetResult().
            appupdateInfoOp_i,
                // The AppUpdateOptions created defining the requested in-app update
                // and its parameteres.
                appUpdateOptions_i
            );
        yield return startUpdateRequest;

        // If the update completes successfully, then the app restarts and this line
        // is never reached. If this line is reached, then handle the failure (for 
        // example, by logging result.error or by displaying a message to the user).
    }

}
