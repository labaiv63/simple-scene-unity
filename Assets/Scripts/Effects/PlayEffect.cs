using UnityEngine;

public class PlayEffect : MonoCache
{
    [SerializeField] private GameObject effect;
    private IInteracteble interactAction => GetComponent<IInteracteble>();

    private void Start()
    {
        //Debug.Log(effect);
        SubscribeTo();
    }
    public void SubscribeTo()
    {
        interactAction.OnInteractEnd += SpawnedEffect;
    }
    public void UnsubscribeTo()
    {
        interactAction.OnInteractEnd -= SpawnedEffect;
    }
    private void OnDestroy() => UnsubscribeTo();
    public void SpawnedEffect() 
    {
        if(effect != null)
            Instantiate(effect, transform.position, Quaternion.identity, transform.parent);
    }
}