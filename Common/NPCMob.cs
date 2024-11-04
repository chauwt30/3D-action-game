using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMob : Mob
{
    public bool IsContactable = true;
    List<KeyHint> KeyHints = new List<KeyHint>();
    List<string> Dialogs = new List<string>();
    public override void InitData()
    {
        base.InitData();
        // Init Dialog
        KeyHints.Add(new KeyHint(KeyCode.F, "Talk", () => {
            if (Dialogs.Count <= 0) { DialogSys.ins.LoadSpeech(" i have nothing to say."); ; }
            else
            {
                DialogSys.ins.LoadSpeech(Dialogs);
            }
        }));
    }

    public void ContactEnter2D(Collider2D col)
    {
        InputHintSys.ins.Show(KeyHints);
    }

    public void ContactExit2D(Collider2D col)
    {
        InputHintSys.ins.Hide();
    }
}
