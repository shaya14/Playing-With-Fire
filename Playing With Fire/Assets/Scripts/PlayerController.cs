using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PlayerInput
{
    WASD,
    ArrowKeys
}
public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerInput _playerInput;
    [SerializeField] string _playerName;
    [SerializeField] float _moveSpeed;
    [SerializeField] Bomb _bombPrefab;
    Rigidbody2D _rb;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        SpawnBomb();
    }

    void Movement()
    {
        switch (_playerInput)
        {
            case PlayerInput.WASD:
                if (Input.GetKey(KeyCode.W))
                {
                    //Move Up
                    Vector3 direction = Vector3.up * _moveSpeed * Time.deltaTime;
                    Vector3 movement = transform.position + direction;
                    _rb.MovePosition(movement);
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    //Move Left
                    Vector3 direction = Vector3.left * _moveSpeed * Time.deltaTime;
                    Vector3 movement = transform.position + direction;
                    _rb.MovePosition(movement);
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    //Move Down
                    Vector3 direction = Vector3.down * _moveSpeed * Time.deltaTime;
                    Vector3 movement = transform.position + direction;
                    _rb.MovePosition(movement);
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    //Move Right
                    Vector3 direction = Vector3.right * _moveSpeed * Time.deltaTime;
                    Vector3 movement = transform.position + direction;
                    _rb.MovePosition(movement);
                }
                break;
            case PlayerInput.ArrowKeys:
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    //Move Up
                    Vector3 direction = Vector3.up * _moveSpeed * Time.deltaTime;
                    Vector3 movement = transform.position + direction;
                    _rb.MovePosition(movement);
                }
                else if (Input.GetKey(KeyCode.LeftArrow))
                {
                    //Move Left
                    Vector3 direction = Vector3.left * _moveSpeed * Time.deltaTime;
                    Vector3 movement = transform.position + direction;
                    _rb.MovePosition(movement);
                }
                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    //Move Down
                    Vector3 direction = Vector3.down * _moveSpeed * Time.deltaTime;
                    Vector3 movement = transform.position + direction;
                    _rb.MovePosition(movement);
                }
                else if (Input.GetKey(KeyCode.RightArrow))
                {
                    //Move Right
                    Vector3 direction = Vector3.right * _moveSpeed * Time.deltaTime;
                    Vector3 movement = transform.position + direction;
                    _rb.MovePosition(movement);
                }
                break;
            default:
                break;
        }     
    }

    void SpawnBomb()
    {
        switch (_playerInput)
        {
            case PlayerInput.WASD:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    //Spawn bomb
                    var bomb = Instantiate(_bombPrefab, transform.position, Quaternion.identity);
                }
                break;
            case PlayerInput.ArrowKeys:
                if (Input.GetKeyDown(KeyCode.Keypad0))
                {
                    //Spawn bomb
                    var bomb = Instantiate(_bombPrefab, transform.position, Quaternion.identity);
                }
                break;
            default:
                break;
        }
        //Spawn bomb
    }
}
