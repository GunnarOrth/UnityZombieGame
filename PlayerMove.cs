using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private MoveSettings _settings = null;

    [SerializeField] private float crouchSpeed = 0.1f;
    //[SerializeField] private float crouchHeight = 1.0f;
    private bool crouching;

    private Vector3 _moveDirection;
    private CharacterController _controller;
    public Camera playerCamera;
    public GameObject gun;

    private void Awake()
    {
        //crouching = Input.GetKey(KeyCode.LeftControl);
        _controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        DefaultMovement();
    }

    private void FixedUpdate()
    {
        _controller.Move(_moveDirection * Time.deltaTime);
    }

    private void Jump()
    {
        _moveDirection.y += _settings.jumpForce;
    }

    private void DefaultMovement()
    {
        if(_controller.isGrounded)
        {
            Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));


            if(input.x != 0 && input.y != 0)
            {
                input *= 0.777f;
            }
            if(Input.GetKey(KeyCode.LeftShift) && !crouching && (!Input.GetKey(KeyCode.Mouse1)))
            {
                _moveDirection.z = input.y * _settings.speed*2;
            }
            else if(Input.GetKey(KeyCode.LeftControl))
            {
                _moveDirection.z = input.y * _settings.speed*crouchSpeed;
                _moveDirection.x = input.x * _settings.speed*crouchSpeed;
                _moveDirection.y = -_settings.antiBump;
                print("walter");
            }
            else{_moveDirection.z = input.y * _settings.speed;}
            _moveDirection.x = input.x * _settings.speed;
            
            _moveDirection.y = -_settings.antiBump;

            _moveDirection = transform.TransformDirection(_moveDirection);

            if(Input.GetKey(KeyCode.Space))
            {
                Jump();
            }
        }
        else
        {
            _moveDirection.y -= _settings.gravity * Time.deltaTime;
        }
    }
}
