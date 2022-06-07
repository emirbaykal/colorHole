using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Magnet : MonoBehaviour
{
    public static Magnet instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    [SerializeField] private float magnetForce;
    private List<Rigidbody> affectedRigidbodies = new List<Rigidbody>();
    private Transform magnet;
    void Start()
    {
        magnet = transform;
        affectedRigidbodies.Clear();
    }

    void FixedUpdate()
    {
        if (!GameManager.isGameover && GameManager.isMoving)
        {
            foreach (Rigidbody rb in affectedRigidbodies)
            {
                rb.AddForce((magnet.position-rb.position)*magnetForce*Time.fixedDeltaTime);
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        string tag = other.tag;
        if (!GameManager.isGameover && (tag.Equals("Obstancle") || tag.Equals("Object")))
        {
            AddToMagnetField(other.attachedRigidbody);
        }
    }
    public void OnTriggerExit(Collider other)
    {
        string tag = other.tag;
        if (!GameManager.isGameover && (tag.Equals("Obstancle") || tag.Equals("Object")))
        {
            RemoveFromMagnetField(other.attachedRigidbody);
        }
    }
    public void AddToMagnetField(Rigidbody rb)
    {
        affectedRigidbodies.Add(rb);
    }

    public void RemoveFromMagnetField(Rigidbody rb)
    {
        affectedRigidbodies.Remove(rb);
    }
}
