using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

[ExecuteInEditMode]
public class Ground : MonoBehaviour
{
    [Header("Ground")]
    [SerializeField] private GroundTile groundTile;
    [SerializeField] private int sizeX;
    [SerializeField] private int sizeY;

    [Header("Water holes")]
    [SerializeField] private int count;
    [SerializeField] private int size;
    private List<GameObject> groundTiles;
    private List<GameObject> holes;

    private void Start() {
        groundTiles = new List<GameObject>();
        holes = new List<GameObject>();
        GenerateGround();
    }

    private void GenerateGround() {
        float tileSizeX = groundTile.gameObject.transform.localScale.x;
        float tileSizeY = groundTile.gameObject.transform.localScale.z;

        for (int i = 0; i < sizeX; i++) {
            for (int j = 0;  j < sizeY; j++) {
                Vector3 position = new Vector3 (i * tileSizeX, 0f, j * tileSizeY);
                GameObject tile = Instantiate(groundTile.gameObject, position, Quaternion.identity, gameObject.transform);
                groundTiles.Add(tile);
            }
        }

        GenerateWaterHoles();
    }

    private void GenerateWaterHoles() {
        int i = 0;
        while (i < groundTiles.Count) {

            int x = (int) UnityEngine.Random.Range(0, sizeX);
            int y = (int)UnityEngine.Random.Range(i * sizeY / count, (i + 1) * sizeY / count);

            foreach (GameObject tile in groundTiles) {
                if (tile.GetComponent<GroundTile>().GetCoords() == new Vector2Int (x, y)) {
                    tile.GetComponent<GroundTile>().SetType(GroundTile.Type.Water);
                    holes.Add(tile);
                }
            }
                
            i++;
        }
        MakeHolesBigger();
    }

    private void MakeHolesBigger() {
        foreach (GameObject hole in holes){
            Vector2Int coords = hole.GetComponent<GroundTile>().GetCoords();
            int startX = (int) Mathf.Clamp(coords.x - (size - 1) / 2, 0f, sizeX);
            int startY = (int)Mathf.Clamp(coords.y - (size - 1) / 2, 0f, sizeY);
            Debug.Log("X:" + startX + "   Y: "  + startY);

            for (int i = 0; i < size; i++) {
                for (int j = 0; j < size; j++) {
                    int currentX = startX + i;
                    int currentY = startY + j;

                    if (!(currentX < 0) && !(currentY < 0) && !(currentX > sizeX) && !(currentY > sizeY)) {
                        Debug.Log("true");
                        foreach (GameObject tile in groundTiles) {
                            if (tile.GetComponent<GroundTile>().GetCoords() == new Vector2Int(startX + i, startY + j)) {
                                tile.GetComponent<GroundTile>().SetType(GroundTile.Type.Water);
 
                                Debug.Log("set to water at X:" + currentX + "    Y:" + currentY);
                            }
                        }
                    }
                }
            }
        }
    }
}
