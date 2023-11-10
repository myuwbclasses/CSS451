// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/451NoCullShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100
		Cull Off

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float3 normal : NORMAL;
				float2 uv : TEXCOORD0;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);   // World to NDC
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);   // WHAT IS THIS DOING?

                // deals with normal
                // 1. simply passing it on
                o.normal = v.normal;

                // 2. transpose(WorldToObject) * n
                // o.normal = mul(transpose(unity_WorldToObject), v.normal);

                //3. n * WorldToObject <-- same as doing inverse-Transpose of Model transform
                // o.normal = normalize(mul(v.normal, (float3x3)unity_WorldToObject));  // in case there is scaling, and ignore translation

                // 4. transform with unit_ObjectToWorld <-- WRONG
                // o.normal = mul(v.normal, unity_ObjectToWorld);
                
                
                // NOTE: transform the object space normal by Inrevse Transpose into the World space
                normalize(o.normal);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
				// col = 0.5 * col + 0.5 * fixed4(i.normal, 1.0);
                col = fixed4(i.normal, 1.0);
				return col;
			}
			ENDCG
		}
	}
}