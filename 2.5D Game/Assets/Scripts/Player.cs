using UnityEngine;

public class Player : MonoBehaviour
{
    //Cache our character controller
    private CharacterController _charController;
    //create some variables for designer friendly movements
    [SerializeField]
    private float _moveSpeed = 3f, _gravity = 1f, _jumpHeight = 15f, _dblJmpHeight;

    //create variable to store changing input values
    [SerializeField]
    private float _horizontalMove, _verticalMove;
    [SerializeField]
    private bool _canDoubleJump = true;
    
    //vector3 variable needed for characterController Move Function
    private Vector2 _movePos;
    // Start is called before the first frame update
    void Start()
    {
        //grab Character controller component in Start method and assign it to our variable.
        _charController = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        // grab input from the input class 
        _horizontalMove = Input.GetAxisRaw("Horizontal");        
        
        //assign a new vector2 to our vector2 
        _movePos = new Vector2(_horizontalMove, 0);
        
        //created a vector called velocity for further control. Velocity = Direction * Speed
        Vector2 velocity = _movePos * _moveSpeed;


        //Character Controler builtin function to Check to see if the character is standing on ground.
        if (_charController.isGrounded)
        {
            if (!_canDoubleJump)
                _canDoubleJump = true;
            
            //check to see if the space key has been pressed this frame and set the _vertical move to our jump height
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _verticalMove = _jumpHeight;
            }
        }
        else // else block meaning we aren't on the ground
        {
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (!_canDoubleJump) return; // return if _candoubleJump is equal to false;
                _verticalMove += _dblJmpHeight;
                _canDoubleJump = false;
            }

            //if our character isn't on the ground apply gravity
            _verticalMove -= _gravity;
        }
        
        //set the velocity y position after all of the calculations have been made
        velocity.y = _verticalMove;

        
        //move the controller/player after all the calculations have been made
        _charController.Move(velocity * Time.deltaTime);
    }
}
