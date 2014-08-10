using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace UnityEditor.PlayAdsSDK
{
	public class PBXList : ArrayList
	{
		public PBXList()
		{
			
		}
		
		public PBXList( object firstValue )
		{
			this.Add( firstValue );
		}
	}
}
