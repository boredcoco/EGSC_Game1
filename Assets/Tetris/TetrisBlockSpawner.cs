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
      if (currentBlock == null)
      {
         return;
      }

      // Move along x-axis
      float horizontalInput = Input.GetAxis("Horizontal");
      int operationX = horizontalInput < 0 ? 1 : horizontalInput > 0 ? -1 : 0;
      Vector3 finalPosX = currentBlock.transform.position + new Vector3(operationX, 0, 0);
      currentBlock.transform.position = finalPosX;

      // Move along z-axis
      float verticalInput = Input.GetAxis("Vertical");
      int operationZ = verticalInput < 0 ? 1 : verticalInput > 0 ? -1 : 0;
      Vector3 finalPosZ = currentBlock.transform.position + new Vector3(0, 0, operationZ);
      currentBlock.transform.position = finalPosZ;
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
}
