using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Magnet : MonoBehaviour
{
    #region Singleton class : Magnet

    public static Magnet Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    #endregion

    [SerializeField] float magnetForce;

    List<Rigidbody> affectedRigidbodies = new List<Rigidbody>();

    Transform magnet;
    
    public  void Start()
    {
        magnet = transform;
        affectedRigidbodies.Clear();
    }

    public void FixedUpdate()
    {
        if (!Game.isGameOver && Game.isMoving)
        {
            foreach (Rigidbody rb in affectedRigidbodies)
            {
                rb.AddForce((magnet.position - rb.position) * magnetForce * Time.fixedDeltaTime);
            }
        }
    }

    public   void OnTriggerEnter(Collider other)
    {
        string tag = other.tag;
        if (!Game.isGameOver && (tag.Equals("Obstacle") || tag.Equals("Object")) )
        {
            AddToMagnetField(other.attachedRigidbody);
        }    
    }
    
    public   void OnTriggerExit(Collider other)
    {
        string tag = other.tag;
        if (!Game.isGameOver && (tag.Equals("Obstacle") || tag.Equals("Object")) )
        {
            RemoveFromMagnetField(other.attachedRigidbody);
        }    
    }

    public  void AddToMagnetField(Rigidbody rb)
    {
        affectedRigidbodies.Add(rb);
    }
    
    public void RemoveFromMagnetField(Rigidbody rb)
    {
        affectedRigidbodies.Remove(rb);
    }
}
