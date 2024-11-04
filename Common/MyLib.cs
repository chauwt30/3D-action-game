using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void Func();
public class MyLib : Singleton<MyLib>
{
    static public void delayCall(Func f, float t)
    {
        ins.StartCoroutine(DelayCall(f,t));
    }
    static public void delayCallRepeat(Func f, float t, float RepeatTimes)
    {
        ins.StartCoroutine(DelayCallRepeat(f, t, RepeatTimes));
    }


    static public IEnumerator DelayCall(Func f, float t)
    {
        yield return new WaitForSeconds(t);
        f();
    }

    static public IEnumerator DelayCallRepeat(Func f, float t,float RepeatTimes)
    {
        for (int i = 0; i <= RepeatTimes - 1; i++)
        {
            yield return new WaitForSeconds(t);
            f();
        }
    }
    static public IEnumerator DelayCallRepeat(Func f, float gaptime, float RepeatTimes,Func CallBack)
    {
        for (int i = 0; i <= RepeatTimes - 1; i++)
        {
            yield return new WaitForSeconds(gaptime);
            f();
        }
        CallBack();
    }

    public static Vector3 rotateVector(Vector3 v, float angle)
    {
        float x, y;
        x = v.x * Mathf.Cos(angle) - v.y * Mathf.Sin(angle);
        y = v.x * Mathf.Sin(angle) + v.y * Mathf.Cos(angle);
        return new Vector3(x, 0, y);
    }

    // Data tranformation

    public static Vector3 NormalizeDir(Vector3 SelfPos, Vector3 TargetPos)
    {
        return Vector3.Normalize(TargetPos - SelfPos);
    }
    public Vector2 GetVector2(Vector3 v3)
    {
        return new Vector2(v3.x, v3.y);
    }
    public static float Arctan(Vector2 Dir)
    {
        float TempAng = (180 / Mathf.PI) * Mathf.Atan(Dir.y / Dir.x);

        if (TempAng >= 0)
        {
            if (Dir.y <= 0) { return TempAng + 180; } // Quad 3 
            else { return TempAng; } // Quad 1 
        }
        else // TempAng <0
        {
            if (Dir.y >= 0) { return 180 + TempAng; }  // Quad 2 
            else { return 360 + TempAng; } // Quad 4
        }
    }

    // Particle system 

    public static void playParticle(ParticleSystem p, Vector3 pos)
    {
        ParticleSystem p2 = Instantiate(p, pos, Quaternion.identity);
        p2.Play();
    }
}