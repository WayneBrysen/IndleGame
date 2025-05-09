﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TWC
{
	//[CreateAssetMenu(menuName = "TileWorldCreator/New Readme")]
	public class TWCReadme : ScriptableObject
	{
		public string version = "version 3.5.4";
		public string text = "Thank you for purchasing TileWorldCreator version 3 and supporting a small indie game studio!" +
			"\n\n" +
			"<b>Getting started</b> \n" +
			"To get started quickly head over to the getting started guide or the getting started video.\n\n" +
			"<b>Demo scenes</b> \n" +
			"You can find all demo scenes in the folder: \n<i>TileWorldCreator / Demo </i> \n\n";
		
		public string documentationLink = "https://doorfortyfour.github.io/TileWorldCreator";
		public string gettingStartedLink = "https://doorfortyfour.github.io/TileWorldCreator/#/GettingStarted?id=quick-start";
		public string faqLink = "https://doorfortyfour.github.io/TileWorldCreator/#/faq";
		public string videoGettingStartedLink = "https://www.youtube.com/watch?v=cscc5_BeY58";
		public string websiteLink = "https://www.tileworldcreator.com/";
		public string assetStoreLink = "https://u3d.as/2Dz4";
		public string emailLink = "mailto:hello@giantgrey.com";
		
		public string databrainLink = "https://assetstore.unity.com/packages/tools/game-toolkits/databrain-ultimate-game-data-framework-244557";	
		public string marzLink = "https://store.steampowered.com/app/682530/MarZ_Tactical_Base_Defense";
		
		public string changelog = "";
	}
}