using UnityEngine;

public class DestroyWithChance : MonoBehaviour
{
    [Range(0, 1)]
    public float ChanceOfStay = 0.5f;

    void Start() {
        if (Random.value > ChanceOfStay)
            Destroy(gameObject);
    }
}
