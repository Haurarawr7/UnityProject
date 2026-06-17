using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlantSO", menuName = "Scriptable Objects/PlantSO")]
public class PlantSO : ScriptableObject
{
    public string PlantName;
    public List<GameObject> PlantPrefabs;

    public int MaxStage { get { return PlantPrefabs.Count; } }

    public GameObject GetPlantByStage(int stage)
    {
        if (stage >= MaxStage)
        {
            return null;
        }

        return PlantPrefabs[stage - 1];
    }
}