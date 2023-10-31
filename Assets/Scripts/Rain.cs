using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : MonoBehaviour
{
    public Light dirLight;
    private ParticleSystem _ps;
    private bool _isRain = false;

    private void Start()
    {
        _ps = GetComponent<ParticleSystem>();
        StartCoroutine(Weather());
    }
    private void Update()
    {
        if (_isRain && dirLight.intensity > 0.25f)
            LightIntencity(-1);
        if (!_isRain && dirLight.intensity < 0.5f)
            LightIntencity(1);
    }
    private void LightIntencity(int mult)
    {
        dirLight.intensity += 0.05f * Time.deltaTime * mult;
    }
    IEnumerator Weather()
    {
        while (true)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(10f, 15f));

            if (_isRain)
                _ps.Stop();
            else
                _ps.Play();

            _isRain = !_isRain;
        }
    }
}