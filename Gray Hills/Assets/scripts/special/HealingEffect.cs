using UnityEngine;
using System.Collections;

public class HealingEffect : MonoBehaviour {
    
    float timeSinceStart=0;
    float duration;
    

    private void Start()
    {
        ParticleSystem[] particles = GetComponentsInChildren<ParticleSystem>();
        duration = particles[0].duration + particles[0].startLifetime;
        gameObject.transform.position = transform.parent.position;
    }
    void Update ()
    {
        timeSinceStart += Time.deltaTime;
        if (timeSinceStart > duration)
            Destroy(gameObject);
	}
}
