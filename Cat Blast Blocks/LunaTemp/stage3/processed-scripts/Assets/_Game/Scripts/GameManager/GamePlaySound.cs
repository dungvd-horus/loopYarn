using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlaySound : MonoBehaviour
{
    [SerializeField] private AudioSource clickCatSound;

    public void PlayClickCat()
    {
        clickCatSound?.Stop();
        clickCatSound?.Play();
    }
}
