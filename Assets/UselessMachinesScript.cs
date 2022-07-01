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

    public GameObject ClampArmBottom;
    public GameObject ClampArmMiddle;
    public GameObject ClampArmTop;
    public GameObject ClampArmHand;
    public GameObject ClampArmHandParent;

    public GameObject StatusCopterBody;

    public Animator Animation;

    private int _moduleId;
    private static int _moduleIdCounter = 1;
    private bool _moduleSolved;
    private bool _canFlip;
    private int animIx = -1;

    private void Start()
    {
        _moduleId = _moduleIdCounter++;
        _canFlip = true;
        SwitchSel.OnInteract += MoveSwitch;
    }

    private bool MoveSwitch()
    {
        if (_canFlip)
            StartCoroutine(FlipSwitchUp());
        return false;
    }

    public void ResetAnim()
    {
        _canFlip = true;
        SwitchObj.transform.localEulerAngles = new Vector3(-70f, 0f, 0f);
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
        //int animIx = Rnd.Range(0, 2);
        animIx++;
        if (animIx == 0)
            StartCoroutine(ShakeModule());
        if (animIx == 1)
            StartCoroutine(FormClampHand());
        if(animIx == 2)
            Animation.SetTrigger("TopDoor");
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
        var durationSecond = 0.1f;
        var elapsedSecond = 0f;
        while (elapsedSecond < durationSecond)
        {
            FullModule.transform.localEulerAngles = new Vector3(Mathf.Lerp(20f, -20f, elapsedSecond / durationSecond), 0f, 0f);
            yield return null;
            elapsedSecond += Time.deltaTime;
        }
        FullModule.transform.localEulerAngles = new Vector3(-20f, 0f, 0f);
        var durationThird = 0.1f;
        var elapsedThird = 0f;
        while (elapsedThird < durationThird)
        {
            SwitchObj.transform.localEulerAngles = new Vector3(Mathf.Lerp(70f, -70f, elapsedThird / durationThird), 0f, 0f);
            yield return null;
            elapsedThird += Time.deltaTime;
        }
        SwitchObj.transform.localEulerAngles = new Vector3(-70f, 0f, 0f);
        yield return new WaitForSeconds(0.5f);
        var durationFourth = 0.5f;
        var elapsedFourth = 0f;
        while (elapsedFourth < durationFourth)
        {
            FullModule.transform.localEulerAngles = new Vector3(Easing.InOutQuad(elapsedFourth, -20f, 0f, durationFourth), 0f, 0f);
            yield return null;
            elapsedFourth += Time.deltaTime;
        }
        FullModule.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
        yield return new WaitForSeconds(0.2f);
        _canFlip = true;
    }

    private IEnumerator FormClampHand()
    {
        var durationFirst = 0.3f;
        var elapsedFirst = 0f;
        while (elapsedFirst < durationFirst)
        {
            ClampArmBottom.transform.localPosition = new Vector3(0f, Easing.InOutQuad(elapsedFirst, 0f, 0.015f, durationFirst), 0f);
            ClampArmMiddle.transform.localPosition = new Vector3(0f, Easing.InOutQuad(elapsedFirst, 0f, 0.015f, durationFirst), 0f);
            ClampArmTop.transform.localPosition = new Vector3(0f, Easing.InOutQuad(elapsedFirst, 0f, 0.015f, durationFirst), 0f);
            ClampArmHand.transform.localPosition = new Vector3(-0.04f, Easing.InOutQuad(elapsedFirst, -0.015f, 0f, durationFirst), 0.04f);
            yield return null;
            elapsedFirst += Time.deltaTime;
        }
        ClampArmBottom.transform.localPosition = new Vector3(0f, 0.015f, 0f);
        ClampArmMiddle.transform.localPosition = new Vector3(0f, 0.015f, 0f);
        ClampArmTop.transform.localPosition = new Vector3(0f, 0.015f, 0f);
        ClampArmHand.transform.localPosition = new Vector3(-0.04f, 0f, 0.04f);
        yield return new WaitForSeconds(0.5f);
        var durationSecond = 0.7f;
        var elapsedSecond = 0f;
        while (elapsedSecond < durationSecond)
        {
            ClampArmBottom.transform.localPosition = new Vector3(Easing.InOutQuad(elapsedSecond, 0f, -0.057f, durationSecond), 0.015f, Easing.InOutQuad(elapsedSecond, 0f, 0.065f, durationSecond));
            ClampArmMiddle.transform.localPosition = new Vector3(Easing.InOutQuad(elapsedSecond, 0f, -0.0284f, durationSecond), 0.015f, Easing.InOutQuad(elapsedSecond, 0f, -0.035f, durationSecond));
            ClampArmTop.transform.localPosition = new Vector3(0f, 0.015f, Easing.InOutQuad(elapsedSecond, 0f, 0.075f, durationSecond));
            ClampArmHand.transform.localPosition = new Vector3(Easing.InOutQuad(elapsedSecond, -0.04f, -0.079f, durationSecond), 0f, Easing.InOutQuad(elapsedSecond, 0.04f, 0.095f, durationSecond));

            ClampArmBottom.transform.localEulerAngles = new Vector3(0f, Easing.InOutQuad(elapsedSecond, 0f, 60f, durationSecond), 0f);
            ClampArmTop.transform.localEulerAngles = new Vector3(0f, Easing.InOutQuad(elapsedSecond, 0f, 30f, durationSecond), 0f);
            yield return null;
            elapsedSecond += Time.deltaTime;
        }
        ClampArmBottom.transform.localPosition = new Vector3(-0.057f, 0.015f, 0.065f);
        ClampArmMiddle.transform.localPosition = new Vector3(-0.0284f, 0.015f, -0.035f);
        ClampArmTop.transform.localPosition = new Vector3(0f, 0.015f, 0.075f);
        ClampArmHand.transform.localPosition = new Vector3(-0.079f, 0f, 0.095f);
        yield return new WaitForSeconds(0.5f);
        var durartionThird = 0.7f;
        var elapsedThird = 0f;
        while (elapsedThird < durartionThird)
        {
            ClampArmHandParent.transform.localEulerAngles = new Vector3(0f, Easing.InOutQuad(elapsedThird, 0f, -30f, durartionThird), 0f);
            yield return null;
            elapsedThird += Time.deltaTime;
        }
        yield return new WaitForSeconds(0.3f);
        var durationFourth = 0.1f;
        var elapsedFourth = 0f;
        while (elapsedFourth < durationFourth)
        {
            ClampArmHandParent.transform.localEulerAngles = new Vector3(0f, Mathf.Lerp(-30f, 0f, elapsedFourth / durationFourth), 0f);
            yield return null;
            elapsedFourth += Time.deltaTime;
        }
        var durationFifth = 0.1f;
        var elapsedFifth = 0f;
        while (elapsedFifth < durationFifth)
        {
            ClampArmHandParent.transform.localEulerAngles = new Vector3(0f, Mathf.Lerp(0f, 50f, elapsedFifth / durationFifth), 0f);
            SwitchObj.transform.localEulerAngles = new Vector3(Easing.InOutQuad(elapsedFifth, 70f, -60f, durationFifth), 0f);
            yield return null;
            elapsedFifth += Time.deltaTime;
        }
        SwitchObj.transform.localEulerAngles = new Vector3(-70f, 0f, 0f);
        yield return new WaitForSeconds(0.3f);
        var durationSixth = 0.4f;
        var elapsedSixth = 0f;
        while (elapsedSixth < durationSixth)
        {
            ClampArmHandParent.transform.localEulerAngles = new Vector3(0f, Easing.InOutQuad(elapsedSixth, 50f, 0f, durationSixth), 0f);
            yield return null;
            elapsedSixth += Time.deltaTime;
        }
        ClampArmHandParent.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
        yield return new WaitForSeconds(0.5f);
        var durationSeventh = 0.5f;
        var elapsedSeventh = 0f;
        while (elapsedSeventh < durationSeventh)
        {
            ClampArmBottom.transform.localPosition = new Vector3(Easing.InOutQuad(elapsedSeventh, -0.057f, 0f, durationSeventh), 0.015f, Easing.InOutQuad(elapsedSeventh, 0.065f, 0f, durationSeventh));
            ClampArmMiddle.transform.localPosition = new Vector3(Easing.InOutQuad(elapsedSeventh, -0.0284f, 0f, durationSeventh), 0.015f, Easing.InOutQuad(elapsedSeventh, -0.035f, 0f, durationSeventh));
            ClampArmTop.transform.localPosition = new Vector3(0f, 0.015f, Easing.InOutQuad(elapsedSeventh, 0.075f, 0f, durationSeventh));
            ClampArmHand.transform.localPosition = new Vector3(Easing.InOutQuad(elapsedSeventh, -0.079f, -0.04f, durationSeventh), 0f, Easing.InOutQuad(elapsedSeventh, 0.095f, 0.04f, durationSeventh));

            ClampArmBottom.transform.localEulerAngles = new Vector3(0f, Easing.InOutQuad(elapsedSeventh, 60f, 0f, durationSeventh), 0f);
            ClampArmTop.transform.localEulerAngles = new Vector3(0f, Easing.InOutQuad(elapsedSeventh, 30f, 0f, durationSeventh), 0f);
            yield return null;
            elapsedSeventh += Time.deltaTime;
        }
        ClampArmBottom.transform.localPosition = new Vector3(-0, 0.015f, 0);
        ClampArmMiddle.transform.localPosition = new Vector3(-0, 0.015f, -0);
        ClampArmTop.transform.localPosition = new Vector3(0f, 0.015f, 0);
        ClampArmHand.transform.localPosition = new Vector3(-0.04f, 0, 0.04f);
        yield return new WaitForSeconds(0.5f);
        var durationEighth = 0.3f;
        var elapsedEighth = 0f;
        while (elapsedEighth < durationEighth)
        {
            ClampArmBottom.transform.localPosition = new Vector3(0f, Easing.InOutQuad(elapsedEighth, 0.015f, 0f, durationEighth), 0f);
            ClampArmMiddle.transform.localPosition = new Vector3(0f, Easing.InOutQuad(elapsedEighth, 0.015f, 0f, durationEighth), 0f);
            ClampArmTop.transform.localPosition = new Vector3(0f, Easing.InOutQuad(elapsedEighth, 0.015f, 0f, durationEighth), 0f);
            ClampArmHand.transform.localPosition = new Vector3(-0.04f, Easing.InOutQuad(elapsedEighth, 0f, -0.015f, durationEighth), 0.04f);
            yield return null;
            elapsedEighth += Time.deltaTime;
        }
        ClampArmBottom.transform.localPosition = new Vector3(0f, 0, 0f);
        ClampArmMiddle.transform.localPosition = new Vector3(0f, 0, 0f);
        ClampArmTop.transform.localPosition = new Vector3(0f, 0, 0f);
        ClampArmHand.transform.localPosition = new Vector3(-0.04f, -0.015f, 0.04f);
        yield return new WaitForSeconds(0.2f);
        _canFlip = true;
    }
}
