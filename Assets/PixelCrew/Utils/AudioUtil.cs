using UnityEngine;

namespace PixelCrew.Utils
{
    public class AudioUtils
    {
        private const string SfxSourceTag = "SfxAudioSource";

        public static AudioSource FindSfxSource()
        {
            return GameObject.Find(SfxSourceTag).GetComponent<AudioSource>();
        }
    }
}