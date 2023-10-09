Shader "Unlit/NodeShader"
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

			sampler2D _MainTex;
            float4x4 MyTRSMatrix;
            fixed4 MyColor;

			
			v2f vert (appdata v)
			{
				v2f o;
                o.vertex = mul(MyTRSMatrix, v.vertex);
                o.vertex = mul(UNITY_MATRIX_VP, o.vertex);

                o.uv = v.uv;
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
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
