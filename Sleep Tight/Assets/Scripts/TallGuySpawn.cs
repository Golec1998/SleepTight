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

    [Space]
    public GameObject kid;

    void Start()
    {
        startAngle.eulerAngles = doorL.rotation.eulerAngles;
        targetAngleL.eulerAngles = new Vector3(doorL.rotation.eulerAngles.x, doorL.rotation.eulerAngles.y, doorL.rotation.eulerAngles.z + target);
        targetAngleR.eulerAngles = new Vector3(doorR.rotation.eulerAngles.x, doorR.rotation.eulerAngles.y, doorR.rotation.eulerAngles.z - target);
        newAngleL = startAngle;
        newAngleR = startAngle;
    }

    void Update()
    {
        doorL.rotation = Quaternion.Slerp(doorL.rotation, newAngleL, Time.deltaTime * 2.3f);
        doorR.rotation = Quaternion.Slerp(doorR.rotation, newAngleR, Time.deltaTime * 1.7f);
    }

    public void spawn()
    {
        GameObject tg = Instantiate(TallGuy, transform.position, transform.rotation);
        tg.GetComponent<TallGuyAI>().setPath(path);
        tg.GetComponent<TallGuyAI>().setKid(kid);
        newAngleL = targetAngleL;
        newAngleR = targetAngleR;
        this.Invoke(() => {
            newAngleL = startAngle;
            newAngleR = startAngle;
        }, 1.5f);
    }

    private void OnDrawGizmos()
    {
        foreach(Transform point in path)
            Gizmos.DrawWireSphere(point.position, 0.2f);
    }

}
