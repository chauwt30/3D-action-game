using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopContact : MonoBehaviour
{
    List<KeyHint> key = new List<KeyHint>();
    public ShopUI shopUI;

    // Start is called before the first frame update
    void Start()
    {
        key.Add(new KeyHint(KeyCode.B,"Open shop",openShop));
    }
    void openShop()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            shopUI.openShopUI();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider col)
    {
        Debug.Log(col.gameObject.name);
        InputHintSys.ins.Show(key);
    }

    private void OnTriggerExit(Collider col)
    {
        InputHintSys.ins.Hide();
        Debug.Log("Exit" + col.gameObject.name);
    }
}
