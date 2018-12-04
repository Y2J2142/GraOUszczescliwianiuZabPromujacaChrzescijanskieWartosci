using UnityEngine.Monetization;
using System.Collections;
using UnityEngine.Advertisements;
using UnityEngine;

public class TrzepaczHajsu : MonoBehaviour {

    public string placementId = "rewardedVideo";

	public delegate void Rewarder(bool p);

	public Rewarder rewarder = null;
	void Start()
	{

	}

    public void ShowAd () {
        	StartCoroutine (WaitForAd ());
    }

    IEnumerator WaitForAd () {
        while (!Monetization.IsReady (placementId)) {
            yield return null;
        }

        ShowAdPlacementContent ad = null;
        ad = Monetization.GetPlacementContent (placementId) as ShowAdPlacementContent;

        if (ad != null) {
            ad.Show(AdFinished);
        }
    }

	


    void AdFinished (UnityEngine.Monetization.ShowResult result) {
        if (result == UnityEngine.Monetization.ShowResult.Finished && rewarder != null) {
			rewarder(false);
        }
    }
}