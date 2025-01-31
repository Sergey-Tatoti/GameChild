using UnityEngine;
using UnityEngine.Events;

public class PlayerTouchTracker : MonoBehaviour
{
    public event UnityAction TouchedHitBox;
    public event UnityAction TouchedStarLevel;
    public event UnityAction<int> TouchedStarExperience;
    public event UnityAction<Vector3> TouchedTeleport;
    public event UnityAction<GameKey> TouchedKey;
    public event UnityAction<Lock> TouchedLock;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<HitBox>())
            TouchedHitBox?.Invoke();

        if (collision.gameObject.TryGetComponent<Teleport>(out Teleport teleport) && !teleport.IsLock)
        {
            teleport.UseLockAnotherTeleport();
            TouchedTeleport?.Invoke(teleport.AnotherTeleportPositionSpawn);
        }

        if (collision.gameObject.GetComponent<StarLevel>())
        {
            collision.gameObject.SetActive(false);
            TouchedStarLevel?.Invoke();
        }

        if (collision.gameObject.TryGetComponent<StarExperience>(out StarExperience starExperience))
        {
            collision.gameObject.SetActive(false);
            TouchedStarExperience?.Invoke(starExperience.CountExperience);
        }

        if (collision.gameObject.TryGetComponent<GameKey>(out GameKey key))
        {
            collision.gameObject.SetActive(false);
            TouchedKey?.Invoke(key);

        }

        if (collision.gameObject.TryGetComponent<Lock>(out Lock locked))
        {
            TouchedLock?.Invoke(locked);
        }    
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Teleport>(out Teleport teleport))
        {
            teleport.UseLock(false);
        }
    }
}