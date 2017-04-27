using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WeakEnemy))]
[CanEditMultipleObjects]
public class WEeditorSc : Editor {


    WeakEnemy enemy;
    bool once = true;
  
    private void Calbackfunc()
    {

        enemy = target as WeakEnemy;
        enemy.Awake();
            
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
