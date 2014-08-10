Shader "Custom/SimpleVertex" {
	Properties {
		_Color ("Main Color", color) = (1,1,1,1)
	}
	SubShader {
		Lighting Off
		Color [_Color]
		Pass {}
	} 
	FallBack "Diffuse"
}
