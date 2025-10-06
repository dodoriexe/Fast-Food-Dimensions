using UnityEngine;

public class VacuumSucc : MonoBehaviour
{
    public GameObject succPoint;
    
    private ParticleSystem _succParticle;
    private bool _isSucking;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _succParticle = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!_isSucking &&  _succParticle.isPlaying)
        {
            _succParticle.Stop();
        }
        
        if (_isSucking)
        {
            _isSucking = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Draggable>())
        {
            Draggable draggable = other.GetComponent<Draggable>();

            if (draggable.beingHeld)
            {
                draggable.InteractLetGo();
            }

            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null && succPoint != null)
            {
                Vector3 direction = (succPoint.transform.position - other.transform.position).normalized;
                rb.AddForce(direction * 10f, ForceMode.Impulse);
                _isSucking = true;
                
                if (!_succParticle.isPlaying)
                {
                    _succParticle.Play();
                }
            }
        }
    }
}
