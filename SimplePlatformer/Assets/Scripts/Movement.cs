using UnityEngine;

[RequireComponent(typeof(Player))]

public class Movement : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private DeadPlayer _deadPlayer;

    private Rigidbody2D _playerRigidbody;
    private int _horisontalMovementMaxSpeed = 5;
    private int _horisontalMovementSpeedMultiplier = 10;
    private Animator _playerAnimator;
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

    private void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody2D>();
        _playerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_player.IsDead != true)
        {
            if (Input.GetKey(KeyCode.D) && _playerRigidbody.velocity.x < _horisontalMovementMaxSpeed)
            {
                _player.transform.rotation = new Quaternion(0, _rightRotation, 0, 0);
                _playerRigidbody.velocity = new Vector2(_playerRigidbody.velocity.x + Time.deltaTime * _horisontalMovementSpeedMultiplier, _playerRigidbody.velocity.y);
                _playerAnimator.SetBool(_isRunningAnimatorParameter, true);
            }

            if (Input.GetKey(KeyCode.A) && _playerRigidbody.velocity.x > -_horisontalMovementMaxSpeed)
            {
                _player.transform.rotation = new Quaternion(0, _leftRotation, 0, 0);
                _playerRigidbody.velocity = new Vector2(_playerRigidbody.velocity.x - Time.deltaTime * _horisontalMovementSpeedMultiplier, _playerRigidbody.velocity.y);
                _playerAnimator.SetBool(_isRunningAnimatorParameter, true);
            }

            if (Input.GetKeyDown(KeyCode.Space) && _playerColissionContactsAmount > 0 && _isOnWall && _player.transform.rotation.eulerAngles.y == _rightRotation)
            {
                _playerRigidbody.velocity = new Vector2(-_jumpForceY, _jumpForceX);
                _player.transform.rotation = new Quaternion(0, _leftRotation, 0, 0);
                _playerAnimator.SetBool(_isJumpingAnimatorParameter, true);
            }
            else if (Input.GetKeyDown(KeyCode.Space) && _playerColissionContactsAmount > 0 && _isOnWall && _player.transform.rotation.eulerAngles.y == _leftRotation)
            {
                _playerRigidbody.velocity = new Vector2(_jumpForceY, _jumpForceX);
                _player.transform.rotation = new Quaternion(0, _rightRotation, 0, 0);
                _playerAnimator.SetBool(_isJumpingAnimatorParameter, true);
            }
            else if (Input.GetKeyDown(KeyCode.Space) && _playerColissionContactsAmount > 0)
            {
                _playerRigidbody.velocity = new Vector2(_playerRigidbody.velocity.x, _jumpForceX);
                _playerAnimator.SetBool(_isJumpingAnimatorParameter, true);
            }

            if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
            {
                _playerAnimator.SetBool(_isRunningAnimatorParameter, false);
            }

            _playerAnimator.SetFloat(_velocityXAnimatorParameter, _playerRigidbody.velocity.x);
            _playerAnimator.SetFloat(_velocityYAnimatorParameter, _playerRigidbody.velocity.y);
            _playerVelocityX = _playerRigidbody.velocity.x;
            _playerVelocityY = _playerRigidbody.velocity.y;
            _playerVelocity = _playerRigidbody.velocity;
            _deadPlayer.transform.position = _player.transform.position;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        _playerContactPoints = collision.contacts;
        _playerColissionContactsAmount = _playerRigidbody.GetContacts(_playerContactPoints);
        _playerAnimator.SetBool(_isFallingAnimatorParameter, false);
        _playerAnimator.SetBool(_isJumpingAnimatorParameter, false);

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
            _playerAnimator.SetBool(_isWallingAnimatorParameter, true);
        }
        else
        {
            _playerAnimator.SetBool(_isWallingAnimatorParameter, false);
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
        _playerAnimator.SetBool(_isFallingAnimatorParameter, true);
        _playerAnimator.SetBool(_isWallingAnimatorParameter, false);
        _playerAnimator.SetBool(_isJumpingAnimatorParameter, true);
    }

    private void MakePlayerDead(Vector2 velocity)
    {
        _player.SetDead();
        _player.SetAnimatorInactive();
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