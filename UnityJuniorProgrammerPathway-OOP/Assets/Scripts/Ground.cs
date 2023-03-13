using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//[ExecuteInEditMode]
public class Ground : MonoBehaviour
{
    // ENCAPSULATION

    [Header("Ground")]
    [SerializeField] private GroundTile groundTile;
    [SerializeField] private int sizeX;
    [SerializeField] private int sizeY;

    [Header("Water holes")]
    [SerializeField] private int count;
    [SerializeField] private int size;
    private List<GameObject> groundTiles;
    private List<GameObject> holes;

    [Header("Animals")]
    [SerializeField] private List<Animal> animals;    
    [SerializeField] private int initialAnimals = 5;
    private int animalsCount;

    private void Start() {
        groundTiles = new List<GameObject>();
        holes = new List<GameObject>();
        GenerateGround();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    // ABSTRACTION

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
            int startX = (int) Mathf.Clamp(coords.x - (size - 1) / 2, -1f, sizeX);
            int startY = (int)Mathf.Clamp(coords.y - (size - 1) / 2, -1f, sizeY);            

            for (int i = 0; i < size; i++) {
                for (int j = 0; j < size; j++) {
                    int currentX = startX + i;
                    int currentY = startY + j;

                    if (!(currentX < 0) && !(currentY < 0) && !(currentX > sizeX) && !(currentY > sizeY)) {                        
                        foreach (GameObject tile in groundTiles) {
                            if (tile.GetComponent<GroundTile>().GetCoords() == new Vector2Int(currentX, currentY)) {
                                tile.GetComponent<GroundTile>().SetType(GroundTile.Type.Water); 
                            }
                        }
                    }
                }
            }
        }
        CheckSand();
    }

    private void CheckSand() {
        foreach (GameObject tile in groundTiles) {
            CheckNeighbours(tile);
        }
    }

    private void CheckNeighbours(GameObject hole) {        

        Vector2Int coords = hole.GetComponent<GroundTile>().GetCoords();
        int startX = (int)Mathf.Clamp(coords.x - 1, -1f, sizeX);
        int startY = (int)Mathf.Clamp(coords.y - 1, -1f, sizeY);

        for (int i = 0; i < 3; i++) {
            for (int j = 0; j < 3; j++) {
                int currentX = startX + i;
                int currentY = startY + j;

                if (!(currentX < 0) && !(currentY < 0) && !(currentX > sizeX) && !(currentY > sizeY)) {
                    foreach (GameObject tile in groundTiles) {
                        if (tile.GetComponent<GroundTile>().GetCoords() == new Vector2Int(currentX, currentY)) {
                            if (tile.GetComponent<GroundTile>().GetTileType() == GroundTile.Type.Water) {
                                if (hole.GetComponent<GroundTile>().GetTileType() == GroundTile.Type.Soil) {
                                    hole.GetComponent<GroundTile>().Sand();
                                }
                            }
                        }
                    }
                }
            }
        }

        SpawnAnimals();
    }

    private void SpawnAnimals() {
        while (animalsCount < initialAnimals) {
            int randomX = UnityEngine.Random.Range(0, sizeX);
            int randomY = UnityEngine.Random.Range(0, sizeY);

            int count = animals.Count;
            int randomAnimalIndex = (int) UnityEngine.Random.Range(0, count);

            foreach (GameObject tile in groundTiles) {
                if (tile.GetComponent<GroundTile>().GetCoords() == new Vector2Int(randomX, randomY)) {
                    if (tile.GetComponent<GroundTile>().GetTileType() == GroundTile.Type.Soil) {
                        Instantiate(animals[randomAnimalIndex].gameObject, tile.transform.position, Quaternion.identity, transform);
                        animalsCount++;
                    }
                }
            }
        }
    }
}
