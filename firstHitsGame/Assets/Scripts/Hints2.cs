using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hints2 : MonoBehaviour
{
    [SerializeField] GameObject Text;

    void Start()
    {
        Text.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hero"))
        {
            Text.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Hero"))
        {
            Text.SetActive(false);
        }
    }
}
