using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOption : MonoBehaviour
{
    [SerializeField] public GameObject canvas;
    [SerializeField] public GameObject option;

    bool flag;

    private void Start()
    {
        canvas.gameObject.SetActive(false);
        option.gameObject.SetActive(true);
        flag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire11_1"))
        {
            if (!flag)
            {
                canvas.gameObject.SetActive(true);
                option.gameObject.SetActive(false);
                flag = true;
            }

            else if (flag)
            {
                canvas.gameObject.SetActive(false);
                option.gameObject.SetActive(true);

                flag = false;
            }

        }

        if (Input.GetButtonDown("Fire11_2"))
        {
            if (!flag)
            {
                canvas.gameObject.SetActive(true);
                option.gameObject.SetActive(false);
                flag = true;
            }

            else if (flag)
            {
                canvas.gameObject.SetActive(false);
                option.gameObject.SetActive(true);
                flag = false;
            }

        }

        if (Input.GetButtonDown("Fire11_3"))
        {
            if (!flag)
            {
                canvas.gameObject.SetActive(true);
                option.gameObject.SetActive(false);
                flag = true;
            }

            else if (flag)
            {
                canvas.gameObject.SetActive(false);
                option.gameObject.SetActive(true);
                flag = false;
            }

        }

        if (Input.GetButtonDown("Fire11_4"))
        {
            if (!flag)
            {
                canvas.gameObject.SetActive(true);
                option.gameObject.SetActive(false);
                flag = true;
            }

            else if (flag)
            {
                canvas.gameObject.SetActive(false);
                option.gameObject.SetActive(true);
                flag = false;
            }

        }

    }
}
