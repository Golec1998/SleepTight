using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TallGuySpawn : MonoBehaviour
{

    public GameObject TallGuy;
    public Transform[] path;

    public Transform doorL;
    public Transform doorR;

    float target = 100f;
    Quaternion startAngle;
    Quaternion targetAngleL;
    Quaternion targetAngleR;
    Quaternion newAngleL;
    Quaternion newAngleR;

    void Start()
    {
        startAngle.eulerAngles = new Vector3(-90, 180, 0);
        targetAngleL.eulerAngles = new Vector3(-90, 180, target);
        targetAngleR.eulerAngles = new Vector3(-90, 180, -target);
        newAngleL = startAngle;
        newAngleR = startAngle;
    }

    void Update()
    {
        doorL.rotation = Quaternion.Slerp(doorL.rotation, newAngleL, Time.deltaTime * 2f);
        doorR.rotation = Quaternion.Slerp(doorR.rotation, newAngleR, Time.deltaTime * 1.7f);
    }

    public void spawn()
    {
        GameObject tg = Instantiate(TallGuy, transform.position, transform.rotation);
        tg.GetComponent<TallGuyAI>().setPath(path);
        /*
        doorL.Rotate(0, 0, target);
        doorR.Rotate(0, 0, -target);
        this.Invoke(() => {
            doorL.Rotate(0, 0, -target);
            doorR.Rotate(0, 0, target);
        }, 1f);
        */
        newAngleL = targetAngleL;
        newAngleR = targetAngleR;
        this.Invoke(() => {
            newAngleL = startAngle;
            newAngleR = startAngle;
        }, 1f);
    }

    private void OnDrawGizmos()
    {
        foreach(Transform point in path)
            Gizmos.DrawWireSphere(point.position, 0.2f);
    }

}
