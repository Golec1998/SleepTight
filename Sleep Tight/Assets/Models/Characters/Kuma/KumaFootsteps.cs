using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class KumaFootsteps : MonoBehaviour
{

    int materialVal;
    RaycastHit rh;
    string eventPath = "event:/Player/Footsteps";
    public Transform kid;
    public LayerMask lm;

    void walkSound()
    {
        playSound(0);
    }

    void runSound()
    {
        playSound(1);
        float damage = 5 / Vector3.Distance(transform.position, kid.position);
        kid.GetComponent<KidController>().getSleepDamage(damage);
    }

    void landingSound()
    {
        playSound(2);
        float damage = 10 / Vector3.Distance(transform.position, kid.position);
        kid.GetComponent<KidController>().getSleepDamage(damage);
    }

    void playSound(int movementType)
    {
        materialCheck();
        EventInstance Sound = RuntimeManager.CreateInstance(eventPath);
        RuntimeManager.AttachInstanceToGameObject(Sound, transform, GetComponent<Rigidbody>());

        Sound.setParameterByName("Terrain", materialVal);
        Sound.setParameterByName("WalkRunLanding", movementType);

        Sound.start();
        Sound.release();
    }

    void materialCheck()
    {
        if(Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z), Vector3.down, out rh, Mathf.Infinity, lm))
        {
            Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z), Vector3.down * 10f, Color.red, 5f, true);
            switch(rh.collider.tag)
            {
                case "Wood":
                    materialVal = 0;
                    break;
                case "Carpet":
                    materialVal = 1;
                    break;
                case "Tiles":
                    materialVal = 2;
                    break;
            }
            //Debug.Log(rh.collider.tag + " " + materialVal);
        }
    }

}
