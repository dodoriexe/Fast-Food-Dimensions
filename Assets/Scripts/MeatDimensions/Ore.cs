using UnityEngine;

public class Ore : MonoBehaviour
{
    [Header("Ore Settings")]
    public int hitPoints = 3;
    public float dropHeightOffset = 1.5f;
    public GameObject dropPrefab;

    private bool isBreaking = false;

    void OnCollisionEnter(Collision collision)
    {
        CheckHit(collision.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        CheckHit(other.gameObject);
    }

    void CheckHit(GameObject hitter)
    {
        if (!hitter.CompareTag("Pickaxe")) return;

        TakeDamage(1);
    }

    void TakeDamage(int amount)
    {
        hitPoints -= amount;

        Debug.Log($"{name} was hit! Remaining HP: {hitPoints}");

        if (hitPoints <= 0 && !isBreaking)
        {
            isBreaking = true;
            BreakOre();
        }
    }

    void BreakOre()
    {
        if (dropPrefab != null)
        {
            Instantiate(dropPrefab, transform.position + (Vector3.up * dropHeightOffset), Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
