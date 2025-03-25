using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ClickEffectSpawner : MonoBehaviour
{
    
    public static ClickEffectSpawner Instance { get; private set; }
    public InputActionReference clickAction; // Kéo "Click" từ Input System vào Inspector
    [SerializeField] private GameObject clickEffect;
    public Transform canvasTransform;

    private Camera mainCamera;
    
    private void Awake()
    {
        // Kiểm tra nếu đã có instance khác, thì hủy object mới để đảm bảo chỉ có một instance duy nhất
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Giữ lại khi chuyển Scene
    }

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        if (clickAction != null)
        {
            clickAction.action.performed += OnClick;
            clickAction.action.Enable();
        }
    }

    private void OnDisable()
    {
        if (clickAction != null)
        {
            clickAction.action.performed -= OnClick;
            clickAction.action.Disable();
        }
    }

    private void OnClick(InputAction.CallbackContext context)
    {
        Debug.Log("Click được phát hiện!");
        if (!context.performed) return;
        if (clickEffect!= null)
        {
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 10f));
            GameObject effectInstance = Instantiate(clickEffect, worldPosition, Quaternion.identity);
            // effectInstance.transform.SetParent(canvasTransform,true);
        }
    }
}
