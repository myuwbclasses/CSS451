// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/451Shader"
{
	Properties
	{
		
	}
	SubShader
	{
		Tags { "RenderType"="Transparent" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex MyVert
			#pragma fragment MyFrag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
			};
			
			v2f MyVert (appdata v)
			{
				v2f o;
                
                // Can use one of the followings:
                // o.vertex = UnityObjectToClipPos(v.vertex);  // Camera + GameObject transform TRS
                
                o.vertex = mul(unity_ObjectToWorld, v.vertex);
                o.vertex = mul(UNITY_MATRIX_V, o.vertex);
                o.vertex = mul(UNITY_MATRIX_P, o.vertex);

                // o.vertex = mul(UNITY_MATRIX_VP, v.vertex);   // camera transform only

                return o;
			}
			
			fixed4 MyFrag (v2f i) : SV_Target
			{
				// sample the texture
                fixed4 col = fixed4(0.2, 0.2, 0.6, 1.0);
				return col;
			}
			ENDCG
		}
	}
}