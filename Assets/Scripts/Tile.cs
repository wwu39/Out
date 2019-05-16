using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : Item
{
    [Header("Tile")]

    public bool isBreakable;
    bool isShattered = false;

    [FMODUnity.EventRef]
    public string CrackSound;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        foreach (var c in GetComponentsInChildren<MeshRenderer>())
        {
            // if (isBreakable) c.material.color = new Color(0.9f, 0, 0);
            if (c.gameObject != gameObject)
            {
                c.enabled = false;
            }
        }
        CenterPos = transform.position;
        CenterPos.y += 0.75f;
        CenterPos.z += 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        Update_regen(isShattered);
    }

    public void cracks()
    {
        foreach (var t in GetComponentsInChildren<Transform>())
        {
            if (t.gameObject != gameObject)
            {
                t.gameObject.GetComponent<MeshRenderer>().enabled = true;
                t.localScale = new Vector3(0.9f, 0.9f, 0.9f);
            }
            else
            {
                t.gameObject.GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }

    public void shatter()
    {
        isShattered = true;
        FMODUnity.RuntimeManager.PlayOneShot(CrackSound);
        foreach (var t in GetComponentsInChildren<Transform>())
        {
            if (t.gameObject != gameObject)
            {
                t.gameObject.GetComponent<MeshRenderer>().enabled = true;
                MeshCollider mc = t.gameObject.AddComponent<MeshCollider>();
                mc.convex = true;
                Rigidbody rb = t.gameObject.AddComponent<Rigidbody>();
                Vector3 force = (t.position - transform.position).normalized * 5;
                rb.AddForce(force, ForceMode.Impulse);
                t.gameObject.GetComponent<TilePiece>().shrinking = true;
            }
        }
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }
}
