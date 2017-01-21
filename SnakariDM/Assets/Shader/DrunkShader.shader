Shader "Hidden/DrunkShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_DisplaceTex ("Displacement Texture", 2D) = "white" {}
		_WaveScale ("Wave Scale", Vector) =  (1, 1, 0, 0)
		_TimeScale("Time Scale", Vector) = (1, 1, 0, 0)
		_GhostImage("Ghost Image", Float) = 0
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

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

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;
			sampler2D _DisplaceTex;
			float4 _WaveScale;
			float4 _TimeScale;
			float _GhostImage;

			fixed4 frag (v2f i) : SV_Target
			{
				float2 distortion = float2(i.uv);
				distortion.x += _WaveScale.x * cos(i.uv.y * 6.0 + _Time.y * _TimeScale.x);
				distortion.y += _WaveScale.y * sin(i.uv.x * 10.0 + _Time.y * _TimeScale.y);

				float2 timeVec2 = float2(_Time.y, _Time.y);
				float2 dispUV = timeVec2 * 0.01 + i.uv * 0.2;
				distortion.x += 0.1 * tex2D(_DisplaceTex, dispUV).r;
				distortion.y += 0.1 * tex2D(_DisplaceTex, dispUV).g;

				float2 distortion2 = float2(i.uv * 3);
				distortion2.x += 2 * _WaveScale.x * cos(i.uv.y * 10.0 + _Time.y * 2);
				distortion2.y += 2 * _WaveScale.y * sin(i.uv.x * 10.0 + _Time.y * 2);

				float2 distortion3 = float2(i.uv);
				distortion3.x += 2 * _WaveScale.x * cos(i.uv.y * 20.0 + _Time.y * _TimeScale.x + 234);
				distortion3.y += 2 * _WaveScale.y * sin(i.uv.x * 20.0 + _Time.y * _TimeScale.y + 789);

				float pii = 3.141592;
				float c = clamp(0, 1, pow(sin(i.uv.x * pii), 2));

				fixed4 col = tex2D(_MainTex, lerp(i.uv, distortion, c));
				col -= 0.4 * pow(tex2D(_DisplaceTex, distortion2 * 2), 2);
				col += _GhostImage * tex2D(_MainTex, distortion3);

				return col;
			}
			ENDCG
		}
	}
}
