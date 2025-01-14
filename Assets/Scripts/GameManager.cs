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

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            OnGameClearAction?.Invoke();
        }
    }
}
