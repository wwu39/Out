using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("Dashing Control")]
    public bool DashingUnder;
    public GameObject DashBelowChecker;
    [Header("Regeneration")]
    public GameObject RegeneratesTo;
    public GameObject RegenFX;
    public GameObject TimerPrefab;
    public Color CountdownColor = Color.red;
    protected float regenerateStartTime;
    public float intRegenTime = -1;
    public float regenerateTime = 3;
    protected bool regenerating = false;
    protected bool isDead = false;
    protected Vector3 CenterPos;
    GameObject timer;
    [Header("Missile Properties")]
    public float explosion_radius = 1.5f;
    public float lockonTime = 0.5f;
    public float range = 5;
    [Header("Cannon Properties")]
    public float ROF = 2;
    public float InitialDelay = 0;
    public float BulletSpeed = 20;
    public float angle = -1;
    [Header("Bomber Properties")]
    public BomberDirection direction = BomberDirection.Left;

    // Start is called before the first frame update
    protected void Start()
    {
        if (DashingUnder)
        {
            var dbc = Instantiate(DashBelowChecker, transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void Update_regen(bool condition)
    {
        if (!isDead)
        {
            if (condition && !regenerating)
            {
                // regenerate in 5s
                regenerateStartTime = Time.time;
                timer = Instantiate(TimerPrefab);
                timer.transform.position = CenterPos;
                timer.GetComponentInChildren<TextMesh>().color = CountdownColor;
                regenerating = true;
            }

            if (Time.time - regenerateStartTime > (intRegenTime > 0 ? intRegenTime : regenerateTime) && regenerating)
            {
                Destroy(timer);

                var newFX = Instantiate(RegenFX);
                newFX.transform.position = CenterPos;
                newFX.transform.localScale *= 10;

                GameObject newblock = Instantiate(RegeneratesTo);
                newblock.transform.position = transform.position;
                //newblock.transform.rotation = transform.rotation;
                var properties = newblock.GetComponent<Item>();
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
                    properties.angle = angle;
                }
                regenerating = false;
                isDead = true;
                Destroy(gameObject);
            }
            else
            {
                if (timer) timer.GetComponentInChildren<TextMesh>().text = ((intRegenTime > 0 ? intRegenTime : regenerateTime) - Mathf.RoundToInt(Time.time - regenerateStartTime)).ToString();
            }
        }
    }
}
