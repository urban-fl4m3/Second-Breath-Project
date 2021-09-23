Shader "Unlit/NewUnlitShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _noiseTex ("noise", 2D) = "white" {}
        normalizeHPValue ("HpValue", float) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
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
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            sampler2D _noiseTex;
            float normalizeHPValue;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                col = float4(0.0, 0.0, 0.0, 1.0);
                float4 hpCol = float4(0.5, 0.0, 0.0, 1.0);
                hpCol *= pow(cos(i.uv.y * 3.14 - 1.57) * 0.5 + 0.5, 2.0);
                float noiseValue = tex2D(_noiseTex, i.uv - float2(_Time.x * 1.5, 0.0)).r * tex2D(_noiseTex, i.uv * 4.0 - float2(_Time.x * 1.2, 0.0)).r * 0.5;
                hpCol = lerp(hpCol, float4(1.0, 0.0, 0.0, 1.0), noiseValue);
                float coef = 0.0;
                if (i.uv.x <= normalizeHPValue) coef = 1.0;
                else
                {
                    float toEdge = i.uv.x - normalizeHPValue;
                    toEdge = clamp(0.0, 0.15, toEdge);
                    toEdge = smoothstep(0.0, 0.15, toEdge);
                    toEdge = 1.0 - toEdge;
                    coef = toEdge;
                }
                
                col = lerp(col, hpCol, coef);
                // col = i.uv.x <= normalizeHPValue ?  hpCol : col;
    
                UNITY_APPLY_FOG(i.fogCoord, col);
                // col = float4(noiseValue, noiseValue, noiseValue, 1.0);
                return col;
            }
            ENDCG
        }
    }
}
