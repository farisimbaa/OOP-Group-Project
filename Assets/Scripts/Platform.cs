using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Platform : MonoBehaviour
{
    public float baseJumpForce = 5.5f;
    public static float jumpForceMultiplier = 1f;
    public float EffectiveJumpForce => baseJumpForce * jumpForceMultiplier;
    
    public abstract void OnCollisionEnter2D(Collision2D collision);
}
