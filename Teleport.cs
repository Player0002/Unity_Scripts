using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//플레이어의 텔레포트 관련 클래스
public class Teleport : MonoBehaviour
{
    public bool canUse;
    public GameObject particle;
    private float time;
    public GameObject cam;
    IEnumerator SkilLCorutain()
    {
        var s = time;
        canUse = false;
        for (int i = 0; i < s*10; i++)
        {
            yield return new WaitForSeconds(1 / 10);
            time-= 0.1f;
        }
        canUse = true;
    }
    private void Awake()
    {
        canUse = true;
    }
    public float GetTime()
    {
        return time;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q) && canUse)
        {
            time = 0.3f;
            StartCoroutine("SkilLCorutain");
            var obj1 = Instantiate(particle, transform.position, transform.rotation);
            Destroy(obj1, particle.GetComponentInChildren<ParticleSystem>().main.duration);
            Vector3 forward = transform.forward;
            Ray ray = new Ray(transform.position, forward);
            Ray ray2 = new Ray(transform.position, -forward);
            Ray ray3 = new Ray(transform.position, Vector3.up);
            if (Physics.Raycast(ray, out var hit, 7))
                if (Physics.Raycast(ray2, out var hit2, 7))
                    if (Physics.Raycast(ray3, out var hit3, 7))
                    {
                        var ydistance = absVector3(hit3.point, transform.position).y;
                        transform.Translate(Vector3.up * (ydistance > 1 ? ydistance - 1 : 0));
                    }
                    else transform.Translate(Vector3.up * 7);
                else
                    transform.Translate(-Vector3.forward * 7);
            else
                transform.Translate(Vector3.forward * 7);

            var obj2 = Instantiate(particle, transform.position, transform.rotation);
            Destroy(obj2 , particle.GetComponentInChildren<ParticleSystem>().main.duration);
        }
    }
    float abs(float a) { return a > 0 ? a : a * -1; }
    Vector3 absVector3(Vector3 a, Vector3 b)
    {
        return new Vector3(abs(a.x - b.x), abs(a.y - b.y), abs(a.z - b.z));
    }
}
