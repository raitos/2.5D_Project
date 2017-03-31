using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemySpawner))]
public class EditorScript : Editor {

    private void Calbackfunc()
    {
        EnemySpawner Spawner = target as EnemySpawner;
        Spawner.Update();
        
    }

    void OnEnable()
    {

        EditorApplication.update += Calbackfunc;
    }
    void OnDisable()
    {
        
        EditorApplication.update -= Calbackfunc;
       
    }
}
