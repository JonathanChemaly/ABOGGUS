using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleSpeed : MonoBehaviour {
    public float speedMultiplier = 0.5f; // Adjust this to change the speed of the particles

    private ParticleSystem particleSystem;

    private void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        var main = particleSystem.main; // Get the main module of the particle system

        // Slow down the speed of the particles by adjusting the main module's simulation speed
        main.simulationSpeed = speedMultiplier;
    }
}
