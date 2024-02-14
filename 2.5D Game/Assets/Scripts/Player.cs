using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //Cache our character controller
    private CharacterController _charController;
    //create some variables for designer friendly movements
    [SerializeField]
    private float _moveSpeed = 3f, _gravity = 1f, _jumpHeight = 15f, _dblJmpHeight, _frameRate = 1f;
    
    [SerializeField]
    private UIManager2 _uiManager;

    //create variable to store changing input values
    private float _horizontalMove, _yVelocity;
    [SerializeField]
    private bool _canDoubleJump = true;

    [SerializeField]
    private int coins = 0, _lives = 3;

    //vector3 variable needed for characterController Move Function
    private Vector3 _movePos;    
    private Vector3 velocity;


    //adding player controls
    PlayerInput _input;
    float _horizontal;

    // Start is called before the first frame update
    void Start()
    {
        //cache input actions
        _input = new PlayerInput();
        _input.Player.Enable();
        //grab Character controller component in Start method and assign it to our variable.
        _charController = GetComponent<CharacterController>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager2>();
        if (_uiManager == null)
        {
            Debug.LogError("The UI Manager is NULL.");
        }

        _uiManager.UpdateCoinDisplay(coins);
        _uiManager.UpdateLivesDisplay(_lives);

    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = _frameRate;
        // grab input from the input class 
        _horizontalMove = _input.Player.Move.ReadValue<float>();

        //assign a new vector2 to our vector2 
        _movePos = new Vector3(_horizontalMove, 0, 0);
        //created a vector called velocity for further control. Velocity = Direction * Speed
        velocity = _movePos * _moveSpeed;

        //Character Controler builtin function to Check to see if the character is standing on ground.
        if (_charController.isGrounded == true)
        {

            if (!_canDoubleJump)
                _canDoubleJump = true;


            //check to see if the space key has been pressed this frame and set the _vertical move to our jump height
            if (_input.Player.Jump.WasPressedThisFrame())
            {
                _yVelocity = _jumpHeight;
            }
        }
        else // else block meaning we aren't on the ground
        {

            if (_input.Player.Jump.WasPressedThisFrame())
            {
                if (!_canDoubleJump) return; // return if _candoubleJump is equal to false;
                _yVelocity = _dblJmpHeight;
                _canDoubleJump = false;
            }

            //if our character isn't on the ground apply gravity
            _yVelocity -= _gravity;
        }
        //set the velocity y position after all of the calculations have been made
        velocity.y = _yVelocity;


        //move the controller/player after all the calculations have been made
        _charController.Move(velocity * Time.deltaTime);


    }   



    #region Methods


    public void AddCoin(int value = 1)
    {
        coins += value;
        _uiManager.UpdateCoinDisplay(coins);
    }

    public void Damage()
    {
        _lives--;
        _uiManager.UpdateLivesDisplay(_lives);

        if(_lives < 1)
        {
            SceneManager.LoadScene(0);
        }
    }
    #endregion
}
