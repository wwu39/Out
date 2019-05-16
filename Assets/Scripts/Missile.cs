using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum MissileState { None, Locking, Launch }

public class Missile : Item
{
    Animation anim;
    [Header("Missile")]
    public GameObject Target;
    public GameObject Explosion;
    public GameObject angry;
    float lockingStartTime = -1;
    MissileState state = MissileState.None;
    Vector3 launchForce;

    LineRenderer lr;
    int segment = 36;

    public HashSet<GameObject> collidedObjects = new HashSet<GameObject>();

    public GameObject playerT;

    [FMODUnity.EventRef]
    public string AngrySound;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        CenterPos = transform.position;
        anim = GetComponent<Animation>();
        anim.Play("Ready");

        GetComponentInChildren<SphereCollider>().radius = range * 20;

        lr = GetComponent<LineRenderer>();
        lr.positionCount = segment;
        lr.startWidth = 0.08f;
        lr.endWidth = 0.08f;
        lr.startColor = Color.red;
        lr.endColor = Color.red;
        lr.material.color = Color.red;
        var points = new Vector3[segment];
        for (int i = 0; i < segment; i++)
        {
            var rad = Mathf.Deg2Rad * (i * 360f / segment - 1);
            points[i] = transform.position + new Vector3(Mathf.Sin(rad) * range, Mathf.Cos(rad) * range, 0);
        }
        lr.SetPositions(points);
    }

    // Update is called once per frame
    void Update()
    {
        if (state == MissileState.None)
        {
            if (!anim.isPlaying) anim.Play("Idle");
            // detect if player in range
            if (Target)
            {
                angry.SetActive(true);
                var angryPos = angry.transform.position;
                FMODUnity.RuntimeManager.PlayOneShot(AngrySound);
                lockingStartTime = Time.time;
                launchForce = (Target.transform.position - transform.position).normalized * 1.5f;
                transform.rotation = Quaternion.LookRotation(launchForce);
                anim.Play("Launch");
                state = MissileState.Locking;
            }
        }
        else if (state == MissileState.Locking)
        {
            if (lockingStartTime >= 0 && Time.time - lockingStartTime > lockonTime)
            {
                lr.enabled = false;
                lockingStartTime = -1;
                var rb = gameObject.AddComponent<Rigidbody>();
                rb.useGravity = false;
                state = MissileState.Launch;
            }

        }
        else if (state == MissileState.Launch)
        {
            GetComponent<Rigidbody>().AddForce(launchForce, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (state == MissileState.Launch)
        {
            if (collision.gameObject.tag == "Player")
            {
                collision.gameObject.GetComponent<PlayerController>().isDead = true;
            }

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

            GameObject exp = Instantiate(Explosion);
            exp.transform.position = transform.position;

            var newTimer = Instantiate(RegeneratesTo);
            newTimer.transform.position = CenterPos;
            var properties = newTimer.GetComponent<Timer>();
            properties.regenerateTime = regenerateTime;
            properties.explosion_radius = explosion_radius;
            properties.lockonTime = lockonTime;
            properties.range = range;
            Destroy(gameObject);

            // reseting
            Destroy(GetComponent<Rigidbody>());
            state = MissileState.None;
            lr.enabled = true;
            gameObject.SetActive(false);
        }
    }
}
