using UnityEngine;

public class PlaySound : MonoCache
{
    [SerializeField] AudioClip SFX;
    private IInteracteble interactAction => GetComponent<IInteracteble>();

    private void Start()
    {
        //Debug.Log(SFX);
        SubscribeTo();
    }
    public void SubscribeTo() 
    {
        interactAction.OnInteract += PlayingSound;
    }
    public void UnsubscribeTo() 
    {
        interactAction.OnInteract -= PlayingSound;
    }
    private void OnDestroy() => UnsubscribeTo();
    public void PlayingSound() => SoundManager.instance.PlaySound(SFX);
}