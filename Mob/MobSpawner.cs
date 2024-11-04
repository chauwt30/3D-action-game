using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MobSpawner : MonoBehaviour
{
    public Mob mob;
    public float radius;
    public float minRadius;

    public float spawnGapTime;
    public float maxMobNum;
    public float maxMobNumFromThis;


    public bool autoSpawn = true;

    [HideInInspector]
    public float mobNum;

    float lastSpawnTime=0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (autoSpawn && Time.time - lastSpawnTime > spawnGapTime && GlobalMng.ins.mobNum<maxMobNum && mobNum < maxMobNumFromThis)
        {
            Spawn();
            lastSpawnTime = Time.time;
        }
    }

    public void Spawn()
    {
        // random distance, how far from the center 
        float d = radius * Random.Range(minRadius, radius);
        float angle = Mathf.PI * 2 * Random.Range(0, 1f);
        Instantiate(mob, transform.position + new Vector3(d*Mathf.Cos(angle), 0, d * Mathf.Sin(angle)), Quaternion.identity);
        GlobalMng.ins.mobNum++;
        mobNum++;
    }

    //void OnDrawGizmosSelected()
    //{
    //    // Draw a yellow sphere at the transform's position
    //    //Gizmos.color = Color.yellow;
    //    //Gizmos.DrawSphere(transform.position, radius);

    //    Handles.color = Color.yellow;
    //    Handles.DrawWireDisc(transform.position  // position
    //                                  , transform.up                      // normal
    //                                  , radius);                              // radius
    //}
}
