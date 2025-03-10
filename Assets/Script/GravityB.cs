using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using FixedUpdate = UnityEngine.PlayerLoop.FixedUpdate;

public class GravityB : MonoBehaviour
{
    Rigidbody rb;
    const float G = 0.006674f;

    public static List<GravityB> planetLists;

    [SerializeField] private bool planet = false;
    [SerializeField] private int oebitSpeed = 1000;
    private void FixedUpdate()
    {
        foreach (var planet in planetLists)
        {
            if (planet != this)
                Attract(planet);

        }
    }

    private void Awake()
    {
        rb =GetComponent<Rigidbody>();
        if (planetLists == null)
        {
            planetLists = new List<GravityB>();
        }
        planetLists.Add(this);

        if (!planet)
        {
            rb. AddForce(Vector3.left * oebitSpeed);
        }
    }

    void Attract(GravityB other)
    {
       Rigidbody otherRb = other.rb;

       Vector3 direction = rb.position - otherRb.position;

       float distance = direction.magnitude;

       float forceMagnitude = G * (rb.mass * otherRb.mass)/Mathf.Pow(distance,2);
       Vector3 finalForce = forceMagnitude * direction.normalized;
       
       otherRb.AddForce(finalForce);
    }
}
