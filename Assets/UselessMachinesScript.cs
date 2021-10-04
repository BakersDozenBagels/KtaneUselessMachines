using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using Rnd = UnityEngine.Random;

public class UselessMachinesScript : MonoBehaviour
{
    public KMBombModule Module;
    public KMBombInfo BombInfo;
    public KMAudio Audio;
    public KMSelectable SwitchSel;
    public GameObject SwitchObj;

    public GameObject FullModule;

    private int _moduleId;
    private static int _moduleIdCounter = 1;
    private bool _moduleSolved;
    private bool _canFlip;

    private void Start()
    {
        _moduleId = _moduleIdCounter++;
        _canFlip = true;
        SwitchSel.OnInteract += MoveSwitch;
    }

    private bool MoveSwitch()
    {
        if (_canFlip)
        {
            StartCoroutine(FlipSwitchUp());
        }
        return false;
    }

    private IEnumerator FlipSwitchUp()
    {
        _canFlip = false;
        var duration = 0.3f;
        var elapsed = 0f;
        while (elapsed < duration)
        {
            SwitchObj.transform.localEulerAngles = new Vector3(Easing.InOutQuad(elapsed, -70f, 70f, duration), 0f, 0f);
            yield return null;
            elapsed += Time.deltaTime;
        }
        SwitchObj.transform.localEulerAngles = new Vector3(70f, 0f, 0f);
        yield return new WaitForSeconds(2f);
        int animIx = Rnd.Range(0, 1);
        if (animIx == 0)
            StartCoroutine(ShakeModule());
    }

    private IEnumerator ShakeModule()
    {
        var durationFirst = 1f;
        var elapsedFirst = 0f;
        while (elapsedFirst < durationFirst)
        {
            FullModule.transform.localEulerAngles = new Vector3(Easing.InOutQuad(elapsedFirst, 0f, 20f, durationFirst), 0f, 0f);
            yield return null;
            elapsedFirst += Time.deltaTime;
        }
        FullModule.transform.localEulerAngles = new Vector3(20f, 0f, 0f);
        yield return new WaitForSeconds(0.5f);
        var durationSecond = 0.2f;
        var elapsedSecond = 0f;
        while (elapsedSecond < durationSecond)
        {
            FullModule.transform.localEulerAngles = new Vector3(Easing.OutQuad(elapsedSecond, 20f, -20f, durationSecond), 0f, 0f);
            SwitchObj.transform.localEulerAngles = new Vector3(Easing.InOutQuad(elapsedSecond, 70f, -70f, durationSecond), 0f, 0f);
            yield return null;
            elapsedSecond += Time.deltaTime;
        }
        FullModule.transform.localEulerAngles = new Vector3(-20f, 0f, 0f);
        SwitchObj.transform.localEulerAngles = new Vector3(-70f, 0f, 0f);
        yield return new WaitForSeconds(0.5f);
        var durationThird = 0.5f;
        var elapsedThird = 0f;
        while (elapsedThird < durationThird)
        {
            FullModule.transform.localEulerAngles = new Vector3(Easing.OutQuad(elapsedThird, -20f, 0f, durationThird), 0f, 0f);
            yield return null;
            elapsedThird += Time.deltaTime;
        }
        FullModule.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
        yield return new WaitForSeconds(0.5f);
        _canFlip = true;
    }
}
