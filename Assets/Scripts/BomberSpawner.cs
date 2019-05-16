using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BomberDirection { Left, Right }

public class BomberSpawner : Item
{
    [Header("Bomber")]
    public GameObject BomberPrefab;
    GameObject Bomber;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        CenterPos = transform.position;
        var newFX = Instantiate(RegenFX);
        newFX.transform.position = CenterPos;
        newFX.transform.localScale *= 10;
        Bomber = Instantiate(BomberPrefab);
        Bomber.transform.position = CenterPos;
        var properties = Bomber.GetComponent<Bomber>();
        properties.explosion_radius = explosion_radius;
        properties.InitialDelay = InitialDelay;
        properties.ROF = ROF;
        properties.bombInitSpeed = BulletSpeed;
        if (direction == BomberDirection.Left)
        {
            Bomber.GetComponent<Rigidbody>().velocity = new Vector3(-5, 0, 0);
            Bomber.transform.rotation = Quaternion.LookRotation(new Vector3(-1, 0, 0));
        }
        else
        {
            Bomber.GetComponent<Rigidbody>().velocity = new Vector3(5, 0, 0);
            Bomber.transform.rotation = Quaternion.LookRotation(new Vector3(1, 0, 0));
        }
    }

    // Update is called once per frame
    void Update()
    {
        Update_regen(Bomber == null);
    }
}
