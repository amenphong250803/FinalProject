
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    public float speed = 6f;

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 dir = new Vector3(h, v, 0f).normalized;
        transform.position += dir * speed * Time.deltaTime;
    }
}
