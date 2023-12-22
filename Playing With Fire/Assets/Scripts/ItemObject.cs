using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class ItemObject : MonoBehaviour
{ 
    [HideInInspector] public float _speedBoostAmount;
    public enum ItemType
    {
        ExtraBomb,
        SpeedBoost,
        ExplosionRadiusBoost
    }

    [SerializeField] public ItemType _itemType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            switch (_itemType)
            {
                case ItemType.ExtraBomb:
                    collision.GetComponent<BombController>().AddBomb();
                    break;
                case ItemType.SpeedBoost:
                    collision.GetComponent<PlayerMovement>().AddSpeed(_speedBoostAmount);
                    break;
                case ItemType.ExplosionRadiusBoost:
                    collision.GetComponent<BombController>().AddExplosionRadius();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            Destroy(gameObject);
        }
    }
}

[CustomEditor(typeof(ItemObject))]
public class ItemObjectEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ItemObject itemObject = (ItemObject)target;

        if (itemObject._itemType == ItemObject.ItemType.SpeedBoost)
        {
            itemObject._speedBoostAmount = EditorGUILayout.FloatField("Speed Boost Amount", itemObject._speedBoostAmount);
        }
    }
}
