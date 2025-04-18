using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Mode", menuName = "Difficulty Modes", order = 1)]
public class Settings : ScriptableObject
{
    [Range(0, 10f)]public float minSpeed, maxSpeed;
    [SerializeField, Range(0, 1f)] float speedIncrement;
    [SerializeField] double timeBTWspawns;
}
