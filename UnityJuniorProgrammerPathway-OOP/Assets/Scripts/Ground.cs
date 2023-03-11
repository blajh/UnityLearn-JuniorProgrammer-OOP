using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Ground : MonoBehaviour
{
    [Header("Ground")]
    [SerializeField] private GroundTile groundTile;
    [SerializeField] private int sizeX;
    [SerializeField] private int sizeY;

    private void Start() {
        GenerateGround();
    }

    private void GenerateGround() {
        float tileSizeX = groundTile.gameObject.transform.localScale.x;
        float tileSizeY = groundTile.gameObject.transform.localScale.z;

        for (int i = 0; i < sizeX; i++) {
            for (int j = 0;  j < sizeY; j++) {
                Vector3 position = new Vector3 (i * tileSizeX, 0f, j * tileSizeY);
                Instantiate(groundTile.gameObject, position, Quaternion.identity, gameObject.transform);
            }
        }
    }
}
