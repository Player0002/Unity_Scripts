using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//플레이어 점프 담당 클래스 ( 플레이어의 발판에 넣음 )
public class PlayerJump : MonoBehaviour
{
    public GameObject player;
    public bool isGround;
    public float jumpPower;

    public GameObject powerEffect;
    private void Start()
    {
        if (jumpPower <= 0) jumpPower = 5;
    }
    // Update is called once per frame
    void Update()
    {
        if (isGround && Input.GetKeyDown(KeyCode.Space))
        {
            player.GetComponent<Rigidbody>().AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
        else if (!isGround && Input.GetKeyDown(KeyCode.Space))
        {
            var obj = Instantiate(powerEffect, transform.position, transform.rotation);
            Destroy(obj, powerEffect.GetComponentInChildren<ParticleSystem>().main.duration);
            player.GetComponent<Rigidbody>().AddForce(player.transform.forward * 3, ForceMode.Impulse);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        isGround = true;
    }
    private void OnTriggerExit(Collider other)
    {
        isGround = false;
    }
}
