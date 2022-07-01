using UnityEngine;

public class AnimateSound : MonoBehaviour
{
    private KMAudio _audio;

    private void Start()
    {
        _audio = GetComponent<KMAudio>();
    }

    public void PistonExtend()
    {
        _audio.PlaySoundAtTransform("Piston_extend", transform);
    }

    public void PistonContract()
    {
        _audio.PlaySoundAtTransform("Piston_contract", transform);
    }
}
