using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using DG.Tweening;



public class PlayerController : MonoBehaviour
{
    public float jumpPower;
    public float downPower;

    Rigidbody rb;
    public int num;
    [SerializeField] Transform myCamera;

    [SerializeField] Transform follow;

    [SerializeField] Transform cameraPos;

    [SerializeField] GameObject head_position;
    [SerializeField] GameObject camera_lean;


    public bool jumpNow;

    public bool nugu;

    public float gravityPower; //調整必要　例 - 1000

    public int Wiper;

    int tyaku;

    private bool Switch_LS;

    public float shoL = 0;


    public int playerID = 0;

    public float triL = 0;


    [System.NonSerialized] public List<ShotBody> SB = new List<ShotBody>();
    [System.NonSerialized] public List<ShotHead> SH = new List<ShotHead>();
    //[System.NonSerialized] public ShotHead SH;
    [System.NonSerialized] public PieCounter PCounter;
    [System.NonSerialized] public PieThrow PT;
    [System.NonSerialized] public TimeController TC;

    [SerializeField] public PieController[] PCntroller;
    [SerializeField] public GameObject[] pct;

    [SerializeField] public GameObject sg;
    private StartGame SG;
    //public ShotHead SH;

    private Atm AT;
    private Camera_Lean CL;
    private bool switchA;

    [SerializeField] public GameObject cc;
    private CameraController CC;

    [SerializeField] string button0 = "Fire7_1";

    //private bool switchL_r;
    //private bool switchL_l;

    public int l;


    void Gravity()
    {
        if (jumpNow == true)
        {
            rb.AddForce(new Vector3(0, gravityPower, 0));
        }
    }

    //GameObject subcamera;
    Animator animator;

    public int RunTrigger = 0;
    //移動速度
    [SerializeField] public float speed = 7;
    public float speed_hold;
    public int betyaCount;

    // Start is called before the first frame update
    void Start()
    {
        switchA = true;

        tyaku = 0;

        speed = 7;

        jumpNow = false;
        rb = GetComponent<Rigidbody>();
        this.animator = GetComponent<Animator>();
        Switch_LS = false;
        nugu = false;
        //switchL_r = false;
        //switchL_l = false;
        l = 0;
        AT = head_position.gameObject.GetComponent<Atm>();
        AT.PC = this;
        CL = camera_lean.gameObject.GetComponent<Camera_Lean>();
        CL.PC = this;
        SG = sg.gameObject.GetComponent<StartGame>();
        SG.PC[num] = this;

        //SH = gameObject.GetComponent<ShotHead>();
        //SH.PC = this;

        CC = cc.gameObject.GetComponent<CameraController>();
        CC.PC = this;

        betyaCount = 0;
        speed_hold = 0;

        for (int i = 0; i < PCntroller.Length; i++)
        {
            PCntroller[i] = pct[i].gameObject.GetComponent<PieController>();
            if (num == 0)
            {
                PCntroller[i].PC[0] = this;
            }
            else if (num == 1)
            {
                PCntroller[i].PC[1] = this;
            }
            else if (num == 2)
            {
                PCntroller[i].PC[2] = this;
            }
            else if (num == 3)
            {
                PCntroller[i].PC[3] = this;
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(betyaCount);

        var gamepad = Gamepad.all;
        if (gamepad.Count > 0)
        {
            triL = gamepad[num].leftTrigger.ReadValue();
            shoL = gamepad[num].leftShoulder.ReadValue();

        }

        Transform mytransform = this.transform;
        Vector3 pos = mytransform.position;


        var horizontalRotation = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up);
        var velocity = horizontalRotation * pos;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, myCamera.transform.eulerAngles.y, transform.eulerAngles.z);


        if (jumpNow == false && tyaku == 1)
        {
            animator.SetBool("Jamp", false);
            speed = speed * 2;
            speed -= speed_hold;
            speed_hold = 0;
            tyaku = 0;
        }

        if ((gamepad.Count > 0 && gamepad[num].buttonSouth.wasPressedThisFrame) && !jumpNow)
        {
            animator.SetBool("Jamp", true);
            rb.AddForce(new Vector3(0, jumpPower, 0));
            jumpNow = true;
            speed = speed / 2;
            tyaku = 1;
        }

        if (shoL > 0)
        {
            myCamera.position = follow.position;
            //myCamera.transform.DOMove(new Vector3(0f, 3f, 0f), 2f);
            Switch_LS = true;
        }
        else if (shoL == 0 && Switch_LS)
        {
            myCamera.position = cameraPos.position;
            //myCamera.transform.DOMove(new Vector3(0f, 3f, 0f), 2f);
            Switch_LS = false;
        }

        if (gamepad[num].rightStickButton.wasPressedThisFrame && (l == 0 || l == 2))
        {
            animator.SetBool("Lean_L", false);
            animator.SetBool("Lean_R", true);
            l = 1;
        }
        else if (gamepad[num].leftStickButton.wasPressedThisFrame && (l == 0 || l == 1))
        {
            animator.SetBool("Lean_R", false);
            animator.SetBool("Lean_L", true);
            l = 2;
        }

        else if ((gamepad[num].rightStickButton.wasPressedThisFrame || gamepad[num].leftStickButton.wasPressedThisFrame) && (l == 1 || l == 2))
        {
            animator.SetBool("Lean_R", false);
            animator.SetBool("Lean_L", false);
            l = 0;
        }

        if (PCounter.Pie >= 1 && !nugu && TC.countdown > 0.3f && PT.Switch == false)
        {
            animator.SetBool("BigPie", false);
            animator.SetBool("Throw", true);
        }
        if (PCounter.Pie == 0)
        {
            animator.SetBool("Throw", false);
        }
        if (PT.Switch == false)
        {
            animator.SetBool("BigPie", false);
        }

        /*if(PT.th)
        {
            animator.SetTrigger("Throw_2");
            animator.ResetTrigger("Throw_2");
        }
        else
        {
            
        }*/
    }

    private void FixedUpdate()
    {

        /*
        if(nugu == true)
        {
            Wiper++;
            animator.SetInteger("Wipe", 1);
            speed = 0;
            if(Wiper >= 100)
            {
                SH.Betyacount = 0;
                speed = 7;
                animator.SetInteger("Wipe", 0);

                if(PCounter.sPie == false)
                {
                    PCounter.sPiece++;
                }
                Wiper = 0;
                nugu = false;
            }
        }
        */
        //Debug.Log(triL);
        //Debug.Log(SH.Betyacount);
        //Debug.Log(speed);

        //拭う
        if (triL > 0 && (betyaCount > 0 || speed < 7) && jumpNow == false && nugu == false)
        {
            jumpNow = true;
            Wiper++;
            animator.SetInteger("Wipe", 1);
            animator.SetBool("Throw", false);
            PT.handPie.SetActive(false);

            speed = 0;
            nugu = true;
            if (Wiper >= 100)
            {
                betyaCount = 0;
                speed = 7;
                animator.SetInteger("Wipe", 0);

                if (PCounter.sPie == false)
                {
                    PCounter.sPiece++;
                }
                Wiper = 0;
                nugu = false;
                jumpNow = false;
            }
        }
        else if (nugu == true)
        {
            PT.handPie.SetActive(false);
            jumpNow = true;
            Wiper++;
            animator.SetInteger("Wipe", 1);
            animator.SetBool("Throw", false);

            if (Wiper >= 100)
            {
                betyaCount = 0;
                speed = 7;
                animator.SetInteger("Wipe", 0);

                if (PCounter.sPie == false)
                {
                    PCounter.sPiece++;
                }
                //spie.GetComponent<SpieGenerator>().Spie();
                Wiper = 0;
                nugu = false;
                jumpNow = false;
            }


        }

        var gamepad = Gamepad.all;
        Gravity();

        if (TC.countdown < 0.3f)
        {
            switchA = false;
            animator.SetFloat("Walk_F", 0);
            animator.SetFloat("Walk_B", 0);
            animator.SetInteger("Wipe", 0);
            animator.SetBool("Jamp", false);
            animator.SetBool("Lean_R", false);
            animator.SetBool("Lean_L", false);
            animator.SetBool("Throw", false);
            PT.handPie.SetActive(false);
            PT.handBigpie.SetActive(false);
            animator.SetBool("Throw", false);
            animator.SetBool("BigPie", false);
            //speed = 7;

        }

        if (gamepad.Count > 0 && TC.countdown > 0.3f && switchA)
        {

            //WASD,↑←↓→,左スティックの入力をVector3に代入
            var input = new Vector3(gamepad[num].leftStick.ReadValue().x, 0f, gamepad[num].leftStick.ReadValue().y);

            //カメラが見ているy軸を基準とした方向(正面)を取得
            //Quaternion.AngleAxisでVector3.up(y軸)を軸にしたカメラのy軸の角度をQuaternion型に変換している
            var horizontalRotation = Quaternion.AngleAxis(myCamera.transform.eulerAngles.y, Vector3.up);

            //Quaternion * Vector3 をすると、Quaternionの角度分Vector3の方向を回転させることができる
            //つまりここでは「カメラの正面を基準とした入力方向」を計算している
            var velocity = horizontalRotation * input;

            //キーボード操作の場合、斜め移動の際にvelocityの長さが１を超えてしまうので、その時は１に調節する
            if (velocity.magnitude > 1)
            {
                velocity = velocity.normalized;
            }

            rb.velocity = new Vector3(velocity.x * speed, rb.velocity.y, velocity.z * speed);

            animator.SetFloat("Walk_F", gamepad[num].leftStick.ReadValue().x);
            animator.SetFloat("Walk_B", gamepad[num].leftStick.ReadValue().y);
        }
        //ゲームパットが繋がっていない+1Pのときはキーボードで動かす
        else if (num == 0 && TC.countdown > 0.3f && switchA)
        {
            var input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

            var horizontalRotation = Quaternion.AngleAxis(myCamera.transform.eulerAngles.y, Vector3.up);

            var velocity = horizontalRotation * input;

            if (velocity.magnitude > 1)
            {
                velocity = velocity.normalized;
            }

            if (num == 0)
            {
                rb.velocity = new Vector3(velocity.x * speed, rb.velocity.y, velocity.z * speed);
            }
            animator.SetFloat("Walk_F", Input.GetAxis("Horizontal"));
            animator.SetFloat("Walk_B", Input.GetAxis("Vertical"));
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            jumpNow = false;
        }
    }
}
