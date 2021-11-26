using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraplayer : MonoBehaviour
{
    public Transform objectToThrow;
    public Transform playerCam;
    public bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        if(playerCam == null)
        {
            Camera cam = transform.GetComponentInChildren<Camera>();
            playerCam = cam.transform;
            
        }

        /* Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; */

    }

    private void Update()
    {
        Quaternion lastRotation = playerCam.rotation;

        float rot = Input.GetAxis("Mouse Y") * -10;
        Quaternion q = Quaternion.AngleAxis(rot,playerCam.right);
        playerCam.rotation = q * playerCam.rotation;

        Vector3 forwardCam = playerCam.forward;
        Vector3 forwardPlayer = transform.forward;

        float regardeDevant = Vector3.Dot(forwardCam, forwardPlayer);

        if(regardeDevant < 0.0f)
           playerCam.rotation = lastRotation;


        rot = Input.GetAxis("Mouse X") * 10;
        q = Quaternion.AngleAxis(rot, transform.up);
        transform.rotation = q * transform.rotation;

        if(Input.GetButtonDown("Fire1"))
        {
            Transform obj = GameObject.Instantiate<Transform>(objectToThrow);
            obj.position = playerCam.position + playerCam.forward;
            obj.GetComponent<Rigidbody>().AddForce(playerCam.forward * 10, ForceMode.Impulse);
        }

        

    }

   

    void FixedUpdate()
    {
       

        Rigidbody rb = GetComponent<Rigidbody>();
        float vert = Input.GetAxis("Vertical");
        float hori = Input.GetAxis("Horizontal");


        Vector3 horizontalVelocity = Vector3.zero;
        horizontalVelocity += vert * transform.forward * 10;
        horizontalVelocity += hori * transform.right * 10;
        rb.velocity = new Vector3(horizontalVelocity.x, rb.velocity.y,horizontalVelocity.z);

        if(rb.velocity.y < 0)
        rb.AddForce(-transform.up * 30);

        /* rb.AddForce(vert * transform.forward * 30);
        rb.AddForce(hori * transform.right * 30); */

        isGrounded = false;
        RaycastHit infos;
        bool trouve = Physics.SphereCast(transform.position + transform.up*0.1f, 0.05f, -transform.up,out infos,2);
        if (trouve && infos.distance < 0.15)
            isGrounded = true;


        if(Input.GetButton("Jump"))
        {
            if(isGrounded)
            {
                rb.AddForce(transform.up * 10, ForceMode.Impulse);
                isGrounded = false;

            }
            else
            {

                if (rb.velocity.y < 3)
                {
                    rb.AddForce(transform.up * 50);
                }
                /* else
                    rb.velocity = new Vector3 (rb.velocity.x,3,rb.velocity.z); */
            }
        }

        /* float rot = 0;

        rot = Input.GetAxis("Mouse X") * 10;
        rb.AddTorque(Vector3.up * rot); */


    }

}
