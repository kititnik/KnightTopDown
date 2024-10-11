using System.Collections;
using System.Collections.Generic;
using NavMeshPlus.Components;
using UnityEngine;

public class NavMeshUpdater : MonoBehaviour
{
    private NavMeshSurface navMeshSurface;
    private NavMeshModifier navMeshModifier;
    private void Start()
    {
        navMeshSurface = FindObjectOfType<NavMeshSurface>();
        navMeshModifier = GetComponent<NavMeshModifier>();
    }

    public void UpdateNavMesh(int navMeshArea)
    {
        navMeshModifier.area = navMeshArea;
        navMeshSurface.BuildNavMesh();
    }
}
