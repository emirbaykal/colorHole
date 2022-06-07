using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleMovement : MonoBehaviour
{
    [Header("Hole Mesh")] 
    [SerializeField] private MeshFilter _meshFilter;
    [SerializeField] private MeshCollider _meshCollider;

    [Header("Hole Vertices Radius")] 
    [SerializeField] private Vector2 moveLimits;
    [SerializeField] private float radius;
    [SerializeField] private Transform holeCenter;
    [SerializeField] private Transform rotatingCircle;
    
    [Space] 
    [SerializeField] private float moveSpeed;

    private Mesh _mesh;
    private List<int> holeVertices;
    private List<Vector3> offSets;
    private int holeVerticesCount;

    private float x, y;
    private Vector3 touch, targetPos;
    void Start()
    {
        GameManager.isGameover = false;
        GameManager.isMoving = false;
        
        holeVertices = new List<int>();
        offSets = new List<Vector3>();

        _mesh = _meshFilter.mesh;
        
        FindHoleVertices();
    }

    void Update()
    {
#if UNITY_EDITOR
        //MOUSE MOVE
        GameManager.isMoving = Input.GetMouseButton(0);
        if (!GameManager.isGameover && GameManager.isMoving)
        {
            MoveHole();
            UpdateHoleVerticecsPosition();
        }
        #else
        // MOBILE TOUCH MOVE

        GameManager.isMoving = Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved;
        if (!GameManager.isGameover && GameManager.isMoving)
        {
            MoveHole();
            UpdateHoleVerticecsPosition();
        }
#endif
        
        
        
    }
    
    void FindHoleVertices()
    {
        for (int i = 0; i < _mesh.vertices.Length; i++)
        {
            float distance = Vector3.Distance(holeCenter.position, _mesh.vertices[i]);

            if (distance < radius)
            {
                holeVertices.Add(i);
                offSets.Add(_mesh.vertices[i]-holeCenter.position);
            }
        }

        holeVerticesCount = holeVertices.Count;
    }

    void UpdateHoleVerticecsPosition()
    {
        Vector3[] vertices = _mesh.vertices;
        for (int i = 0; i < holeVerticesCount; i++)
        {
            vertices[holeVertices[i]] = holeCenter.position + offSets[i];
        }

        _mesh.vertices = vertices;
        _meshFilter.mesh = _mesh;
        _meshCollider.sharedMesh = _mesh;
    }
    void MoveHole()
    {
        x = Input.GetAxis("Mouse X");
        y = Input.GetAxis("Mouse Y");

        touch = Vector3.Lerp(holeCenter.position, 
            holeCenter.position + new Vector3(x, 0f, y),
            moveSpeed - Time.deltaTime
            );

        targetPos = new Vector3(
            Mathf.Clamp(touch.x,-moveLimits.x,moveLimits.x),
            touch.y,
            Mathf.Clamp(touch.z,-moveLimits.y,moveLimits.y)
        );
        
        holeCenter.position = targetPos;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(holeCenter.position,radius);
    }
}
