using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collision2D))]
public class ContactTrigger : MonoBehaviour
{
    public NPCMob ParentNpc;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ParentNpc.ContactEnter2D(collision);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        ParentNpc.ContactExit2D(collision);
    }
}
