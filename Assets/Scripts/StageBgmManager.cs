using JyCustomTool;
using UnityEngine;

public class StageBgmManager : MonoBehaviour
{
    [SerializeField]
    private AudioClip audioClip;

    private void Start()
    {
        SoundManager.Instance.PlayBGM(audioClip, loop: true);
    }
}
