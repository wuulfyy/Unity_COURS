using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMover : MonoBehaviour
{
    [Tooltip("Vitesse de déplacement"), Range(1,15)]
    public float linearSpeed = 4;
    [Tooltip("Vitesse de rotation"), Range(1,5)]
    public float angularSpeed = 1;

    private Transform player;

    public Vector3 dirPlayer;

    public float life = 100;
    public bool plusFaim = false;



    public void Start()
    {

       GameObject goPlayer = GameObject.FindGameObjectWithTag("Player");
        player = goPlayer.transform;

    }

  

    void FixedUpdate()
    {
       Rigidbody rb = GetComponent<Rigidbody>();
        if(rb != null)
        {

            /* if (Input.GetButton("Fire1") && rb.velocity.magnitude < 6)
            rb.AddForce(transform.forward * 30);
            if (Input.GetButton("Fire1") && rb.angularVelocity.magnitude < 1)
            rb.AddTorque(transform.forward * 30);
             if (rb.angularVelocity.magnitude < angularSpeed)
             {
               rb.AddTorque(transform.up * 30);
             } */

            dirPlayer = player.position - transform.position;
            dirPlayer = dirPlayer.normalized;

            float angle = Vector3.SignedAngle(dirPlayer, transform.forward, transform.up);

            if (angle < -4) 
                rb.AddTorque(transform.up * 5);
            else if (angle > 4) 
                rb.AddTorque(transform.up * -5);

            if (Mathf.Abs(angle) < 10 && rb.velocity.magnitude < 3)
            {
                rb.AddForce(transform.forward * 70);
            }


            Animator anim = GetComponent<Animator>();
             if(anim != null)
             {
                 anim.SetFloat("speed", rb.velocity.magnitude); 
             } 
        }

        if (life <= 0 && !plusFaim)
        {
            //Destroy(gameObject);
            transform.localScale = new Vector3(5, 5, 5);
            transform.position += transform.up * 10;
            plusFaim = true;
        }
           

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + dirPlayer);

        }
}
