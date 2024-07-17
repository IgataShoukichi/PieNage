using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeController : MonoBehaviour
{

    GameObject timer;

    public AudioClip finish;
    public AudioClip last;
    public AudioClip warning;
    AudioSource aud;

    [SerializeField] private AudioSource bgm;
    [SerializeField] private AudioSource lastb;


    [SerializeField] public GameObject camera1P;
    [SerializeField] public GameObject camera2P;
    [SerializeField] public GameObject camera3P;
    [SerializeField] public GameObject camera4P;


    [SerializeField] public GameObject player1;
    [SerializeField] public GameObject player2;
    [SerializeField] public GameObject player3;
    [SerializeField] public GameObject player4;

    [SerializeField] public GameObject Set1;
    [SerializeField] public GameObject Set2;
    [SerializeField] public GameObject Set3;
    [SerializeField] public GameObject Set4;
    [SerializeField] public GameObject Setcamera;

    [SerializeField] public GameObject pgenerator;

    [SerializeField] public Image timefill;

    [SerializeField] public GameObject cameraR;

    [SerializeField] public GameObject canvas;
    [SerializeField] public GameObject sc;

    [SerializeField] public GameObject slight_r;
    [SerializeField] public GameObject slight_b;
    [SerializeField] public GameObject slight_y;
    [SerializeField] public GameObject slight_g;

    [SerializeField] public GameObject firstLight_r;
    [SerializeField] public GameObject firstLight_b;
    [SerializeField] public GameObject firstLight_y;
    [SerializeField] public GameObject firstLight_g;

    [SerializeField] public GameObject[] mob;
    [SerializeField] public MobController[] MC;
    private CameraResult CR;

    private PieThrow PT;

    private PlayerController PC;

    // Update is called once per frame
    //カウントダウン
    public float countdown = 180.0f;

    //時間を表示するText型の変数
    public Text timeText;

    private TimerScale SC;

    bool sound,sound1;

    public GameObject pd;
    private UIDirector PD;

    public GameObject[] door;
    public Door[] DR;

    [SerializeField] PieDirector pieD;

    [SerializeField] public GameObject ms;
    private Mob_Slide MS;

    private void Start()
    {
        this.aud = GetComponent<AudioSource>();

        CR = cameraR.gameObject.GetComponent<CameraResult>();
        CR.TC = this;

        PT = player1.gameObject.GetComponent<PieThrow>();
        PT.TC = this;
        PT = player2.gameObject.GetComponent<PieThrow>();
        PT.TC = this;
        PT = player3.gameObject.GetComponent<PieThrow>();
        PT.TC = this;
        PT = player4.gameObject.GetComponent<PieThrow>();
        PT.TC = this;

        PC = player1.gameObject.GetComponent<PlayerController>();
        PC.TC = this;
        PC = player2.gameObject.GetComponent<PlayerController>();
        PC.TC = this;
        PC = player3.gameObject.GetComponent<PlayerController>();
        PC.TC = this;
        PC = player4.gameObject.GetComponent<PlayerController>();
        PC.TC = this;

        PD = pd.gameObject.GetComponent<UIDirector>();
        PD.TC = this;

        MS = ms.gameObject.GetComponent<Mob_Slide>();
        MS.TC = this;

        for (int i = 0; i < door.Length; i++)
        {
            DR[i] = door[i].gameObject.GetComponent<Door>();
            DR[i].TC = this;
        }

        for (int i = 0; i < mob.Length; i++)
        {
            MC[i] = mob[i].gameObject.GetComponent<MobController>();
            MC[i].TC = this;
        }

        SC = sc.gameObject.GetComponent<TimerScale>();
        SC.TC = this;

        slight_r.SetActive(false);
        slight_b.SetActive(false);
        slight_y.SetActive(false);
        slight_g.SetActive(false);

        this.timer = GameObject.Find("Timer");

        camera1P.SetActive(true);
        camera2P.SetActive(true);
        camera3P.SetActive(true);
        camera4P.SetActive(true);

        Set1.SetActive(false);
        Set2.SetActive(false);
        Set3.SetActive(false);
        Set4.SetActive(false);
        Setcamera.SetActive(false);

        cameraR.SetActive(false);
        canvas.SetActive(true);

        pgenerator.SetActive(true);

        player1.gameObject.GetComponent<Result_set>().enabled = false;

        sound = false;

    }


    void Update()
    {
        //時間をカウントダウンする
        countdown -= Time.deltaTime;

        //時間を表示する
        timeText.text = countdown.ToString("f0");


        if (countdown <= 29 && countdown > 28.5)
        {
            bgm.Stop();
        }

        if (countdown <= 28.5 && countdown > 28)
        {
            lastb.Play();
        }

        if(countdown <= 70 && !sound1)
        {
            this.aud.PlayOneShot(this.warning,7.0f);
            sound1 = true;
        }

        //countdownが0以下になったとき
        if (countdown <= 0)
        {
            if (!sound)
            {
                this.aud.PlayOneShot(this.finish);
                sound = true;
            }

            //体についているすべてのパイを消す
            for (int i = 0; i < pieD.pieCons1.Length; i++)
            {
                if (pieD.pieCons1[i].gameObject.activeSelf)
                {
                    pieD.pieCons1[i].gameObject.SetActive(false);
                }
            }
            for (int i = 0; i < pieD.bigpieCons.Length; i++)
            {
                if (pieD.bigpieCons[i].gameObject.activeSelf)
                {
                    pieD.bigpieCons[i].gameObject.SetActive(false);
                }
            }


            lastb.Stop();

            player1.gameObject.GetComponent<PlayerController>().enabled = false;
            player2.gameObject.GetComponent<PlayerController>().enabled = false;
            player3.gameObject.GetComponent<PlayerController>().enabled = false;
            player4.gameObject.GetComponent<PlayerController>().enabled = false;

            player1.gameObject.GetComponent<PieThrow>().enabled = false;
            player2.gameObject.GetComponent<PieThrow>().enabled = false;
            player3.gameObject.GetComponent<PieThrow>().enabled = false;
            player4.gameObject.GetComponent<PieThrow>().enabled = false;

            player1.gameObject.GetComponent<Outline>().enabled = false;
            player2.gameObject.GetComponent<Outline>().enabled = false;
            player3.gameObject.GetComponent<Outline>().enabled = false;
            player4.gameObject.GetComponent<Outline>().enabled = false;

            player1.gameObject.GetComponent<Result_set>().enabled = true;
            player2.gameObject.GetComponent<Result_set>().enabled = true;
            player3.gameObject.GetComponent<Result_set>().enabled = true;
            player4.gameObject.GetComponent<Result_set>().enabled = true;

            camera1P.SetActive(false);
            camera2P.SetActive(false);
            camera3P.SetActive(false);
            camera4P.SetActive(false);

            Set1.SetActive(true);
            Set2.SetActive(true);
            Set3.SetActive(true);
            Set4.SetActive(true);
            Setcamera.SetActive(true);

            cameraR.SetActive(true);

            canvas.SetActive(false);

            pgenerator.SetActive(false);

            obstacleDestroy();
        }
    }

    void obstacleDestroy()
    {
        GameObject[] piepiece = GameObject.FindGameObjectsWithTag("Piece");
        foreach (GameObject obs in piepiece)
        {
            obs.gameObject.SetActive(false);
        }
    }
    private void FixedUpdate()
    {
        timefill.fillAmount -= Time.deltaTime * 0.00556f;

    }
}
