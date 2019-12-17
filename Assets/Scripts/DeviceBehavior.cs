using UnityEngine;
using System.Collections;
using Vuforia;

public class DeviceBehavior : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        CameraDevice.Instance.SetFlashTorchMode(true);
    }

    // Update is called once per frame
    void Update()
    {
        CameraDevice.Instance.SetFlashTorchMode(true);
    }
}
