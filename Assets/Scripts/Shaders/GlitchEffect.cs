/**
This work is licensed under a Creative Commons Attribution 3.0 Unported License.
http://creativecommons.org/licenses/by/3.0/deed.en_GB

You are free:

to copy, distribute, display, and perform the work
to make derivative works
to make commercial use of the work
*/

using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/GlitchEffect")]
public class GlitchEffect : MonoBehaviour {
	public Texture2D displacementMap;
	float glitchup, glitchdown, flicker,
			glitchupTime = 0.05f, glitchdownTime = 0.05f, flickerTime = 0.5f;
	
	public float intensity;

	private Material curMaterial;
	public Shader curShader;
	Material material
	{
		get
		{
			if(curMaterial == null)
			{
				curMaterial = new Material(curShader);
				curMaterial.hideFlags = HideFlags.HideAndDontSave;
			}
			return curMaterial;
		}
	}

    private void Start()
    {
        material.SetTexture("_DispTex", displacementMap);
    }

    // Called by camera to apply image effect
    void OnRenderImage (RenderTexture source, RenderTexture destination) {
		
		material.SetFloat("_Intensity", intensity);
		
		glitchup += Time.deltaTime * intensity;
		glitchdown += Time.deltaTime * intensity;
		flicker += Time.deltaTime * intensity;

        if (flicker > flickerTime) //FLIP
        {
            material.SetFloat("filterRadius", Random.Range(-3f, 3f) * intensity);
            flicker = 0;
            flickerTime = Random.value;
            Sound.instance.PlayGlitch(1);

        }

        if (glitchup > glitchupTime) //FLIP
        {
            if (Random.value < 0.1f * intensity)
                material.SetFloat("flip_up", Random.Range(0, 1f) * intensity);
            else
                material.SetFloat("flip_up", 0);

            Sound.instance.PlayGlitch(2);
            glitchup = 0;
            glitchupTime = Random.value / 10f;

        }

        if (glitchdown > glitchdownTime)
        {
            if (Random.value < 0.1f * intensity)
                material.SetFloat("flip_down", 1 - Random.Range(0, 1f) * intensity);
            else
                material.SetFloat("flip_down", 1);

            glitchdown = 0;
            glitchdownTime = Random.value / 10f;
        }

        if (Random.value < 0.05 * intensity)
        {
            material.SetFloat("displace", Random.value * intensity);
            material.SetFloat("scale", 1 - Random.value * intensity);
            Sound.instance.PlayGlitch(0);
        }
        else
            material.SetFloat("displace", 0);

        Graphics.Blit (source, destination, material);
	}
}