using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomber : MonoBehaviour
{
    public GameObject Bomb;
    public GameObject Impact;
    public float ROF = 0.01f;
    public float InitialDelay = 0;
    public float explosion_radius;
    public float bombInitSpeed = 10;
    float ROFCounter;
    public HashSet<GameObject> collidedObjects = new HashSet<GameObject>();
    float bornTime;
    GameObject Player;

    [FMODUnity.EventRef]
    public string DropBombSound;
    [FMODUnity.EventRef]
    public string StartSound;

    // Start is called before the first frame update
    void Start()
    {
        FMODUnity.RuntimeManager.PlayOneShot(StartSound);
        ROFCounter = Time.time;
        bornTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - ROFCounter > ROF)
        {
            FMODUnity.RuntimeManager.PlayOneShot(DropBombSound);
            var b = Instantiate(Bomb);
            Physics.IgnoreCollision(b.GetComponent<Collider>(), GetComponent<Collider>());
            b.transform.position = transform.position;
            b.GetComponent<Bomb>().explosion_radius = explosion_radius;
            b.GetComponent<Rigidbody>().velocity = new Vector3(0, -bombInitSpeed, 0);
            ROFCounter = Time.time;
        }
        if (Time.time - bornTime > 0.5 && !GetComponent<Collider>().enabled) GetComponent<Collider>().enabled = true;
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
                collidedObjects.Add(i.gameObject);
            }
        }
        var exp = Instantiate(Impact);
        exp.transform.position = transform.position;
        Destroy(gameObject);
    }
}
