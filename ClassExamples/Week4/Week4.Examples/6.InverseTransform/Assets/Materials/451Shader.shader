Shader "Unlit/451Shader"
{
    Properties
    {
        MyColor("ColorLabel", Color) = (0.2, 0.2, 0.8, 1)
        // Syntax here:
        //    MyColor           is the variable name
        //    ColorLabel        is shown at the Editor 
        //    Color             is Unity pre-defined data type (other data types, 
        //                          refer to: https://docs.unity3d.com/Manual/SL-PropertiesInPrograms.html
        //   (value, v, v, v)   is the default value
    }
    SubShader
    {
        Tags{ "RenderType" = "Opaque" }
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

    // our own matrix
    float4x4 MyXformMat;  // our own transform matrix!!
    float4 MyColor;

    v2f MyVert(appdata v)
    {
        v2f o;

        // Can use one of the followings:
        // o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);  // Camera + GameObject transform TRS

        o.vertex = mul(MyXformMat, v.vertex);  // use our own transform matrix!
                                               // MUST apply before camrea!

        o.vertex = mul(UNITY_MATRIX_VP, o.vertex);   // camera transform only



        return o;
    }

    fixed4 MyFrag(v2f i) : SV_Target
    {
        // sample the texture
        return MyColor;
    }
        ENDCG
    }
    }
}