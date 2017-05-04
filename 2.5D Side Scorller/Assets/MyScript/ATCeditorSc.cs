using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Attacker))]
[CanEditMultipleObjects]
public class ATCeditorSc : Editor {

    Attacker enemy;
    bool once = true;

    private void Calbackfunc()
    {

        enemy = target as Attacker;
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
