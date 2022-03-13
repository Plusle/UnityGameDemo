Shader "CustomPipeline/HalfLambertianTexture" {
	Properties{
		_Color("Color Tint", Color) = (1, 1, 1, 1)
		_MainTex("Main Tex", 2D) = "white" {}
	}
	SubShader{
		Tags { "RenderType" = "Opaque" "Queue" = "Geometry"}

		Pass {
			Tags { "LightMode" = "ForwardBase" }

			CGPROGRAM

			#pragma multi_compile_fwdbase

			#pragma vertex vert
			#pragma fragment frag

			#include "Lighting.cginc"
			#include "AutoLight.cginc"

			fixed4 _Color;
			sampler2D _MainTex;
			float4 _MainTex_ST;

			struct a2v {
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f {
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				float3 world_pos : TEXCOORD1;
				float3 normal : TEXCOORD2;
				SHADOW_COORDS(3)
			};

			v2f vert(a2v v) {
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);

				o.uv = v.texcoord.xy * _MainTex_ST.xy + _MainTex_ST.zw;

				float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;

				o.world_pos = worldPos;
				o.normal = UnityObjectToWorldNormal(v.normal);

				TRANSFER_SHADOW(o);

				return o;
			}

			fixed4 frag(v2f i) : SV_Target {
				float3 world_pos = i.world_pos;
				fixed3 light_dir = normalize(UnityWorldSpaceLightDir(world_pos));

				fixed3 normal = i.normal;

				fixed3 albedo = tex2D(_MainTex, i.uv).rgb * _Color.rgb;

				fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz * albedo;

				fixed3 diffuse = _LightColor0.rgb * albedo * max(0, dot(normal, light_dir));

				UNITY_LIGHT_ATTENUATION(atten, i, world_pos);

				return fixed4(ambient + diffuse * atten, 1.0);
			}

			ENDCG
		}

	}
		FallBack "Diffuse"
}
