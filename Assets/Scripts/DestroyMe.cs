using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMe : MonoBehaviour {

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject); 
    }

}
