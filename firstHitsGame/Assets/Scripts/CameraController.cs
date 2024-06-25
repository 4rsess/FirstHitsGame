using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Vector3 pos;
    private float CameraSpeed = 4f;
    [SerializeField] private float posZ;
    [SerializeField] private float posY;
    [SerializeField] private float posX;

    private void Awake()
    {
        if (!player)
            player = FindObjectOfType<Hero>().transform;
    }

    private void Update()
    {
        pos = player.position;
        pos.z = posZ;
        pos.y += posY;
        pos.x += posX;

        transform.position = Vector3.Lerp(transform.position, pos, CameraSpeed * Time.deltaTime);
    }
}
