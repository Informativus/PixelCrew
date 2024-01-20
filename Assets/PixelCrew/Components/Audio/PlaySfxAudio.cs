using System;
using PixelCrew.Utils;
using UnityEngine;

namespace PixelCrew.Components.Audio
{
    public class PlaySfxAudio : MonoBehaviour
    {
        [SerializeField] private AudioClip _audioClip;
        private AudioSource _source;

        public void Play()
        {
            if (_source == null)
                _source = AudioUtils.FindSfxSource();
            
            _source.PlayOneShot(_audioClip);
        }
    }
}