using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hotel : MonoBehaviour
{
    List<KeyHint> key = new List<KeyHint>();
    // Start is called before the first frame update
    void Start()
    {
        key.Add(new KeyHint(KeyCode.E, "Rest", rest));
    }

    void rest()
    {

        GlobalMng.ins.player.HP = GlobalMng.ins.player.MaxHP;
        GlobalMng.ins.player.healFx.Play();
    }
    private void OnTriggerEnter(Collider col)
    {
        InputHintSys.ins.Show(key);
    }

    private void OnTriggerExit(Collider col)
    {
        InputHintSys.ins.Hide();
    }
}
