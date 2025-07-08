using UnityEngine;

public class PlayEffect : MonoBehaviour
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
        interactAction.OnInteract += SpawnedEffect;
    }
    public void UnsubscribeTo()
    {
        interactAction.OnInteract -= SpawnedEffect;
    }
    private void OnDestroy() => UnsubscribeTo();
    public void SpawnedEffect() 
    {
        if(effect != null)
            Instantiate(effect);
    }
}