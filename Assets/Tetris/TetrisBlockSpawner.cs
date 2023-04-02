using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisBlockSpawner : MonoBehaviour
{
    [SerializeField] private GameObject SpawnLocation;

    // Timer
    [SerializeField] private float spawnDuration = 3f;
    private float currentTime;

    // All blocks, order important
    [SerializeField] private GameObject[] pieces;
    [SerializeField] private GameObject[] spawnPosition;

    private GameObject currentBlock;

    private void Start()
    {
      currentTime = spawnDuration;
    }

    private void Update()
    {
        if (currentTime <= 0f)
        {
          spawnTetrisBlock();
          currentTime = spawnDuration;
        }
        currentTime -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
      /*
      if (currentBlock == null)
      {
         return;
      }

      // Move along x-axis
      float horizontalInput = Input.GetAxis("Horizontal");
      int operationX = horizontalInput < 0 ? 1 : horizontalInput > 0 ? -1 : 0;
      Vector3 directionX = new Vector3(operationX, 0, 0);
      if (!checkBoundsWhenMove(directionX, currentBlock.transform.position))
      {
        currentBlock.transform.position = currentBlock.transform.position + directionX;
      }

      // Move along z-axis
      float verticalInput = Input.GetAxis("Vertical");
      int operationZ = verticalInput < 0 ? 1 : verticalInput > 0 ? -1 : 0;
      Vector3 directionZ = new Vector3(0, 0, operationZ);
      if (!checkBoundsWhenMove(directionZ, currentBlock.transform.position))
      {
        currentBlock.transform.position = currentBlock.transform.position + directionZ;
      }
      */
    }

    private bool checkBoundsWhenMove(Vector3 direction, Vector3 position)
    {
      RaycastHit hit;
      if (Physics.Raycast(position, transform.TransformDirection(direction), out hit, Mathf.Infinity))
      {
        Debug.Log(hit.collider.gameObject);
          if (hit.distance < 1f)
          {
            return true;
          }
          return false;
      }
      else
      {
          return false;
      }
    }

    private void spawnTetrisBlock()
    {
      // Choose a random index
      int index = (int) Mathf.Floor(Random.Range(0f, pieces.Length - 0.1f));
      GameObject toSpawn = pieces[index];
      Vector3 spawnPos = spawnPosition[index].transform.position;

      currentBlock = Instantiate(toSpawn, spawnPos, Quaternion.identity);
    }

    public void ClearCurrentBlock()
    {
      currentBlock = null;
    }

    public bool isCurrentBlock(GameObject block)
    {
      return block == currentBlock;
    }
}
