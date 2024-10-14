using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float arrowDamage;
    [SerializeField] private Rigidbody2D rb;
    private Vector2 direction;
    private float arrowSpeed = 10f;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("wall"))
        {
            if (other.TryGetComponent<BaseHealthScript>(out BaseHealthScript health))
            {
                health.TakeDamage(arrowDamage);
            }
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        rb.velocity = direction * arrowSpeed;
    }

    public void SetDirection(Vector2 direction)
    {
        this.direction = direction;
    }
}
