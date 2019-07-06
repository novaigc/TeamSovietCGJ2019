Shader "Unlit/blackwhite"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_Edgeonly("edge only",float) = 1.0
		_Edgecolor("edge color", Color) = (0,0,0,1)
		_Blackground("blackground", Color) = (1,1,1,1)
	}
		SubShader
		{
			Tags { "Reandertype" = "Opaque" }
			LOD 100

			Pass
			{
				ZTest Always Cull Off ZWrite Off				
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog

			#include "UnityCG.cginc"

			sampler2D _MainTex;
		    half4 _MainTex_TexelSize;
			fixed _Edgeonly;
			fixed4 _Edgecolor;
			fixed4 _Blackground;

            

            struct v2f
            {
                float2 uv[9] : TEXCOORD0;              
                float4 vertex : SV_POSITION;
			};
			fixed lum(fixed4 color)
			{
				return 0.2125*color.r + 0.7154*color.g + 0.0721*color.b;
			}

			half sobel(v2f i)
			{
				const half gx[9] = { -1,-2,-1,0,0,0,1,2,1 };
				const half gy[9] = { -1,0,1,-2,0,2,-1,0,1 };
				half tex;
				half edgex = 0;
				half edgey = 0;
				for (int x = 0; x < 9; x++)
				{
					tex = lum(tex2D(_MainTex, i.uv[x]));
					edgex += tex * gx[x];
					edgey += tex * gy[x];
				}
				return 1 - abs(edgex) - abs(edgey);
			}

            v2f vert (appdata_img v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
				half2 uv = v.texcoord;
                o.uv[0] = uv+ _MainTex_TexelSize.xy * half2(-1,-1);
				o.uv[1] = uv + _MainTex_TexelSize.xy * half2(0, -1);
				o.uv[2] = uv + _MainTex_TexelSize.xy * half2(1, -1);
				o.uv[3] = uv + _MainTex_TexelSize.xy * half2(-1, 0);
				o.uv[4] = uv + _MainTex_TexelSize.xy * half2(0, 0);
				o.uv[5] = uv + _MainTex_TexelSize.xy * half2(1, 0);
				o.uv[6] = uv + _MainTex_TexelSize.xy * half2(-1, 1);
				o.uv[7] = uv + _MainTex_TexelSize.xy * half2(0, 1);
				o.uv[8] = uv + _MainTex_TexelSize.xy * half2(1, 1);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
			   half edge = sobel(i);
			   fixed4 withcolor = lerp(_Edgecolor, tex2D(_MainTex, i.uv[4]), edge);
			   fixed4 onlycolor = lerp(_Edgecolor, _Blackground, edge);
			   return lerp(withcolor, onlycolor, _Edgeonly);
            }
			
            ENDCG
        }
    }
}
