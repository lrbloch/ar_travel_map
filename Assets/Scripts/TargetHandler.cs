using System;
using UnityEngine;
using Vuforia;


public class TargetHandler : MonoBehaviour, ITrackableEventHandler
{
    public string imageFolder;
    public string URL;
    private TrackableBehaviour mTrackableBehaviour;

    public void Start()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
    }

    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        Debug.Log("MY Trackable " + mTrackableBehaviour.TrackableName +
                  " " + mTrackableBehaviour.CurrentStatus +
                  " -- " + mTrackableBehaviour.CurrentStatusInfo);

        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED ||
            newStatus == TrackableBehaviour.Status.LIMITED)
        {
            // Show image when target is found
            LoadImage.Show(imageFolder);
            OpenUrl.SetUrl(URL);
        }
    }

    protected virtual void OnDestroy()
    {
        if (mTrackableBehaviour)
            mTrackableBehaviour.UnregisterTrackableEventHandler(this);
    }
}
