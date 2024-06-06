using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Vector3 pos;
    private float CameraSpeed = 3f;

    private void Awake()
    {
        if (!player)
            player = FindObjectOfType<Hero>().transform;
    }

    private void Update()
    {
        pos = player.position;
        pos.z = -10f;
        pos.y += 2f;
        pos.x += 3f;

        transform.position = Vector3.Lerp(transform.position, pos, CameraSpeed * Time.deltaTime);
    }
}
