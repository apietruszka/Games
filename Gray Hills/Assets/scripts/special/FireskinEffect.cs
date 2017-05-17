using UnityEngine;
using System.Collections;

public class FireskinEffect : MonoBehaviour {

    float timeSinceStart = 0;
    float duration;


    private void Start()
    {
        ParticleSystem particles = GetComponentInChildren<ParticleSystem>();
        duration = particles.duration + particles.startLifetime;
        gameObject.transform.position = transform.parent.position;
    }
    void Update()
    {
        timeSinceStart += Time.deltaTime;
        if (timeSinceStart > duration)
            Destroy(gameObject);
    }
}
