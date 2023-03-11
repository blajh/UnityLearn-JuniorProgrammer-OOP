using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

[ExecuteInEditMode]
public class GroundTile : MonoBehaviour
{
    [SerializeField]
    private enum Type {
        Water,
        Soil,
    }

    [Header("Type & Material")]
    [SerializeField] private Type type;
    [SerializeField] private Material soilMaterial;
    [SerializeField] private Material waterMaterial;


    private MeshRenderer meshRenderer;
        
    private void Awake() {
        meshRenderer = GetComponent<MeshRenderer>();
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
}
