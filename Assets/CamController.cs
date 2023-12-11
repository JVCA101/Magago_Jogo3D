using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float camHeight = 5f;
    [SerializeField] private float camDistance = 10f;
    [SerializeField] private GameObject player;
    private float zoom = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPos = player.transform.position;

        Vector3 camPos = playerPos + new Vector3(0f, 0f, -1 * camDistance); // posição do jogador
        camPos += Vector3.up * camHeight; // ajusta a altura

        transform.position = camPos;
        transform.LookAt(player.transform);

        zoom -= Input.GetAxis("Mouse ScrollWheel");
        zoom = Mathf.Clamp(zoom, 0.5f, 10f);
        camDistance = 10f * zoom;
        camHeight = 5f * zoom;
    }
}
