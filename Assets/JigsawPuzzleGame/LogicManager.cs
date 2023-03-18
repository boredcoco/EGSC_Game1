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

    [SerializeField] private int offset = 1;

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
}
