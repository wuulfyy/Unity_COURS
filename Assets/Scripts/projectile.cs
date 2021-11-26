using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{

    public float puissance = 30;

    public void OnCollisionEnter(Collision collision)
    {
        AIMover other = collision.gameObject.GetComponent<AIMover>();
        if(other != null)
        {
            other.life -= puissance;
            Destroy(gameObject);
        }


    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
