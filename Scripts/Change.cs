using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Change : MonoBehaviour
{
    public Canvas canvas1;
    public Canvas canvas2;

    // Start is called before the first frame update
    void Update()
    {
        if (ARAVRInput.GetDown(ARAVRInput.Button.Two))
        {
            SceneChange();
        }
        if (ARAVRInput.GetDown(ARAVRInput.Button.One))
        {
            canvas1.enabled = false;
            canvas2.enabled = true;
        }
        if (OVRInput.GetDown(OVRInput.Button.Three, OVRInput.Controller.LTouch))
        {
            canvas1.enabled = true;
            canvas2.enabled = false;
        }
    }
    public void SceneChange()
    {
        SceneManager.LoadScene("urpterrain");
    }
}
