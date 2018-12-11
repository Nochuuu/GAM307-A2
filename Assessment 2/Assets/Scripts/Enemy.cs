using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Variables
    public float health;
    public float pointsToGive;

    public GameObject player;
    public GameObject projectile;
    public Transform projectileSpawnPoint;
    private Transform projectileSpawned;
    private Transform cameraHolder;

    public float chaseDistance = 15;
    public float distToPlayer;
    public float waitTime;
    private float currentTime;
    private bool shot;
    public bool tracking;

    Patrol patrol;

    //Methods
    public void Start()
    {
        patrol = GetComponent<Patrol>();
        player = GameObject.FindWithTag("Player");

        cameraHolder = this.transform.GetChild(0);
        projectileSpawnPoint = cameraHolder.GetChild(2);
    }

    public void Update()
    {
        distToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if(distToPlayer < chaseDistance)
        {
            tracking = true;
            patrol.patrolState = Patrol.PatrolState.CHASE;
        }
        else
        {
            tracking = false;
            patrol.patrolState = Patrol.PatrolState.PATROL;
        }
       
        if(!projectileSpawnPoint)
        {
            cameraHolder = this.transform.GetChild(0);
            projectileSpawnPoint = cameraHolder.GetChild(2);
        }

        if (health <= 0)
        {
            Die();
        }

        if(tracking)
        {
            this.transform.LookAt(player.transform);

            if (currentTime == 0)
                Shoot();

            if (shot && currentTime < waitTime)
                currentTime += 1 * Time.deltaTime;

            if (currentTime >= waitTime)
                currentTime = 0;
        }
    }

    public void Die()
    {
        Destroy(this.gameObject);

        player.GetComponent<Player>().points += pointsToGive;
    }
    
    public void Shoot()
    {
        shot = true;

        projectileSpawned = Instantiate(projectile.transform, projectileSpawnPoint.transform.position, Quaternion.identity);
        projectileSpawned.rotation = this.transform.rotation;
    }
    
}
