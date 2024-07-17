using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Atm : MonoBehaviour
{
    [System.NonSerialized] public PlayerController PC;

    // Update is called once per frame
    void Update()
    {
        if (PC.l == 1)
        {
            transform.DOLocalMove(new Vector3(2.55f, -1.45f, 2.5f), 0.1f);
        }

        if(PC.l == 2)
        {
            transform.DOLocalMove(new Vector3(-2.25f, -1.45f, 2.5f), 0.1f);
        }

        if(PC.l == 0)
        {
            transform.DOLocalMove(new Vector3(0.25f, -0.4f, 2.5f), 0.1f);
        }
    }
}
