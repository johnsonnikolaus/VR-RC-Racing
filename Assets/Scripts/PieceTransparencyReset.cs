using UnityEngine;
using System.Collections;

public class PieceTransparencyReset : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(ResetTransparency());
	}

    IEnumerator ResetTransparency()
    {
        yield return new WaitForSeconds(3);
        transform.GetComponent<MeshRenderer>().enabled = true;
        Destroy(this);
    }
}
