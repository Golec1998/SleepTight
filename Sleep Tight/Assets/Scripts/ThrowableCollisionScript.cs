using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableCollisionScript : MonoBehaviour
{

    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.white);
        }

        if (collision.relativeVelocity.magnitude > 0.5f)
            Debug.Log(collision.relativeVelocity.magnitude);
    }

}
