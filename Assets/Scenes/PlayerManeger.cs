using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManeger : MonoBehaviour
{
    private Rigidbody rb;
    private float moveSpeed = 10f;
    private float cameraSpeed = 10f;
    private float jumpPower = 5f;
    private bool isJump = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Player移動の処理
        //前に進む
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(transform.forward * moveSpeed);
        }
        //後ろに進む
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(-transform.forward * moveSpeed);
        }
        //右に進む
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(transform.right * moveSpeed);
        }

        //左に進む
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(-transform.right * moveSpeed);
        }

        //スペースキーを押すとジャンプする
        if (Input.GetKey(KeyCode.Space) && !isJump)
        {
            rb.AddForce(transform.up * jumpPower, ForceMode.Impulse);
            isJump = true;
        }

    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.W) ||
            Input.GetKeyUp(KeyCode.S) ||
            Input.GetKeyUp(KeyCode.D) ||
            Input.GetKeyUp(KeyCode.A))
        {
            rb.velocity = Vector3.zero;
        }

        //マウスでカメラの視点を操作する
        if (Input.GetMouseButton(0))
        {
            float x = Input.GetAxis("Mouse X") * cameraSpeed;

            if (Mathf.Abs(x) > 0.1f)
            {
                transform.RotateAround(transform.position, Vector3.up, x);
            }
            float Y = Input.GetAxis("Mouse Y") * cameraSpeed;
            if (Mathf.Abs(Y) > 0.1f)
            {
                transform.RotateAround(transform.position, Vector3.up, Y);
            }
        }
    }

    // Ground着地したらisJumpをfalseにする
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJump = false;
        }
    }
}
