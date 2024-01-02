using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ARAVRInput
{

#if PC
    public enum ButtonTarget
    {
        Fire1, Fire2, Fire3, Jump
    }
    static Vector3 originScale = Vector3.one * 0.02f;
#endif

    static Transform rootTransform;
    static Transform GetTransform()
    {
        if (rootTransform == null)
        {
            rootTransform = GameObject.Find("TrackingSpace").transform;
        }
        return rootTransform;
    }
    static IEnumerator VibrationCoroutine(float duration, float frequency, float amplitude, Controller hand)
    {
        float currentTime = 0;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            OVRInput.SetControllerVibration(frequency, amplitude, (OVRInput.Controller)hand);
            yield return null;
        }
        OVRInput.SetControllerVibration(0, 0, (OVRInput.Controller)hand);
    }
    static Vector3 originScale = Vector3.one * 0.005f;


    public enum Button
    {
#if PC
        One = ButtonTarget.Fire1, Two = ButtonTarget.Jump,
        Thumbstick = ButtonTarget.Fire1, IndexTrigger = ButtonTarget.Fire3,
        HandTrigger = ButtonTarget.Fire2
#endif

        One = OVRInput.Button.One, Two = OVRInput.Button.Two,
        Thumbstick = OVRInput.Button.PrimaryThumbstick,
        IndexTrigger = OVRInput.Button.PrimaryIndexTrigger,
        HandTrigger = OVRInput.Button.PrimaryHandTrigger

    }
    public enum Controller
    {
#if PC
        LTouch, RTouch
#endif

        LTouch = OVRInput.Controller.LTouch,
        RTouch = OVRInput.Controller.RTouch

    }

    static Transform lHand;
    static Transform rHand;

    public static Transform LHand
    {
        get
        {
            if (lHand == null)
            {
#if PC
                GameObject handObj = new GameObject("LHand");
                lHand = handObj.transform;
                lHand.parent = Camera.main.transform;
#endif

                lHand = GameObject.Find("LeftControllerAnchor").transform;

            }
            return lHand;
        }
    }
    public static Transform RHand
    {
        get
        {
            if (rHand == null)
            {
#if PC
                GameObject handObj = new GameObject("RHand");
                rHand = handObj.transform;
                rHand.parent = Camera.main.transform;
#endif

                rHand = GameObject.Find("RightControllerAnchor").transform;


            }
            return rHand;
        }
    }
    public static Vector3 RHandPosition
    {
        get
        {
#if PC
            Vector3 pos = Input.mousePosition;
            pos.z = 0.7f;
            pos = Camera.main.ScreenToWorldPoint(pos);
            RHand.position = pos;
            return pos;
#endif


            Vector3 pos = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
            pos = GetTransform().TransformPoint(pos);
            return pos;

        }
    }
    public static Vector3 RHandDirection
    {
        get
        {
#if PC
            Vector3 direction = RHandPosition - Camera.main.transform.position;
            RHand.forward = direction;
            return direction;
#endif

            Vector3 direction = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch) * Vector3.forward;
            direction = GetTransform().TransformDirection(direction);
            return direction;

        }
    }
    public static Vector3 LHandPosition
    {
        get
        {
#if PC
            Vector3 pos = Input.mousePosition;
            pos.z = 0.7f;
            pos = Camera.main.ScreenToWorldPoint(pos);
            RHand.position = pos;
            return pos;
#endif

            Vector3 pos = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
            pos = GetTransform().TransformPoint(pos);
            return pos;

        }
    }
    public static Vector3 LHandDirection
    {
        get
        {
#if PC
            Vector3 direction = RHandPosition - Camera.main.transform.position;
            RHand.forward = direction;
            return direction;
#endif

            Vector3 direction = OVRInput.GetLocalControllerRotation(OVRInput.Controller.LTouch) * Vector3.forward;
            direction = GetTransform().TransformDirection(direction);
            return direction;

        }
    }
    public static bool Get(Button virtualMask, Controller hand = Controller.RTouch)
    {
#if PC
        return Input.GetButton(((ButtonTarget)virtualMask).ToString());
#endif

        return OVRInput.Get((OVRInput.Button)virtualMask, (OVRInput.Controller)hand);

    }
    public static bool GetDown(Button virtualMask, Controller hand = Controller.RTouch)
    {
#if PC
        return Input.GetButtonDown(((ButtonTarget)virtualMask).ToString());
#endif

        return OVRInput.GetDown((OVRInput.Button)virtualMask, (OVRInput.Controller)hand);

    }
    public static bool GetUP(Button virtualMask, Controller hand = Controller.RTouch)
    {
#if PC
        return Input.GetButtonUp(((ButtonTarget)virtualMask).ToString());
#endif

        return OVRInput.GetUp((OVRInput.Button)virtualMask, (OVRInput.Controller)hand);

    }
    public static float GetAxis(string axis, Controller hand = Controller.LTouch)
    {
#if PC
        return Input.GetAxis(axis);
#endif

        if (axis == "Horizontal")
        {
            return OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, (OVRInput.Controller)hand).x;
        }
        else
        {
            return OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, (OVRInput.Controller)hand).y;
        }

    }
    public static void PlayVibration(Controller hand)
    {

        PlayVibration(0.06f, 1, 1, hand);

    }
    public static void PlayVibration(float duration, float fequency, float amplitude, Controller hand)
    {

        if (CoroutineInstance.coroutineInstance == null)
        {
            GameObject coroutineObj = new GameObject("coroutineInstance");
            coroutineObj.AddComponent<CoroutineInstance>();
        }
        CoroutineInstance.coroutineInstance.StopAllCoroutines();
        CoroutineInstance.coroutineInstance.StartCoroutine(VibrationCoroutine(duration, fequency, amplitude, hand));

    }
    public static void Recenter()
    {

        OVRManager.display.RecenterPose();

    }
    public static void Recenter(Transform target, Vector3 direction)
    {
        target.forward = target.rotation * direction;
    }
    public static void DrawCrosshair(Transform crosshair, bool isHand = true, Controller hand = Controller.RTouch)
    {
        Ray ray;
        if (isHand)
        {
#if PC
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
#endif

            if (hand == Controller.RTouch)
            {
                ray = new Ray(RHandPosition, RHandDirection);
            }
            else
            {
                ray = new Ray(LHandPosition, LHandDirection);
            }

        }
        else
        {
            ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        }

        Plane plane = new Plane(Vector3.up, 0);
        float distance = 0;

        if (plane.Raycast(ray, out distance))
        {
            crosshair.position = ray.GetPoint(distance);
            crosshair.forward = -Camera.main.transform.forward;
            crosshair.localScale = originScale * Mathf.Max(1, distance);
        }
        else
        {
            crosshair.position = ray.origin + ray.direction * 100;
            crosshair.forward = -Camera.main.transform.forward;
            distance = (crosshair.position - ray.origin).magnitude;
            crosshair.localScale = originScale * Mathf.Max(1, distance);
        }
    }

}

class CoroutineInstance : MonoBehaviour
{
    public static CoroutineInstance coroutineInstance = null;
    private void Awake()
    {
        if (coroutineInstance == null)
        {
            coroutineInstance = this;
        }
        DontDestroyOnLoad(gameObject);
    }
}


