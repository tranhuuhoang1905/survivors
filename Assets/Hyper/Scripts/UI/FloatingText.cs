using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{
    public float moveSpeed = 5f; // Tốc độ bay lên
    public float destroyTime = 0.5f; // Thời gian tồn tại
    [SerializeField] private TextMeshProUGUI textMesh; // Text hiển thị
    private Color textColor;

    void Start()
    {
        if (textMesh == null)
        {
            textMesh = GetComponent<TextMeshProUGUI>();
        }
        textColor = textMesh.color;
        Destroy(gameObject, destroyTime); // Xóa sau khi hết thời gian
    }
    public void Initialize(int damage)
    {
        textMesh.text = damage.ToString(); // Set text hiển thị
    }

    void Update()
    {
        transform.position += new Vector3(0, moveSpeed * Time.deltaTime, 0); // Bay lên
        textColor.a -= Time.deltaTime / destroyTime; // Mờ dần
        textMesh.color = textColor;
    }
}