using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TallGuyResp : MonoBehaviour
{

    public GameObject TallGuy;

    //void Start() { resp(); }

    public void resp()
    {
        //Tu respimy

        Instantiate(TallGuy, transform.position, transform.rotation);
    }

}
