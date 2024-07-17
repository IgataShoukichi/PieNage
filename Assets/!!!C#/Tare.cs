using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tare : MonoBehaviour
{
    [SerializeField] public GameObject[] tare;
    float time;
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        for(int i = 0; i < tare.Length; i++)
        {
            tare[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if(time > 0)
        {
            tare[0].SetActive(true);
        }
        if (time > 0.5)
        {
            tare[1].SetActive(true);
        }
        if(time > 1)
        {
            tare[2].SetActive(true);
        }
    }
}
