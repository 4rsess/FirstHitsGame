using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Vector3 pos;
    private float CameraSpeed = 4f;

    private void Awake()
    {
        if (!player)
            player = FindObjectOfType<Hero>().transform;
    }

    private void Update()
    {
        pos = player.position;
        pos.z = -10f;
        pos.y += 2.5f;

        transform.position = Vector3.Lerp(transform.position, pos, CameraSpeed * Time.deltaTime);
    }
}
