using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorMiddle : MonoBehaviour
{
    public GameObject QBPrefab;
    public GameObject TilePrefab;
    public GameObject SpikePrefab;
    public GameObject[] randomized;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i< randomized.Length; ++i)
        {
            MysteriousBox m = randomized[i].GetComponentInChildren<MysteriousBox>();
            if (i % 3 == 0) m.becomesWhat = SpikePrefab;
            else m.becomesWhat = TilePrefab;
            m.regenerateTime = 15;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
