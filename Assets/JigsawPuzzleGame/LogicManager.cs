using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicManager : MonoBehaviour
{
    // The boundaries
    [SerializeField] private Transform rightBounds;
    [SerializeField] private Transform leftBounds;
    [SerializeField] private Transform upperBounds;
    [SerializeField] private Transform lowerBounds;

    [SerializeField] private float offset = 1;

    // Boundaries for board
    [SerializeField] private float right;
    [SerializeField] private float left;
    [SerializeField] private float upper;
    [SerializeField] private float lower;

    public bool IsWithinBounds(Vector3 pos)
    {
      if (pos.x > rightBounds.position.x - offset
      || pos.x < leftBounds.position.x + offset
      || pos.y > upperBounds.position.y - offset
      || pos.y < lowerBounds.position.y + offset)
      {
        return false;
      }
      return true;
    }

    public Vector3 GenerateRandomPoint()
    {
      float posX = Random.Range(left + offset, right - offset);
      float posY = Random.Range(lower + offset, upper - offset);
      return new Vector3(posX, posY, 0);
    }
}
