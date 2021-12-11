// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/451NoCullShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
        _SecTex("Second Texture", 2D) = "white" {}
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 200
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
                float2 uv1 : TEXCOORD1;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float3 normal : NORMAL;
				float2 uv : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
			};

			sampler2D _MainTex;
            sampler2D _SecTex;
			
			float MyTexOffset_X;
			float MyTexOffset_Y;
			float MyTexScale_X;
			float MyTexScale_Y;

            // checker texture support
            float URepeat;
            float VRepeat;
            float4 Color1;
            float4 Color2;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);   // World to NDC

				// o.uv = TRANSFORM_TEX(v.uv, _MainTex);   // WHAT IS THIS DOING?
				// replace with the following
					o.uv.x = v.uv.x * MyTexScale_X + MyTexOffset_X;
					o.uv.y = v.uv.y * MyTexScale_Y + MyTexOffset_Y;
				o.normal = v.normal; // NOTE: this is in the original object space!!

                o.uv1 = v.uv1;  // passing on the second texture
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
                fixed4 c2 = tex2D(_SecTex, i.uv1);
                col = 0.2 * col + 0.8 * c2;

                // OK, lets compute checker texture and apply
                //   algorithm goes, multiple U by URepeat, take int, if odd use Color1 else Color2
                int scaledU = int(i.uv.x * URepeat);
                int uEven = fmod(scaledU, 2);
                int scaledV = int(i.uv.y * VRepeat);
                int vEven = fmod(scaledV, 2);
                fixed4 ct = Color1;
                if (uEven != vEven)
                    ct = Color2;

				return col * 0.2 + ct * 0.8;
			}
			ENDCG
		}
	}
}