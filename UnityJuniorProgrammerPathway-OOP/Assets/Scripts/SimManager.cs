using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimManager : MonoBehaviour
{
    [SerializeField] private GameObject groundGenerator;
    private GameObject world;

    public void GenerateWorld() {
        if (world != null) {
            Destroy(world);
        }        
        world = Instantiate (groundGenerator, transform.position, Quaternion.identity);
    }
}
