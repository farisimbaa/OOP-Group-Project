using UnityEngine;

public class ScrollItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // FindObjectOfType<Level3Manager>().CollectItem();
            Destroy(gameObject);
        }
    }
}

