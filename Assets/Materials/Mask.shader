Shader "mask shader"
{
	Properties
	{
		_Color("Main Color",color) = (1,1,1,1)
		_MainTex("Base (RGB)", 2D) = "white" {}
	_Mask("Culling Mask", 2D) = "white" {}
	_Cutoff("Alpha cutoff", Range(0,1)) = 0.1
	}
		SubShader
	{
		color[_Color]
		Tags{ "Queue" = "Transparent" }
		Lighting Off
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha
		AlphaTest GEqual[_Cutoff]
		Pass
	{
		material
		{
		diffuse[_Color]
		}
		SetTexture[_Mask]{ combine texture  }
		SetTexture[_MainTex]{ 
		constantColor[_Color]
		combine texture, previous*constant  }
	}
	}
}

