using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
    private Transform target;

    public float waitTime;
    private float currentTime;
    private bool shot;

    //Methods
    public void Start()
    {
        player = GameObject.FindWithTag("Player");

        cameraHolder = this.transform.GetChild(0);
        projectileSpawnPoint = cameraHolder.GetChild(2);
    }

    public void Update()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        if (!projectileSpawnPoint)
        {
            cameraHolder = this.transform.GetChild(0);
            projectileSpawnPoint = cameraHolder.GetChild(2);
        }

        if (health <= 0)
        {
            Die();
        }

        this.transform.LookAt(player.transform);

        if (currentTime == 0)
            Shoot();

        if (shot && currentTime < waitTime)
            currentTime += 1 * Time.deltaTime;

        if (currentTime >= waitTime)
            currentTime = 0;
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
