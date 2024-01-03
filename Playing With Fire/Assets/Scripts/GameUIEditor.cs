#if UNITY_EDITOR

using UnityEditor;

[CustomEditor(typeof(GameUI))]
public class UiManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GameUI uiManager = (GameUI)target;
        if (uiManager.showControlsInputs)
        {
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_p1Input"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_p1NameText"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_p1UpText"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_p1DownText"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_p1LeftText"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_p1RightText"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_p1BombText"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_p1Color"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_p2Input"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_p2NameText"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_p2UpText"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_p2DownText"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_p2LeftText"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_p2RightText"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_p2BombText"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_p2Color"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_p3Input"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_p3NameText"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_p3UpText"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_p3DownText"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_p3LeftText"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_p3RightText"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_p3BombText"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_p3Color"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_p4Input"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_p4NameText"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_p4UpText"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_p4DownText"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_p4LeftText"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_p4RightText"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_p4BombText"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_p4Color"));
        }
        serializedObject.ApplyModifiedProperties();
    }
}

#endif