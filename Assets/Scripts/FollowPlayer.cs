using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject targetPlayer;

    [SerializeField] private Vector3 offset;

    void Update()
    {
        if (targetPlayer == null) { return; }

        transform.position = targetPlayer.transform.position + offset;
    }

}
