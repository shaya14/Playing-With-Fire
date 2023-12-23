using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void DecreceLives()
    {
        for (int i = 0; i < UiManager.Instance._lives.Length; i++)
        {
            if (UiManager.Instance._lives[i].activeSelf)
            {
                UiManager.Instance._lives[i].SetActive(false);
                break;
            }
        }
    }
}
