using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

//[ExecuteInEditMode]
public class GroundTile : MonoBehaviour
{
    [SerializeField]
    public enum Type {
        Water,
        Soil,
    }

    [Header("Type & Material")]
    [SerializeField] private Type type;
    [SerializeField] private Material soilMaterial;
    [SerializeField] private Material waterMaterial;
    [SerializeField] private Material sandMaterial;

    [Header("Plants")]
    [SerializeField] private List<Plant> plants;
    [SerializeField][Range(0f, 1f)] private float chanceTresholdPlant;
    [SerializeField] private float spawnTimePlant = 10f;
    private float timePassedPlant = 0f;
    private GameObject plant;
    private bool hasPlant;

    private Vector2Int coords;
    private MeshRenderer meshRenderer;
        
    private void Awake() {
        meshRenderer = GetComponent<MeshRenderer>();
        coords.x = (int) transform.position.x;
        coords.y = (int) transform.position.z;
        SetMaterial();
        timePassedPlant = spawnTimePlant;
    }

    private void Update() {
        if (type == Type.Water) {
            Oscillate();
        }

        if (!hasPlant) {
            if (type == Type.Soil) {
                GrowPlant();
            }
        }
    }

    private void Oscillate() {
        transform.position = new Vector3 (transform.position.x, - Mathf.PingPong(Time.time / 2, 0.25f), transform.position.z);
    }

    private void SetMaterial() {
        switch(type) {
            case Type.Water:
                meshRenderer.material = waterMaterial;
                break;
            case Type.Soil:
                meshRenderer.material = soilMaterial;
                break;
        }
    }

    public void SetType(Type type) {
        this.type = type;
        SetMaterial();
    }

    public Type GetTileType() {
        return type;
    }

    public Vector2Int GetCoords() {
        return coords;
    }

    public void Sand() {
        meshRenderer.material = sandMaterial;
    }

    private void GrowPlant() {
        int count = plants.Count;
        int randomPlantIndex = (int) Random.Range(0, count);

        float chance = Random.Range(0f, 1f);  
        
        if (timePassedPlant <= spawnTimePlant) {
            timePassedPlant += Time.deltaTime;
        }

        else if (timePassedPlant > spawnTimePlant) {
            Debug.Log("timer");
            if (chance < chanceTresholdPlant) {
                Debug.Log("chance");
                if (plants[randomPlantIndex] != null) {
                    plant = Instantiate(plants[randomPlantIndex].gameObject, transform.position, Quaternion.identity, transform);
                }
                hasPlant = true;
            }
            timePassedPlant = 0f;
        }
    }
}
