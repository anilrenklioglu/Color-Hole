using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HoleMovement : MonoBehaviour
{
    [Header("Hole Mesh")] 
    
    [SerializeField]  MeshFilter meshFilter;
    [SerializeField] private MeshCollider meshCollider;

    [Header("Hole Vertices Radius")] 
    
    [SerializeField] Vector2 moveLimits;
    [SerializeField] float radius;
    [SerializeField] Transform holeCenter;
    [SerializeField] Transform rotatingCircle;
    
    [Space]
    [SerializeField] float moveSpeed;

    Mesh mesh;
    List<int> holeVertices;
    List<Vector3> offsets;
    int holeVerticesCount;

    private float x, y;
    private Vector3 touch, targetPosition;

    void Start()
    {
        RotatingCircleAnimation();
        Game.isMoving = false;
        Game.isGameOver = false;
        
        mesh = meshFilter.mesh;
        holeVertices = new List<int>();
        offsets = new List<Vector3>();
        
        //Find hole vertices on the mesh
        FindHoleVertices();
    }

    void RotatingCircleAnimation()
    {
        rotatingCircle.DORotate(new Vector3(90f, 0f, -90f), .2f)
            .SetEase(Ease.Linear).From(new Vector3(90f, 0f, 0f))
            .SetLoops(-1, LoopType.Incremental);
    }
    void Update()
    {
        #if UNITY_EDITOR
        //Mouse movement
        Game.isMoving = Input.GetMouseButton(0);

        if (!Game.isGameOver && Game.isMoving)
        {
            //Move Hole Center
            MoveHole();

            //Update Hole Vertices
            UpdateHoleVerticesPosition();
        }
        #else
        //mobile touch movement
        
        Game.isMoving = Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved;

        if (!Game.isGameOver && Game.isMoving)
        {
            //Move Hole Center
            MoveHole();

            //Update Hole Vertices
            UpdateHoleVerticesPosition();
        }
        #endif
    }

    void FindHoleVertices()
    {
        for (int i = 0; i < mesh.vertices.Length; i++)
        {
            float distance = Vector3.Distance(holeCenter.position, mesh.vertices[i]);

            if (distance < radius)
            {
                holeVertices.Add(i);
                offsets.Add(mesh.vertices[i] - holeCenter.position);
            }
        }

        holeVerticesCount = holeVertices.Count;
    }

    void MoveHole()
    {
        x = Input.GetAxis("Mouse X");
        y = Input.GetAxis("Mouse Y");

        touch = Vector3.Lerp(holeCenter.position, 
            holeCenter.position + 
            new Vector3(x, 0f, y),
            moveSpeed * Time.deltaTime);

        targetPosition = new Vector3(Mathf.Clamp(touch.x, -moveLimits.x, moveLimits.x),
            touch.y,
            Mathf.Clamp(touch.z, -moveLimits.y, moveLimits.y));

        holeCenter.position = targetPosition;
    }

    void UpdateHoleVerticesPosition()
    {
        Vector3[] vertices = mesh.vertices;

        for (int i = 0; i < holeVerticesCount; i++)
        {
            vertices[holeVertices[i]] = holeCenter.position + offsets[i];
        }
        
        //Update Mesh

        mesh.vertices = vertices;
        meshFilter.mesh = mesh;
        meshCollider.sharedMesh = mesh;
    }
}