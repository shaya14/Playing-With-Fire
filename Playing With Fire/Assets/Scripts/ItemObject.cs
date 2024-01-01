using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class ItemObject : MonoBehaviour
{ 
    [HideInInspector] public int _speedBoostAmount;
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
                    collision.GetComponent<PlayerUiHandler>().UpdateNumOfBombsText(collision.GetComponent<BombController>().numOfBombs);
                    //GameManager.Instance.UpdateNumOfBombsText(collision.GetComponent<BombController>().NumOfBombs);
                    break;
                case ItemType.SpeedBoost:
                    collision.GetComponent<PlayerMovement>().AddSpeed(_speedBoostAmount);
                    collision.GetComponent<PlayerUiHandler>().UpdateNumOfSpeedBoostsText(collision.GetComponent<PlayerMovement>().NumOfSpeedBoosts);
                    //GameManager.Instance.UpdateNumOfSpeedBoostsText(collision.GetComponent<PlayerMovement>().NumOfSpeedBoosts);
                    break;
                case ItemType.ExplosionRadiusBoost:
                    collision.GetComponent<BombController>().AddExplosionRadius();
                    collision.GetComponent<PlayerUiHandler>().UpdateNumOfRadiusBoostsText(collision.GetComponent<BombController>().explosionRadius);
                    //GameManager.Instance.UpdateNumOfRadiusBoostsText(collision.GetComponent<BombController>().ExplosionRadius);
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
            itemObject._speedBoostAmount = EditorGUILayout.IntField("Speed Boost Amount", itemObject._speedBoostAmount);
        }
    }
}
