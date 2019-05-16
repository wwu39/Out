using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warning : MonoBehaviour
{
    float i = 0;
    bool reverse;
    Color c;

    // Start is called before the first frame update
    void Start()
    {
        c = new Color();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (i >= 200) reverse = true;
        if (i <= 0) reverse = false;
        if (reverse) i -= 10f; else i += 10f;
        c.r = i / 255f;
        foreach (var j in GetComponentsInChildren<MeshRenderer>()) if (!j.GetComponent<TextMesh>()) j.material.color = c;
    }
}
