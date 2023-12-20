using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private MoveSettings _settings = null;

    [SerializeField] private float crouchSpeed = 0.1f;
    //[SerializeField] private float crouchHeight = 1.0f;

    private Vector3 _moveDirection;
    private CharacterController _controller;
    public Camera playerCamera;
    //public GameObject gun;

    public Slider slider;
    public float maxStamina = 300;
    public float staminaDrain = 0.5f;
    public float staminaRegen = 0.5f;
    public float currentStamina;

    public Transform cameraHeight;
    public Transform crouchHeight;
    public float crouchTime = .5f;
    public float elapsedTime = 0;

    [SerializeField] private AudioSource jumpSoundEffect;
    [SerializeField] private AudioSource walkSoundEffect;
    [SerializeField] private AudioSource runSoundEffect;

    private void Awake()
    {
        //crouching = Input.GetKey(KeyCode.LeftControl);
        _controller = GetComponent<CharacterController>();
        currentStamina = maxStamina;
    }
    //
    // Update is called once per frame
    void Update()
    {
        DefaultMovement();
        slider.value = currentStamina;
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
            if(Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.LeftControl) && (!Input.GetKey(KeyCode.Mouse1)) && currentStamina >1)
            {
                if(input.y > 0)
                {
                    Drain();
                    _moveDirection.z = input.y * _settings.speed*2;
                }
                else{
                    _moveDirection.z = input.y * _settings.speed;
                }
            }
            else if(Input.GetKey(KeyCode.LeftControl))
            {
                _moveDirection.z = input.y * _settings.speed*crouchSpeed;
                _moveDirection.x = input.x * _settings.speed*crouchSpeed;
                _moveDirection.y = -_settings.antiBump;
                print("walter");
                Regen();
            }
            else{
                _moveDirection.z = input.y * _settings.speed;
                Regen();
            }
            _moveDirection.x = input.x * _settings.speed;
            
            _moveDirection.y = -_settings.antiBump;

            _moveDirection = transform.TransformDirection(_moveDirection);
            if(Input.GetKey(KeyCode.Space) && currentStamina > 10)
            {
                jumpSoundEffect.Play();
                Jump();
            }
        }
        else
        {
            _moveDirection.y -= _settings.gravity * Time.deltaTime;
            currentStamina -= staminaDrain * Time.deltaTime * 4;
        }
    }

    private void Regen()
    {
        if(currentStamina <= maxStamina -.01)
        {
            currentStamina += staminaRegen * Time.deltaTime;

            if(currentStamina >= maxStamina)
            {
                currentStamina = maxStamina;
            }
        }
    }

     private void Drain()
    {
        currentStamina -= staminaDrain * Time.deltaTime;

        if(currentStamina < 0)
        {
            currentStamina = 0;
                
        }
    }

    private void camMove()
    {
        elapsedTime += Time.deltaTime;
        float percentageComplete = elapsedTime/crouchTime;



        if(Input.GetKey(KeyCode.LeftControl))
        {
            if(percentageComplete < 1)
            {
                playerCamera.transform.position = Vector3.Lerp(cameraHeight.position, crouchHeight.position, percentageComplete);
            }
            else{}
            return;
        }
        else if(Input.GetKeyUp(KeyCode.LeftControl))
        {
            if(percentageComplete < 1)
            {
                playerCamera.transform.position = Vector3.Lerp(crouchHeight.position, cameraHeight.position, percentageComplete);
            }
            return;
        }
        elapsedTime = 0;
        return;

    }
}
