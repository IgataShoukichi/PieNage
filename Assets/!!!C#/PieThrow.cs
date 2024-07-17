using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PieThrow : MonoBehaviour
{
    [SerializeField] PieDirector pieD;

    public GameObject BigPiePrefab;

    [SerializeField] public Transform follow;

    public GameObject follow_h;
    [SerializeField] public Transform follow_HT;

    [SerializeField] public Transform follow_b;

    private Vector3 latestPos;  //�O���Position
    [SerializeField] public Camera myCamera;

    private PlayerController PC;

    [SerializeField] public GameObject handPie;
    [SerializeField] public GameObject handBigpie;

    //���݂̃`���[�W�J�E���g
    public float ChargeCount = 0;
    public float ChargeCount_s = 0;
    public int NageP = 0;

    public bool Switch = false;

    public float triR = 0;

    public float shoR = 0;


    public int fra = 0;

    public Vector3 shootVelocity;
    public bool drawArc = false;

    [System.NonSerialized] public TimeController TC;

    public bool th = false;

    Animator animator;

    public AudioClip throwSound;
    public AudioClip BigPie;
    AudioSource aud;

    bool sound;

    private void Awake()
    {
        transform.root.gameObject.GetComponent<PlayerController>().PT = this;
    }

    void Start()
    {
        this.aud = GetComponent<AudioSource>();

        Switch = false;
        fra = 0;

        this.animator = GetComponent<Animator>();
        PC = transform.root.gameObject.GetComponent<PlayerController>();

        th = false;

        handPie.SetActive(false);
        handBigpie.SetActive(false);

        sound = false;
    }

    void Update()
    {
        if (PC.PCounter.sPie && !sound)
        {
            this.aud.PlayOneShot(this.BigPie);
            sound = true;
        }

        if (TC.countdown < 0.1f)
        {
            drawArc = false;
        }

        var gamepad = Gamepad.all;
        if (gamepad.Count > 0)
        {
            triR = gamepad[PC.num].rightTrigger.ReadValue();
            shoR = gamepad[PC.num].rightShoulder.ReadValue();
        }

        Transform mytransform = this.transform;
        Vector3 pos = mytransform.position;

        var horizontalRotation = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up);
        var velocity = horizontalRotation * pos;

        transform.eulerAngles = new Vector3(transform.eulerAngles.x, myCamera.transform.eulerAngles.y, transform.eulerAngles.z);

        if ((PC.PCounter.Pie >= 1 && PC.TC.countdown > 0.3f) && (!Switch && !PC.nugu))
        {
            handPie.SetActive(true);
        }
        else
        {
            handPie.SetActive(false);
        }

        if (gamepad.Count > 0 && (ChargeCount >= 1 || ChargeCount_s >= 1) /*|| (Input.GetMouseButtonUp(0) && PC.num == 0)*/ )
        {
            if (PC.nugu)
            {
                ChargeCount = 0;
                ChargeCount_s = 0;
                drawArc = false;
                handBigpie.SetActive(false);
                animator.SetBool("BigPie", false);
            }
            else
            {
                if (PC.PCounter.Pie >= 1 && triR == 0 && !Switch)
                {
                    this.aud.PlayOneShot(this.throwSound);
                    animator.SetTrigger("Throw_tri");
                    handPie.SetActive(false);
                    Throw1();
                    drawArc = false;
                }
                if (PC.PCounter.sPie && shoR == 0 && Switch)
                {
                    animator.SetTrigger("BigPie_tri");
                    handBigpie.SetActive(false);
                    Throw2();
                    drawArc = false;
                    Switch = false;
                }
            }

        }


    }

    private void FixedUpdate()
    {
        //�m�[�}����Ԃ��p�C�������Ă��邩�A�X�y�V������ԂŃX�y�V�����������Ă���Ƃ��ɁA�g���K�[�������Ă���ƃ`���[�W��������
        if (triR > 0 && shoR == 0 && PC.PCounter.Pie >= 1 && ChargeCount_s == 0 && TC.countdown > 0.1f && !PC.nugu)
        {
            drawArc = true;

            ChargeCount++;
            Switch = false;
            if (ChargeCount >= 100)
            {
                NageP = 30;
            }
            else if (ChargeCount >= 50)
            {
                NageP = (int)(ChargeCount * ChargeCount * 0.005f);
            }
            else if (ChargeCount >= 1)
            {
                NageP = (int)(ChargeCount * ChargeCount * 0.005f);
            }
        }

        if (shoR > 0 && triR == 0 && PC.PCounter.sPie && ChargeCount == 0 && TC.countdown > 0.1f)
        {
            ChargeCount_s++;
            Switch = true;
            animator.SetBool("Throw", false);
            animator.SetBool("BigPie", true);

            handPie.SetActive(false);
            handBigpie.SetActive(true);

            if (ChargeCount_s >= 100)
            {
                NageP = 40;
            }
            else if (ChargeCount_s >= 50)
            {
                NageP = (int)(ChargeCount_s * ChargeCount_s * 0.005f);
            }
            else if (ChargeCount_s >= 1)
            {
                NageP = (int)(ChargeCount_s * ChargeCount_s * 0.005f);
            }

            drawArc = true;
        }
    }

    private void Throw1()
    {
        PC.PCounter.Pie--;
        for (int i = 0; i < pieD.pieCons1.Length; i++)
        {
            if (!pieD.pieCons1[i].gameObject.activeSelf)
            {
                pieD.pieCons1[i].gameObject.transform.position = follow_HT.position;
                pieD.pieCons1[i].gameObject.SetActive(true);

                //������
                Ray ray = new Ray(follow_h.transform.position, follow_h.transform.forward);
                Vector3 worldDir = ray.direction;
                pieD.pieCons1[i].gameObject.GetComponent<PieController>().Shoot(worldDir.normalized * NageP, transform.root.gameObject, PC.playerID, false);

                ChargeCount = 0;

                break;
            }
        }
    }



    private void Throw2()
    {
        PC.PCounter.sPie = false;

        for (int i = 0; i < pieD.bigpieCons.Length; i++)
        {
            if (!pieD.bigpieCons[i].gameObject.activeSelf)
            {
                pieD.bigpieCons[i].gameObject.transform.position = follow_b.position;
                pieD.bigpieCons[i].gameObject.SetActive(true);

                //������
                Ray ray = myCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));
                Vector3 worldDir = ray.direction;
                pieD.bigpieCons[i].gameObject.GetComponent<PieController>().Shoot(worldDir.normalized * NageP / 3, transform.root.gameObject, PC.playerID, true);

                sound = false;
                ChargeCount_s = 0;
                break;
            }
        }
    }

}
