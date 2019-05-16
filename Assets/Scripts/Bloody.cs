using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloody : MonoBehaviour
{
    Material mat;
    float i = 0;
    bool reverse;
    Color c;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player") collision.gameObject.GetComponent<PlayerController>().isDead = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<MeshRenderer>().material;
        c = new Color();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (i >= 200) reverse = true;
        if (i <= 0) reverse = false;
        if (reverse) i -= 10f; else i += 10f;
        c.r = i / 255f;
        mat.color = c;
    }
}
