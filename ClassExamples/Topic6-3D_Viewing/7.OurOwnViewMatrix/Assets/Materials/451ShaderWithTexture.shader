Shader "Unlit/451ShaderWithTexture"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex MyVert
			#pragma fragment MyFrag
			// make fog work
			#pragma multi_compile_fog
			
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

			sampler2D _MainTex;
			float4 _MainTex_ST;
            float4x4 MyXformMat;
            fixed4 MyColor;
            float4x4 CameraViewMatrix;
			
			v2f MyVert (appdata v)
			{
				v2f o;
				o.vertex = mul(MyXformMat, v.vertex);  // Object to Wolrd (From SceneNode + Primitive/GameObject)
                // o.vertex = mul(UNITY_MATRIX_V, o.vertex);  // From Camera's View: World to View transform
				o.vertex = mul(CameraViewMatrix, o.vertex);  // From Camera's View: World to View transform
                o.vertex = mul(UNITY_MATRIX_P, o.vertex);    // From Camera's FOV+N+F: View to Perspective
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}
			
			fixed4 MyFrag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
				return fixed4(1.0, 0.0, 0.0, 1.0); // col * (1.0 - MyColor.a) + (MyColor * MyColor.a);
			}
			ENDCG
		}
	}
}
