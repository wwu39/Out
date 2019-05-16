using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBillTimer : Item
{
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        CenterPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Update_regen(true);
    }
}
