using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaders_HologramGlitchChance : MonoBehaviour
{
    public AudioClip[] sparkSound;
    private AudioSource audioSource;
    public float glitchChance = 0.1f;
    private Color emissionColor = new Color(191f, 0f, 1f / 255f, 1f);
    private float emissionIntensity = 2f;
    [SerializeField] private Color initEmissionColor;
    private Renderer renderer;
    private WaitForSeconds glitchLoopWait = new WaitForSeconds(0.25f);
    private WaitForSeconds glitchDuration = new WaitForSeconds(0.1f);

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
        audioSource = GetComponent<AudioSource>();
    }

    IEnumerator Start()
    {
        while (true)
        {
            float glitchTest = Random.Range(0f, 1f);
            if (glitchTest <= glitchChance)
            {
                StartCoroutine(Glitch());
            }
            yield return glitchLoopWait;
        }
    }

    IEnumerator Glitch()
    {
        glitchDuration = new WaitForSeconds(Random.Range(0.1f, 0.25f));
        renderer.material.SetFloat("_Amount", 1f);
        renderer.material.SetFloat("_CutoutThresh", 0.4f);
        renderer.material.SetFloat("_Amplitude", Random.Range(100f, 250f));
        renderer.material.SetFloat("_Speed", Random.Range(1f, 10f));
        audioSource.PlayOneShot(sparkSound[Random.Range(0,sparkSound.Length)]);
        yield return glitchDuration;
        renderer.material.SetFloat("_Amount", 0f);
        renderer.material.SetFloat("_CutoutThresh", 0.27f);
    }
}
