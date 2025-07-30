using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Platform : MonoBehaviour
{
    public AudioClip jumpSound;
    public AudioSource audioSource;
    public abstract void Start();
    public abstract void OnCollisionEnter2D(Collision2D collision);
}
