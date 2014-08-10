using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class sharing_on_social_network : MonoBehaviour
{
    //Sharing on facebook or Twitter. Twitter can be shared only by using openURL. For facebook
    // a plug in is neccessary. This is using official facebook unity SDK. However, they are currently
    // in May/2014, using a beta version so it has a bug on Android. The code may not function on Android
    string lastResponse = "";
    
//    void OnGUI()
//    {
//        if (GUI.Button(new Rect(10, 10, 150, 100), "Start sharing"))
//        {
//
//            CallFBInit();
//
//
//
//        }
//        if (GUI.Button(new Rect(300, 150, 150, 100), "Log In"))
//        {
//            CallFBLogin();
//        }
//
//
//        if (GUI.Button(new Rect(10, 150, 150, 100), "Twitter"))
//        {
//            ShareToTwitter("Hi, I got high score");
//        }
//        if (GUI.Button(new Rect(300, 10, 150, 100), "Facebook"))
//        {
//            share();
//        }
//
//    }
//    //This is the method to tweet something on Twitter.
//    private const string add = "http://twitter.com/intent/tweet";
//    private const string lang = "en";
//
//    void ShareToTwitter(string textToDisplay)
//    {
//        Application.OpenURL(add +
//                    "?text=" + WWW.EscapeURL(textToDisplay) +
//                    "&amp;lang=" + WWW.EscapeURL(lang));
//    }
//    private bool isInit = false;
//    //For facebook, SDK, intitilization before doing anthing else
//    private void CallFBInit()
//    {
//        FB.Init(OnInitComplete, OnHideUnity);
//    }
//
//    private void OnInitComplete()
//    {
//        Debug.Log("FB.Init completed: Is user logged in? " + FB.IsLoggedIn);
//        isInit = true;
//    }
//
//    private void OnHideUnity(bool isGameShown)
//    {
//        Debug.Log("Is game showing? " + isGameShown);
//    }
//    //Log in, request user permission. This is where the bug appear. The bug happens only on Android
//    // device which already installed Facebook App. If facebook fixed this bug, the code will work fine.
//    private void CallFBLogin()
//    {
//        FB.Login("email,publish_actions", LoginCallback);
//    }
//
//    void LoginCallback(FBResult result)
//    {
//        if (result.Error != null)
//            lastResponse = "Error Response:\n" + result.Error;
//        else if (!FB.IsLoggedIn)
//        {
//            lastResponse = "Login cancelled by Player";
//        }
//        else
//        {
//            lastResponse = "Login was successful!";
//        }
//    }
//
//    private void CallFBLogout()
//    {
//        FB.Logout();
//    }
//    void Callback(FBResult result)
//    {
//        String lastResponseTexture = null;
//        // Some platforms return the empty string instead of null.
//        if (!String.IsNullOrEmpty(result.Error))
//            lastResponse = "Error Response:\n" + result.Error;
//
//        else
//        {
//
//            lastResponse = "Success Response:\n";
//        }
//    }
//
//
//
//
//    void LogCallback(FBResult response)
//    {
//        Debug.Log(response.ToString());
//    }
//
//    //share the high score on facebook
//    public void share()
//    {
//        FB.Feed(
//
//         link: "http://www.lingoteachapp.com/",
//         linkName: "LingoTeach language learning",
//        linkCaption: "I got high score here",
//        linkDescription: "The app is nice, isn't it?",
//       picture: "",
//       callback: LogCallback);
//
//    }
}
