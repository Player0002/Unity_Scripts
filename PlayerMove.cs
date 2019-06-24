using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//플레이어의 이동 담당 클래스
public class PlayerMove : MonoBehaviour
{
    public float speed;
    public GameObject camera;
    [RangeAttribute(0, 1)]
    public float operacity;
    public float camspeed;
    public bool isInvert;
    void Update()
    {
        var direction = Input.GetAxis("Horizontal") * Vector3.right +
                                Input.GetAxis("Vertical") * Vector3.forward;
        transform.Translate(direction * speed * Time.deltaTime);

        transform.Rotate((isInvert ? -1 : 1) * Vector3.up * Input.GetAxis("Mouse X") * Mathf.Lerp(0, camspeed, operacity));
        if(camera != null)
        {
            Vector3 localRotation = camera.transform.eulerAngles;
            float x = localRotation.x;
            if (x < 45 || x > 315) MoveCam();
            else if (x >= 45 && x < 300 && Input.GetAxis("Mouse Y") > 0) MoveCam();
            else if (x > 100 && x <= 315 && Input.GetAxis("Mouse Y") < 0) MoveCam();
            else camera.transform.eulerAngles = new Vector3((x >= 45 && x < 300 ? 45 : 315), localRotation.y, localRotation.z);
        }
    }
    void MoveCam()
    {
        camera.transform.Rotate(Vector3.right * (Mathf.Lerp(0, camspeed, operacity) * Input.GetAxis("Mouse Y")) * (isInvert ? 1 : -1));
    }
}