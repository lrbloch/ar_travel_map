using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenUrl : MonoBehaviour
{
    private static string url;

    public void Open()
    {
        Application.OpenURL(url);
    }

    public static void SetUrl(string newURL)
    {
        url = newURL;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
