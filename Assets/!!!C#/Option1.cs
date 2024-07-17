using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Option1 : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    float speed = 0.0001f;

    void Update()
    {
        if (Input.GetButtonDown("Fire10_1"))
        {
            FadeManager.Instance.LoadScene("GameScene", 0.5f);
            while (audioSource.volume > 0)
            {
                audioSource.volume -= speed * Time.deltaTime;
            }
        }

        if (Input.GetButtonDown("Fire10_2"))
        {
            FadeManager.Instance.LoadScene("GameScene", 0.5f);
            while (audioSource.volume > 0)
            {
                audioSource.volume -= speed * Time.deltaTime;
            }
        }

        if (Input.GetButtonDown("Fire10_3"))
        {
            FadeManager.Instance.LoadScene("GameScene", 0.5f);
            while (audioSource.volume > 0)
            {
                audioSource.volume -= speed * Time.deltaTime;
            }
        }

        if (Input.GetButtonDown("Fire10_4"))
        {
            FadeManager.Instance.LoadScene("GameScene", 0.5f);
            while (audioSource.volume > 0)
            {
                audioSource.volume -= speed * Time.deltaTime;
            }
        }
    }
}

