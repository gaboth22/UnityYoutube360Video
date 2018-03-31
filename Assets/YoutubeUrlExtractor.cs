using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using System.Collections;

public class YoutubeUrlExtractor
{
	public List<List<string>> Extract(string url){
		var html_content = "";
		Hashtable headers = new Hashtable ();
		headers.Add ("User-Agent", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_10_1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2227.1 Safari/537.36");
		using (WWW www = new WWW(url, null, headers))
		{
			while(!www.isDone);
			html_content += www.text;
		}

		var Regex1 = new Regex(@"url=(.*?tags=\\u0026)",RegexOptions.Multiline);
		var matched = Regex1.Match(html_content);
		var download_infos = new List<List<string>>();
		foreach (var matched_group in matched.Groups)
		{
			var urls = Regex.Split(WWW.UnEscapeURL(matched_group.ToString().Replace("\\u0026", " &")), ",?url=");

			foreach (var vid_url in urls.Skip(1))
			{
				var download_url = vid_url.Split(' ')[0].Split(',')[0].ToString();
				var Regex2 = new Regex("(quality=|quality_label=)(.*?)(,|&| |\")");
				var QualityInfo = Regex2.Match(vid_url);
				var quality = QualityInfo.Groups[2].ToString(); //quality_info
				download_infos.Add((new List<string>{ download_url,quality})); //contains url and resolution
			}
		}

		return download_infos;
	}
}