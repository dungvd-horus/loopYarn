using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingGridEffects : MonoBehaviour
{
    #region PROPERTIES
    [Header("SFX")]
    public AudioSource GridAudioSource;
    public AudioClip BlockDestroyedClip;

    #endregion

    #region MAIN
    public void PlayBlockDestroyedAudio()
    {
        GridAudioSource.PlayOneShot(BlockDestroyedClip);
    }
    #endregion
}
