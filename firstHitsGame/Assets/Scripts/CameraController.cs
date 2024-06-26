using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Vector3 pos;
    [SerializeField] private float cameraSpeed = 4f;
    [SerializeField] private float posZ;
    [SerializeField] private float posY;
    [SerializeField] private float posX = 3f;

    private SpriteRenderer playerSpriteRenderer;

    private void Awake()
    {
        if (!player)
            player = FindObjectOfType<Hero>().transform;

        playerSpriteRenderer = player.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        pos = player.position;
        pos.z = posZ;
        pos.y += posY;

        if (playerSpriteRenderer.flipX)
        {
            pos.x += 0f;
        }
        else
        {
            pos.x += posX;
        }

        transform.position = Vector3.Lerp(transform.position, pos, cameraSpeed * Time.deltaTime);
    }
}
