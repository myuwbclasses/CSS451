Shader "Unlit/451Shader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 200

		Pass
		{
			CGPROGRAM
			#pragma vertex MyVert
			#pragma fragment MyFrag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

            // our own matrix
            float4x4 MyXformMat;  // our own transform matrix!!
            fixed4   MyColor;

			// Texture support
			sampler2D _MainTex;
			float4 _MainTex_ST;
			
			v2f MyVert (appdata v)
			{
				v2f o;
                
                // Can use one of the followings:
                // o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);  // Camera + GameObject transform TRS

                o.vertex = mul(MyXformMat, v.vertex);  // use our own transform matrix!
                    // MUST apply before camrea!

                o.vertex = mul(UNITY_MATRIX_VP, o.vertex);   // camera transform only                
				
				// Texture support
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
			}
			
			fixed4 MyFrag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
				col += MyColor;
				return col;
			}
			ENDCG
		}
	}
}