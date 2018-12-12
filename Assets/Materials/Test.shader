// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "SmartCity/WaveTransparent1"
{
	Properties
	{
		_Color("MainColor", Color) = (1,1,1,1)
		_MainTex("Texture", 2D) = "white" {}
	_TransRatio("Transparency Ratio Value", Range(-1,20)) = 0
	}
		SubShader
	{
		Tags{ "RenderType" = "Transparent" }
		Tags{ "Queue" = "Transparent" }
		LOD 100

		Pass
	{
		ZWrite Off
		Blend  SrcAlpha OneMinusSrcAlpha
		Cull off

		CGPROGRAM
#pragma vertex vert
#pragma fragment frag


#include "UnityCG.cginc"

		uniform float4 _ColorWithAlpha;

	struct appdata
	{
		float4 vertex : POSITION;
		float2 uv : TEXCOORD0;
	};

	struct v2f
	{
		float2 uv : TEXCOORD0;
		float4 vertex : SV_POSITION;
	};

	fixed4 _Color;
	sampler2D _MainTex;
	float4 _MainTex_ST;

	float _TransRatio;

	v2f vert(appdata v)
	{
		v2f o;
		o.vertex = UnityObjectToClipPos(v.vertex);
		o.uv = TRANSFORM_TEX(v.uv, _MainTex);
		return o;
	}

	fixed4 frag(v2f i) : SV_Target
	{
		// sample the texture
		fixed2 center = fixed2(0.5,0.5);
	fixed2 uv = i.uv - center;

	fixed rMax = length(0 - center);
	fixed tMax = 5;

	fixed rr = length(uv);
	fixed rrr = rr * 30 * (1 - 0.3*rr / rMax);//波的周期衰减

	fixed xx = rrr - _Time.y;// fmod(rrr -_TimeV,2);//
	//fixed xx = rrr;
	fixed sinA = uv.y / rr;
	fixed cosA = uv.x / rr;

	//sin(A*x*PI); 周期为At = 2/A；
	fixed At = 2;
	fixed A = 1;

	fixed ss = fmod(xx,At) - 0.5*At;
	fixed sinYy = sin(A*3.14159*ss);

	fixed4 col;
	rr += 0.05*sinYy*(1 - 0.2*rr / rMax);//波的高度衰减
	fixed2 uvn = fixed2(cosA,sinA)*rr;
	col = tex2D(_MainTex, uvn + center);

	col.a *= 1 - _TransRatio * rr / rMax;
	return col * _Color;
	}
		ENDCG
	}
	}
		FallBack "Diffuse"
}
