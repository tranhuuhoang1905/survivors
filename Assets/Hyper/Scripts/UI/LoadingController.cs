using UnityEngine;
using UnityEngine.UI;

public class LoadingController : MonoBehaviour
{
    public static LoadingController Instance { get; private set; }
    [SerializeField] private Slider progressBar;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        StartCoroutine(GameController.Instance.LoadTargetScene()); // 🔥 Bắt đầu load scene cần thiết
    }

    public void UpdateProgress(float progress)
    {
        if (progressBar != null)
            progressBar.value = progress; // 🔥 Cập nhật thanh tiến trình
    }
}