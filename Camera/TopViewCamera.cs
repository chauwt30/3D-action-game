using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopViewCamera : CameraController
{
    public Transform player;
    
    // Start is called before the first frame update
    void Start()
    {
        type = CameraType.TopView;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = player.position;
    }

    public override void display()
    {
        base.display();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
