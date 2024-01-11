using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController _charController;
    [SerializeField]
    private float _moveSpeed = 3f, _gravity = 1f, _jumpHeight = 15f;
    [SerializeField]
    private float _horizontalMove, _verticalMove;
    private Vector3 _movePos;
    // Start is called before the first frame update
    void Start()
    {
        _charController = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        _horizontalMove = Input.GetAxisRaw("Horizontal");
        _movePos = new Vector3(_horizontalMove, 0);
        Vector3 velocity = _movePos * _moveSpeed;

        if (_charController.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _verticalMove = _jumpHeight;
            }
        }
        else
            _verticalMove -= _gravity;

        velocity.y = _verticalMove;

        _charController.Move(velocity * Time.deltaTime);

    }
}
