#if UNITY_EDITOR

using UnityEditor;

[CustomEditor(typeof(ItemObject))]
public class ItemObjectEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ItemObject itemObject = (ItemObject)target;

        if (itemObject._itemType == ItemType.SpeedBoost)
        {
            itemObject.SetSpeedBoostAmount(EditorGUILayout.IntField("Speed Boost Amount", itemObject.speedBoostAmount));
        }
    }
}

#endif