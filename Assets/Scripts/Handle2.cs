using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handle2 : MonoBehaviour
{
    public GameObject FX;
    public GameObject CopperTile;
    public GameObject TilePrefab;
    public GameObject[] activates;
    bool isDead = false;
    Animation anim;

    [FMODUnity.EventRef]
    public string Knob;
    [FMODUnity.EventRef]
    public string StarSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            FMODUnity.RuntimeManager.PlayOneShot(Knob);
            var mFX = Instantiate(FX);
            var newOne = Instantiate(TilePrefab);
            var mFXPos = CopperTile.transform.position;
            Destroy(CopperTile);
            newOne.transform.position = mFXPos;
            mFXPos.y += 0.75f;
            mFXPos.z += 1.5f;
            mFX.transform.position = mFXPos;
            mFX.transform.localScale *= 15;
            foreach (var a in activates)
            {
                var aFX = Instantiate(FX);
                var aFXPos = a.transform.position;
                aFXPos.z += 1.5f;
                mFXPos.y += 0.75f;
                aFX.transform.position = aFXPos;
                a.SetActive(true);
            }

            anim.Play();
            Destroy(GetComponent<Collider>());
            isDead = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead && !anim.isPlaying)
        {
            FMODUnity.RuntimeManager.PlayOneShot(StarSound);
            var mFX = Instantiate(FX);
            var mFXPos = transform.position;
            mFXPos.z += 1.5f;
            mFX.transform.position = mFXPos;
            Destroy(gameObject);
            isDead = false;
        }
    }
}
