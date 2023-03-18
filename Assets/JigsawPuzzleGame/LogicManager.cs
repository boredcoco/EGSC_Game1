using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    // Keep track of how many pieces are fitted;
    private HashSet<string> isInRightPosition = new HashSet<string>();
    private int totalPoints = 0;

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

      // Increment the total number of puzzle pieces
      totalPoints += 1;
      return new Vector3(posX, posY, 0);
    }

    public void HandlePieceUpdate(string pieceId, bool inPos)
    {
      if (inPos)
      {
        isInRightPosition.Add(pieceId);
      } else
      {
        isInRightPosition.Remove(pieceId);
      }
      if (isInRightPosition.Count >= totalPoints)
      {
        SceneManager.LoadScene("GameOver");
      }
    }
}
