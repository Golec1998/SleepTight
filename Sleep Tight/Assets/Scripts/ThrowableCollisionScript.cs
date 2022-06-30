using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableCollisionScript : MonoBehaviour
{

    public Transform kid;

    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.white);
        }

        if (collision.relativeVelocity.magnitude > 0.5f)
        {
            Debug.Log("Velocity: " + (collision.relativeVelocity.magnitude / Vector3.Distance(kid.position, transform.position)));
            kid.GetComponent<KidController>().getSleepDamage((collision.relativeVelocity.magnitude / Vector3.Distance(kid.position, transform.position)) * 6f);
            kid.GetComponent<KidController>().getComfortDamage((collision.relativeVelocity.magnitude / Vector3.Distance(kid.position, transform.position)) * 4f);
        }
    }

}
