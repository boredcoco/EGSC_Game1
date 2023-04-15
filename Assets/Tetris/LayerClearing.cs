using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerClearing : MonoBehaviour
{
    private GameObject[] objectsOnGround = new GameObject[9];

/*
    public void clearLayer(GameObject block)
    {
      Vector3 center = block.transform.TransformPoint(Vector3.zero);
      float layer = block.transform.TransformPoint(Vector3.zero).y;

      GameObject[] allBlocks = GameObject.FindGameObjectsWithTag("Floor");
      GameObject[] blocksWithinLayer = Array.FindAll(allBlocks,
        currentBlock => Mathf.Abs(currentBlock.transform.TransformPoint(Vector3.zero).y - layer) < 0.1f
          && currentBlock.name != "Floor"
      );

      if (blocksWithinLayer.Length < 9)
      {
        return;
      }

      foreach(GameObject currentBlock in blocksWithinLayer)
      {
        Debug.Log(currentBlock.transform.position.y);
        Destroy(currentBlock);
      }

      // get all colliders above the block and move them downwards
      GameObject[] restOfBlocks = Array.FindAll(allBlocks,
        currentBlock => currentBlock.transform.position.y - layer > 0.5f);
      foreach (GameObject currentBlock in restOfBlocks)
      {
          if (currentBlock.tag == "Floor")
          {
            currentBlock.transform.position += new Vector3(0, -0.9f, 0);
          }
      }
    }
    */


    public void clearLayer(GameObject block)
    {
        Vector3 center = block.transform.TransformPoint(Vector3.zero);
        float radius = 3;
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);

        // filter for ideal yCoordinate
        float layer = block.transform.TransformPoint(Vector3.zero).y;
        Collider[] hitCollidersWithSameLayer = Array.FindAll(hitColliders,
          hitCollider => Mathf.Abs(
            hitCollider.gameObject.transform.TransformPoint(Vector3.zero).y - layer) < 0.1f
            && hitCollider.gameObject.tag == "Floor"
            && hitCollider.gameObject.name != "Floor");


        if (hitCollidersWithSameLayer.Length < 9)
        {
          return;
        }

        // clear layer
        foreach (Collider hitCollider in hitCollidersWithSameLayer)
        {
            // Debug.Log(hitCollider.gameObject.transform.TransformPoint(Vector3.zero).y);
            Destroy(hitCollider.gameObject);
        }
    }

    public void moveLayersDown(GameObject block)
    {
      Vector3 center = block.transform.TransformPoint(Vector3.zero);
      float layer = block.transform.TransformPoint(Vector3.zero).y;

      GameObject[] allBlocks = GameObject.FindGameObjectsWithTag("Floor");
      GameObject[] restOfBlocks = Array.FindAll(allBlocks,
        currentBlock => currentBlock.transform.position.y - layer > 0.5f);
      foreach (GameObject currentBlock in restOfBlocks)
      {
          if (currentBlock.tag == "Floor")
          {
            currentBlock.transform.position += new Vector3(0, -0.9f, 0);
          }
      }
    }

}
