using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBullet : MonoBehaviour
{
    public GameObject Impact;
    public float Lifetime = 10;
    float borntime;
    public float explosion_radius = 1;
    public HashSet<GameObject> collidedObjects = new HashSet<GameObject>();

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player") collision.gameObject.GetComponent<PlayerController>().isDead = true;
        // radius explosion
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosion_radius);
        foreach (var i in hitColliders)
        {
            if (!collidedObjects.Contains(i.gameObject))
            {
                if (i.tag == "Tile") if (i.GetComponent<Tile>().isBreakable) i.GetComponent<Tile>().shatter();
                if (i.tag == "Cannon") if (i.GetComponent<Cannon>()) i.GetComponent<Cannon>().die();
                collidedObjects.Add(i.gameObject);
            }
        }
        var exp = Instantiate(Impact);
        exp.transform.position = transform.position;
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        borntime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - borntime > Lifetime)
        {
            var exp = Instantiate(Impact);
            exp.transform.position = transform.position;
            Destroy(gameObject);
        }
    }
}
