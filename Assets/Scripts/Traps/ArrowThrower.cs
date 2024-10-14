using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowThrower : MonoBehaviour
{
    [SerializeField] private float arrowSpawnTime;
    [SerializeField] private ArrowSO arrow;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private ArrowMoveDirection arrowMoveDirection;
    private Vector2 arrowDirection;
    private float timer;
    
    private enum ArrowMoveDirection
    {
        Left,
        Right,
        Up,
        Down
    }

    private void Awake()
    {
        switch (arrowMoveDirection)
        {
            case ArrowMoveDirection.Down:
                arrowDirection = new Vector2(0, -1);
                break;
            case ArrowMoveDirection.Left:
                arrowDirection = new Vector2(-1, 0);
                break;
            case ArrowMoveDirection.Up:
                arrowDirection = new Vector2(0, 1);
                break;
            case ArrowMoveDirection.Right:
                arrowDirection = new Vector2(1, 0);
                break;
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > arrowSpawnTime)
        {
            timer = 0;
            GameObject ar = Instantiate(arrow.arrow, spawnPoint.position, transform.rotation);
            ar.GetComponent<Arrow>().SetDirection(arrowDirection);
        }
    }
}
