using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public GameObject gameManagers;  // gameManagers 오브젝트의 레퍼런스

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Gretel"))
        {
            gameManagers.SetActive(true);
        }
    }
}

