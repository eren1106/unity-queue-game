using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    private bool mute = false;

    void Awake(){
        if(instance == null){
            instance = this;
        }
        else{
            Destroy(gameObject);
            return;
        }
        
        DontDestroyOnLoad(gameObject);

        foreach(Sound s in sounds){
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    void Start(){
        AudioListener.pause = false;
        Loop("BGM");
    }

    public void Play(string name){
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null) return;
        s.source.Play();
    }

    public void Loop(string name){
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null) return;
        s.source.loop = true;
        s.source.Play(); 
    }

    public void Mute(){
        mute = true;
        AudioListener.pause = true;
    }

    public void Unmute(){
        mute = false;
        AudioListener.pause = false;
    }

    public bool getMute(){
        return mute;
    }
}
