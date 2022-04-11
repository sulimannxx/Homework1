using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerAnimationController))]

public class Movement : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private DeadPlayer _deadPlayer;

    private Rigidbody2D _playerRigidbody;
    private int _horisontalMovementMaxSpeed = 5;
    private int _horisontalMovementSpeedMultiplier = 10;
    private string _velocityXAnimatorParameter = "VelocityX";
    private string _velocityYAnimatorParameter = "VelocityY";
    private string _isRunningAnimatorParameter = "IsRunning";
    private string _isFallingAnimatorParameter = "IsFalling";
    private string _isWallingAnimatorParameter = "IsWalling";
    private string _isJumpingAnimatorParameter = "IsJumping";
    private int _rightRotation = 0;
    private int _leftRotation = 180;
    private int _jumpForceX = 10;
    private int _jumpForceY = 5;
    private int _playerColissionContactsAmount;
    private ContactPoint2D[] _playerContactPoints;
    private bool _isOnWall = false;
    private float _playerVelocityY;
    private float _playerVelocityX;
    private float _maxVelocityToDie = -13;
    private Vector2 _playerVelocity;
    private PlayerAnimationController _animationController;

    private void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody2D>();
        _animationController = GetComponent<PlayerAnimationController>();
    }

    private void Update()
    {
        if (_player.IsDead != true)
        {
            if (Input.GetKey(KeyCode.D) && _playerRigidbody.velocity.x < _horisontalMovementMaxSpeed)
            {
                SetPlayerDirection(_rightRotation);
                _playerRigidbody.velocity = new Vector2(_playerRigidbody.velocity.x + Time.deltaTime * _horisontalMovementSpeedMultiplier, _playerRigidbody.velocity.y);
                SetAnimatorBool(_isRunningAnimatorParameter, true);
            }

            if (Input.GetKey(KeyCode.A) && _playerRigidbody.velocity.x > -_horisontalMovementMaxSpeed)
            {
                SetPlayerDirection(_leftRotation);
                _playerRigidbody.velocity = new Vector2(_playerRigidbody.velocity.x - Time.deltaTime * _horisontalMovementSpeedMultiplier, _playerRigidbody.velocity.y);
                SetAnimatorBool(_isRunningAnimatorParameter, true);
            }

            if (Input.GetKeyDown(KeyCode.Space) && _playerColissionContactsAmount > 0 && _isOnWall && _player.transform.rotation.eulerAngles.y == _rightRotation)
            {
                JumpFromWall(-_jumpForceY, _jumpForceX, _leftRotation, _isJumpingAnimatorParameter, true);
            }
            else if (Input.GetKeyDown(KeyCode.Space) && _playerColissionContactsAmount > 0 && _isOnWall && _player.transform.rotation.eulerAngles.y == _leftRotation)
            {
                JumpFromWall(_jumpForceY, _jumpForceX, _rightRotation, _isJumpingAnimatorParameter, true);
            }
            else if (Input.GetKeyDown(KeyCode.Space) && _playerColissionContactsAmount > 0)
            {
                _playerRigidbody.velocity = new Vector2(_playerRigidbody.velocity.x, _jumpForceX);
                SetAnimatorBool(_isJumpingAnimatorParameter, true);
            }

            if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
            {
                SetAnimatorBool(_isRunningAnimatorParameter, false);
            }

            SetAnimatorFloat(_velocityXAnimatorParameter, _playerRigidbody.velocity.x);
            SetAnimatorFloat(_velocityYAnimatorParameter, _playerRigidbody.velocity.y);
            _playerVelocityX = _playerRigidbody.velocity.x;
            _playerVelocityY = _playerRigidbody.velocity.y;
            _playerVelocity = _playerRigidbody.velocity;
            _deadPlayer.transform.position = _player.transform.position;
        }
    }

    private void JumpFromWall(int jumpForceY, int jumpForceX, int direction, string animationParameter, bool state)
    {
        SetPlayerJumpVelocity(jumpForceY, jumpForceX);
        SetPlayerDirection(direction);
        SetAnimatorBool(animationParameter, state);
    }

    private void SetPlayerDirection(int direction)
    {
        _player.transform.rotation = new Quaternion(0, direction, 0, 0);
    }

    private void SetAnimatorBool(string name, bool state)
    {
        _animationController.SetBool(name, state);
    }

    private void SetAnimatorFloat(string name, float number)
    {
        _animationController.SetFloat(name, number);
    }

    private void SetPlayerJumpVelocity(int velocityY, int velocityX)
    {
        _playerRigidbody.velocity = new Vector2(velocityY, velocityX);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        _playerContactPoints = collision.contacts;
        _playerColissionContactsAmount = _playerRigidbody.GetContacts(_playerContactPoints);
        SetAnimatorBool(_isFallingAnimatorParameter, false);
        SetAnimatorBool(_isJumpingAnimatorParameter, false);

        if (collision.collider.TryGetComponent(out Wall wall) == true)
        {
            _isOnWall = true;
        }
        else
        {
            _isOnWall = false;
        }

        if (_isOnWall)
        {
            SetAnimatorBool(_isWallingAnimatorParameter, true);
        }
        else
        {
            SetAnimatorBool(_isWallingAnimatorParameter, false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_playerVelocityY < _maxVelocityToDie && collision.collider.TryGetComponent(out Wall wall) == false)
        {
            MakePlayerDead(_playerVelocity);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _playerContactPoints = collision.contacts;
        _playerColissionContactsAmount = _playerRigidbody.GetContacts(_playerContactPoints);
        _isOnWall = false; 
        SetAnimatorBool(_isFallingAnimatorParameter, true);
        SetAnimatorBool(_isWallingAnimatorParameter, false);
        SetAnimatorBool(_isJumpingAnimatorParameter, true);
    }

    private void MakePlayerDead(Vector2 velocity)
    {
        _player.Die();
        _deadPlayer.transform.position = _player.transform.position;
        _deadPlayer.transform.rotation = _player.transform.rotation;
        _player.gameObject.SetActive(false);
        _deadPlayer.gameObject.SetActive(true);

        foreach (var bodyPart in _deadPlayer.GetComponentsInChildren<Rigidbody2D>())
        {
            bodyPart.velocity = velocity;
        }
      
    }
}