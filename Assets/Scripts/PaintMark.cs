using UnityEngine;
using System.Collections;

public class PaintMark : MonoBehaviour {

    public int timeToDestruct;

	// Use this for initialization
	void Start () {

        StartCoroutine(DestroyPaint(timeToDestruct));
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator DestroyPaint(int time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
