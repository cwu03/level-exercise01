using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    //玩家输入

    [Header("=========== Key Setting ==========")]
    public string keyUp = "w";
    public string keyLeft = "a";
    public string keyRight = "d";
    public string keyDown = "s";
    public string keyA;
    public string keyB;
    public string keyC;
    public string keyD;


    [Header("=========== Output Signals ==========")]

    public float DUp = 0;
    public float DRight = 0;
    public bool inputEnable = true;
    public float Dmag;
    public Vector3 Dvec;
    public bool run;

    [Header("=========== Others ==========")]
    private float targetUp = 0;
    private float targetRight = 0;
    private float currentVelocityUp;
    private float currentVelocityRight;
    private float smoothTime = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //接收玩家輸入值
        if (inputEnable)
        {
            targetUp = ((Input.GetKey(keyUp) ? 1.0f : 0f) - (Input.GetKey(keyDown) ? 1.0f : 0f));
            targetRight = ((Input.GetKey(keyRight) ? 1.0f : 0f) - (Input.GetKey(keyLeft) ? 1.0f : 0f));
        }
        else
        {
            targetUp = 0;
            targetRight = 0;
        }

        //根據輸入Damp得到Up，Right值
        DUp = Mathf.SmoothDamp(DUp, targetUp, ref currentVelocityUp, smoothTime);
        DRight = Mathf.SmoothDamp(DRight, targetRight, ref currentVelocityRight, smoothTime);
        Vector2 dvecCircle = SquareToCircle(new Vector2(DRight,DUp));
        float DUp2 = dvecCircle.y;
        float DRight2 = dvecCircle.x;

        print("DUp2 = "+DUp2);
        print("DRight2 = "+DRight2);

        //計算向量的 magnitude（長度）和Vector（方向）
        Dmag = Mathf.Sqrt((DUp2 * DUp2) +(DRight2 * DRight2));
        Dvec = transform.forward * DUp2 + transform.right * DRight2;

        //跑步鍵是否被按下
        run = Input.GetKey(keyA);

    }

    private Vector2 SquareToCircle(Vector2 input)
    {
        Vector2 output = Vector2.zero;
        output.x = input.x * Mathf.Sqrt(1 - (input.y * input.y) / 2.0f);
        output.y = input.y * Mathf.Sqrt(1 - (input.x * input.x) / 2.0f);

        return output;
    }
}
