using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    private GameManager _gameManager;
    private GameObject _player;
    private Rigidbody _playerRigidbody;
    private Rigidbody _rigidbody;
    
    public float moveSpeed;
    public float turnSpeed;
    public float followDistance;
    public float ifNotFollowingTimeTillDeath;

    public float launchForceVertical;
    public float launchForceHorizontal;

    private bool _launchingPlayer;
    private bool _followingPlayer;
    private bool _waitingToDie;

    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _player = _gameManager.player;
        _playerRigidbody = _player.GetComponent<Rigidbody>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        _followingPlayer = Vector3.Distance(_player.transform.position, transform.position) < followDistance;

        if (!_followingPlayer)
        {
            if (!_waitingToDie)
            {
                _waitingToDie = true;
                StartCoroutine(WaitToDie(ifNotFollowingTimeTillDeath));
            }

            return;
        }

        if (_waitingToDie)
        {
            _waitingToDie = false;
            StopAllCoroutines();
        }
        
        // Look towards
        var lookPos = _player.transform.position - transform.position;
        lookPos.y = 0;

        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * turnSpeed);

        // Move towards
        _rigidbody.AddForce(transform.forward * moveSpeed);
    }

    private void FixedUpdate()
    {
        if (!_launchingPlayer) return;
        
        var lookPos = _player.transform.position - transform.position;
        lookPos.y = 0;
        lookPos *= launchForceHorizontal;
        lookPos.y = launchForceVertical;
        _playerRigidbody.AddForce(lookPos);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _launchingPlayer = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _launchingPlayer = false;
        }
    }

    private System.Collections.IEnumerator WaitToDie(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}