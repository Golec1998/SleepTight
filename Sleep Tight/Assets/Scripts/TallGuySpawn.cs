using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TallGuySpawn : MonoBehaviour
{

    public GameObject TallGuy;
    public Transform[] path;

    //void Start() { spawn(); }

    public void spawn()
    {
        GameObject tg = Instantiate(TallGuy, transform.position, transform.rotation);
        tg.GetComponent<TallGuyAI>().setPath(path);
    }

    private void OnDrawGizmos()
    {
        foreach(Transform point in path)
            Gizmos.DrawWireSphere(point.position, 0.2f);
    }

}
