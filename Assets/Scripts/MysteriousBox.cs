using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteriousBox : Item
{
    [Header("--------------------------------")]
    public GameObject parent;
    public GameObject becomesWhat;
    public GameObject FX;
    public Transform Center;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Crate") becomes();
    }

    public void becomes()
    {
        if (becomesWhat)
        {
            var newobj = Instantiate(becomesWhat);
            newobj.transform.position = transform.position;
            var properties = newobj.GetComponent<Item>();
            if (properties)
            {
                properties.CountdownColor = CountdownColor;
                properties.regenerateTime = regenerateTime;
                properties.explosion_radius = explosion_radius;
                properties.lockonTime = lockonTime;
                properties.range = range;
                properties.ROF = ROF;
                properties.InitialDelay = InitialDelay;
                properties.BulletSpeed = BulletSpeed;
                properties.direction = direction;
            }
        }
        var newFX = Instantiate(FX);
        newFX.transform.position = Center.position;
        newFX.transform.localScale *= 10;
        Destroy(parent);
    }

    new private void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
