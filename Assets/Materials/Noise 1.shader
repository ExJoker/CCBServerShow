Shader "Noise/UI_Noise2"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
		//颜色
		_MainTex("MainTex", 2D) = "white" {}
	//主纹理
	_NoiseTex("NoiseTex", 2D) = "white" {}
	//噪波纹理
	_ShakeStrength("ShakeStrength", Float) = 1
		//振动值
	}

		SubShader
	{
		Blend SrcAlpha OneMinusSrcAlpha

		Pass
	{
		CGPROGRAM
#pragma vertex vert
#pragma fragment frag

#include "UnityCG.cginc"
#include "UnityUI.cginc"

		struct a2v
	{
		float4 vertex   : POSITION;
		float4 color    : COLOR;
		float2 texcoord : TEXCOORD0;
	};

	struct v2f
	{
		float4 vertex   : SV_POSITION;
		fixed4 color : COLOR;
		half2 texcoord  : TEXCOORD0;
		float4 worldPosition : TEXCOORD1;
	};

	fixed4 _Color;

	sampler2D _MainTex;
	sampler2D _NoiseTex;

	fixed _ShakeStrength;
	fixed _ProgressRate;


	v2f vert(a2v v)
	{
		v2f f;
		f.worldPosition = v.vertex;

		float3 noiseOffset = tex2Dlod(_NoiseTex, float4(v.texcoord* _Time, 0.0, 0.0)).rgb;
		//计算噪波偏移

		float strength = _ShakeStrength;

		noiseOffset = noiseOffset *strength;

		f.worldPosition.xy += noiseOffset.xy * 15;
		//噪波的扭曲程度

		f.vertex = UnityObjectToClipPos(f.worldPosition);

		f.texcoord = v.texcoord;

		f.color = v.color * _Color;

		f.color.a *= pow(_ProgressRate,2);
		f.color.a+=
		return f;
	}


	fixed4 frag(v2f f) : SV_Target
	{

		half4 color = (tex2D(_MainTex, f.texcoord)) * f.color;

		return color;
	}
		ENDCG
	}

	}
}