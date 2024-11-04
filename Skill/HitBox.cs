using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class HitBox : MonoBehaviour
{
    public Skill recevier;

    private void Start()
    {
        tag = recevier.tag;
    }
    private void Update()
    {
        tag = recevier.tag;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        recevier.Hit(col);
    }

    private void OnTriggerEnter(Collider col)
    {
        recevier.Hit(col);
    }

}
