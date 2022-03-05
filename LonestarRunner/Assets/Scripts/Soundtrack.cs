using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Soundtrack {
    public string m_Name;

    public AudioClip m_Clip;
    
    [Range(0f, 1f)]
    public float m_Volume;
    
    public bool m_Loop;

    [HideInInspector]
    public AudioSource m_Source;
}
