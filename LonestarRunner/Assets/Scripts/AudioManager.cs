using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using System;

public class AudioManager : MonoBehaviour {
    public static AudioManager instance;

    [SerializeField]
    private Soundtrack[] m_Soundtracks;

    public float m_Volume;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
            return;
        }

        m_Volume = 0.2f;

        DontDestroyOnLoad(gameObject);

        foreach (Soundtrack track in m_Soundtracks) {
            track.m_Source = gameObject.AddComponent<AudioSource>();
            track.m_Source.clip = track.m_Clip;
            track.m_Source.volume = m_Volume;
            track.m_Source.loop = track.m_Loop;
        }

        //SceneManager.sceneLoaded += OnLevelLoad;
    }

    //private void OnLevelLoad(Scene scene, LoadSceneMode mode) {
    //    foreach (Soundtrack track in m_Soundtracks) {
    //        if (track.m_LevelNo == scene.buildIndex) {
    //            track.m_Source = track.m_Object.AddComponent<AudioSource>();
    //            track.m_Source.clip = track.m_Clip;
    //            track.m_Source.volume = track.m_Volume;
    //            track.m_Source.loop = track.m_Loop;
    //        }
    //    }
    //}

    private void Start() {
        Play("BGM");
    }

    private void Update() {
        foreach (var source in GetComponents<AudioSource>()) {
            source.volume = m_Volume;
        }
    }

    public void Stop(string name) {
        var sound = Array.Find(m_Soundtracks, s => s.m_Name == name); 
        if (sound == null) {
            Debug.LogWarning("Sound " + name + "doesn't exist");
            return;
        }
        sound.m_Source.Stop();
    }

    public void Play(string name) {
        var sound = Array.Find(m_Soundtracks, s => s.m_Name == name);
        if (sound == null) {
            Debug.LogWarning("Sound " + name + "doesn't exist");
            return;
        }
        sound.m_Source.Play();
    }
}
