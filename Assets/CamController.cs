using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    [Header("GameObjects")]
    [SerializeField] private GameObject player;
    private Transform target;
    [SerializeField] private LayerMask collisionLayer;

    [Header("Camera Settings")]
    [SerializeField] private float camDistanceDefault = 10f;
    [SerializeField] private float camHeightDefault = 5f;
    [SerializeField] private float minY = -1.5f;
    [SerializeField] private float maxY = 20f;
    private float camDistance = 10f;
    private float camHeight = 5f;
    private float mouseV = 0;
    private float zoom = 1;
    private float adjustedDistance;
    
    // Start is called before the first frame update
    void Start()
    {
        adjustedDistance = camDistance;
        target = player.transform;
        camDistance = camDistanceDefault;
        camHeight = camHeightDefault;
    }

    // Update is called once per frame
    void Update()
    {
        mouseV += Input.GetAxis("Mouse Y");
        float newHeight = camHeight-mouseV;
        if(newHeight < minY)
            newHeight = minY;
        if(newHeight > maxY)
            newHeight = maxY;
        Vector3 height = Vector3.up * newHeight;

        Vector3 expectedPosition = target.position - target.forward * camDistance + height;
        Ray ray = new Ray(target.position, (expectedPosition - target.position).normalized);
        RaycastHit hit;
        Debug.DrawLine(target.position, expectedPosition, Color.green);
        if(Physics.Raycast(ray, out hit, camDistance, collisionLayer))
            adjustedDistance = hit.distance;
        else
            adjustedDistance = camDistance;
            
        transform.position = target.position - target.forward * adjustedDistance;
        transform.position += height;

        transform.LookAt(target);

        zoom += Input.GetAxis("Mouse ScrollWheel");
        zoom = Mathf.Clamp(zoom, 0.5f, 10f);
        camDistance = camDistanceDefault * zoom;
        camHeight = camHeightDefault * zoom;
    }
}
