using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

[ExecuteInEditMode]
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

    private Vector2Int coords;

    private MeshRenderer meshRenderer;
        
    private void Awake() {
        meshRenderer = GetComponent<MeshRenderer>();
        coords.x = (int) transform.position.x;
        coords.y = (int) transform.position.z;
    }

    private void Update() {
        SetMaterial();           
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
    }

    public Vector2Int GetCoords() {
        return coords;
    }
}
