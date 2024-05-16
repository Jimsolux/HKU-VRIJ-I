using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinColorGenerator : MonoBehaviour
{
    [SerializeField] private Color[] possibleColors;
    [SerializeField] private Renderer skinRenderer;

    void Start()
    {
        Material material = skinRenderer.material;

        // Replace the material in the editor with a copy of it. Otherwise Unity doesn't like changing it for some reason.
        skinRenderer.material = material;

        Color color = possibleColors[Random.Range(0, possibleColors.Length)];

        skinRenderer.material.SetColor("_SkinColor", color);
    }
}
