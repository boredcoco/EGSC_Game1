using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisBlockSpawner : MonoBehaviour
{
    // Timer
    [SerializeField] private float spawnDuration = 3f;
    private float currentTime;

    // All blocks, order important
    [SerializeField] private GameObject[] pieces;
    [SerializeField] private GameObject[] spawnPosition;

    // list of names to load from PlayerPrefs (order same as above)
    [SerializeField] private string[] allTetrisBlockNames;

    // final list of all blocks and their positions from PlayerPrefs
    private GameObject[] finalPieces;
    private GameObject[] finalSpawnPositions;

    private GameObject currentBlock;

    // for layer clearing
    private HashSet<int> layersCleared = new HashSet<int>();

    // for score updates
    [SerializeField] private ScoreHandler scoreHandler;

    private void Start()
    {
      currentTime = spawnDuration;

      // load playerprefs into game
      List<GameObject> piecesList = new List<GameObject>();
      List<GameObject> positionsList = new List<GameObject>();

      for (int i = 0; i < allTetrisBlockNames.Length; i++)
      {
        if (PlayerPrefs.HasKey(allTetrisBlockNames[i]))
        {
          piecesList.Add(pieces[i]);
          positionsList.Add(spawnPosition[i]);
        }
      }
      finalPieces = piecesList.ToArray();
      finalSpawnPositions = positionsList.ToArray();
    }

    private void Update()
    {
        if (currentTime <= 0f && currentBlock == null)
        {
          spawnTetrisBlock();
          currentTime = spawnDuration;
        }
        currentTime -= Time.deltaTime;
    }

    private void LateUpdate()
    {
      moveLayersDown();
    }

    private void spawnTetrisBlock()
    {
      // Choose a random index
      int index = (int) Mathf.Floor(UnityEngine.Random.Range(0f, finalPieces.Length - 0.1f));
      GameObject toSpawn = finalPieces[index];
      Vector3 spawnPos = finalSpawnPositions[index].transform.position;

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

    public void RotateAlongX()
    {
      if (currentBlock == null)
      {
        return;
      }
      currentBlock.GetComponent<ParentController>().RotateAlong(Quaternion.Euler(90, 0, 0));
    }

    public void RotateAlongY()
    {
      if (currentBlock == null)
      {
        return;
      }
      currentBlock.GetComponent<ParentController>().RotateAlong(Quaternion.Euler(0, 90, 0));
    }

    public void RotateAlongZ()
    {
      if (currentBlock == null)
      {
        return;
      }
      currentBlock.GetComponent<ParentController>().RotateAlong(Quaternion.Euler(0, 0, 90));
    }

    public void SetClearedLayers(HashSet<int> layers)
    {
      layersCleared = layers;
    }

    private void moveLayersDown()
    {
      if (layersCleared.Count == 0)
      {
        return;
      }
      GameObject[] allBlocks = Array.FindAll(GameObject.FindGameObjectsWithTag("Floor"),
        currentBlock => currentBlock.name != "Floor" && currentBlock.activeSelf
      );
      int[] allEntries = new int[layersCleared.Count];
      layersCleared.CopyTo(allEntries);
      foreach (GameObject currentBlock in allBlocks)
      {
        int[] layersAbove = Array.FindAll(allEntries,
          layer => currentBlock.transform.position.y - layer > 0.5f);
        currentBlock.transform.position += new Vector3(0, -1f * layersAbove.Length, 0);
      }
      layersCleared = new HashSet<int>();

    }

    public void UpdateScore()
    {
      scoreHandler.UpdateScore();
    }
}
