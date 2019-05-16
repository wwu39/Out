using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadbody : MonoBehaviour
{
    public GameObject DeadFX;
    public int fragcount;
    GameObject deadmsg;
    // Start is called before the first frame update
    void Start()
    {
        deadmsg = Instantiate(DeadFX);
        var deadmsgpos = transform.position;
        deadmsgpos.y += 2;
        deadmsg.transform.position = deadmsgpos;
        fragcount = GetComponentsInChildren<Transform>().Length - 1;
        Rigidbody[] rbs = GetComponentsInChildren<Rigidbody>();
        foreach (var i in rbs)
        {
            var force = (i.gameObject.transform.position - transform.position).normalized * 3;
            i.AddForce(force, ForceMode.VelocityChange);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (deadmsg == null && GameManager.ins.state == GameState.Running)
            GameManager.ins.state = GameState.Lose;
    }
}
