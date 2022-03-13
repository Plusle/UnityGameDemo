Shader "CustomPipeline/Waterwave" {
    Properties{
        _Diffuse("Water Color", Color) = (1.0, 1.0, 1.0, 1.0)
        _Specular("Specular Color", Color) = (1.0, 1.0, 1.0, 1.0)
        _Smoothness("Smoothness", Range(8.0, 256.0)) = 16.0
        _NoiseScale("Noise Scale", Float) = 1
        _WaterHeight("Water Height", Float) = 2
        _WaterSpeed("Water Speed", Float) = 2
        _NoiseTex("Gradient Noise", 2D) = "white" {}
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
                float _NoiseScale;
                float _WaterHeight;
                float _WaterSpeed; 
                sampler2D _NoiseTex;

                struct Attributes {
                    float4 position : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct Varyings {
                    float4 position : SV_POSITION;
                    float3 world_pos : TEXCOORD0;
                    float3 object_pos : TEXCOORD1;
                };

                Varyings vs(Attributes i) {
                    //float3 object_pos = i.position.xyz;
                    //float3 inital_world_pos = (float3)mul(unity_ObjectToWorld, i.position);
                    //float2 uv = inital_world_pos.xz;
                    //float time_scale = _WaterSpeed * _Time.x;
                    //float2 offset = float2(time_scale, time_scale);
                    //uv = uv + offset;
                    //float height = tex2Dlod(_NoiseTex, float4(uv, 0, 0)).r * _NoiseScale * _WaterHeight;

                    //Varyings o;
                    //o.object_pos = float3(object_pos.x, height, object_pos.z);
                    //o.world_pos = mul((float3x3)unity_ObjectToWorld, o.object_pos);
                    //o.position = UnityObjectToClipPos(o.object_pos);

                    //return o;



                    //float3 object_pos = mul(unity_ObjectToWorld, i.position);
                    //float2 uv = object_pos.xz;
                    //float time_scale = _WaterSpeed * _Time.x;
                    //float2 offset = float2(time_scale, time_scale);
                    //uv = uv + offset;
                    //float height = tex2Dlod(_NoiseTex, float4(uv, 0, 0)).r * _NoiseScale * _WaterHeight;

                    //Varyings o;
                    //o.world_pos = mul(unity_ObjectToWorld, float3(object_pos.x, height, object_pos.z));
                    //o.position = mul(UNITY_MATRIX_VP, o.world_pos);
                    //return o;


                    Varyings o;
                    float4 object_pos = i.position;
                    float2 uv = i.uv + float2(_Time.y * _WaterSpeed, _Time.x * _WaterSpeed);
                    float displacement = tex2Dlod(_NoiseTex, float4(uv, 0, 0)).r;
                    object_pos.y = object_pos.y + (displacement * _NoiseScale) + _WaterHeight;
                    float4 world_pos = mul(unity_ObjectToWorld, object_pos);
                    o.position = mul(UNITY_MATRIX_VP, world_pos);
                    o.world_pos = world_pos;
                    o.object_pos = object_pos;
                    return o;
                }

                float4 fs(Varyings i) : SV_Target {
                    float3 _ddx = ddx(i.object_pos);
                    float3 _ddy = ddy(i.object_pos);
                    float3 normal = normalize(cross(_ddy, _ddx));
                    normal = mul(unity_ObjectToWorld, normal);

                    float3 light_dir = normalize(UnityWorldSpaceLightDir(i.world_pos));
                    float3 view_dir = UnityWorldSpaceViewDir(i.world_pos);

                    float3 albedo = _Diffuse.rgb;
                    float3 ambient = UNITY_LIGHTMODEL_AMBIENT.rgb * albedo;

                    float diff = dot(light_dir, normal) * 0.5 + 0.5;
                    float3 diffuse = diff * _LightColor0.rgb * albedo;

                    float3 half_vec = normalize(view_dir + light_dir);
                    float spec = pow(max(0.0, dot(half_vec, normal)), _Smoothness);
                    float3 specular = spec * _LightColor0.rgb * _Specular.rgb;

                    return float4(albedo + diffuse, 1.0);
                    //return _Diffuse;
                }



                ENDCG
            }
    }
        FallBack "Diffuse"
}
