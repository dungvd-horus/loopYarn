using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class KeyObject : MonoBehaviour
{
    [Header("Wall Structure")]
    public Transform KeyTransform;                    // Head transform
    public List<PaintingPixel> PaintingPixelsCovered;
    public List<Vector2> BorderPixels;

    [Header("VISUAL")]
    public Renderer KeyRenderer;
    public IdleRotate Rotating;
    public IdleMoveUpDown IdleMoving;
    public ParticleSystem CollectedFX;
    public ParticleSystem TwinkleFX;

    [Header("SOUND")]
    public AudioSource KeyAudioSource;
    public AudioClip CollectedSoundFX;
    public AudioClip KeyFlySoundFX;

    public KeyState CurrentState = KeyState.Locked;
    
    public bool Locked => CurrentState == KeyState.Locked;
    public bool Collected => CurrentState == KeyState.Collected;
    public bool ReadyToCollected => CurrentState == KeyState.ReadyToCollect;

    public enum KeyState
    {
        Locked,
        ReadyToCollect,
        Collected
    }

    public void Awake()
    {
        GameplayEventsManager.OnGridObjectChanged += OnGridChange;
    }

    private void OnDestroy()
    {
        GameplayEventsManager.OnGridObjectChanged -= OnGridChange;
    }

    private void OnGridChange(PaintingGridObject gridObject)
    {
        foreach (var pixel in BorderPixels)
        {
            if (gridObject.IsPixelDestroyed((int)pixel.x, (int)pixel.y))
            {
                OnAPixelBorderDestroyed();
                return;
            }
        }
    }

    /// <summary>
    /// Initialize the pipe structure with head and body parts
    /// </summary>
    /// <param name="head">The head transform</param>
    /// <param name="bodyParts">List of body parts transforms (including tail), ordered from head to tail</param>
    /// <param name="isHorizontal">True if the pipe is horizontal (in same row), false if vertical (in same column)</param>
    public void Initialize(List<PaintingPixel> pipePixels)
    {
        CurrentState = KeyState.Locked;
        PaintingPixelsCovered = pipePixels ?? new List<PaintingPixel>();
        BorderPixels = new List<Vector2>();
        Rotating.StartRotate();
        IdleMoving.PlayTween();
        TwinkleFX.Play();
    }

    public void OnAPixelBorderDestroyed()
    {
        if (Collected) return;
        CurrentState = KeyState.ReadyToCollect;
        GameplayEventsManager.OnAKeyReadyToBeCollected?.Invoke();
    }

    public void OnCollectedByLock(CollectorController _unlocker)
    {
        _unlocker.LockController.Unlock();
        CurrentState = KeyState.Collected;
        for (int i = 0; i < PaintingPixelsCovered.Count; i++)
        {
            PaintingPixelsCovered[i].DestroyPixel(invokeEvent: false);
        }
        OnCollected(_unlocker);
        GameplayEventsManager.OnCollectAKey?.Invoke();
    }

    private void OnCollected(CollectorController _locker)
    {
        //TODO: Add collected animation
        TwinkleFX.Stop();
        CollectedFX.Play();
        Rotating.BoostSpeed(25);
        KeyAudioSource.PlayOneShot(CollectedSoundFX);
        Sequence _unlockTween = DOTween.Sequence();
        _unlockTween.Append(KeyTransform.DOMoveY(KeyTransform.position.y  + 2, 1.5f).OnComplete(() =>
        {
            KeyAudioSource.PlayOneShot(KeyFlySoundFX);
        }));
        _unlockTween.Append(KeyTransform.DOMove(_locker.transform.position, 0.8f));
        _unlockTween.OnComplete(() =>
        {
            _locker.LockController.UnlockWithVisual();
            KeyRenderer.enabled = false;
            Rotating.StopRotate();
            IdleMoving.StopTween();
        });

    }

    public void SelfDestroy()
    {
        if (Application.isPlaying) GameObject.Destroy(gameObject);
        else GameObject.DestroyImmediate(gameObject);
    }

    public bool BorderContains(PaintingPixel _pixel)
    {
        return BorderPixels.Any(x => x.x == _pixel.column && x.y == _pixel.row);
    }
}