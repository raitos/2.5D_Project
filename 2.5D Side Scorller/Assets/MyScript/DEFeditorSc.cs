using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Defender))]
[CanEditMultipleObjects]
public class DEFeditorSc : Editor {


    Defender enemy;
    bool once = true;

    private void Calbackfunc()
    {

        enemy = target as Defender;
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
