using System.Collections;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform target;
    public AudioClip MySFX;
    int AudioIndex;

    private void Start()
    {
        AudioIndex = SoundBoard.Instance.AddSoundEffect(MySFX);
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.tag == "Player") & (target != null))
        {
            other.transform.position = target.position;
            other.transform.rotation = target.rotation;
            Physics.SyncTransforms();  // update transforms so Physics and Move controller work properly after teleport
            SoundBoard.Instance.PlaySFX(AudioIndex);


        }
        //else
        //{
        //    Debug.Log("No target transform for " + this.gameObject.name);
        //    // This triggers when a duck hits the teleporter.
        //}
    }
}