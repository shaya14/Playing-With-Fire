using System.Diagnostics;
using UnityEngine;

public class GameSettings : MonoBehaviour {
  [SerializeField] private int _numOfPlayers;

  private static GameSettings _instance;
  public static GameSettings instance => _instance;

  public int numOfPlayers => _numOfPlayers;

  void Awake() {
    if (_instance == null) {
      _instance = this;
      DontDestroyOnLoad(gameObject);
      return;
    }

    Destroy(gameObject);
  }

  public void SetNumOfPlayers(int numOfPlayers) {
    _numOfPlayers = numOfPlayers;
  }
}