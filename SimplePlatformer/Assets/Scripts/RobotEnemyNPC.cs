using UnityEngine;

public class RobotEnemyNPC : MonoBehaviour
{
    [SerializeField] private GameObject _floorChecker;
    [SerializeField] private Player _player;
    [SerializeField] private DeadPlayer _deadPlayer;
    
    private PlayerAnimationController _animationController;
    private float _rayLength = 0.6f;
    private float _speed = 1f;
    private float _impulseForce = 3f;
    private int _rightRotation = 0;
    private int _leftRotation = 180;
    private bool _isOnGround;
    private Vector2 _playerVelocity;
    private Rigidbody2D _playerRigidbody;

    private void Start()
    {
        _playerRigidbody = _player.GetComponent<Rigidbody2D>();
        _animationController = _player.GetComponent<PlayerAnimationController>();
    }

    // Update is called once per frame
    private void Update()
    {
        CheckFloor();
        MoveEnemyRight();
        CheckFloor();
        MoveEnemyLeft();
        _playerVelocity = _playerRigidbody.velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            _animationController.SetAnimatorInactive();
            _deadPlayer.transform.position = _player.transform.position;
            _player.gameObject.SetActive(false);
            _deadPlayer.gameObject.SetActive(true);

            foreach (var bodyPart in _deadPlayer.GetComponentsInChildren<Rigidbody2D>())
            {
                bodyPart.AddForce(-_playerVelocity * _impulseForce, ForceMode2D.Impulse);
            }
        }
    }

    private bool CheckFloor()
    {
        RaycastHit2D floor = Physics2D.Raycast(_floorChecker.transform.position, -_floorChecker.transform.up, _rayLength);
        if (floor.collider != null && floor.collider.TryGetComponent(out Ground ground) == true)
        {
            return _isOnGround = true;
        }
        else
        {
            return _isOnGround = false;
        }
    }

    private void MoveEnemyRight()
    {
        if (transform.eulerAngles.y == _rightRotation)
        {
            if (_isOnGround)
            {
                transform.Translate(Vector3.right * _speed * Time.deltaTime);
            }
            else
            {
                transform.rotation = new Quaternion(0, _leftRotation, 0, 0);
            }
        }
    }

    private void MoveEnemyLeft()
    {
        if (transform.eulerAngles.y == _leftRotation)
        {
            if (_isOnGround)
            {
                transform.Translate(Vector3.left * _speed * -Time.deltaTime);
            }
            else
            {
                transform.rotation = new Quaternion(0, _rightRotation, 0, 0);
            }
        }
    }
}
