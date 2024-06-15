using UnityEngine;

public class TeleporterHandler : MonoBehaviour
{
    private GameObject currentTeleporter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleporter"))
        {
            currentTeleporter = collision.gameObject;
            Debug.Log("����� � ���� ������������.");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Teleporter"))
        {
            if (collision.gameObject == currentTeleporter)
            {
                currentTeleporter = null;
                Debug.Log("����� �� ���� ������������.");
            }
        }
    }
}
