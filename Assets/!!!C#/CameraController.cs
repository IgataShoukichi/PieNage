using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class CameraController : MonoBehaviour
{
    //追いかける対象の情報
    [SerializeField] Transform follow;
    [System.NonSerialized] public PlayerController PC;

    //マウス感度
    //[SerializeField] float mouseSensitivity = 1;

    public int num;

    //x軸中心の回転値、y軸中心の回転値
    float yaw, pitch;

    // Start is called before the first frame update
    void Start()
    {
        //カーソルの非表示
        Cursor.visible = false;

    }

    private void Update()
    {
        //追従対象と同じpositionに瞬間移動
        transform.position = follow.position;

        var gamepad = Gamepad.all;

        if (gamepad.Count >= 4)
        {
            if(num == 0 || num == 2)
            {
                yaw += gamepad[num].rightStick.ReadValue().x * Time.deltaTime * 200;
                pitch -= gamepad[num].rightStick.ReadValue().y * Time.deltaTime * 200;
            }
            else if(num == 1 || num == 3)
            {
                yaw += gamepad[num].rightStick.ReadValue().x * Time.deltaTime * 200;
                pitch += gamepad[num].rightStick.ReadValue().y * Time.deltaTime * 200;
            }
            //数値の制限(下限が-60,上限が60)を設定し、pitchの値が制限を超えたり下回ったら制限値を代入する
            //これをしないとカメラの上下の回転が１回転できてしまう
            pitch = Mathf.Clamp(pitch, -60, 40);

            //マウスの動かした距離をそのまま角度にしてしまう
            transform.eulerAngles = new Vector3(pitch, yaw, 0);

            if (gamepad[PC.num].buttonWest.wasPressedThisFrame)
            {
                pitch = 0;
            }

        }

    }
}
