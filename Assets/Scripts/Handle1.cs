using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handle1 : MonoBehaviour
{
    public GameObject TilePrefab;
    public GameObject Related;
    public GameObject[] Modified;
    public GameObject[] Destroyed;
    public GameObject Hint;
    public GameObject Show;
    public GameObject FX;
    bool isDead = false;
    Animation anim;

    [FMODUnity.EventRef]
    public string Knob;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            FMODUnity.RuntimeManager.PlayOneShot(Knob);
            var FXPos = Related.transform.position;
            Destroy(Related);
            var newFX = Instantiate(FX);
            newFX.transform.position = FXPos;
            newFX.transform.localScale *= 15;

            foreach (var m in Modified)
            {
                var mFX = Instantiate(FX);
                var newOne = Instantiate(TilePrefab);
                var mFXPos = m.transform.position;
                Destroy(m);
                newOne.transform.position = mFXPos;
                mFXPos.z += 1.5f;
                mFX.transform.position = mFXPos;
                mFX.transform.localScale *= 15;
            }
            foreach (var d in Destroyed)
            {
                var mFX = Instantiate(FX);
                var mFXPos = d.transform.position;
                Destroy(d);
                mFXPos.z += 1.5f;
                mFX.transform.position = mFXPos;
            }
            Show.SetActive(true);
            Destroy(Hint);
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
            var mFX = Instantiate(FX);
            var mFXPos = transform.position;
            mFXPos.z += 1.5f;
            mFX.transform.position = mFXPos;
            Destroy(gameObject);
            isDead = false;
        }
    }
}
