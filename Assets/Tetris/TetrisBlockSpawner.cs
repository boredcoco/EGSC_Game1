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
