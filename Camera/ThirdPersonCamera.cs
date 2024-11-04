using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class ThirdPersonCamera : CameraController
{
    public Transform player;
    public Vector2 Sensitivity = new Vector2(2, -1);
    public GameObject aimPoint;

    Vector3 CurrentAngel = new Vector3(0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        type = CameraType.ThridPerson;
    }
    // Update is called once per frame
    void Update()
    {       
        transform.position = player.position;
        if (GlobalMng.ins.worldState != GlobalMng.WorldState.Menu)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            aimPoint.SetActive(true);
            CurrentAngel.x += Sensitivity.x * Input.GetAxis("Mouse X");
            CurrentAngel.y += Sensitivity.y * Input.GetAxis("Mouse Y");
            transform.rotation = Quaternion.Euler(CurrentAngel.y, CurrentAngel.x, 0);
        }

        else
        {
            aimPoint.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    void OnGUI()
    {
        transform.position = player.position;
    }

    public override void disable()
    {
        base.disable();
        aimPoint.SetActive(false);
    }

    public override void display()
    {
        base.display();
        aimPoint.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

}
