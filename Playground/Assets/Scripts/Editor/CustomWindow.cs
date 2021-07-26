using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

class CustomWindow : EditorWindow
{
    GameObject newObject;
    Transform parent;

    [MenuItem("Custom/Spawner")]
    public static void ShowWindow()
    {
        GetWindow(typeof(CustomWindow));
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Select Object to Spawn", EditorStyles.boldLabel);

        if (GUILayout.Button("Empty Object"))
        {
            SpawnObject();
        }
    }

    private void SpawnObject()
    {
        parent = Selection.activeTransform;

        if (!parent)
            PrefabUtility.InstantiatePrefab(GetObject());
        else
            PrefabUtility.InstantiatePrefab(GetObject(), parent);
    }

    private GameObject GetObject()
    {
        if (!newObject)
            newObject = Resources.Load<GameObject>("Empty Object");

        return newObject;
    }
}
