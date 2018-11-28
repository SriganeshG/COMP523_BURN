using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class SimpleGestureListener : MonoBehaviour, KinectGestures.GestureListenerInterface
{
    // GUI Text to display the gesture messages.
    public Text gestureInfo;

    //planets
    public GameObject mercury;
    public GameObject mars;
    public GameObject venus;
    public GameObject earth;
    public GameObject jupiter;
    public GameObject saturn;
    public GameObject neptune;
    public GameObject uranus;
    public GameObject pluto;

    public GameObject spaceship;

    // private bool to track if progress message has been displayed
    private bool progressDisplayed;

    private bool planetClicked = false;
    private GameObject selectedPlanet;

    public void UserDetected(uint userId, int userIndex)
    {
        // as an example - detect these user specific gestures
        KinectManager manager = KinectManager.Instance;

        manager.DetectGesture(userId, KinectGestures.Gestures.Jump);
        manager.DetectGesture(userId, KinectGestures.Gestures.Squat);

        //		manager.DetectGesture(userId, KinectGestures.Gestures.Push);
        //		manager.DetectGesture(userId, KinectGestures.Gestures.Pull);

        //		manager.DetectGesture(userId, KinectWrapper.Gestures.SwipeUp);
        //		manager.DetectGesture(userId, KinectWrapper.Gestures.SwipeDown);

        if (gestureInfo != null)
        {
            gestureInfo.text = "SwipeLeft, SwipeRight, Jump or Squat.";
        }
    }

    public void UserLost(uint userId, int userIndex)
    {
        if (gestureInfo != null)
        {
            gestureInfo.text = string.Empty;
        }
    }

    public void GestureInProgress(uint userId, int userIndex, KinectGestures.Gestures gesture,
                                  float progress, KinectWrapper.NuiSkeletonPositionIndex joint, Vector3 screenPos)
    {
        if (gesture == KinectGestures.Gestures.Click && progress > 0.3f)
        {
            string sGestureText = string.Format("{0} {1:F1}% complete", gesture, progress * 100);
            if (gestureInfo != null)
                gestureInfo.text = sGestureText;

            progressDisplayed = true;
        }
        else if ((gesture == KinectGestures.Gestures.ZoomOut || gesture == KinectGestures.Gestures.ZoomIn) && progress > 0.5f)
        {
            string sGestureText = string.Format("{0} detected, zoom={1:F1}%", gesture, screenPos.z * 100);
            if (gestureInfo != null)
                gestureInfo.text = sGestureText;

            progressDisplayed = true;
        }
        else if (gesture == KinectGestures.Gestures.Wheel && progress > 0.5f)
        {
            string sGestureText = string.Format("{0} detected, angle={1:F1} deg", gesture, screenPos.z);
            if (gestureInfo != null)
                gestureInfo.text = sGestureText;

            progressDisplayed = true;
        }
    }

    public bool GestureCompleted(uint userId, int userIndex, KinectGestures.Gestures gesture,
                                  KinectWrapper.NuiSkeletonPositionIndex joint, Vector3 screenPos)
    {
        string sGestureText = gesture + " detected";
        if (gesture == KinectGestures.Gestures.Click)
        {
            if (planetClicked)
            {
                planetClicked = false;
                selectedPlanet = null;
            }

            if (!planetClicked)
            {
                Vector2 rayPos = new Vector2(Camera.main.ScreenToWorldPoint(spaceship.transform.position).x, Camera.main.ScreenToWorldPoint(spaceship.transform.position).y);
                RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.zero, 0f);
                if (hit)
                {
                    Debug.Log(hit.transform.name);
                    Debug.Log(hit.transform.gameObject.tag);
                    planetClicked = true;
                    selectedPlanet = hit.transform.gameObject;
                }
                //raycast stuff
                //if it hits, set planetClicked to true
                //set selectedPlanet to hit planet
            }

            sGestureText += string.Format(" at ({0:F1}, {1:F1})", screenPos.x, screenPos.y);
        }



        if (gestureInfo != null)
        {
            gestureInfo.text = sGestureText;
        }


        progressDisplayed = false;

        return true;
    }

    public bool GestureCancelled(uint userId, int userIndex, KinectGestures.Gestures gesture,
                                  KinectWrapper.NuiSkeletonPositionIndex joint)
    {
        if (progressDisplayed)
        {
            // clear the progress info
            if (gestureInfo != null)
                gestureInfo.text = String.Empty;

            progressDisplayed = false;
        }

        return true;
    }

    void Update()
    {
        if (planetClicked == true && selectedPlanet)
        {
            selectedPlanet.transform.position = spaceship.transform.position;
        }
    }
}
