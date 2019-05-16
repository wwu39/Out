using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject Impact;
    public float explosion_radius;
    public HashSet<GameObject> collidedObjects = new HashSet<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
                if (i.tag == "MysteriousBox") if (i.GetComponent<MysteriousBox>()) i.GetComponent<MysteriousBox>().becomes();
                collidedObjects.Add(i.gameObject);
            }
        }
        var exp = Instantiate(Impact);
        exp.transform.position = transform.position;
        Destroy(gameObject);
    }
}
