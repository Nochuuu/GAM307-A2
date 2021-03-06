﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    // Variables
    public float movementSpeed;
    public GameObject camera;

    public GameObject playerObj;

    public GameObject projectileSpawnPoint;
    public float waitTime;
    public GameObject projectile;

    private Transform projectileSpawned;

    public float points;

    public float maxFame;
    public float fame;

    [Header("UI Elements")]
    public TextMeshProUGUI fameUI;
    public Image fameBar;

    public float healthBarSections;

    // Methods
    void Start()
    {
        fame = maxFame;
    }

    void Update()
    {
        #region Player
        //Player facing mouse
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitDist = 0.0f;

        //If plane is being hit by object
        if (playerPlane.Raycast(ray, out hitDist))
        {
            Vector3 targetPoint = ray.GetPoint(hitDist);
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
            targetRotation.x = 0;
            targetRotation.z = 0;
            playerObj.transform.rotation = Quaternion.Slerp(playerObj.transform.rotation, targetRotation, 7f * Time.deltaTime);
        }
        #endregion

        #region Movement
        //Player Movement
        if (Input.GetKey(KeyCode.W))
            transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.A))
            transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.S))
            transform.Translate(Vector3.back * movementSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.D))
            transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);
        #endregion

        #region Shooting & Death
        //Shooting
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        //Player death
        if (fame <= 0)
            Busted();
    }


    void Shoot()
    {
        projectileSpawned = Instantiate(projectile.transform, projectileSpawnPoint.transform.position, Quaternion.identity);
        projectileSpawned.rotation = projectileSpawnPoint.transform.rotation;
    }
    #endregion  


    public void Busted()
    {
        print("You've been busted!!!");
    }
}
