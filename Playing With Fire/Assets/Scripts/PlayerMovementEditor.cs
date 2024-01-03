#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerMovement))]
public class PlayerMovementEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        PlayerMovement playerController = (PlayerMovement)target;

        EditorGUILayout.Space();
        playerController.showSpriteRenderers = EditorGUILayout.Toggle("Show Sprite Renderers", playerController.showSpriteRenderers);
        EditorGUILayout.Space();

        if (playerController.showSpriteRenderers)
        {
            // Draw the AnimatedSpriteRenderer fields
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_spriteRendererUp"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_spriteRendererDown"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_spriteRendererLeft"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_spriteRendererRight"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("_spriteRendererDie"));
        }

        // Apply any changes
        serializedObject.ApplyModifiedProperties();
    }

}

#endif