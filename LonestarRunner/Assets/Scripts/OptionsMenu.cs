using UnityEngine;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour {
    public void SetVolume(float v) {
        AudioManager.instance.m_Volume = v;
    }
}
