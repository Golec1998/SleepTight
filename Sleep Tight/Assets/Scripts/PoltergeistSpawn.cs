using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoltergeistSpawn : MonoBehaviour
{

    public GameObject Poltergeist;
    public float scale = 1f;
    public float spinnerOffset;

    void Start()
    {
        spawn();
    }

    public void spawn()
    {
        GameObject p = Instantiate(Poltergeist, transform.position, transform.rotation);
        p.transform.localScale = new Vector3(scale, scale, scale);
        Transform sp = p.transform.FindChild("Spinner").transform;
        sp.localScale = new Vector3(1f / scale, 1f / scale, 1f / scale);
        sp.position = new Vector3(sp.position.x, sp.position.y + spinnerOffset, sp.position.z);
    }

    private void OnDrawGizmos()
    {
        
    }

}
