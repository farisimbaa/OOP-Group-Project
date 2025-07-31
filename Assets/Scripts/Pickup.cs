using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    public float duration = 5f;
    [SerializeField] protected string playerTag = "Player";
    public abstract void OnTriggerEnter2D(Collider2D other);
}