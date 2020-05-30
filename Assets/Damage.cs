using UnityEngine;

public class Damage : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("_player"))
        {
            GameHandler._Instance.FncHealthHandler(10);
        }
       
    }
}
