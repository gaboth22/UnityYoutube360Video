using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class LiveVideoStream : MonoBehaviour {

	public RenderTexture RenderTexture;
	private VideoPlayer videoPlayer;
	private VideoSource videoSource;
	private string videoUrl;

	private string GetVideoUrl()
	{
		var yt = new YoutubeUrlExtractor();
		var links = yt.Extract(StreamUrl.UserInputUrl);
		var url = string.Empty;

		foreach (var link in links) {
			var acquiredStream = false; 

			if (!link[0].Equals(string.Empty) && link[1].Contains("hd720")) {
				acquiredStream = true;
			}

			if (!link[0].Equals(string.Empty) && link[1].Contains("medium")) {
				acquiredStream = true;
			}

			if (!link[0].Equals(string.Empty) && link[1].Contains("small")) {
				acquiredStream = true;
			}

			if(acquiredStream) {
				url = link [0];
				print ("Stream acquired:\n");
				print ("Quality: " + link [1] + "\n");
				print (link [0]);
				break;
			}
		}

		return url;
	}

	void Start () {

		videoUrl = string.Empty;

		while (videoUrl.Equals (string.Empty)) {
			videoUrl = GetVideoUrl ();
		}
			
		Application.runInBackground = true;
		StartCoroutine (StreamVideo());
	}

	void Update () {
		
	}

	IEnumerator StreamVideo()
	{
		videoPlayer = gameObject.AddComponent<VideoPlayer>();
		videoPlayer.playOnAwake = false;
		videoPlayer.source = VideoSource.Url;
		videoPlayer.url = videoUrl;
		videoPlayer.Prepare();
		while (!videoPlayer.isPrepared)
		{
			yield return null;
		}

		print ("Stream Loaded. Ready to play.");
		videoPlayer.targetTexture = RenderTexture;
		videoPlayer.Play();

		yield return null;
	}
}
