using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
//public class Dia
//{
//    static public DialogSys dia;
//    public Dia()
//    {
//        if (dia == null)
//            dia = new DialogSys();
//    }
//    public void ChangeText()
//    {
//        dia.gameObject.name = "haha";
//    }
//}

public class DialogSys:Singleton<DialogSys>
{
    List<string> dialog;
    int CurrentLine=0;
    public CanvasGroup DialogUIGroup;
    public Text DialogText;

    // Event 
    public Func DialogEnd;

    public void LoadSpeech(List<string> speech)
    {
        DialogEnd = null;
        dialog = new List<string>();
        dialog.AddRange(speech);
        DisplayDialogSys();
    }
    public void LoadSpeech(string sentense)
    {
        DialogEnd = null;
        dialog = new List<string>();
        dialog.Add(sentense);
        DisplayDialogSys();
    }
    public void NextLine()
    {
        CurrentLine++;
        if (CurrentLine >= dialog.Count) { ExitDialogSys();return; };
        DialogText.text = dialog[CurrentLine];
    }
    public void DisplayDialogSys()
    {
        CurrentLine = 0;
        DialogUIGroup.alpha = 1;
        DialogUIGroup.interactable = true ;

        DialogText.text = dialog[CurrentLine];
    }
    public void ExitDialogSys()
    {
        Debug.Log("Exiting DialogSys");
        DialogUIGroup.alpha = 0;
        DialogUIGroup.interactable = false;

        if (DialogEnd != null) { DialogEnd(); }
    }
    private void Start()
    {
        List<string> Openpharse = new List<string>();
        Openpharse.Add(" Greeting,..");
        Openpharse.Add(" this is your dialog system");
       // LoadSpeech(Openpharse);
    }
    private void Update()
    {
        HandleInput();
    }
    public void HandleInput()
    {
        if (DialogUIGroup.interactable == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (DialogUIGroup.interactable == true)
                    NextLine();
            }
        }
    }
}


