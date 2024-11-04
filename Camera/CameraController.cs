using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera cam;
    public enum CameraType
    {
        TopView,
        ThridPerson
    }
    public CameraType type;

    // the close behavior such as set curso visible, set UI  
    public virtual void disable()
    {
        cam.gameObject.SetActive(false);
    }

    // the display behavior such as set curso visible, set UI  
    public virtual void display()
    {
        cam.gameObject.SetActive(true);
    }
}
