using System.Collections;
using System.Collections.Generic;
using NavMeshPlus.Components;
using UnityEngine;

public class NavMeshUpdater : MonoBehaviour
{
    private NavMeshSurface navMeshSurface;
    private void Start()
    {
        navMeshSurface = FindObjectOfType<NavMeshSurface>();
    }

    public void UpdateNavMesh()
    {
        navMeshSurface.BuildNavMesh();
    }
}
