using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class volume : MonoBehaviour
{
    public string volumeName;
    public AudioMixer mixer;
    public Slider slider;
    private float _volumeValue;
    private const float _multipliyer = 20f;

    private void Awake()
    {
        slider.onValueChanged.AddListener(HandleSliderValueChanged);
    }

    private void HandleSliderValueChanged(float value)
    {
        _volumeValue = Mathf.Log10(value) * _multipliyer;
        mixer.SetFloat(volumeName, _volumeValue);
    }

    private void Start()
    {
        _volumeValue=PlayerPrefs.GetFloat(volumeName, Mathf.Log10(slider.value) * _multipliyer);
        slider.value = Mathf.Pow(10f,_volumeValue/_multipliyer);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(volumeName, _volumeValue);
    }
}
