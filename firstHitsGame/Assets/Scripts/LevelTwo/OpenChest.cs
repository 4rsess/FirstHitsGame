using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenChest : MonoBehaviour
{
    private GameObject chest;
    [SerializeField] GameObject Text;

    void Start()
    {
        Text.SetActive(false);
    }

    void Update()
    {
        if (chest != null && Input.GetKeyDown(KeyCode.E))
        {
            GameObject.Find("ChestClose").SetActive(false);
            GameObject.Find("ChestOpen").GetComponent<SpriteRenderer>().enabled = true;
            Text.SetActive(true);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Chest"))
        {
            Debug.Log("Вошел в зону телепортации.");
            chest = collision.gameObject;
            

        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Chest"))
        {
            if (collision.gameObject == chest)
            {
                chest = null;
               
            }
        }
    }
}
