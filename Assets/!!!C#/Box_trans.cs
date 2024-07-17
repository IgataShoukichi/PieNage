using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box_trans : MonoBehaviour
{
    Transform myTransform;
    Vector3 pos;
    int point;
    float speed;
    // Start is called before the first frame update
    void Start()
    {
        speed = 0.13f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        myTransform = this.transform;

        // ���W���擾
        pos = myTransform.position;
        if(point == 0)
        {
            pos.x += speed;    // x���W��0.01���Z
        }
        else if(point == 1)
        {
            pos.z += speed;    // x���W��0.01���Z
        }
        else if(point == 2)
        {
            pos.x -= speed;    // x���W��0.01���Z
        }
        else if (point == 3)
        {
            pos.z -= speed;
        }
        myTransform.position = pos;  // ���W��ݒ�
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Shell")
        {
            point = 1;
        }
        if(other.gameObject.tag == "Pie2")
        {
            point = 2;
        }
        if(other.gameObject.tag == "Pie3")
        {
            point = 3;
        }
        if(other.gameObject.tag == "Pie4")
        {
            point = 0;
        }
    }
}
