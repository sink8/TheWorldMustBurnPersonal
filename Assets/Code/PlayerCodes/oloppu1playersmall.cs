using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oloppu1playersmall : MonoBehaviour
{
    public float fadeDuration = 30f; // How long until emission fully stops
    public ParticleSystem[] particleSystems;
    private float[] initialRates;
    private float timer;
    private bool fading;
    public GameObject loppucanvas;
    void Start()
    {

    }

    private void OnEnable()
    {
        initialRates = new float[particleSystems.Length];

        // Store their initial emission rates
        for (int i = 0; i < particleSystems.Length; i++)
        {
            var emission = particleSystems[i].emission;
            initialRates[i] = emission.rateOverTime.constant;
        }

        TriggerFade();
    }

    public void TriggerFade()
    {
        fading = true;
        timer = 0f;
    }


    // Update is called once per frame
    void Update()
    {
        if (!fading) return;

        timer += Time.deltaTime;
        float t = Mathf.Clamp01(timer / fadeDuration);

        for (int i = 0; i < particleSystems.Length; i++)
        {
            var emission = particleSystems[i].emission;
            emission.rateOverTime = Mathf.Lerp(initialRates[i], 0, t);
        }

        // When done, stop fading
        if (t >= 1f)
        {
            fading = false;

            if (loppucanvas != null)
                loppucanvas.SetActive(true);
        }
    }
}
