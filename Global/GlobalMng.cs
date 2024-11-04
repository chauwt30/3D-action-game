using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GlobalMng : Singleton<GlobalMng>
{
    //Global Data
    [Header("Global data")]
    public Player player;
    [HideInInspector]
    public Mob SelectedMob;
    [HideInInspector]
    public Mob ContactMob;

    // Debug text 
    [HideInInspector]
    public Dictionary<string, string> debugText = new Dictionary<string, string>();
    [Space]
    [Header("UI")]
    public TextMeshProUGUI debugTextUI;
    public TextMeshProUGUI errorMsgUI;
    public DeathMenu deathMenu;

    public GameObject SelectRing;    // Target Selection

    // Camera list
    [Space]
    [Header("Camera")]
    public List<CameraController> cams;

    [HideInInspector]
    public CameraController currCam;
    [HideInInspector]
    public int currCamIndex;

    public enum WorldState {
        Game,
        Menu
    }

    [HideInInspector]
    public WorldState worldState = WorldState.Game;
    [HideInInspector]
    public string currMenu = "";

    // Mob List
    [HideInInspector]
    public int mobNum=0;
    public void SelectMob(Mob mob)
    {
        Debug.Log("Selecting mob : " + mob.MobName);
        SelectedMob = mob;
        SelectRing.transform.position = mob.transform.position;
        SelectRing.transform.SetParent(mob.transform);
    }

    // function 
    public void setDebugText(string text)
    {
        debugTextUI.text = text;
    }

    public void changeCamera()
    {
        if (currCamIndex + 1 >= cams.Capacity)
            currCamIndex = 0;
        else
            currCamIndex++;

        currCam.disable();
        currCam = cams[currCamIndex];
        currCam.display();

        setDebugText("Camera Mode", currCam.type.ToString());
    }


    public void setDebugText(string key, string text)
    {
        if (debugText.ContainsKey(key))
            debugText[key] = text;
        else
            debugText.Add(key, text);

        debugTextUI.text = "";
        foreach (var OneItem in debugText)
        {
           debugTextUI.text+= OneItem.Key + " : " + OneItem.Value+ "\n";
        }
    }

    public void showErrorMsg(string msg)
    {
        errorMsgUI.text = msg;
        errorMsgUI.transform.gameObject.SetActive(true);
        MyLib.delayCall(()=> { errorMsgUI.transform.gameObject.SetActive(false); },1f);
    }

    public Func InitData;
    public Func DataUpdate; // Such as AimPos , Dir 
    public Func MobAI;
    public Func PlayerInput;// Player Input in actual game

    float lasttime = 0;
    float deltatime = 0;
    public float FrameRate = 60;
    public bool FrameRateControl = false;

    private void Awake()
    {
        deltatime = 1f / FrameRate;

        //Default world state 
        worldState = WorldState.Game;
        // Default camera
        currCam = cams[0];
        currCam.display();
        setDebugText("Camera Mode", currCam.type.ToString());
    }
    
    //Input 
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
            changeCamera();
    }
}



