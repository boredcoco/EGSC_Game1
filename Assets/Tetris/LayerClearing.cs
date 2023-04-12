using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerClearing : MonoBehaviour
{
    private GameObject[] objectsOnGround = new GameObject[9];

    public void clearLayer(GameObject block)
    {
        Vector3 center = block.transform.TransformPoint(Vector3.zero);
        float radius = 8;
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

        // get all colliders above the block and move them downwards
        Collider[] restOfColliders = Array.FindAll(hitColliders,
          hitCollider => hitCollider.gameObject.transform.position.y - layer > 0.5f);
        foreach (Collider hitCollider in restOfColliders)
        {
            if (hitCollider.gameObject.tag == "Floor")
            {
              hitCollider.gameObject.transform.position += new Vector3(0, -0.9f, 0);
            }
        }
    }

}
