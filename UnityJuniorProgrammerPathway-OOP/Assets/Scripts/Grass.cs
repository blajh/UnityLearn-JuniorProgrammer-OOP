using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : Plant
{
    // INHERITANCE

    [SerializeField] private float lifetime = 10f;
    private float lifeTimer = 0f;

    private void Update() {
        Decay();
    }

    // POLYMORPHISM

    public override void Decay() {
        lifeTimer += Time.deltaTime;
        if (lifeTimer >= lifetime) {
            Destroy(gameObject);
        }


    }

    public override void BeEaten() {

    }
}
