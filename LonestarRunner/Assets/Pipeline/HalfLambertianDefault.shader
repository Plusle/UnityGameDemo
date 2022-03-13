Shader "CustomPipeline/HalfLambertianDefault" {
    Properties {
        _Diffuse("Diffuse Color", Color) = (1.0, 1.0, 1.0, 1.0)
        _Specular("Specular Color", Color) = (1.0, 1.0, 1.0, 1.0)
        _Smoothness("Smoothness", Range(8.0, 256.0)) = 16.0
    }
    SubShader{
        Pass {
            Tags { "LightMode" = "ForwardBase" }

            CGPROGRAM
            #pragma vertex vs
            #pragma fragment fs

            #include "UnityCG.cginc"
            #include "Lighting.cginc"

            float4 _Diffuse;
            float4 _Specular;
            float _Smoothness;

            struct a2v {
                float4 position : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f {
                float4 position : SV_POSITION;
                float3 normal : COLOR0;
                float3 world_pos : TEXCOORD1;
            };

            v2f vs(a2v i) {
                v2f o;
                o.position = UnityObjectToClipPos(i.position);
                o.normal = UnityObjectToWorldNormal(i.normal);
                o.world_pos = (float3)(mul(unity_ObjectToWorld, i.position));
                return o;
            }

            float4 fs(v2f i) : SV_Target {
                float3 light_dir = normalize(UnityWorldSpaceLightDir(i.world_pos));
                float3 view_dir = UnityWorldSpaceViewDir(i.world_pos);

                float3 albedo = _Diffuse;
                float3 ambient = UNITY_LIGHTMODEL_AMBIENT.rgb * albedo;
                
                float diff = max(0.0, dot(light_dir, i.normal));
                float3 diffuse = diff * _LightColor0.rgb * albedo;

                float3 half_vec = normalize(view_dir + light_dir);
                float spec = pow(max(0.0, dot(half_vec, i.normal)), _Smoothness);
                float3 specular = spec * _LightColor0.rgb * _Specular.rgb;

                return float4(ambient + diffuse + specular, 1.0);
            }



            ENDCG
        }
    }
    FallBack "Diffuse"
}
