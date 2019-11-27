Shader "Unlit/PlanarMappingXZ"
{

	Properties {
		_Tint ("Tint", Color) = (1, 1, 1, 1)
		_MainTex ("Texture", 2D) = "white" {}
	}

	SubShader {
		Pass {
			CGPROGRAM
			#pragma vertex MyVertexProgram
			#pragma fragment MyFragmentProgram

			#include "UnityCG.cginc"
			
			float4 _Tint;
			sampler2D _MainTex;
			float4 _MainTex_ST;

			struct Interpolators {
				float4 position : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct VetextData {
				float4 position : POSITION;
				float2 uv : TEXCOORD0;
			};

			Interpolators MyVertexProgram (VetextData v) {
				Interpolators i;
				i.position = UnityObjectToClipPos(v.position);
				float4 worldPos = mul(unity_ObjectToWorld, v.position);
				i.uv = float4(worldPos.x, worldPos.z, worldPos.y, 1) * _MainTex_ST.xy + _MainTex_ST.zw;
				return i;
			}

			float4 MyFragmentProgram (Interpolators i) : SV_TARGET {
				return tex2D(_MainTex, i.uv) * _Tint;
			}
			ENDCG
		}
	}
}
