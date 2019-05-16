using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashing : MonoBehaviour
{
    public float Delay = 0.5f;
    MeshRenderer[] mr;
    TextMesh[] tm;
    float counter;

    // Start is called before the first frame update
    void Start()
    {
        mr = GetComponentsInChildren<MeshRenderer>();
        tm = GetComponentsInChildren<TextMesh>();
        counter = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - counter > Delay)
        {
            foreach (var m in mr) m.enabled = !m.enabled;
            foreach (var t in tm)
            {
                var c = t.color;
                if (c.a != 0) c.a = 0; else c.a = 1;
                t.color = c;

            }
            counter = Time.time;
        }
    }
}
