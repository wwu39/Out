using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : Item
{
    [Header("Cannon")]
    public GameObject Target;
    public GameObject FiringPoint;
    public GameObject MuzzlePrefab;
    public GameObject Bullet;
    public GameObject DeathExplosion;
    float ROFCounter;
    bool isHit;
    Vector3 DefaultDirection;
    float ROT = 0.2f;
    float ROTCounter;
    Vector3 curDirection;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        if (!Target) Target = GameObject.FindGameObjectWithTag("Player");
        if (Target) curDirection = Target.transform.position - transform.position;
        ROTCounter = Time.time;
        ROFCounter = InitialDelay + Time.time;
        isHit = false;
        CenterPos = transform.position;
        if (angle >= 0) DefaultDirection = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle), 0);
        else while (DefaultDirection.magnitude == 0) DefaultDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isHit)
        {
            if (Target)
            {
                if (Time.time - ROTCounter > ROT)
                {
                    curDirection = Target.transform.position - transform.position;
                    ROTCounter = Time.time;
                }
            }

            Vector3 bulletVelocity;
            if (angle >= 0) bulletVelocity = DefaultDirection.normalized * BulletSpeed;
            else bulletVelocity = (Target ? curDirection.normalized : DefaultDirection.normalized) * BulletSpeed;
            transform.rotation = Quaternion.LookRotation(bulletVelocity);
            if (Time.time - ROFCounter > ROF)
            {
                var Muzzle = Instantiate(MuzzlePrefab);
                Muzzle.transform.position = FiringPoint.transform.position;
                Muzzle.transform.localScale = 0.25f * Vector3.one;
                ROFCounter = Time.time;
                var bullet = Instantiate(Bullet);
                Physics.IgnoreCollision(bullet.GetComponent<Collider>(), GetComponent<Collider>());
                bullet.transform.position = FiringPoint.transform.position;
                bullet.GetComponent<CannonBullet>().explosion_radius = explosion_radius;
                bullet.GetComponent<Rigidbody>().velocity = bulletVelocity;
            }
        }
        Update_regen(isHit);
    }

    public void die()
    {
        isHit = true;
        Destroy(GetComponent<Collider>());
        foreach (var t in GetComponentsInChildren<Transform>())
        {
            if (t.gameObject == gameObject)
            {
                t.gameObject.GetComponent<MeshRenderer>().enabled = false;
            }
            else
            {
                MeshCollider mc = t.gameObject.AddComponent<MeshCollider>();
                mc.convex = true;
                Rigidbody rb = t.gameObject.AddComponent<Rigidbody>();
                Vector3 force = (t.position - transform.position).normalized * 5;
                rb.AddForce(force, ForceMode.Impulse);
                t.gameObject.AddComponent<TilePiece>().shrinking = true;
            }
        }
    }
}
