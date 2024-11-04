using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item 
{
    virtual public void Use(Mob user)
    {
    }
    virtual public void OnPick(Mob user)
    {
    }
    virtual public void GenPickableItem(Mob user)
    {
    }
}

public class Equipment:Item
{ }

public class Consumables : Item
{ }


public class Useable : Item
{ }