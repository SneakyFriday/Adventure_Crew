using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class URLDirecter : MonoBehaviour
{
    public string socialMediaURL;

    public void OpenSocialMediaProfile()
    {
        Application.OpenURL(socialMediaURL);
    }
}
