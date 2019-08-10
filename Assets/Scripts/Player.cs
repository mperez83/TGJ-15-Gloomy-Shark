﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public InstanceMaster instanceMaster;

    public float pushForce;
    public float rotateSpeed;
    public float maxSpeed;

    public float strokeCooldownLength;
    float strokeCooldownTimer;

    public List<Trash> trashList;

    Rigidbody2D rb;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float rotateDirection = Input.GetAxisRaw("Horizontal");
        transform.Rotate(new Vector3(0, 0, (rotateSpeed * -rotateDirection) * Time.deltaTime));

        strokeCooldownTimer -= Time.deltaTime;
        if (strokeCooldownTimer < 0) strokeCooldownTimer = 0;

        if (Input.GetKey(KeyCode.Space))
        {
            if (strokeCooldownTimer <= 0)
            {
                strokeCooldownTimer = strokeCooldownLength;
                rb.AddForce(-transform.right * pushForce);
            }
        }

        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }



    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Collector"))
        {
            foreach (Trash trash in trashList)
            {
                instanceMaster.AddToScore(trash.GetScore());
                Destroy(trash.gameObject);
            }
        }
    }
}