using System;
using System.Collections;
using Source.GwizdBackend;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UIElements;

public class PhotoController : MonoBehaviour
{
    private UIDocument document;
    private Button tookPhotoBtn;
    // Start is called before the first frame update
    void Awake()
    {
        document = GetComponent<UIDocument>();
        tookPhotoBtn = document.rootVisualElement.Q<Button>();
        tookPhotoBtn.clicked += OnPhotoClicked;
        if (!Permission.HasUserAuthorizedPermission("android.permission.ACCESS_FINE_LOCATION"))
        {
            Permission.RequestUserPermission("android.permission.ACCESS_FINE_LOCATION");
        }
    }

    private void OnPhotoClicked()
    {
        StartCoroutine(TookPhotoOnClicked());
    }

    private IEnumerator TookPhotoOnClicked()
    {
#if UNITY_EDITOR
        Texture2D tex = Resources.Load<Texture2D>("Test");
#else
        ToggleUI(false);
        yield return new WaitForEndOfFrame();
        Texture2D tex = ScreenCapture.CaptureScreenshotAsTexture(ScreenCapture.StereoScreenCaptureMode.BothEyes);
        ToggleUI(true);
#endif

        byte[] photo = tex.EncodeToJPG(75);

        AnimalReport animalReport = new AnimalReport();
        animalReport.AnimalId = "test";
        yield return GetLocation(animalReport);

        animalReport.PhotoBase64 = Convert.ToBase64String(photo);

        GwizdBackendService.SendAnimalReport(animalReport);
    }

    private IEnumerator GetLocation(AnimalReport animalReport)
    {
        // Check if the user has location service enabled.
        if (!Input.location.isEnabledByUser)
            Debug.Log("Location not enabled on device or app does not have permission to access location");

        // Starts the location service.
        Input.location.Start(5, 5);

        // Waits until the location service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // If the service didn't initialize in 20 seconds this cancels location service use.
        if (maxWait < 1)
        {
            Debug.Log("Timed out");
            yield break;
        }

        // If the connection failed this cancels location service use.
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.LogError("Unable to determine device location");
            yield break;
        }
        else
        {
            // If the connection succeeded, this retrieves the device's current location and displays it in the Console window.
            Debug.Log("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
        }

        animalReport.Latitude = Input.location.lastData.latitude;
        animalReport.Longitude = Input.location.lastData.longitude;

        // Stops the location service if there is no need to query location updates continuously.
        Input.location.Stop();
    }

    private void ToggleUI(bool value)
    {
        document.rootVisualElement.visible = value;
    }

    private void OnDestroy()
    {
        Input.location.Stop();
    }
}
