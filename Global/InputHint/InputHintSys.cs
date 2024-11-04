using System.Collections.Generic;
using UnityEngine;

public class InputHintSys : Singleton<InputHintSys>
{
    List<KeyHint> KeyHints = new List<KeyHint>();

    public bool IsActive =false;

    // UI ref
    public CanvasGroup InputHintList;
    // Prefab
    public InputHintUI HintUI;

    public void Show(List<KeyHint> hints)
    {
        KeyHints = new List<KeyHint>();
        KeyHints.AddRange(hints);

        foreach(Transform t in InputHintList.transform)
        {
            Destroy(t.gameObject);
        }

        foreach (KeyHint k  in KeyHints)
        {
            InputHintUI ui= Instantiate(HintUI,InputHintList.transform);
            ui.transform.gameObject.SetActive(true);
            ui.LoadKeyHint(k);
        }

        InputHintList.alpha = 1;
        InputHintList.interactable = true;
        IsActive = true;
    }

    public void Hide()
    {
        InputHintList.alpha = 0;
        InputHintList.interactable = false;
        IsActive = false;
    }

    private void Update()
    {
        if(IsActive)
        {
            foreach(KeyHint k in KeyHints)
            {
                if( Input.GetKeyDown(k.Key))
                    k.Action();
            }
        }
    }
}

public class KeyHint
{
    public KeyHint(KeyCode key,string text,Func action)
    {
        Key = key;
        Text = text;
        Action += action;
    }
    public KeyCode Key;
    public string Text;
    public Func Action; 
}
