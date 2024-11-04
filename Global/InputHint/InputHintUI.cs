using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputHintUI : MonoBehaviour
{
    public TextMeshProUGUI Discription;
    public TextMeshProUGUI Key;

    // Start is called before the first frame update

    public void LoadKeyHint(KeyHint k)
    {
        Key.text = k.Key.ToString();
        Discription.text = k.Text;
    }
}
