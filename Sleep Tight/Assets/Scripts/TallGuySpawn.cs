using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TallGuySpawn : MonoBehaviour
{

    public GameObject TallGuy;

    //void Start() { spawn(); }

    public void spawn() { Instantiate(TallGuy, transform.position, transform.rotation); }

}
