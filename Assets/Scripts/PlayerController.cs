using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(CharacterController))]

public class PlayerController : MonoBehaviour
{
    private CharacterController _controller;
    private Vector3 _velocity;
    private const float k_gravity = -9.81f;

    [Header("Physic")]
    [SerializeField] private float playerSpeed;
    [SerializeField] private float gravityStrenght;
    [SerializeField] private float jumpHeight;

    private void OnValidate()
    {
        if (playerSpeed <= 0 || gravityStrenght <= 0 || jumpHeight <= 0)
            Debug.LogWarning("Physic value less or equal to zero");
    }

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {

    }

    public void UpdateWhenSelected(float input, bool hasJumped)
    {
        bool isGrounded = _controller.isGrounded;

        // avoid character controller bugs when velocity is 0
        if (isGrounded && _velocity.y <= 0)
            _velocity.y = -1f;

        // movement
        _velocity.x = input * playerSpeed;

        // jump
        if (isGrounded && hasJumped)
            _velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * k_gravity);

        // gravity
        _velocity.y += k_gravity * gravityStrenght * Time.deltaTime;

        // distance = velocity * time    
        Vector3 finalPosition = _velocity * Time.deltaTime;
        _controller.Move(finalPosition);
    }
}
