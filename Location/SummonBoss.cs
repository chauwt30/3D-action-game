using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonBoss : MonoBehaviour
{
    
    List<KeyHint> key = new List<KeyHint>();

    public Mob boss;
    // Start is called before the first frame update
    void Start()
    {
        key.Add(new KeyHint(KeyCode.E, "Summon", summon));
    }

    void summon()
    {
        if (GlobalMng.ins.player.shatterNum<4)
        {
            GlobalMng.ins.showErrorMsg("You have to collect 4 orb shatter to summom");
        }
        else
        {
            Instantiate(boss, transform.position, Quaternion.identity);
        }
    }
    
    // Update is called once per frame
    void Update()
    {

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
