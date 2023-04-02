using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentController : MonoBehaviour
{
    private Rigidbody[] childrenRbs;
    private TetrisBlockMovement[] childMovements;
    private TetrisBlockSpawner tetrisBlockSpawner;

    private void Start()
    {
      childrenRbs = GetComponentsInChildren<Rigidbody>();
      childMovements = GetComponentsInChildren<TetrisBlockMovement>();

      // find the block spawner
      GameObject blockSpawner = GameObject.FindWithTag("BlockSpawner");
      if (blockSpawner != null)
      {
        tetrisBlockSpawner = blockSpawner.GetComponent<TetrisBlockSpawner>();
      }
    }

    public void FreezeAllChildrenPositions()
    {
      foreach(Rigidbody rb in childrenRbs)
      {
         rb.constraints = RigidbodyConstraints.FreezePosition;
      }
      foreach(TetrisBlockMovement childMovement in childMovements)
      {
        childMovement.SetIsGrounded();
      }
      tetrisBlockSpawner.ClearCurrentBlock();
    }
}
