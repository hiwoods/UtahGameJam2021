
using UnityEngine;

public class PoopController : MonoBehaviour
{
    public GameObject explosion;



    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.GetComponent<LocalBlackboard>())
        //if (!other.gameObject.tag.StartsWith("Player"))
        //{
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        //}

    }
}
