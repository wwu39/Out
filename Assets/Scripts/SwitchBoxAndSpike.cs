using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBoxAndSpike : MonoBehaviour
{
    public GameObject BoxPrefab;
    public GameObject Spike4Prefab;
    public MysteriousBox BoxM;
    public MysteriousBox Spike4M;
    // Start is called before the first frame update
    void Start()
    {
        if (GlobalManager.ins.EasySceneLoadCount % 2 == 1)
        {
            BoxM.becomesWhat = BoxPrefab;
            BoxM.GetComponentInChildren<TextMesh>().gameObject.SetActive(false);
            BoxM.GetComponentInParent<Warning>().enabled = false;
            Spike4M.becomesWhat = Spike4Prefab;
        }
        else
        {
            BoxM.becomesWhat = Spike4Prefab;
            Spike4M.becomesWhat = BoxPrefab;
            Spike4M.GetComponentInChildren<TextMesh>().gameObject.SetActive(false);
            Spike4M.GetComponentInParent<Warning>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
