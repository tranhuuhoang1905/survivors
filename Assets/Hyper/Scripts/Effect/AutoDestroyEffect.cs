using UnityEngine;

public class AutoDestroyEffect : MonoBehaviour
{
    private ParticleSystem ps;

    void Start()
    {
        ps = GetComponent<ParticleSystem>(); // Lấy Particle System trên object
        Debug.Log($"check ps************************: {ps!= null}");
    }

    void Update()
    {
        if (ps != null && !ps.IsAlive()) // Kiểm tra nếu hiệu ứng đã kết thúc
        {
            Destroy(gameObject); // Hủy object khi hiệu ứng kết thúc
        }
    }
}