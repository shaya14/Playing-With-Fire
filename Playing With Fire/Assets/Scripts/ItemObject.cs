using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public enum ItemType
{
    ExtraBomb,
    SpeedBoost,
    ExplosionRadiusBoost
}
public class ItemObject : MonoBehaviour
{
    [HideInInspector][SerializeField] private int _speedBoostAmount;
    public ItemType _itemType;
    public int speedBoostAmount => _speedBoostAmount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            switch (_itemType)
            {
                case ItemType.ExtraBomb:
                    collision.GetComponent<BombController>().AddBomb();
                    collision.GetComponent<PlayerUiHandler>().UpdateNumOfBombsText(collision.GetComponent<BombController>().numOfBombs);
                    break;
                case ItemType.SpeedBoost:
                    collision.GetComponent<PlayerMovement>().AddSpeed(_speedBoostAmount);
                    collision.GetComponent<PlayerUiHandler>().UpdateNumOfSpeedBoostsText(collision.GetComponent<PlayerMovement>().numOfSpeedBoosts);
                    break;
                case ItemType.ExplosionRadiusBoost:
                    collision.GetComponent<BombController>().AddExplosionRadius();
                    collision.GetComponent<PlayerUiHandler>().UpdateNumOfRadiusBoostsText(collision.GetComponent<BombController>().explosionRadius);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            Destroy(gameObject);
        }
    }
    public void SetSpeedBoostAmount(int amount)
    {
        _speedBoostAmount = amount;
    }
}

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
