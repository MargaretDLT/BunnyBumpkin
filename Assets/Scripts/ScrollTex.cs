using UnityEngine;

public class ScrollTex : MonoBehaviour
{
	public float ScrollU = 0.5f;
	public float ScrollV = 0.5f;

    // Update is called once per frame
    void Update()
    {
		GetComponent<Renderer>().material.mainTextureOffset = new Vector2(Time.time * ScrollU, Time.time * ScrollV);
    }
}
