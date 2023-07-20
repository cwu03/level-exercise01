using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ActorController : MonoBehaviour
{
    public GameObject model;

    private Animator anim;
    private PlayerInput playerInput;
    private Rigidbody rigidbody;
    private Vector3 movingVec;
    public float movingSpeed = 2.0f;
    public float runMultiplier = 2.5f;


    // Start is called before the first frame update
    void Start()
    {
        anim = model.GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()   //1/60秒
    {
        //旋轉模型
        if(playerInput.Dmag > 0.1f )
        {
            model.transform.forward = Vector3.Slerp(model.transform.forward,playerInput.Dvec,0.3f);
        }

        //計算移動向量
        movingVec = model.transform.forward * playerInput.Dmag * movingSpeed * (playerInput.run ? runMultiplier : 1.0f);

        //播放動畫

        anim.SetFloat("forward", Mathf.Lerp(anim.GetFloat("forward"), playerInput.Dmag * (playerInput.run ? 2.0f : 1.0f),0.3f));
    }

    private void FixedUpdate()  //1/50秒
    {
        //移動
        if(movingVec != null)
        {
            //rigidbody.position += movingVec * Time.fixedDeltaTime;
            rigidbody.velocity = new Vector3(movingVec.x,rigidbody.velocity.y,movingVec.z);
        }
    }
}
