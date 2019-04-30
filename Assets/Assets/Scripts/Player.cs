﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // config params
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float xPadding = 1f;
    [SerializeField] float yPadding = 1f;
    [SerializeField] List<Projectile> projectiles;
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 20f;
    [SerializeField] float projectileFiringPeriod = .1f;

    int currentProjectile = 0;

    float xMin;
    float xMax;
    float yMin;
    float yMax;

    bool isFiring = false;

    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);

        transform.position = new Vector2(newXPos, newYPos);
        
    }

    IEnumerator FireContinuosly(Projectile projectile)
    {
        isFiring = true;
        while (Input.GetButton("Fire1"))
        {
            var newProjectile = Instantiate(projectile.GetProjPrefab(), transform.position, Quaternion.identity) as GameObject;
            //GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
            newProjectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectile.GetProjSpeed());
            yield return new WaitForSeconds(projectile.GetProjFiringPeriod());
        }
        isFiring = false;
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1") && !isFiring)
        {
            StartCoroutine(FireContinuosly(projectiles[currentProjectile]));
         
        }
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + xPadding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - xPadding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + yPadding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - yPadding;

    }
}
