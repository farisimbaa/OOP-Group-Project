using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    public string PlayerTag { get; }
    public abstract void OnTriggerEnter2D(Collider2D other);
}