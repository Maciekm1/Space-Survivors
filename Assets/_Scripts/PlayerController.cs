using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    public PlayerHealth PlayerHealth { get; private set; }
    public PlayerLevel PlayerLevel { get; private set; }
    public PlayerDash PlayerDash { get; private set; }
    public Rigidbody2D Rb { get; private set; }
    [SerializeField] CinemachineVirtualCamera cam;
    [SerializeField] private Collider2D col;

    // Movement
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float turnSpeed = 5f;
    private Vector2 inputVector;

    //Weapon
    [SerializeField] private PlayerWeapon current_weapon;
    private bool isShooting = false;

    //Singleton
    public static PlayerController Instance;

    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        PlayerHealth = GetComponent<PlayerHealth>();
        PlayerLevel = GetComponent<PlayerLevel>();
        PlayerDash = GetComponent<PlayerDash>();
    }

    private void Start()
    {
        Instance = this;
        PlayerUI.instance.UpdatePlayerLevel();

        playerInput.OnShootPressed += PlayerInput_OnShootPressed;
        playerInput.OnShootReleased += PlayerInput_OnShootReleased;
    }

    private void OnDisable()
    {
        playerInput.OnShootPressed -= PlayerInput_OnShootPressed;
        playerInput.OnShootReleased -= PlayerInput_OnShootReleased;
    }

    private void PlayerInput_OnShootReleased()
    {
        isShooting = false;
    }

    private void PlayerInput_OnShootPressed()
    {
        current_weapon.Shoot();
        isShooting = true;
    }

    private void Update()
    {
        inputVector = playerInput.GetMovementVector();
        if(isShooting)
        {
            current_weapon.Shoot();
        }
    }

    private void FixedUpdate()
    {
        // Works well with moveSpeed:4, turnspeed:0.2, drag:0.95
        Rb.AddForce(transform.up * inputVector.y * moveSpeed);
        Rb.AddTorque(-inputVector.x * turnSpeed);
    }

    public bool IsMoving()
    {
        return inputVector.y > 0;
    }

    public bool IsTurningRight()
    {
        return inputVector.x > 0;
    }

    public bool IsTurningLeft()
    {
        return inputVector.x < 0;
    }

    public CinemachineVirtualCamera GetCamera()
    {
        return cam;
    }
}
