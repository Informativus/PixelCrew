using System;
using UnityEngine;

namespace PixelCrew.Components.Audio
{
    public class PlaySoundsComponent : MonoBehaviour
    {
        [SerializeField] private AudioData[] _sounds;
        
        private AudioSource _source;

        public void Play(string id)
        {
            foreach (var audioData in _sounds)
            {
                if (audioData.Id != id) continue;

                if (_source == null)
                    _source = GameObject.Find("SfxAudioSource").GetComponent<AudioSource>();
                    
                
                _source.PlayOneShot(audioData.Clip);
                break;
                
            }
        }
        
        [Serializable]
        public class  AudioData
        {
            [SerializeField] private string _id;
            [SerializeField] private AudioClip _clips;

            public string Id => _id;
            public AudioClip Clip => _clips;
        }
    }
}