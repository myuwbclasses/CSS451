Shader "Unlit/451Shader"
{
	Properties
	{
		
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
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
			};

            // our own matrix
            float4x4 MyXformMat;  // our own transform matrix!!
            fixed4   MyColor;
			
			v2f MyVert (appdata v)
			{
				v2f o;
                
                // Can use one of the followings:
                // o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);  // Camera + GameObject transform TRS

                o.vertex = mul(MyXformMat, v.vertex);  // use our own transform matrix!
                    // MUST apply before camrea!

                o.vertex = mul(UNITY_MATRIX_VP, o.vertex);   // camera transform only                
				
                return o;
			}
			
			fixed4 MyFrag (v2f i) : SV_Target
			{
				// sample the texture
                fixed4 col = MyColor;
				return col;
			}
			ENDCG
		}
	}
}