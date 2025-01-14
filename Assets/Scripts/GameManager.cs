using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public event Action OnGameClearAction;
    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Application.targetFrameRate = 120;
    }

    public void Goal()
    {
        OnGameClearAction?.Invoke();
    }
}
