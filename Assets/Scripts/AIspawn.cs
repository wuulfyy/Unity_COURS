using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIspawn : MonoBehaviour
{
   
    [Tooltip("Point de spawn de l IA")]
    public Transform spawnPoint;

    [System.Serializable]
    public class Vague
    {
        public int nbSpawn;
        public Transform prefabSpawn;
        
    }

    [Tooltip("vague ennemies")]
    public Vague[] vagues = new Vague[0];
    private int currentVague = 0;
    private int nbSpawned = 0;

    private float timeSpawn = 0;
    [Range(1, 3)]
    public float timeNextSpawn = 1;
    private float timeVague = 0;
    [Range(5, 50)]
    public float timeNextVague = 1;


    // Start is called before the first frame update
    void Start()
    {
        /* Transform ai = SpawnAI();
         AddPichenette(ai, ai.forward * 5); */

        currentVague = 0;
        nbSpawned = 0;

      
    }

    Transform SpawnAI(Transform prefabAI)
    {
        Transform ai = GameObject.Instantiate<Transform>(prefabAI);
        ai.position = spawnPoint.position;
        ai.rotation = spawnPoint.rotation;
        return ai;
    }

    void AddPichenette(Transform ai,Vector3 pichenette)
    {
        Rigidbody rb = ai.GetComponent<Rigidbody>();
        rb.AddForce(pichenette,ForceMode.Impulse);
    }

   

    private Vector3 lastPichenette;

    // Update is called once per frame
    void Update()
    {

        timeVague += Time.deltaTime;
        if (timeVague >= timeNextVague)
        {
            timeVague = 0;
            currentVague++;
            nbSpawned = 0;

        }

        if(currentVague < vagues.Length)
        {
            Vague vagueNow = vagues[currentVague];
            int nbToSpawn = vagueNow.nbSpawn;
            if (nbSpawned < nbToSpawn)
            {

                timeSpawn = timeSpawn + Time.deltaTime;
                if (timeSpawn >= timeNextSpawn)
                {
                    Transform ai = SpawnAI(vagueNow.prefabSpawn);
                    nbSpawned++;
                    Vector3 pichenette = ai.forward * 10;
                    //magie
                    pichenette.x += Random.Range(-2.0f, 2.0f);
                    pichenette.y += Random.Range(0.0f, 2.0f);

                    AddPichenette(ai, pichenette);
                    lastPichenette = pichenette;
                    timeSpawn = 0;

                }
            }


        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(spawnPoint.position, spawnPoint.position + lastPichenette);
    }
}
