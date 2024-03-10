Shader "SpinShader"
{
    Properties
    {
        _Speed("Speed", Float) = 1
        _SpinDir("SpinDir", Float) = 1
        _Width("Width", Float) = 0.3
        _TailLength("TailLength", Range(0.5, 10)) = 1
        _Color("Color", Color) = (1, 1, 1, 0.003921569)
        [HideInInspector]_QueueOffset("_QueueOffset", Float) = 0
        [HideInInspector]_QueueControl("_QueueControl", Float) = -1
        [HideInInspector][NoScaleOffset]unity_Lightmaps("unity_Lightmaps", 2DArray) = "" {}
        [HideInInspector][NoScaleOffset]unity_LightmapsInd("unity_LightmapsInd", 2DArray) = "" {}
        [HideInInspector][NoScaleOffset]unity_ShadowMasks("unity_ShadowMasks", 2DArray) = "" {}
        
        _Stencil("Stencil ID", Float) = 0
        _StencilComp("StencilComp", Float) = 8
        _StencilOp("StencilOp", Float) = 0
        _StencilReadMask("StencilReadMask", Float) = 255
        _StencilWriteMask("StencilWriteMask", Float) = 255
        _ColorMask("ColorMask", Float) = 15
    }
    SubShader
    {
        Tags
        {
            "RenderPipeline"="UniversalPipeline"
            "RenderType"="Transparent"
            "UniversalMaterialType" = "Unlit"
            "Queue"="Transparent"
            "ShaderGraphShader"="true"
            "ShaderGraphTargetId"="UniversalUnlitSubTarget"
        }
        Pass
        {
            Name "Universal Forward"
            Tags
            {
                // LightMode: <None>
            }
        
        // Render State
        Cull Back
        Blend SrcAlpha OneMinusSrcAlpha, One OneMinusSrcAlpha
        ZTest [unity_]
        ZWrite Off
        
        // Debug
        // <None>
        
        // --------------------------------------------------
        // Pass
        
        HLSLPROGRAM
        
        // Pragmas
        #pragma target 4.5
        #pragma exclude_renderers gles gles3 glcore
        #pragma multi_compile_instancing
        #pragma multi_compile_fog
        #pragma instancing_options renderinglayer
        #pragma multi_compile _ DOTS_INSTANCING_ON
        #pragma vertex vert
        #pragma fragment frag
        
        // DotsInstancingOptions: <None>
        // HybridV1InjectedBuiltinProperties: <None>
        
        // Keywords
        #pragma multi_compile _ LIGHTMAP_ON
        #pragma multi_compile _ DIRLIGHTMAP_COMBINED
        #pragma shader_feature _ _SAMPLE_GI
        #pragma multi_compile_fragment _ _DBUFFER_MRT1 _DBUFFER_MRT2 _DBUFFER_MRT3
        #pragma multi_compile_fragment _ DEBUG_DISPLAY
        // GraphKeywords: <None>
        
        // Defines
        
        #define ATTRIBUTES_NEED_NORMAL
        #define ATTRIBUTES_NEED_TANGENT
        #define ATTRIBUTES_NEED_TEXCOORD0
        #define VARYINGS_NEED_POSITION_WS
        #define VARYINGS_NEED_NORMAL_WS
        #define VARYINGS_NEED_TEXCOORD0
        #define VARYINGS_NEED_VIEWDIRECTION_WS
        #define FEATURES_GRAPH_VERTEX
        /* WARNING: $splice Could not find named fragment 'PassInstancing' */
        #define SHADERPASS SHADERPASS_UNLIT
        #define _FOG_FRAGMENT 1
        #define _SURFACE_TYPE_TRANSPARENT 1
        /* WARNING: $splice Could not find named fragment 'DotsInstancingVars' */
        
        
        // custom interpolator pre-include
        /* WARNING: $splice Could not find named fragment 'sgci_CustomInterpolatorPreInclude' */
        
        // Includes
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DBuffer.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
        
        // --------------------------------------------------
        // Structs and Packing
        
        // custom interpolators pre packing
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPrePacking' */
        
        struct Attributes
        {
             float3 positionOS : POSITION;
             float3 normalOS : NORMAL;
             float4 tangentOS : TANGENT;
             float4 uv0 : TEXCOORD0;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : INSTANCEID_SEMANTIC;
            #endif
        };
        struct Varyings
        {
             float4 positionCS : SV_POSITION;
             float3 positionWS;
             float3 normalWS;
             float4 texCoord0;
             float3 viewDirectionWS;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        struct SurfaceDescriptionInputs
        {
             float4 uv0;
             float3 TimeParameters;
        };
        struct VertexDescriptionInputs
        {
             float3 ObjectSpaceNormal;
             float3 ObjectSpaceTangent;
             float3 ObjectSpacePosition;
        };
        struct PackedVaryings
        {
             float4 positionCS : SV_POSITION;
             float3 interp0 : INTERP0;
             float3 interp1 : INTERP1;
             float4 interp2 : INTERP2;
             float3 interp3 : INTERP3;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        
        PackedVaryings PackVaryings (Varyings input)
        {
            PackedVaryings output;
            ZERO_INITIALIZE(PackedVaryings, output);
            output.positionCS = input.positionCS;
            output.interp0.xyz =  input.positionWS;
            output.interp1.xyz =  input.normalWS;
            output.interp2.xyzw =  input.texCoord0;
            output.interp3.xyz =  input.viewDirectionWS;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        Varyings UnpackVaryings (PackedVaryings input)
        {
            Varyings output;
            output.positionCS = input.positionCS;
            output.positionWS = input.interp0.xyz;
            output.normalWS = input.interp1.xyz;
            output.texCoord0 = input.interp2.xyzw;
            output.viewDirectionWS = input.interp3.xyz;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        
        // --------------------------------------------------
        // Graph
        
        // Graph Properties
        CBUFFER_START(UnityPerMaterial)
        float _Speed;
        float _SpinDir;
        float _Width;
        float _TailLength;
        float4 _Color;
        CBUFFER_END
        
        // Object and Global properties
        
        // Graph Includes
        // GraphIncludes: <None>
        
        // -- Property used by ScenePickingPass
        #ifdef SCENEPICKINGPASS
        float4 _SelectionID;
        #endif
        
        // -- Properties used by SceneSelectionPass
        #ifdef SCENESELECTIONPASS
        int _ObjectId;
        int _PassValue;
        #endif
        
        // Graph Functions
        
        void Unity_Multiply_float_float(float A, float B, out float Out)
        {
            Out = A * B;
        }
        
        void Unity_Rotate_Radians_float(float2 UV, float2 Center, float Rotation, out float2 Out)
        {
            //rotation matrix
            UV -= Center;
            float s = sin(Rotation);
            float c = cos(Rotation);
        
            //center rotation matrix
            float2x2 rMatrix = float2x2(c, -s, s, c);
            rMatrix *= 0.5;
            rMatrix += 0.5;
            rMatrix = rMatrix*2 - 1;
        
            //multiply the UVs by the rotation matrix
            UV.xy = mul(UV.xy, rMatrix);
            UV += Center;
        
            Out = UV;
        }
        
        void Unity_PolarCoordinates_float(float2 UV, float2 Center, float RadialScale, float LengthScale, out float2 Out)
        {
            float2 delta = UV - Center;
            float radius = length(delta) * 2 * RadialScale;
            float angle = atan2(delta.x, delta.y) * 1.0/6.28 * LengthScale;
            Out = float2(radius, angle);
        }
        
        void Unity_Rectangle_Fastest_float(float2 UV, float Width, float Height, out float Out)
        {
            float2 d = abs(UV * 2 - 1) - float2(Width, Height);
        #if defined(SHADER_STAGE_RAY_TRACING)
            d = saturate((1 - saturate(d * 1e7)));
        #else
            d = saturate(1 - d / fwidth(d));
        #endif
            Out = min(d.x, d.y);
        }
        
        void Unity_Rotate_Degrees_float(float2 UV, float2 Center, float Rotation, out float2 Out)
        {
            //rotation matrix
            Rotation = Rotation * (3.1415926f/180.0f);
            UV -= Center;
            float s = sin(Rotation);
            float c = cos(Rotation);
        
            //center rotation matrix
            float2x2 rMatrix = float2x2(c, -s, s, c);
            rMatrix *= 0.5;
            rMatrix += 0.5;
            rMatrix = rMatrix*2 - 1;
        
            //multiply the UVs by the rotation matrix
            UV.xy = mul(UV.xy, rMatrix);
            UV += Center;
        
            Out = UV;
        }
        
        void Unity_Preview_float(float In, out float Out)
        {
            Out = In;
        }
        
        void Unity_Saturate_float(float In, out float Out)
        {
            Out = saturate(In);
        }
        
        void Unity_Power_float(float A, float B, out float Out)
        {
            Out = pow(A, B);
        }
        
        void Unity_Smoothstep_float(float Edge1, float Edge2, float In, out float Out)
        {
            Out = smoothstep(Edge1, Edge2, In);
        }
        
        // Custom interpolators pre vertex
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreVertex' */
        
        // Graph Vertex
        struct VertexDescription
        {
            float3 Position;
            float3 Normal;
            float3 Tangent;
        };
        
        VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
        {
            VertexDescription description = (VertexDescription)0;
            description.Position = IN.ObjectSpacePosition;
            description.Normal = IN.ObjectSpaceNormal;
            description.Tangent = IN.ObjectSpaceTangent;
            return description;
        }
        
        // Custom interpolators, pre surface
        #ifdef FEATURES_GRAPH_VERTEX
        Varyings CustomInterpolatorPassThroughFunc(inout Varyings output, VertexDescription input)
        {
        return output;
        }
        #define CUSTOMINTERPOLATOR_VARYPASSTHROUGH_FUNC
        #endif
        
        // Graph Pixel
        struct SurfaceDescription
        {
            float3 BaseColor;
            float Alpha;
        };
        
        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            float4 _Property_bec96e8a375c459c9444e2dec1d14a9a_Out_0 = _Color;
            float _Property_d94c15e3e2f84f569def2f6f4d8bbcab_Out_0 = _Speed;
            float _Multiply_ed2b4a35ed9147c59203777f0c7b2b2e_Out_2;
            Unity_Multiply_float_float(_Property_d94c15e3e2f84f569def2f6f4d8bbcab_Out_0, IN.TimeParameters.x, _Multiply_ed2b4a35ed9147c59203777f0c7b2b2e_Out_2);
            float _Property_f4a3c549f0874108876488320540244d_Out_0 = _SpinDir;
            float _Multiply_5f0c40c7364e4dcf81df67e0b9a85adb_Out_2;
            Unity_Multiply_float_float(_Property_f4a3c549f0874108876488320540244d_Out_0, -1, _Multiply_5f0c40c7364e4dcf81df67e0b9a85adb_Out_2);
            float _Multiply_7f7f94f247b24711bae1d438f6d4cdfa_Out_2;
            Unity_Multiply_float_float(_Multiply_ed2b4a35ed9147c59203777f0c7b2b2e_Out_2, _Multiply_5f0c40c7364e4dcf81df67e0b9a85adb_Out_2, _Multiply_7f7f94f247b24711bae1d438f6d4cdfa_Out_2);
            float2 _Rotate_35cdf476638c4ddfbfc5a472b4555a08_Out_3;
            Unity_Rotate_Radians_float(IN.uv0.xy, float2 (0.5, 0.5), _Multiply_7f7f94f247b24711bae1d438f6d4cdfa_Out_2, _Rotate_35cdf476638c4ddfbfc5a472b4555a08_Out_3);
            float2 _PolarCoordinates_137488258455411ea60aa4c0c170754f_Out_4;
            Unity_PolarCoordinates_float(_Rotate_35cdf476638c4ddfbfc5a472b4555a08_Out_3, float2 (0.5, 0.5), 0.7, 1.2, _PolarCoordinates_137488258455411ea60aa4c0c170754f_Out_4);
            float _Property_319ca09021c945c2857c1b7ea2a1fa6d_Out_0 = _Width;
            float _Rectangle_fa3b515096cf4847937839ee010b43e4_Out_3;
            Unity_Rectangle_Fastest_float(_PolarCoordinates_137488258455411ea60aa4c0c170754f_Out_4, _Property_319ca09021c945c2857c1b7ea2a1fa6d_Out_0, 3, _Rectangle_fa3b515096cf4847937839ee010b43e4_Out_3);
            float2 _Rotate_e221fbd58f8d426aa1a4907dd2ada1e1_Out_3;
            Unity_Rotate_Degrees_float(_Rotate_35cdf476638c4ddfbfc5a472b4555a08_Out_3, float2 (0.5, 0.5), -45, _Rotate_e221fbd58f8d426aa1a4907dd2ada1e1_Out_3);
            float _Property_860963b9226a4064a3c9487923211bfc_Out_0 = _TailLength;
            float2 _PolarCoordinates_dd40abfe6e5842c2b45c2a148178b7f6_Out_4;
            Unity_PolarCoordinates_float(_Rotate_e221fbd58f8d426aa1a4907dd2ada1e1_Out_3, float2 (0.5, 0.5), 1, _Property_860963b9226a4064a3c9487923211bfc_Out_0, _PolarCoordinates_dd40abfe6e5842c2b45c2a148178b7f6_Out_4);
            float _Split_e374f964ab2f4dcfb3b5d389cac488ee_R_1 = _PolarCoordinates_dd40abfe6e5842c2b45c2a148178b7f6_Out_4[0];
            float _Split_e374f964ab2f4dcfb3b5d389cac488ee_G_2 = _PolarCoordinates_dd40abfe6e5842c2b45c2a148178b7f6_Out_4[1];
            float _Split_e374f964ab2f4dcfb3b5d389cac488ee_B_3 = 0;
            float _Split_e374f964ab2f4dcfb3b5d389cac488ee_A_4 = 0;
            float _Preview_1959caf9d2b54c5a88bf965479f95f73_Out_1;
            Unity_Preview_float(_Split_e374f964ab2f4dcfb3b5d389cac488ee_G_2, _Preview_1959caf9d2b54c5a88bf965479f95f73_Out_1);
            float _Saturate_7a8a4ea4de02406dbafae645cfaa04ee_Out_1;
            Unity_Saturate_float(_Preview_1959caf9d2b54c5a88bf965479f95f73_Out_1, _Saturate_7a8a4ea4de02406dbafae645cfaa04ee_Out_1);
            float _Multiply_12051c8d973743848260fcea125d26ff_Out_2;
            Unity_Multiply_float_float(_Rectangle_fa3b515096cf4847937839ee010b43e4_Out_3, _Saturate_7a8a4ea4de02406dbafae645cfaa04ee_Out_1, _Multiply_12051c8d973743848260fcea125d26ff_Out_2);
            float _Power_40414304ee674ccdb6c4065358e09770_Out_2;
            Unity_Power_float(_Multiply_12051c8d973743848260fcea125d26ff_Out_2, 1.56, _Power_40414304ee674ccdb6c4065358e09770_Out_2);
            float _Smoothstep_91dc040ce0744d8880e840faeacb9a07_Out_3;
            Unity_Smoothstep_float(0.03, 0.35, _Power_40414304ee674ccdb6c4065358e09770_Out_2, _Smoothstep_91dc040ce0744d8880e840faeacb9a07_Out_3);
            surface.BaseColor = (_Property_bec96e8a375c459c9444e2dec1d14a9a_Out_0.xyz);
            surface.Alpha = _Smoothstep_91dc040ce0744d8880e840faeacb9a07_Out_3;
            return surface;
        }
        
        // --------------------------------------------------
        // Build Graph Inputs
        #ifdef HAVE_VFX_MODIFICATION
        #define VFX_SRP_ATTRIBUTES Attributes
        #define VFX_SRP_VARYINGS Varyings
        #define VFX_SRP_SURFACE_INPUTS SurfaceDescriptionInputs
        #endif
        VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
        {
            VertexDescriptionInputs output;
            ZERO_INITIALIZE(VertexDescriptionInputs, output);
        
            output.ObjectSpaceNormal =                          input.normalOS;
            output.ObjectSpaceTangent =                         input.tangentOS.xyz;
            output.ObjectSpacePosition =                        input.positionOS;
        
            return output;
        }
        SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
        {
            SurfaceDescriptionInputs output;
            ZERO_INITIALIZE(SurfaceDescriptionInputs, output);
        
        #ifdef HAVE_VFX_MODIFICATION
            // FragInputs from VFX come from two places: Interpolator or CBuffer.
            /* WARNING: $splice Could not find named fragment 'VFXSetFragInputs' */
        
        #endif
        
            
        
        
        
        
        
            output.uv0 = input.texCoord0;
            output.TimeParameters = _TimeParameters.xyz; // This is mainly for LW as HD overwrite this value
        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign =                    IS_FRONT_VFACE(input.cullFace, true, false);
        #else
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        #endif
        #undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        
                return output;
        }
        
        // --------------------------------------------------
        // Main
        
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/Varyings.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/UnlitPass.hlsl"
        
        // --------------------------------------------------
        // Visual Effect Vertex Invocations
        #ifdef HAVE_VFX_MODIFICATION
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/VisualEffectVertex.hlsl"
        #endif
        
        ENDHLSL
        }
        Pass
        {
            Name "DepthNormalsOnly"
            Tags
            {
                "LightMode" = "DepthNormalsOnly"
            }
        
        // Render State
        Cull Back
        ZTest LEqual
        ZWrite On
        
        // Debug
        // <None>
        
        // --------------------------------------------------
        // Pass
        
        HLSLPROGRAM
        
        // Pragmas
        #pragma target 4.5
        #pragma exclude_renderers gles gles3 glcore
        #pragma multi_compile_instancing
        #pragma multi_compile _ DOTS_INSTANCING_ON
        #pragma vertex vert
        #pragma fragment frag
        
        // DotsInstancingOptions: <None>
        // HybridV1InjectedBuiltinProperties: <None>
        
        // Keywords
        // PassKeywords: <None>
        // GraphKeywords: <None>
        
        // Defines
        
        #define ATTRIBUTES_NEED_NORMAL
        #define ATTRIBUTES_NEED_TANGENT
        #define ATTRIBUTES_NEED_TEXCOORD0
        #define ATTRIBUTES_NEED_TEXCOORD1
        #define VARYINGS_NEED_NORMAL_WS
        #define VARYINGS_NEED_TANGENT_WS
        #define VARYINGS_NEED_TEXCOORD0
        #define FEATURES_GRAPH_VERTEX
        /* WARNING: $splice Could not find named fragment 'PassInstancing' */
        #define SHADERPASS SHADERPASS_DEPTHNORMALSONLY
        /* WARNING: $splice Could not find named fragment 'DotsInstancingVars' */
        
        
        // custom interpolator pre-include
        /* WARNING: $splice Could not find named fragment 'sgci_CustomInterpolatorPreInclude' */
        
        // Includes
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
        
        // --------------------------------------------------
        // Structs and Packing
        
        // custom interpolators pre packing
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPrePacking' */
        
        struct Attributes
        {
             float3 positionOS : POSITION;
             float3 normalOS : NORMAL;
             float4 tangentOS : TANGENT;
             float4 uv0 : TEXCOORD0;
             float4 uv1 : TEXCOORD1;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : INSTANCEID_SEMANTIC;
            #endif
        };
        struct Varyings
        {
             float4 positionCS : SV_POSITION;
             float3 normalWS;
             float4 tangentWS;
             float4 texCoord0;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        struct SurfaceDescriptionInputs
        {
             float4 uv0;
             float3 TimeParameters;
        };
        struct VertexDescriptionInputs
        {
             float3 ObjectSpaceNormal;
             float3 ObjectSpaceTangent;
             float3 ObjectSpacePosition;
        };
        struct PackedVaryings
        {
             float4 positionCS : SV_POSITION;
             float3 interp0 : INTERP0;
             float4 interp1 : INTERP1;
             float4 interp2 : INTERP2;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        
        PackedVaryings PackVaryings (Varyings input)
        {
            PackedVaryings output;
            ZERO_INITIALIZE(PackedVaryings, output);
            output.positionCS = input.positionCS;
            output.interp0.xyz =  input.normalWS;
            output.interp1.xyzw =  input.tangentWS;
            output.interp2.xyzw =  input.texCoord0;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        Varyings UnpackVaryings (PackedVaryings input)
        {
            Varyings output;
            output.positionCS = input.positionCS;
            output.normalWS = input.interp0.xyz;
            output.tangentWS = input.interp1.xyzw;
            output.texCoord0 = input.interp2.xyzw;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        
        // --------------------------------------------------
        // Graph
        
        // Graph Properties
        CBUFFER_START(UnityPerMaterial)
        float _Speed;
        float _SpinDir;
        float _Width;
        float _TailLength;
        float4 _Color;
        CBUFFER_END
        
        // Object and Global properties
        
        // Graph Includes
        // GraphIncludes: <None>
        
        // -- Property used by ScenePickingPass
        #ifdef SCENEPICKINGPASS
        float4 _SelectionID;
        #endif
        
        // -- Properties used by SceneSelectionPass
        #ifdef SCENESELECTIONPASS
        int _ObjectId;
        int _PassValue;
        #endif
        
        // Graph Functions
        
        void Unity_Multiply_float_float(float A, float B, out float Out)
        {
            Out = A * B;
        }
        
        void Unity_Rotate_Radians_float(float2 UV, float2 Center, float Rotation, out float2 Out)
        {
            //rotation matrix
            UV -= Center;
            float s = sin(Rotation);
            float c = cos(Rotation);
        
            //center rotation matrix
            float2x2 rMatrix = float2x2(c, -s, s, c);
            rMatrix *= 0.5;
            rMatrix += 0.5;
            rMatrix = rMatrix*2 - 1;
        
            //multiply the UVs by the rotation matrix
            UV.xy = mul(UV.xy, rMatrix);
            UV += Center;
        
            Out = UV;
        }
        
        void Unity_PolarCoordinates_float(float2 UV, float2 Center, float RadialScale, float LengthScale, out float2 Out)
        {
            float2 delta = UV - Center;
            float radius = length(delta) * 2 * RadialScale;
            float angle = atan2(delta.x, delta.y) * 1.0/6.28 * LengthScale;
            Out = float2(radius, angle);
        }
        
        void Unity_Rectangle_Fastest_float(float2 UV, float Width, float Height, out float Out)
        {
            float2 d = abs(UV * 2 - 1) - float2(Width, Height);
        #if defined(SHADER_STAGE_RAY_TRACING)
            d = saturate((1 - saturate(d * 1e7)));
        #else
            d = saturate(1 - d / fwidth(d));
        #endif
            Out = min(d.x, d.y);
        }
        
        void Unity_Rotate_Degrees_float(float2 UV, float2 Center, float Rotation, out float2 Out)
        {
            //rotation matrix
            Rotation = Rotation * (3.1415926f/180.0f);
            UV -= Center;
            float s = sin(Rotation);
            float c = cos(Rotation);
        
            //center rotation matrix
            float2x2 rMatrix = float2x2(c, -s, s, c);
            rMatrix *= 0.5;
            rMatrix += 0.5;
            rMatrix = rMatrix*2 - 1;
        
            //multiply the UVs by the rotation matrix
            UV.xy = mul(UV.xy, rMatrix);
            UV += Center;
        
            Out = UV;
        }
        
        void Unity_Preview_float(float In, out float Out)
        {
            Out = In;
        }
        
        void Unity_Saturate_float(float In, out float Out)
        {
            Out = saturate(In);
        }
        
        void Unity_Power_float(float A, float B, out float Out)
        {
            Out = pow(A, B);
        }
        
        void Unity_Smoothstep_float(float Edge1, float Edge2, float In, out float Out)
        {
            Out = smoothstep(Edge1, Edge2, In);
        }
        
        // Custom interpolators pre vertex
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreVertex' */
        
        // Graph Vertex
        struct VertexDescription
        {
            float3 Position;
            float3 Normal;
            float3 Tangent;
        };
        
        VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
        {
            VertexDescription description = (VertexDescription)0;
            description.Position = IN.ObjectSpacePosition;
            description.Normal = IN.ObjectSpaceNormal;
            description.Tangent = IN.ObjectSpaceTangent;
            return description;
        }
        
        // Custom interpolators, pre surface
        #ifdef FEATURES_GRAPH_VERTEX
        Varyings CustomInterpolatorPassThroughFunc(inout Varyings output, VertexDescription input)
        {
        return output;
        }
        #define CUSTOMINTERPOLATOR_VARYPASSTHROUGH_FUNC
        #endif
        
        // Graph Pixel
        struct SurfaceDescription
        {
            float Alpha;
        };
        
        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            float _Property_d94c15e3e2f84f569def2f6f4d8bbcab_Out_0 = _Speed;
            float _Multiply_ed2b4a35ed9147c59203777f0c7b2b2e_Out_2;
            Unity_Multiply_float_float(_Property_d94c15e3e2f84f569def2f6f4d8bbcab_Out_0, IN.TimeParameters.x, _Multiply_ed2b4a35ed9147c59203777f0c7b2b2e_Out_2);
            float _Property_f4a3c549f0874108876488320540244d_Out_0 = _SpinDir;
            float _Multiply_5f0c40c7364e4dcf81df67e0b9a85adb_Out_2;
            Unity_Multiply_float_float(_Property_f4a3c549f0874108876488320540244d_Out_0, -1, _Multiply_5f0c40c7364e4dcf81df67e0b9a85adb_Out_2);
            float _Multiply_7f7f94f247b24711bae1d438f6d4cdfa_Out_2;
            Unity_Multiply_float_float(_Multiply_ed2b4a35ed9147c59203777f0c7b2b2e_Out_2, _Multiply_5f0c40c7364e4dcf81df67e0b9a85adb_Out_2, _Multiply_7f7f94f247b24711bae1d438f6d4cdfa_Out_2);
            float2 _Rotate_35cdf476638c4ddfbfc5a472b4555a08_Out_3;
            Unity_Rotate_Radians_float(IN.uv0.xy, float2 (0.5, 0.5), _Multiply_7f7f94f247b24711bae1d438f6d4cdfa_Out_2, _Rotate_35cdf476638c4ddfbfc5a472b4555a08_Out_3);
            float2 _PolarCoordinates_137488258455411ea60aa4c0c170754f_Out_4;
            Unity_PolarCoordinates_float(_Rotate_35cdf476638c4ddfbfc5a472b4555a08_Out_3, float2 (0.5, 0.5), 0.7, 1.2, _PolarCoordinates_137488258455411ea60aa4c0c170754f_Out_4);
            float _Property_319ca09021c945c2857c1b7ea2a1fa6d_Out_0 = _Width;
            float _Rectangle_fa3b515096cf4847937839ee010b43e4_Out_3;
            Unity_Rectangle_Fastest_float(_PolarCoordinates_137488258455411ea60aa4c0c170754f_Out_4, _Property_319ca09021c945c2857c1b7ea2a1fa6d_Out_0, 3, _Rectangle_fa3b515096cf4847937839ee010b43e4_Out_3);
            float2 _Rotate_e221fbd58f8d426aa1a4907dd2ada1e1_Out_3;
            Unity_Rotate_Degrees_float(_Rotate_35cdf476638c4ddfbfc5a472b4555a08_Out_3, float2 (0.5, 0.5), -45, _Rotate_e221fbd58f8d426aa1a4907dd2ada1e1_Out_3);
            float _Property_860963b9226a4064a3c9487923211bfc_Out_0 = _TailLength;
            float2 _PolarCoordinates_dd40abfe6e5842c2b45c2a148178b7f6_Out_4;
            Unity_PolarCoordinates_float(_Rotate_e221fbd58f8d426aa1a4907dd2ada1e1_Out_3, float2 (0.5, 0.5), 1, _Property_860963b9226a4064a3c9487923211bfc_Out_0, _PolarCoordinates_dd40abfe6e5842c2b45c2a148178b7f6_Out_4);
            float _Split_e374f964ab2f4dcfb3b5d389cac488ee_R_1 = _PolarCoordinates_dd40abfe6e5842c2b45c2a148178b7f6_Out_4[0];
            float _Split_e374f964ab2f4dcfb3b5d389cac488ee_G_2 = _PolarCoordinates_dd40abfe6e5842c2b45c2a148178b7f6_Out_4[1];
            float _Split_e374f964ab2f4dcfb3b5d389cac488ee_B_3 = 0;
            float _Split_e374f964ab2f4dcfb3b5d389cac488ee_A_4 = 0;
            float _Preview_1959caf9d2b54c5a88bf965479f95f73_Out_1;
            Unity_Preview_float(_Split_e374f964ab2f4dcfb3b5d389cac488ee_G_2, _Preview_1959caf9d2b54c5a88bf965479f95f73_Out_1);
            float _Saturate_7a8a4ea4de02406dbafae645cfaa04ee_Out_1;
            Unity_Saturate_float(_Preview_1959caf9d2b54c5a88bf965479f95f73_Out_1, _Saturate_7a8a4ea4de02406dbafae645cfaa04ee_Out_1);
            float _Multiply_12051c8d973743848260fcea125d26ff_Out_2;
            Unity_Multiply_float_float(_Rectangle_fa3b515096cf4847937839ee010b43e4_Out_3, _Saturate_7a8a4ea4de02406dbafae645cfaa04ee_Out_1, _Multiply_12051c8d973743848260fcea125d26ff_Out_2);
            float _Power_40414304ee674ccdb6c4065358e09770_Out_2;
            Unity_Power_float(_Multiply_12051c8d973743848260fcea125d26ff_Out_2, 1.56, _Power_40414304ee674ccdb6c4065358e09770_Out_2);
            float _Smoothstep_91dc040ce0744d8880e840faeacb9a07_Out_3;
            Unity_Smoothstep_float(0.03, 0.35, _Power_40414304ee674ccdb6c4065358e09770_Out_2, _Smoothstep_91dc040ce0744d8880e840faeacb9a07_Out_3);
            surface.Alpha = _Smoothstep_91dc040ce0744d8880e840faeacb9a07_Out_3;
            return surface;
        }
        
        // --------------------------------------------------
        // Build Graph Inputs
        #ifdef HAVE_VFX_MODIFICATION
        #define VFX_SRP_ATTRIBUTES Attributes
        #define VFX_SRP_VARYINGS Varyings
        #define VFX_SRP_SURFACE_INPUTS SurfaceDescriptionInputs
        #endif
        VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
        {
            VertexDescriptionInputs output;
            ZERO_INITIALIZE(VertexDescriptionInputs, output);
        
            output.ObjectSpaceNormal =                          input.normalOS;
            output.ObjectSpaceTangent =                         input.tangentOS.xyz;
            output.ObjectSpacePosition =                        input.positionOS;
        
            return output;
        }
        SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
        {
            SurfaceDescriptionInputs output;
            ZERO_INITIALIZE(SurfaceDescriptionInputs, output);
        
        #ifdef HAVE_VFX_MODIFICATION
            // FragInputs from VFX come from two places: Interpolator or CBuffer.
            /* WARNING: $splice Could not find named fragment 'VFXSetFragInputs' */
        
        #endif
        
            
        
        
        
        
        
            output.uv0 = input.texCoord0;
            output.TimeParameters = _TimeParameters.xyz; // This is mainly for LW as HD overwrite this value
        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign =                    IS_FRONT_VFACE(input.cullFace, true, false);
        #else
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        #endif
        #undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        
                return output;
        }
        
        // --------------------------------------------------
        // Main
        
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/Varyings.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/DepthNormalsOnlyPass.hlsl"
        
        // --------------------------------------------------
        // Visual Effect Vertex Invocations
        #ifdef HAVE_VFX_MODIFICATION
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/VisualEffectVertex.hlsl"
        #endif
        
        ENDHLSL
        }
        Pass
        {
            Name "ShadowCaster"
            Tags
            {
                "LightMode" = "ShadowCaster"
            }
        
        // Render State
        Cull Back
        ZTest LEqual
        ZWrite On
        ColorMask 0
        
        // Debug
        // <None>
        
        // --------------------------------------------------
        // Pass
        
        HLSLPROGRAM
        
        // Pragmas
        #pragma target 4.5
        #pragma exclude_renderers gles gles3 glcore
        #pragma multi_compile_instancing
        #pragma multi_compile _ DOTS_INSTANCING_ON
        #pragma vertex vert
        #pragma fragment frag
        
        // DotsInstancingOptions: <None>
        // HybridV1InjectedBuiltinProperties: <None>
        
        // Keywords
        #pragma multi_compile_vertex _ _CASTING_PUNCTUAL_LIGHT_SHADOW
        // GraphKeywords: <None>
        
        // Defines
        
        #define ATTRIBUTES_NEED_NORMAL
        #define ATTRIBUTES_NEED_TANGENT
        #define ATTRIBUTES_NEED_TEXCOORD0
        #define VARYINGS_NEED_NORMAL_WS
        #define VARYINGS_NEED_TEXCOORD0
        #define FEATURES_GRAPH_VERTEX
        /* WARNING: $splice Could not find named fragment 'PassInstancing' */
        #define SHADERPASS SHADERPASS_SHADOWCASTER
        /* WARNING: $splice Could not find named fragment 'DotsInstancingVars' */
        
        
        // custom interpolator pre-include
        /* WARNING: $splice Could not find named fragment 'sgci_CustomInterpolatorPreInclude' */
        
        // Includes
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
        
        // --------------------------------------------------
        // Structs and Packing
        
        // custom interpolators pre packing
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPrePacking' */
        
        struct Attributes
        {
             float3 positionOS : POSITION;
             float3 normalOS : NORMAL;
             float4 tangentOS : TANGENT;
             float4 uv0 : TEXCOORD0;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : INSTANCEID_SEMANTIC;
            #endif
        };
        struct Varyings
        {
             float4 positionCS : SV_POSITION;
             float3 normalWS;
             float4 texCoord0;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        struct SurfaceDescriptionInputs
        {
             float4 uv0;
             float3 TimeParameters;
        };
        struct VertexDescriptionInputs
        {
             float3 ObjectSpaceNormal;
             float3 ObjectSpaceTangent;
             float3 ObjectSpacePosition;
        };
        struct PackedVaryings
        {
             float4 positionCS : SV_POSITION;
             float3 interp0 : INTERP0;
             float4 interp1 : INTERP1;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        
        PackedVaryings PackVaryings (Varyings input)
        {
            PackedVaryings output;
            ZERO_INITIALIZE(PackedVaryings, output);
            output.positionCS = input.positionCS;
            output.interp0.xyz =  input.normalWS;
            output.interp1.xyzw =  input.texCoord0;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        Varyings UnpackVaryings (PackedVaryings input)
        {
            Varyings output;
            output.positionCS = input.positionCS;
            output.normalWS = input.interp0.xyz;
            output.texCoord0 = input.interp1.xyzw;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        
        // --------------------------------------------------
        // Graph
        
        // Graph Properties
        CBUFFER_START(UnityPerMaterial)
        float _Speed;
        float _SpinDir;
        float _Width;
        float _TailLength;
        float4 _Color;
        CBUFFER_END
        
        // Object and Global properties
        
        // Graph Includes
        // GraphIncludes: <None>
        
        // -- Property used by ScenePickingPass
        #ifdef SCENEPICKINGPASS
        float4 _SelectionID;
        #endif
        
        // -- Properties used by SceneSelectionPass
        #ifdef SCENESELECTIONPASS
        int _ObjectId;
        int _PassValue;
        #endif
        
        // Graph Functions
        
        void Unity_Multiply_float_float(float A, float B, out float Out)
        {
            Out = A * B;
        }
        
        void Unity_Rotate_Radians_float(float2 UV, float2 Center, float Rotation, out float2 Out)
        {
            //rotation matrix
            UV -= Center;
            float s = sin(Rotation);
            float c = cos(Rotation);
        
            //center rotation matrix
            float2x2 rMatrix = float2x2(c, -s, s, c);
            rMatrix *= 0.5;
            rMatrix += 0.5;
            rMatrix = rMatrix*2 - 1;
        
            //multiply the UVs by the rotation matrix
            UV.xy = mul(UV.xy, rMatrix);
            UV += Center;
        
            Out = UV;
        }
        
        void Unity_PolarCoordinates_float(float2 UV, float2 Center, float RadialScale, float LengthScale, out float2 Out)
        {
            float2 delta = UV - Center;
            float radius = length(delta) * 2 * RadialScale;
            float angle = atan2(delta.x, delta.y) * 1.0/6.28 * LengthScale;
            Out = float2(radius, angle);
        }
        
        void Unity_Rectangle_Fastest_float(float2 UV, float Width, float Height, out float Out)
        {
            float2 d = abs(UV * 2 - 1) - float2(Width, Height);
        #if defined(SHADER_STAGE_RAY_TRACING)
            d = saturate((1 - saturate(d * 1e7)));
        #else
            d = saturate(1 - d / fwidth(d));
        #endif
            Out = min(d.x, d.y);
        }
        
        void Unity_Rotate_Degrees_float(float2 UV, float2 Center, float Rotation, out float2 Out)
        {
            //rotation matrix
            Rotation = Rotation * (3.1415926f/180.0f);
            UV -= Center;
            float s = sin(Rotation);
            float c = cos(Rotation);
        
            //center rotation matrix
            float2x2 rMatrix = float2x2(c, -s, s, c);
            rMatrix *= 0.5;
            rMatrix += 0.5;
            rMatrix = rMatrix*2 - 1;
        
            //multiply the UVs by the rotation matrix
            UV.xy = mul(UV.xy, rMatrix);
            UV += Center;
        
            Out = UV;
        }
        
        void Unity_Preview_float(float In, out float Out)
        {
            Out = In;
        }
        
        void Unity_Saturate_float(float In, out float Out)
        {
            Out = saturate(In);
        }
        
        void Unity_Power_float(float A, float B, out float Out)
        {
            Out = pow(A, B);
        }
        
        void Unity_Smoothstep_float(float Edge1, float Edge2, float In, out float Out)
        {
            Out = smoothstep(Edge1, Edge2, In);
        }
        
        // Custom interpolators pre vertex
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreVertex' */
        
        // Graph Vertex
        struct VertexDescription
        {
            float3 Position;
            float3 Normal;
            float3 Tangent;
        };
        
        VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
        {
            VertexDescription description = (VertexDescription)0;
            description.Position = IN.ObjectSpacePosition;
            description.Normal = IN.ObjectSpaceNormal;
            description.Tangent = IN.ObjectSpaceTangent;
            return description;
        }
        
        // Custom interpolators, pre surface
        #ifdef FEATURES_GRAPH_VERTEX
        Varyings CustomInterpolatorPassThroughFunc(inout Varyings output, VertexDescription input)
        {
        return output;
        }
        #define CUSTOMINTERPOLATOR_VARYPASSTHROUGH_FUNC
        #endif
        
        // Graph Pixel
        struct SurfaceDescription
        {
            float Alpha;
        };
        
        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            float _Property_d94c15e3e2f84f569def2f6f4d8bbcab_Out_0 = _Speed;
            float _Multiply_ed2b4a35ed9147c59203777f0c7b2b2e_Out_2;
            Unity_Multiply_float_float(_Property_d94c15e3e2f84f569def2f6f4d8bbcab_Out_0, IN.TimeParameters.x, _Multiply_ed2b4a35ed9147c59203777f0c7b2b2e_Out_2);
            float _Property_f4a3c549f0874108876488320540244d_Out_0 = _SpinDir;
            float _Multiply_5f0c40c7364e4dcf81df67e0b9a85adb_Out_2;
            Unity_Multiply_float_float(_Property_f4a3c549f0874108876488320540244d_Out_0, -1, _Multiply_5f0c40c7364e4dcf81df67e0b9a85adb_Out_2);
            float _Multiply_7f7f94f247b24711bae1d438f6d4cdfa_Out_2;
            Unity_Multiply_float_float(_Multiply_ed2b4a35ed9147c59203777f0c7b2b2e_Out_2, _Multiply_5f0c40c7364e4dcf81df67e0b9a85adb_Out_2, _Multiply_7f7f94f247b24711bae1d438f6d4cdfa_Out_2);
            float2 _Rotate_35cdf476638c4ddfbfc5a472b4555a08_Out_3;
            Unity_Rotate_Radians_float(IN.uv0.xy, float2 (0.5, 0.5), _Multiply_7f7f94f247b24711bae1d438f6d4cdfa_Out_2, _Rotate_35cdf476638c4ddfbfc5a472b4555a08_Out_3);
            float2 _PolarCoordinates_137488258455411ea60aa4c0c170754f_Out_4;
            Unity_PolarCoordinates_float(_Rotate_35cdf476638c4ddfbfc5a472b4555a08_Out_3, float2 (0.5, 0.5), 0.7, 1.2, _PolarCoordinates_137488258455411ea60aa4c0c170754f_Out_4);
            float _Property_319ca09021c945c2857c1b7ea2a1fa6d_Out_0 = _Width;
            float _Rectangle_fa3b515096cf4847937839ee010b43e4_Out_3;
            Unity_Rectangle_Fastest_float(_PolarCoordinates_137488258455411ea60aa4c0c170754f_Out_4, _Property_319ca09021c945c2857c1b7ea2a1fa6d_Out_0, 3, _Rectangle_fa3b515096cf4847937839ee010b43e4_Out_3);
            float2 _Rotate_e221fbd58f8d426aa1a4907dd2ada1e1_Out_3;
            Unity_Rotate_Degrees_float(_Rotate_35cdf476638c4ddfbfc5a472b4555a08_Out_3, float2 (0.5, 0.5), -45, _Rotate_e221fbd58f8d426aa1a4907dd2ada1e1_Out_3);
            float _Property_860963b9226a4064a3c9487923211bfc_Out_0 = _TailLength;
            float2 _PolarCoordinates_dd40abfe6e5842c2b45c2a148178b7f6_Out_4;
            Unity_PolarCoordinates_float(_Rotate_e221fbd58f8d426aa1a4907dd2ada1e1_Out_3, float2 (0.5, 0.5), 1, _Property_860963b9226a4064a3c9487923211bfc_Out_0, _PolarCoordinates_dd40abfe6e5842c2b45c2a148178b7f6_Out_4);
            float _Split_e374f964ab2f4dcfb3b5d389cac488ee_R_1 = _PolarCoordinates_dd40abfe6e5842c2b45c2a148178b7f6_Out_4[0];
            float _Split_e374f964ab2f4dcfb3b5d389cac488ee_G_2 = _PolarCoordinates_dd40abfe6e5842c2b45c2a148178b7f6_Out_4[1];
            float _Split_e374f964ab2f4dcfb3b5d389cac488ee_B_3 = 0;
            float _Split_e374f964ab2f4dcfb3b5d389cac488ee_A_4 = 0;
            float _Preview_1959caf9d2b54c5a88bf965479f95f73_Out_1;
            Unity_Preview_float(_Split_e374f964ab2f4dcfb3b5d389cac488ee_G_2, _Preview_1959caf9d2b54c5a88bf965479f95f73_Out_1);
            float _Saturate_7a8a4ea4de02406dbafae645cfaa04ee_Out_1;
            Unity_Saturate_float(_Preview_1959caf9d2b54c5a88bf965479f95f73_Out_1, _Saturate_7a8a4ea4de02406dbafae645cfaa04ee_Out_1);
            float _Multiply_12051c8d973743848260fcea125d26ff_Out_2;
            Unity_Multiply_float_float(_Rectangle_fa3b515096cf4847937839ee010b43e4_Out_3, _Saturate_7a8a4ea4de02406dbafae645cfaa04ee_Out_1, _Multiply_12051c8d973743848260fcea125d26ff_Out_2);
            float _Power_40414304ee674ccdb6c4065358e09770_Out_2;
            Unity_Power_float(_Multiply_12051c8d973743848260fcea125d26ff_Out_2, 1.56, _Power_40414304ee674ccdb6c4065358e09770_Out_2);
            float _Smoothstep_91dc040ce0744d8880e840faeacb9a07_Out_3;
            Unity_Smoothstep_float(0.03, 0.35, _Power_40414304ee674ccdb6c4065358e09770_Out_2, _Smoothstep_91dc040ce0744d8880e840faeacb9a07_Out_3);
            surface.Alpha = _Smoothstep_91dc040ce0744d8880e840faeacb9a07_Out_3;
            return surface;
        }
        
        // --------------------------------------------------
        // Build Graph Inputs
        #ifdef HAVE_VFX_MODIFICATION
        #define VFX_SRP_ATTRIBUTES Attributes
        #define VFX_SRP_VARYINGS Varyings
        #define VFX_SRP_SURFACE_INPUTS SurfaceDescriptionInputs
        #endif
        VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
        {
            VertexDescriptionInputs output;
            ZERO_INITIALIZE(VertexDescriptionInputs, output);
        
            output.ObjectSpaceNormal =                          input.normalOS;
            output.ObjectSpaceTangent =                         input.tangentOS.xyz;
            output.ObjectSpacePosition =                        input.positionOS;
        
            return output;
        }
        SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
        {
            SurfaceDescriptionInputs output;
            ZERO_INITIALIZE(SurfaceDescriptionInputs, output);
        
        #ifdef HAVE_VFX_MODIFICATION
            // FragInputs from VFX come from two places: Interpolator or CBuffer.
            /* WARNING: $splice Could not find named fragment 'VFXSetFragInputs' */
        
        #endif
        
            
        
        
        
        
        
            output.uv0 = input.texCoord0;
            output.TimeParameters = _TimeParameters.xyz; // This is mainly for LW as HD overwrite this value
        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign =                    IS_FRONT_VFACE(input.cullFace, true, false);
        #else
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        #endif
        #undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        
                return output;
        }
        
        // --------------------------------------------------
        // Main
        
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/Varyings.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShadowCasterPass.hlsl"
        
        // --------------------------------------------------
        // Visual Effect Vertex Invocations
        #ifdef HAVE_VFX_MODIFICATION
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/VisualEffectVertex.hlsl"
        #endif
        
        ENDHLSL
        }
        Pass
        {
            Name "SceneSelectionPass"
            Tags
            {
                "LightMode" = "SceneSelectionPass"
            }
        
        // Render State
        Cull Off
        
        // Debug
        // <None>
        
        // --------------------------------------------------
        // Pass
        
        HLSLPROGRAM
        
        // Pragmas
        #pragma target 4.5
        #pragma exclude_renderers gles gles3 glcore
        #pragma vertex vert
        #pragma fragment frag
        
        // DotsInstancingOptions: <None>
        // HybridV1InjectedBuiltinProperties: <None>
        
        // Keywords
        // PassKeywords: <None>
        // GraphKeywords: <None>
        
        // Defines
        
        #define ATTRIBUTES_NEED_NORMAL
        #define ATTRIBUTES_NEED_TANGENT
        #define ATTRIBUTES_NEED_TEXCOORD0
        #define VARYINGS_NEED_TEXCOORD0
        #define FEATURES_GRAPH_VERTEX
        /* WARNING: $splice Could not find named fragment 'PassInstancing' */
        #define SHADERPASS SHADERPASS_DEPTHONLY
        #define SCENESELECTIONPASS 1
        #define ALPHA_CLIP_THRESHOLD 1
        /* WARNING: $splice Could not find named fragment 'DotsInstancingVars' */
        
        
        // custom interpolator pre-include
        /* WARNING: $splice Could not find named fragment 'sgci_CustomInterpolatorPreInclude' */
        
        // Includes
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
        
        // --------------------------------------------------
        // Structs and Packing
        
        // custom interpolators pre packing
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPrePacking' */
        
        struct Attributes
        {
             float3 positionOS : POSITION;
             float3 normalOS : NORMAL;
             float4 tangentOS : TANGENT;
             float4 uv0 : TEXCOORD0;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : INSTANCEID_SEMANTIC;
            #endif
        };
        struct Varyings
        {
             float4 positionCS : SV_POSITION;
             float4 texCoord0;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        struct SurfaceDescriptionInputs
        {
             float4 uv0;
             float3 TimeParameters;
        };
        struct VertexDescriptionInputs
        {
             float3 ObjectSpaceNormal;
             float3 ObjectSpaceTangent;
             float3 ObjectSpacePosition;
        };
        struct PackedVaryings
        {
             float4 positionCS : SV_POSITION;
             float4 interp0 : INTERP0;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        
        PackedVaryings PackVaryings (Varyings input)
        {
            PackedVaryings output;
            ZERO_INITIALIZE(PackedVaryings, output);
            output.positionCS = input.positionCS;
            output.interp0.xyzw =  input.texCoord0;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        Varyings UnpackVaryings (PackedVaryings input)
        {
            Varyings output;
            output.positionCS = input.positionCS;
            output.texCoord0 = input.interp0.xyzw;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        
        // --------------------------------------------------
        // Graph
        
        // Graph Properties
        CBUFFER_START(UnityPerMaterial)
        float _Speed;
        float _SpinDir;
        float _Width;
        float _TailLength;
        float4 _Color;
        CBUFFER_END
        
        // Object and Global properties
        
        // Graph Includes
        // GraphIncludes: <None>
        
        // -- Property used by ScenePickingPass
        #ifdef SCENEPICKINGPASS
        float4 _SelectionID;
        #endif
        
        // -- Properties used by SceneSelectionPass
        #ifdef SCENESELECTIONPASS
        int _ObjectId;
        int _PassValue;
        #endif
        
        // Graph Functions
        
        void Unity_Multiply_float_float(float A, float B, out float Out)
        {
            Out = A * B;
        }
        
        void Unity_Rotate_Radians_float(float2 UV, float2 Center, float Rotation, out float2 Out)
        {
            //rotation matrix
            UV -= Center;
            float s = sin(Rotation);
            float c = cos(Rotation);
        
            //center rotation matrix
            float2x2 rMatrix = float2x2(c, -s, s, c);
            rMatrix *= 0.5;
            rMatrix += 0.5;
            rMatrix = rMatrix*2 - 1;
        
            //multiply the UVs by the rotation matrix
            UV.xy = mul(UV.xy, rMatrix);
            UV += Center;
        
            Out = UV;
        }
        
        void Unity_PolarCoordinates_float(float2 UV, float2 Center, float RadialScale, float LengthScale, out float2 Out)
        {
            float2 delta = UV - Center;
            float radius = length(delta) * 2 * RadialScale;
            float angle = atan2(delta.x, delta.y) * 1.0/6.28 * LengthScale;
            Out = float2(radius, angle);
        }
        
        void Unity_Rectangle_Fastest_float(float2 UV, float Width, float Height, out float Out)
        {
            float2 d = abs(UV * 2 - 1) - float2(Width, Height);
        #if defined(SHADER_STAGE_RAY_TRACING)
            d = saturate((1 - saturate(d * 1e7)));
        #else
            d = saturate(1 - d / fwidth(d));
        #endif
            Out = min(d.x, d.y);
        }
        
        void Unity_Rotate_Degrees_float(float2 UV, float2 Center, float Rotation, out float2 Out)
        {
            //rotation matrix
            Rotation = Rotation * (3.1415926f/180.0f);
            UV -= Center;
            float s = sin(Rotation);
            float c = cos(Rotation);
        
            //center rotation matrix
            float2x2 rMatrix = float2x2(c, -s, s, c);
            rMatrix *= 0.5;
            rMatrix += 0.5;
            rMatrix = rMatrix*2 - 1;
        
            //multiply the UVs by the rotation matrix
            UV.xy = mul(UV.xy, rMatrix);
            UV += Center;
        
            Out = UV;
        }
        
        void Unity_Preview_float(float In, out float Out)
        {
            Out = In;
        }
        
        void Unity_Saturate_float(float In, out float Out)
        {
            Out = saturate(In);
        }
        
        void Unity_Power_float(float A, float B, out float Out)
        {
            Out = pow(A, B);
        }
        
        void Unity_Smoothstep_float(float Edge1, float Edge2, float In, out float Out)
        {
            Out = smoothstep(Edge1, Edge2, In);
        }
        
        // Custom interpolators pre vertex
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreVertex' */
        
        // Graph Vertex
        struct VertexDescription
        {
            float3 Position;
            float3 Normal;
            float3 Tangent;
        };
        
        VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
        {
            VertexDescription description = (VertexDescription)0;
            description.Position = IN.ObjectSpacePosition;
            description.Normal = IN.ObjectSpaceNormal;
            description.Tangent = IN.ObjectSpaceTangent;
            return description;
        }
        
        // Custom interpolators, pre surface
        #ifdef FEATURES_GRAPH_VERTEX
        Varyings CustomInterpolatorPassThroughFunc(inout Varyings output, VertexDescription input)
        {
        return output;
        }
        #define CUSTOMINTERPOLATOR_VARYPASSTHROUGH_FUNC
        #endif
        
        // Graph Pixel
        struct SurfaceDescription
        {
            float Alpha;
        };
        
        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            float _Property_d94c15e3e2f84f569def2f6f4d8bbcab_Out_0 = _Speed;
            float _Multiply_ed2b4a35ed9147c59203777f0c7b2b2e_Out_2;
            Unity_Multiply_float_float(_Property_d94c15e3e2f84f569def2f6f4d8bbcab_Out_0, IN.TimeParameters.x, _Multiply_ed2b4a35ed9147c59203777f0c7b2b2e_Out_2);
            float _Property_f4a3c549f0874108876488320540244d_Out_0 = _SpinDir;
            float _Multiply_5f0c40c7364e4dcf81df67e0b9a85adb_Out_2;
            Unity_Multiply_float_float(_Property_f4a3c549f0874108876488320540244d_Out_0, -1, _Multiply_5f0c40c7364e4dcf81df67e0b9a85adb_Out_2);
            float _Multiply_7f7f94f247b24711bae1d438f6d4cdfa_Out_2;
            Unity_Multiply_float_float(_Multiply_ed2b4a35ed9147c59203777f0c7b2b2e_Out_2, _Multiply_5f0c40c7364e4dcf81df67e0b9a85adb_Out_2, _Multiply_7f7f94f247b24711bae1d438f6d4cdfa_Out_2);
            float2 _Rotate_35cdf476638c4ddfbfc5a472b4555a08_Out_3;
            Unity_Rotate_Radians_float(IN.uv0.xy, float2 (0.5, 0.5), _Multiply_7f7f94f247b24711bae1d438f6d4cdfa_Out_2, _Rotate_35cdf476638c4ddfbfc5a472b4555a08_Out_3);
            float2 _PolarCoordinates_137488258455411ea60aa4c0c170754f_Out_4;
            Unity_PolarCoordinates_float(_Rotate_35cdf476638c4ddfbfc5a472b4555a08_Out_3, float2 (0.5, 0.5), 0.7, 1.2, _PolarCoordinates_137488258455411ea60aa4c0c170754f_Out_4);
            float _Property_319ca09021c945c2857c1b7ea2a1fa6d_Out_0 = _Width;
            float _Rectangle_fa3b515096cf4847937839ee010b43e4_Out_3;
            Unity_Rectangle_Fastest_float(_PolarCoordinates_137488258455411ea60aa4c0c170754f_Out_4, _Property_319ca09021c945c2857c1b7ea2a1fa6d_Out_0, 3, _Rectangle_fa3b515096cf4847937839ee010b43e4_Out_3);
            float2 _Rotate_e221fbd58f8d426aa1a4907dd2ada1e1_Out_3;
            Unity_Rotate_Degrees_float(_Rotate_35cdf476638c4ddfbfc5a472b4555a08_Out_3, float2 (0.5, 0.5), -45, _Rotate_e221fbd58f8d426aa1a4907dd2ada1e1_Out_3);
            float _Property_860963b9226a4064a3c9487923211bfc_Out_0 = _TailLength;
            float2 _PolarCoordinates_dd40abfe6e5842c2b45c2a148178b7f6_Out_4;
            Unity_PolarCoordinates_float(_Rotate_e221fbd58f8d426aa1a4907dd2ada1e1_Out_3, float2 (0.5, 0.5), 1, _Property_860963b9226a4064a3c9487923211bfc_Out_0, _PolarCoordinates_dd40abfe6e5842c2b45c2a148178b7f6_Out_4);
            float _Split_e374f964ab2f4dcfb3b5d389cac488ee_R_1 = _PolarCoordinates_dd40abfe6e5842c2b45c2a148178b7f6_Out_4[0];
            float _Split_e374f964ab2f4dcfb3b5d389cac488ee_G_2 = _PolarCoordinates_dd40abfe6e5842c2b45c2a148178b7f6_Out_4[1];
            float _Split_e374f964ab2f4dcfb3b5d389cac488ee_B_3 = 0;
            float _Split_e374f964ab2f4dcfb3b5d389cac488ee_A_4 = 0;
            float _Preview_1959caf9d2b54c5a88bf965479f95f73_Out_1;
            Unity_Preview_float(_Split_e374f964ab2f4dcfb3b5d389cac488ee_G_2, _Preview_1959caf9d2b54c5a88bf965479f95f73_Out_1);
            float _Saturate_7a8a4ea4de02406dbafae645cfaa04ee_Out_1;
            Unity_Saturate_float(_Preview_1959caf9d2b54c5a88bf965479f95f73_Out_1, _Saturate_7a8a4ea4de02406dbafae645cfaa04ee_Out_1);
            float _Multiply_12051c8d973743848260fcea125d26ff_Out_2;
            Unity_Multiply_float_float(_Rectangle_fa3b515096cf4847937839ee010b43e4_Out_3, _Saturate_7a8a4ea4de02406dbafae645cfaa04ee_Out_1, _Multiply_12051c8d973743848260fcea125d26ff_Out_2);
            float _Power_40414304ee674ccdb6c4065358e09770_Out_2;
            Unity_Power_float(_Multiply_12051c8d973743848260fcea125d26ff_Out_2, 1.56, _Power_40414304ee674ccdb6c4065358e09770_Out_2);
            float _Smoothstep_91dc040ce0744d8880e840faeacb9a07_Out_3;
            Unity_Smoothstep_float(0.03, 0.35, _Power_40414304ee674ccdb6c4065358e09770_Out_2, _Smoothstep_91dc040ce0744d8880e840faeacb9a07_Out_3);
            surface.Alpha = _Smoothstep_91dc040ce0744d8880e840faeacb9a07_Out_3;
            return surface;
        }
        
        // --------------------------------------------------
        // Build Graph Inputs
        #ifdef HAVE_VFX_MODIFICATION
        #define VFX_SRP_ATTRIBUTES Attributes
        #define VFX_SRP_VARYINGS Varyings
        #define VFX_SRP_SURFACE_INPUTS SurfaceDescriptionInputs
        #endif
        VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
        {
            VertexDescriptionInputs output;
            ZERO_INITIALIZE(VertexDescriptionInputs, output);
        
            output.ObjectSpaceNormal =                          input.normalOS;
            output.ObjectSpaceTangent =                         input.tangentOS.xyz;
            output.ObjectSpacePosition =                        input.positionOS;
        
            return output;
        }
        SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
        {
            SurfaceDescriptionInputs output;
            ZERO_INITIALIZE(SurfaceDescriptionInputs, output);
        
        #ifdef HAVE_VFX_MODIFICATION
            // FragInputs from VFX come from two places: Interpolator or CBuffer.
            /* WARNING: $splice Could not find named fragment 'VFXSetFragInputs' */
        
        #endif
        
            
        
        
        
        
        
            output.uv0 = input.texCoord0;
            output.TimeParameters = _TimeParameters.xyz; // This is mainly for LW as HD overwrite this value
        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign =                    IS_FRONT_VFACE(input.cullFace, true, false);
        #else
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        #endif
        #undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        
                return output;
        }
        
        // --------------------------------------------------
        // Main
        
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/Varyings.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/SelectionPickingPass.hlsl"
        
        // --------------------------------------------------
        // Visual Effect Vertex Invocations
        #ifdef HAVE_VFX_MODIFICATION
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/VisualEffectVertex.hlsl"
        #endif
        
        ENDHLSL
        }
        Pass
        {
            Name "ScenePickingPass"
            Tags
            {
                "LightMode" = "Picking"
            }
        
        // Render State
        Cull Back
        
        // Debug
        // <None>
        
        // --------------------------------------------------
        // Pass
        
        HLSLPROGRAM
        
        // Pragmas
        #pragma target 4.5
        #pragma exclude_renderers gles gles3 glcore
        #pragma vertex vert
        #pragma fragment frag
        
        // DotsInstancingOptions: <None>
        // HybridV1InjectedBuiltinProperties: <None>
        
        // Keywords
        // PassKeywords: <None>
        // GraphKeywords: <None>
        
        // Defines
        
        #define ATTRIBUTES_NEED_NORMAL
        #define ATTRIBUTES_NEED_TANGENT
        #define ATTRIBUTES_NEED_TEXCOORD0
        #define VARYINGS_NEED_TEXCOORD0
        #define FEATURES_GRAPH_VERTEX
        /* WARNING: $splice Could not find named fragment 'PassInstancing' */
        #define SHADERPASS SHADERPASS_DEPTHONLY
        #define SCENEPICKINGPASS 1
        #define ALPHA_CLIP_THRESHOLD 1
        /* WARNING: $splice Could not find named fragment 'DotsInstancingVars' */
        
        
        // custom interpolator pre-include
        /* WARNING: $splice Could not find named fragment 'sgci_CustomInterpolatorPreInclude' */
        
        // Includes
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
        
        // --------------------------------------------------
        // Structs and Packing
        
        // custom interpolators pre packing
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPrePacking' */
        
        struct Attributes
        {
             float3 positionOS : POSITION;
             float3 normalOS : NORMAL;
             float4 tangentOS : TANGENT;
             float4 uv0 : TEXCOORD0;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : INSTANCEID_SEMANTIC;
            #endif
        };
        struct Varyings
        {
             float4 positionCS : SV_POSITION;
             float4 texCoord0;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        struct SurfaceDescriptionInputs
        {
             float4 uv0;
             float3 TimeParameters;
        };
        struct VertexDescriptionInputs
        {
             float3 ObjectSpaceNormal;
             float3 ObjectSpaceTangent;
             float3 ObjectSpacePosition;
        };
        struct PackedVaryings
        {
             float4 positionCS : SV_POSITION;
             float4 interp0 : INTERP0;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        
        PackedVaryings PackVaryings (Varyings input)
        {
            PackedVaryings output;
            ZERO_INITIALIZE(PackedVaryings, output);
            output.positionCS = input.positionCS;
            output.interp0.xyzw =  input.texCoord0;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        Varyings UnpackVaryings (PackedVaryings input)
        {
            Varyings output;
            output.positionCS = input.positionCS;
            output.texCoord0 = input.interp0.xyzw;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        
        // --------------------------------------------------
        // Graph
        
        // Graph Properties
        CBUFFER_START(UnityPerMaterial)
        float _Speed;
        float _SpinDir;
        float _Width;
        float _TailLength;
        float4 _Color;
        CBUFFER_END
        
        // Object and Global properties
        
        // Graph Includes
        // GraphIncludes: <None>
        
        // -- Property used by ScenePickingPass
        #ifdef SCENEPICKINGPASS
        float4 _SelectionID;
        #endif
        
        // -- Properties used by SceneSelectionPass
        #ifdef SCENESELECTIONPASS
        int _ObjectId;
        int _PassValue;
        #endif
        
        // Graph Functions
        
        void Unity_Multiply_float_float(float A, float B, out float Out)
        {
            Out = A * B;
        }
        
        void Unity_Rotate_Radians_float(float2 UV, float2 Center, float Rotation, out float2 Out)
        {
            //rotation matrix
            UV -= Center;
            float s = sin(Rotation);
            float c = cos(Rotation);
        
            //center rotation matrix
            float2x2 rMatrix = float2x2(c, -s, s, c);
            rMatrix *= 0.5;
            rMatrix += 0.5;
            rMatrix = rMatrix*2 - 1;
        
            //multiply the UVs by the rotation matrix
            UV.xy = mul(UV.xy, rMatrix);
            UV += Center;
        
            Out = UV;
        }
        
        void Unity_PolarCoordinates_float(float2 UV, float2 Center, float RadialScale, float LengthScale, out float2 Out)
        {
            float2 delta = UV - Center;
            float radius = length(delta) * 2 * RadialScale;
            float angle = atan2(delta.x, delta.y) * 1.0/6.28 * LengthScale;
            Out = float2(radius, angle);
        }
        
        void Unity_Rectangle_Fastest_float(float2 UV, float Width, float Height, out float Out)
        {
            float2 d = abs(UV * 2 - 1) - float2(Width, Height);
        #if defined(SHADER_STAGE_RAY_TRACING)
            d = saturate((1 - saturate(d * 1e7)));
        #else
            d = saturate(1 - d / fwidth(d));
        #endif
            Out = min(d.x, d.y);
        }
        
        void Unity_Rotate_Degrees_float(float2 UV, float2 Center, float Rotation, out float2 Out)
        {
            //rotation matrix
            Rotation = Rotation * (3.1415926f/180.0f);
            UV -= Center;
            float s = sin(Rotation);
            float c = cos(Rotation);
        
            //center rotation matrix
            float2x2 rMatrix = float2x2(c, -s, s, c);
            rMatrix *= 0.5;
            rMatrix += 0.5;
            rMatrix = rMatrix*2 - 1;
        
            //multiply the UVs by the rotation matrix
            UV.xy = mul(UV.xy, rMatrix);
            UV += Center;
        
            Out = UV;
        }
        
        void Unity_Preview_float(float In, out float Out)
        {
            Out = In;
        }
        
        void Unity_Saturate_float(float In, out float Out)
        {
            Out = saturate(In);
        }
        
        void Unity_Power_float(float A, float B, out float Out)
        {
            Out = pow(A, B);
        }
        
        void Unity_Smoothstep_float(float Edge1, float Edge2, float In, out float Out)
        {
            Out = smoothstep(Edge1, Edge2, In);
        }
        
        // Custom interpolators pre vertex
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreVertex' */
        
        // Graph Vertex
        struct VertexDescription
        {
            float3 Position;
            float3 Normal;
            float3 Tangent;
        };
        
        VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
        {
            VertexDescription description = (VertexDescription)0;
            description.Position = IN.ObjectSpacePosition;
            description.Normal = IN.ObjectSpaceNormal;
            description.Tangent = IN.ObjectSpaceTangent;
            return description;
        }
        
        // Custom interpolators, pre surface
        #ifdef FEATURES_GRAPH_VERTEX
        Varyings CustomInterpolatorPassThroughFunc(inout Varyings output, VertexDescription input)
        {
        return output;
        }
        #define CUSTOMINTERPOLATOR_VARYPASSTHROUGH_FUNC
        #endif
        
        // Graph Pixel
        struct SurfaceDescription
        {
            float Alpha;
        };
        
        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            float _Property_d94c15e3e2f84f569def2f6f4d8bbcab_Out_0 = _Speed;
            float _Multiply_ed2b4a35ed9147c59203777f0c7b2b2e_Out_2;
            Unity_Multiply_float_float(_Property_d94c15e3e2f84f569def2f6f4d8bbcab_Out_0, IN.TimeParameters.x, _Multiply_ed2b4a35ed9147c59203777f0c7b2b2e_Out_2);
            float _Property_f4a3c549f0874108876488320540244d_Out_0 = _SpinDir;
            float _Multiply_5f0c40c7364e4dcf81df67e0b9a85adb_Out_2;
            Unity_Multiply_float_float(_Property_f4a3c549f0874108876488320540244d_Out_0, -1, _Multiply_5f0c40c7364e4dcf81df67e0b9a85adb_Out_2);
            float _Multiply_7f7f94f247b24711bae1d438f6d4cdfa_Out_2;
            Unity_Multiply_float_float(_Multiply_ed2b4a35ed9147c59203777f0c7b2b2e_Out_2, _Multiply_5f0c40c7364e4dcf81df67e0b9a85adb_Out_2, _Multiply_7f7f94f247b24711bae1d438f6d4cdfa_Out_2);
            float2 _Rotate_35cdf476638c4ddfbfc5a472b4555a08_Out_3;
            Unity_Rotate_Radians_float(IN.uv0.xy, float2 (0.5, 0.5), _Multiply_7f7f94f247b24711bae1d438f6d4cdfa_Out_2, _Rotate_35cdf476638c4ddfbfc5a472b4555a08_Out_3);
            float2 _PolarCoordinates_137488258455411ea60aa4c0c170754f_Out_4;
            Unity_PolarCoordinates_float(_Rotate_35cdf476638c4ddfbfc5a472b4555a08_Out_3, float2 (0.5, 0.5), 0.7, 1.2, _PolarCoordinates_137488258455411ea60aa4c0c170754f_Out_4);
            float _Property_319ca09021c945c2857c1b7ea2a1fa6d_Out_0 = _Width;
            float _Rectangle_fa3b515096cf4847937839ee010b43e4_Out_3;
            Unity_Rectangle_Fastest_float(_PolarCoordinates_137488258455411ea60aa4c0c170754f_Out_4, _Property_319ca09021c945c2857c1b7ea2a1fa6d_Out_0, 3, _Rectangle_fa3b515096cf4847937839ee010b43e4_Out_3);
            float2 _Rotate_e221fbd58f8d426aa1a4907dd2ada1e1_Out_3;
            Unity_Rotate_Degrees_float(_Rotate_35cdf476638c4ddfbfc5a472b4555a08_Out_3, float2 (0.5, 0.5), -45, _Rotate_e221fbd58f8d426aa1a4907dd2ada1e1_Out_3);
            float _Property_860963b9226a4064a3c9487923211bfc_Out_0 = _TailLength;
            float2 _PolarCoordinates_dd40abfe6e5842c2b45c2a148178b7f6_Out_4;
            Unity_PolarCoordinates_float(_Rotate_e221fbd58f8d426aa1a4907dd2ada1e1_Out_3, float2 (0.5, 0.5), 1, _Property_860963b9226a4064a3c9487923211bfc_Out_0, _PolarCoordinates_dd40abfe6e5842c2b45c2a148178b7f6_Out_4);
            float _Split_e374f964ab2f4dcfb3b5d389cac488ee_R_1 = _PolarCoordinates_dd40abfe6e5842c2b45c2a148178b7f6_Out_4[0];
            float _Split_e374f964ab2f4dcfb3b5d389cac488ee_G_2 = _PolarCoordinates_dd40abfe6e5842c2b45c2a148178b7f6_Out_4[1];
            float _Split_e374f964ab2f4dcfb3b5d389cac488ee_B_3 = 0;
            float _Split_e374f964ab2f4dcfb3b5d389cac488ee_A_4 = 0;
            float _Preview_1959caf9d2b54c5a88bf965479f95f73_Out_1;
            Unity_Preview_float(_Split_e374f964ab2f4dcfb3b5d389cac488ee_G_2, _Preview_1959caf9d2b54c5a88bf965479f95f73_Out_1);
            float _Saturate_7a8a4ea4de02406dbafae645cfaa04ee_Out_1;
            Unity_Saturate_float(_Preview_1959caf9d2b54c5a88bf965479f95f73_Out_1, _Saturate_7a8a4ea4de02406dbafae645cfaa04ee_Out_1);
            float _Multiply_12051c8d973743848260fcea125d26ff_Out_2;
            Unity_Multiply_float_float(_Rectangle_fa3b515096cf4847937839ee010b43e4_Out_3, _Saturate_7a8a4ea4de02406dbafae645cfaa04ee_Out_1, _Multiply_12051c8d973743848260fcea125d26ff_Out_2);
            float _Power_40414304ee674ccdb6c4065358e09770_Out_2;
            Unity_Power_float(_Multiply_12051c8d973743848260fcea125d26ff_Out_2, 1.56, _Power_40414304ee674ccdb6c4065358e09770_Out_2);
            float _Smoothstep_91dc040ce0744d8880e840faeacb9a07_Out_3;
            Unity_Smoothstep_float(0.03, 0.35, _Power_40414304ee674ccdb6c4065358e09770_Out_2, _Smoothstep_91dc040ce0744d8880e840faeacb9a07_Out_3);
            surface.Alpha = _Smoothstep_91dc040ce0744d8880e840faeacb9a07_Out_3;
            return surface;
        }
        
        // --------------------------------------------------
        // Build Graph Inputs
        #ifdef HAVE_VFX_MODIFICATION
        #define VFX_SRP_ATTRIBUTES Attributes
        #define VFX_SRP_VARYINGS Varyings
        #define VFX_SRP_SURFACE_INPUTS SurfaceDescriptionInputs
        #endif
        VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
        {
            VertexDescriptionInputs output;
            ZERO_INITIALIZE(VertexDescriptionInputs, output);
        
            output.ObjectSpaceNormal =                          input.normalOS;
            output.ObjectSpaceTangent =                         input.tangentOS.xyz;
            output.ObjectSpacePosition =                        input.positionOS;
        
            return output;
        }
        SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
        {
            SurfaceDescriptionInputs output;
            ZERO_INITIALIZE(SurfaceDescriptionInputs, output);
        
        #ifdef HAVE_VFX_MODIFICATION
            // FragInputs from VFX come from two places: Interpolator or CBuffer.
            /* WARNING: $splice Could not find named fragment 'VFXSetFragInputs' */
        
        #endif
        
            
        
        
        
        
        
            output.uv0 = input.texCoord0;
            output.TimeParameters = _TimeParameters.xyz; // This is mainly for LW as HD overwrite this value
        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign =                    IS_FRONT_VFACE(input.cullFace, true, false);
        #else
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        #endif
        #undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        
                return output;
        }
        
        // --------------------------------------------------
        // Main
        
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/Varyings.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/SelectionPickingPass.hlsl"
        
        // --------------------------------------------------
        // Visual Effect Vertex Invocations
        #ifdef HAVE_VFX_MODIFICATION
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/VisualEffectVertex.hlsl"
        #endif
        
        ENDHLSL
        }
        Pass
        {
            Name "DepthNormals"
            Tags
            {
                "LightMode" = "DepthNormalsOnly"
            }
        
        // Render State
        Cull Back
        ZTest LEqual
        ZWrite On
        
        // Debug
        // <None>
        
        // --------------------------------------------------
        // Pass
        
        HLSLPROGRAM
        
        // Pragmas
        #pragma target 4.5
        #pragma exclude_renderers gles gles3 glcore
        #pragma multi_compile_instancing
        #pragma multi_compile _ DOTS_INSTANCING_ON
        #pragma vertex vert
        #pragma fragment frag
        
        // DotsInstancingOptions: <None>
        // HybridV1InjectedBuiltinProperties: <None>
        
        // Keywords
        // PassKeywords: <None>
        // GraphKeywords: <None>
        
        // Defines
        
        #define ATTRIBUTES_NEED_NORMAL
        #define ATTRIBUTES_NEED_TANGENT
        #define ATTRIBUTES_NEED_TEXCOORD0
        #define VARYINGS_NEED_NORMAL_WS
        #define VARYINGS_NEED_TEXCOORD0
        #define FEATURES_GRAPH_VERTEX
        /* WARNING: $splice Could not find named fragment 'PassInstancing' */
        #define SHADERPASS SHADERPASS_DEPTHNORMALSONLY
        #define _SURFACE_TYPE_TRANSPARENT 1
        /* WARNING: $splice Could not find named fragment 'DotsInstancingVars' */
        
        
        // custom interpolator pre-include
        /* WARNING: $splice Could not find named fragment 'sgci_CustomInterpolatorPreInclude' */
        
        // Includes
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
        
        // --------------------------------------------------
        // Structs and Packing
        
        // custom interpolators pre packing
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPrePacking' */
        
        struct Attributes
        {
             float3 positionOS : POSITION;
             float3 normalOS : NORMAL;
             float4 tangentOS : TANGENT;
             float4 uv0 : TEXCOORD0;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : INSTANCEID_SEMANTIC;
            #endif
        };
        struct Varyings
        {
             float4 positionCS : SV_POSITION;
             float3 normalWS;
             float4 texCoord0;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        struct SurfaceDescriptionInputs
        {
             float4 uv0;
             float3 TimeParameters;
        };
        struct VertexDescriptionInputs
        {
             float3 ObjectSpaceNormal;
             float3 ObjectSpaceTangent;
             float3 ObjectSpacePosition;
        };
        struct PackedVaryings
        {
             float4 positionCS : SV_POSITION;
             float3 interp0 : INTERP0;
             float4 interp1 : INTERP1;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        
        PackedVaryings PackVaryings (Varyings input)
        {
            PackedVaryings output;
            ZERO_INITIALIZE(PackedVaryings, output);
            output.positionCS = input.positionCS;
            output.interp0.xyz =  input.normalWS;
            output.interp1.xyzw =  input.texCoord0;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        Varyings UnpackVaryings (PackedVaryings input)
        {
            Varyings output;
            output.positionCS = input.positionCS;
            output.normalWS = input.interp0.xyz;
            output.texCoord0 = input.interp1.xyzw;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        
        // --------------------------------------------------
        // Graph
        
        // Graph Properties
        CBUFFER_START(UnityPerMaterial)
        float _Speed;
        float _SpinDir;
        float _Width;
        float _TailLength;
        float4 _Color;
        CBUFFER_END
        
        // Object and Global properties
        
        // Graph Includes
        // GraphIncludes: <None>
        
        // -- Property used by ScenePickingPass
        #ifdef SCENEPICKINGPASS
        float4 _SelectionID;
        #endif
        
        // -- Properties used by SceneSelectionPass
        #ifdef SCENESELECTIONPASS
        int _ObjectId;
        int _PassValue;
        #endif
        
        // Graph Functions
        
        void Unity_Multiply_float_float(float A, float B, out float Out)
        {
            Out = A * B;
        }
        
        void Unity_Rotate_Radians_float(float2 UV, float2 Center, float Rotation, out float2 Out)
        {
            //rotation matrix
            UV -= Center;
            float s = sin(Rotation);
            float c = cos(Rotation);
        
            //center rotation matrix
            float2x2 rMatrix = float2x2(c, -s, s, c);
            rMatrix *= 0.5;
            rMatrix += 0.5;
            rMatrix = rMatrix*2 - 1;
        
            //multiply the UVs by the rotation matrix
            UV.xy = mul(UV.xy, rMatrix);
            UV += Center;
        
            Out = UV;
        }
        
        void Unity_PolarCoordinates_float(float2 UV, float2 Center, float RadialScale, float LengthScale, out float2 Out)
        {
            float2 delta = UV - Center;
            float radius = length(delta) * 2 * RadialScale;
            float angle = atan2(delta.x, delta.y) * 1.0/6.28 * LengthScale;
            Out = float2(radius, angle);
        }
        
        void Unity_Rectangle_Fastest_float(float2 UV, float Width, float Height, out float Out)
        {
            float2 d = abs(UV * 2 - 1) - float2(Width, Height);
        #if defined(SHADER_STAGE_RAY_TRACING)
            d = saturate((1 - saturate(d * 1e7)));
        #else
            d = saturate(1 - d / fwidth(d));
        #endif
            Out = min(d.x, d.y);
        }
        
        void Unity_Rotate_Degrees_float(float2 UV, float2 Center, float Rotation, out float2 Out)
        {
            //rotation matrix
            Rotation = Rotation * (3.1415926f/180.0f);
            UV -= Center;
            float s = sin(Rotation);
            float c = cos(Rotation);
        
            //center rotation matrix
            float2x2 rMatrix = float2x2(c, -s, s, c);
            rMatrix *= 0.5;
            rMatrix += 0.5;
            rMatrix = rMatrix*2 - 1;
        
            //multiply the UVs by the rotation matrix
            UV.xy = mul(UV.xy, rMatrix);
            UV += Center;
        
            Out = UV;
        }
        
        void Unity_Preview_float(float In, out float Out)
        {
            Out = In;
        }
        
        void Unity_Saturate_float(float In, out float Out)
        {
            Out = saturate(In);
        }
        
        void Unity_Power_float(float A, float B, out float Out)
        {
            Out = pow(A, B);
        }
        
        void Unity_Smoothstep_float(float Edge1, float Edge2, float In, out float Out)
        {
            Out = smoothstep(Edge1, Edge2, In);
        }
        
        // Custom interpolators pre vertex
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreVertex' */
        
        // Graph Vertex
        struct VertexDescription
        {
            float3 Position;
            float3 Normal;
            float3 Tangent;
        };
        
        VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
        {
            VertexDescription description = (VertexDescription)0;
            description.Position = IN.ObjectSpacePosition;
            description.Normal = IN.ObjectSpaceNormal;
            description.Tangent = IN.ObjectSpaceTangent;
            return description;
        }
        
        // Custom interpolators, pre surface
        #ifdef FEATURES_GRAPH_VERTEX
        Varyings CustomInterpolatorPassThroughFunc(inout Varyings output, VertexDescription input)
        {
        return output;
        }
        #define CUSTOMINTERPOLATOR_VARYPASSTHROUGH_FUNC
        #endif
        
        // Graph Pixel
        struct SurfaceDescription
        {
            float Alpha;
        };
        
        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            float _Property_d94c15e3e2f84f569def2f6f4d8bbcab_Out_0 = _Speed;
            float _Multiply_ed2b4a35ed9147c59203777f0c7b2b2e_Out_2;
            Unity_Multiply_float_float(_Property_d94c15e3e2f84f569def2f6f4d8bbcab_Out_0, IN.TimeParameters.x, _Multiply_ed2b4a35ed9147c59203777f0c7b2b2e_Out_2);
            float _Property_f4a3c549f0874108876488320540244d_Out_0 = _SpinDir;
            float _Multiply_5f0c40c7364e4dcf81df67e0b9a85adb_Out_2;
            Unity_Multiply_float_float(_Property_f4a3c549f0874108876488320540244d_Out_0, -1, _Multiply_5f0c40c7364e4dcf81df67e0b9a85adb_Out_2);
            float _Multiply_7f7f94f247b24711bae1d438f6d4cdfa_Out_2;
            Unity_Multiply_float_float(_Multiply_ed2b4a35ed9147c59203777f0c7b2b2e_Out_2, _Multiply_5f0c40c7364e4dcf81df67e0b9a85adb_Out_2, _Multiply_7f7f94f247b24711bae1d438f6d4cdfa_Out_2);
            float2 _Rotate_35cdf476638c4ddfbfc5a472b4555a08_Out_3;
            Unity_Rotate_Radians_float(IN.uv0.xy, float2 (0.5, 0.5), _Multiply_7f7f94f247b24711bae1d438f6d4cdfa_Out_2, _Rotate_35cdf476638c4ddfbfc5a472b4555a08_Out_3);
            float2 _PolarCoordinates_137488258455411ea60aa4c0c170754f_Out_4;
            Unity_PolarCoordinates_float(_Rotate_35cdf476638c4ddfbfc5a472b4555a08_Out_3, float2 (0.5, 0.5), 0.7, 1.2, _PolarCoordinates_137488258455411ea60aa4c0c170754f_Out_4);
            float _Property_319ca09021c945c2857c1b7ea2a1fa6d_Out_0 = _Width;
            float _Rectangle_fa3b515096cf4847937839ee010b43e4_Out_3;
            Unity_Rectangle_Fastest_float(_PolarCoordinates_137488258455411ea60aa4c0c170754f_Out_4, _Property_319ca09021c945c2857c1b7ea2a1fa6d_Out_0, 3, _Rectangle_fa3b515096cf4847937839ee010b43e4_Out_3);
            float2 _Rotate_e221fbd58f8d426aa1a4907dd2ada1e1_Out_3;
            Unity_Rotate_Degrees_float(_Rotate_35cdf476638c4ddfbfc5a472b4555a08_Out_3, float2 (0.5, 0.5), -45, _Rotate_e221fbd58f8d426aa1a4907dd2ada1e1_Out_3);
            float _Property_860963b9226a4064a3c9487923211bfc_Out_0 = _TailLength;
            float2 _PolarCoordinates_dd40abfe6e5842c2b45c2a148178b7f6_Out_4;
            Unity_PolarCoordinates_float(_Rotate_e221fbd58f8d426aa1a4907dd2ada1e1_Out_3, float2 (0.5, 0.5), 1, _Property_860963b9226a4064a3c9487923211bfc_Out_0, _PolarCoordinates_dd40abfe6e5842c2b45c2a148178b7f6_Out_4);
            float _Split_e374f964ab2f4dcfb3b5d389cac488ee_R_1 = _PolarCoordinates_dd40abfe6e5842c2b45c2a148178b7f6_Out_4[0];
            float _Split_e374f964ab2f4dcfb3b5d389cac488ee_G_2 = _PolarCoordinates_dd40abfe6e5842c2b45c2a148178b7f6_Out_4[1];
            float _Split_e374f964ab2f4dcfb3b5d389cac488ee_B_3 = 0;
            float _Split_e374f964ab2f4dcfb3b5d389cac488ee_A_4 = 0;
            float _Preview_1959caf9d2b54c5a88bf965479f95f73_Out_1;
            Unity_Preview_float(_Split_e374f964ab2f4dcfb3b5d389cac488ee_G_2, _Preview_1959caf9d2b54c5a88bf965479f95f73_Out_1);
            float _Saturate_7a8a4ea4de02406dbafae645cfaa04ee_Out_1;
            Unity_Saturate_float(_Preview_1959caf9d2b54c5a88bf965479f95f73_Out_1, _Saturate_7a8a4ea4de02406dbafae645cfaa04ee_Out_1);
            float _Multiply_12051c8d973743848260fcea125d26ff_Out_2;
            Unity_Multiply_float_float(_Rectangle_fa3b515096cf4847937839ee010b43e4_Out_3, _Saturate_7a8a4ea4de02406dbafae645cfaa04ee_Out_1, _Multiply_12051c8d973743848260fcea125d26ff_Out_2);
            float _Power_40414304ee674ccdb6c4065358e09770_Out_2;
            Unity_Power_float(_Multiply_12051c8d973743848260fcea125d26ff_Out_2, 1.56, _Power_40414304ee674ccdb6c4065358e09770_Out_2);
            float _Smoothstep_91dc040ce0744d8880e840faeacb9a07_Out_3;
            Unity_Smoothstep_float(0.03, 0.35, _Power_40414304ee674ccdb6c4065358e09770_Out_2, _Smoothstep_91dc040ce0744d8880e840faeacb9a07_Out_3);
            surface.Alpha = _Smoothstep_91dc040ce0744d8880e840faeacb9a07_Out_3;
            return surface;
        }
        
        // --------------------------------------------------
        // Build Graph Inputs
        #ifdef HAVE_VFX_MODIFICATION
        #define VFX_SRP_ATTRIBUTES Attributes
        #define VFX_SRP_VARYINGS Varyings
        #define VFX_SRP_SURFACE_INPUTS SurfaceDescriptionInputs
        #endif
        VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
        {
            VertexDescriptionInputs output;
            ZERO_INITIALIZE(VertexDescriptionInputs, output);
        
            output.ObjectSpaceNormal =                          input.normalOS;
            output.ObjectSpaceTangent =                         input.tangentOS.xyz;
            output.ObjectSpacePosition =                        input.positionOS;
        
            return output;
        }
        SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
        {
            SurfaceDescriptionInputs output;
            ZERO_INITIALIZE(SurfaceDescriptionInputs, output);
        
        #ifdef HAVE_VFX_MODIFICATION
            // FragInputs from VFX come from two places: Interpolator or CBuffer.
            /* WARNING: $splice Could not find named fragment 'VFXSetFragInputs' */
        
        #endif
        
            
        
        
        
        
        
            output.uv0 = input.texCoord0;
            output.TimeParameters = _TimeParameters.xyz; // This is mainly for LW as HD overwrite this value
        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign =                    IS_FRONT_VFACE(input.cullFace, true, false);
        #else
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        #endif
        #undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        
                return output;
        }
        
        // --------------------------------------------------
        // Main
        
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/Varyings.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/DepthNormalsOnlyPass.hlsl"
        
        // --------------------------------------------------
        // Visual Effect Vertex Invocations
        #ifdef HAVE_VFX_MODIFICATION
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/VisualEffectVertex.hlsl"
        #endif
        
        ENDHLSL
        }
    }
    SubShader
    {
        Tags
        {
            "RenderPipeline"="UniversalPipeline"
            "RenderType"="Transparent"
            "UniversalMaterialType" = "Unlit"
            "Queue"="Transparent"
            "ShaderGraphShader"="true"
            "ShaderGraphTargetId"="UniversalUnlitSubTarget"
        }
        Pass
        {
            Name "Universal Forward"
            Tags
            {
                // LightMode: <None>
            }
        
        // Render State
        Cull Back
        Blend SrcAlpha OneMinusSrcAlpha, One OneMinusSrcAlpha
        ZTest LEqual
        ZWrite Off
        
        // Debug
        // <None>
        
        // --------------------------------------------------
        // Pass
        
        HLSLPROGRAM
        
        // Pragmas
        #pragma target 2.0
        #pragma only_renderers gles gles3 glcore d3d11
        #pragma multi_compile_instancing
        #pragma multi_compile_fog
        #pragma instancing_options renderinglayer
        #pragma vertex vert
        #pragma fragment frag
        
        // DotsInstancingOptions: <None>
        // HybridV1InjectedBuiltinProperties: <None>
        
        // Keywords
        #pragma multi_compile _ LIGHTMAP_ON
        #pragma multi_compile _ DIRLIGHTMAP_COMBINED
        #pragma shader_feature _ _SAMPLE_GI
        #pragma multi_compile_fragment _ _DBUFFER_MRT1 _DBUFFER_MRT2 _DBUFFER_MRT3
        #pragma multi_compile_fragment _ DEBUG_DISPLAY
        // GraphKeywords: <None>
        
        // Defines
        
        #define ATTRIBUTES_NEED_NORMAL
        #define ATTRIBUTES_NEED_TANGENT
        #define ATTRIBUTES_NEED_TEXCOORD0
        #define VARYINGS_NEED_POSITION_WS
        #define VARYINGS_NEED_NORMAL_WS
        #define VARYINGS_NEED_TEXCOORD0
        #define VARYINGS_NEED_VIEWDIRECTION_WS
        #define FEATURES_GRAPH_VERTEX
        /* WARNING: $splice Could not find named fragment 'PassInstancing' */
        #define SHADERPASS SHADERPASS_UNLIT
        #define _FOG_FRAGMENT 1
        #define _SURFACE_TYPE_TRANSPARENT 1
        /* WARNING: $splice Could not find named fragment 'DotsInstancingVars' */
        
        
        // custom interpolator pre-include
        /* WARNING: $splice Could not find named fragment 'sgci_CustomInterpolatorPreInclude' */
        
        // Includes
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DBuffer.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
        
        // --------------------------------------------------
        // Structs and Packing
        
        // custom interpolators pre packing
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPrePacking' */
        
        struct Attributes
        {
             float3 positionOS : POSITION;
             float3 normalOS : NORMAL;
             float4 tangentOS : TANGENT;
             float4 uv0 : TEXCOORD0;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : INSTANCEID_SEMANTIC;
            #endif
        };
        struct Varyings
        {
             float4 positionCS : SV_POSITION;
             float3 positionWS;
             float3 normalWS;
             float4 texCoord0;
             float3 viewDirectionWS;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        struct SurfaceDescriptionInputs
        {
             float4 uv0;
             float3 TimeParameters;
        };
        struct VertexDescriptionInputs
        {
             float3 ObjectSpaceNormal;
             float3 ObjectSpaceTangent;
             float3 ObjectSpacePosition;
        };
        struct PackedVaryings
        {
             float4 positionCS : SV_POSITION;
             float3 interp0 : INTERP0;
             float3 interp1 : INTERP1;
             float4 interp2 : INTERP2;
             float3 interp3 : INTERP3;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        
        PackedVaryings PackVaryings (Varyings input)
        {
            PackedVaryings output;
            ZERO_INITIALIZE(PackedVaryings, output);
            output.positionCS = input.positionCS;
            output.interp0.xyz =  input.positionWS;
            output.interp1.xyz =  input.normalWS;
            output.interp2.xyzw =  input.texCoord0;
            output.interp3.xyz =  input.viewDirectionWS;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        Varyings UnpackVaryings (PackedVaryings input)
        {
            Varyings output;
            output.positionCS = input.positionCS;
            output.positionWS = input.interp0.xyz;
            output.normalWS = input.interp1.xyz;
            output.texCoord0 = input.interp2.xyzw;
            output.viewDirectionWS = input.interp3.xyz;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        
        // --------------------------------------------------
        // Graph
        
        // Graph Properties
        CBUFFER_START(UnityPerMaterial)
        float _Speed;
        float _SpinDir;
        float _Width;
        float _TailLength;
        float4 _Color;
        CBUFFER_END
        
        // Object and Global properties
        
        // Graph Includes
        // GraphIncludes: <None>
        
        // -- Property used by ScenePickingPass
        #ifdef SCENEPICKINGPASS
        float4 _SelectionID;
        #endif
        
        // -- Properties used by SceneSelectionPass
        #ifdef SCENESELECTIONPASS
        int _ObjectId;
        int _PassValue;
        #endif
        
        // Graph Functions
        
        void Unity_Multiply_float_float(float A, float B, out float Out)
        {
            Out = A * B;
        }
        
        void Unity_Rotate_Radians_float(float2 UV, float2 Center, float Rotation, out float2 Out)
        {
            //rotation matrix
            UV -= Center;
            float s = sin(Rotation);
            float c = cos(Rotation);
        
            //center rotation matrix
            float2x2 rMatrix = float2x2(c, -s, s, c);
            rMatrix *= 0.5;
            rMatrix += 0.5;
            rMatrix = rMatrix*2 - 1;
        
            //multiply the UVs by the rotation matrix
            UV.xy = mul(UV.xy, rMatrix);
            UV += Center;
        
            Out = UV;
        }
        
        void Unity_PolarCoordinates_float(float2 UV, float2 Center, float RadialScale, float LengthScale, out float2 Out)
        {
            float2 delta = UV - Center;
            float radius = length(delta) * 2 * RadialScale;
            float angle = atan2(delta.x, delta.y) * 1.0/6.28 * LengthScale;
            Out = float2(radius, angle);
        }
        
        void Unity_Rectangle_Fastest_float(float2 UV, float Width, float Height, out float Out)
        {
            float2 d = abs(UV * 2 - 1) - float2(Width, Height);
        #if defined(SHADER_STAGE_RAY_TRACING)
            d = saturate((1 - saturate(d * 1e7)));
        #else
            d = saturate(1 - d / fwidth(d));
        #endif
            Out = min(d.x, d.y);
        }
        
        void Unity_Rotate_Degrees_float(float2 UV, float2 Center, float Rotation, out float2 Out)
        {
            //rotation matrix
            Rotation = Rotation * (3.1415926f/180.0f);
            UV -= Center;
            float s = sin(Rotation);
            float c = cos(Rotation);
        
            //center rotation matrix
            float2x2 rMatrix = float2x2(c, -s, s, c);
            rMatrix *= 0.5;
            rMatrix += 0.5;
            rMatrix = rMatrix*2 - 1;
        
            //multiply the UVs by the rotation matrix
            UV.xy = mul(UV.xy, rMatrix);
            UV += Center;
        
            Out = UV;
        }
        
        void Unity_Preview_float(float In, out float Out)
        {
            Out = In;
        }
        
        void Unity_Saturate_float(float In, out float Out)
        {
            Out = saturate(In);
        }
        
        void Unity_Power_float(float A, float B, out float Out)
        {
            Out = pow(A, B);
        }
        
        void Unity_Smoothstep_float(float Edge1, float Edge2, float In, out float Out)
        {
            Out = smoothstep(Edge1, Edge2, In);
        }
        
        // Custom interpolators pre vertex
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreVertex' */
        
        // Graph Vertex
        struct VertexDescription
        {
            float3 Position;
            float3 Normal;
            float3 Tangent;
        };
        
        VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
        {
            VertexDescription description = (VertexDescription)0;
            description.Position = IN.ObjectSpacePosition;
            description.Normal = IN.ObjectSpaceNormal;
            description.Tangent = IN.ObjectSpaceTangent;
            return description;
        }
        
        // Custom interpolators, pre surface
        #ifdef FEATURES_GRAPH_VERTEX
        Varyings CustomInterpolatorPassThroughFunc(inout Varyings output, VertexDescription input)
        {
        return output;
        }
        #define CUSTOMINTERPOLATOR_VARYPASSTHROUGH_FUNC
        #endif
        
        // Graph Pixel
        struct SurfaceDescription
        {
            float3 BaseColor;
            float Alpha;
        };
        
        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            float4 _Property_bec96e8a375c459c9444e2dec1d14a9a_Out_0 = _Color;
            float _Property_d94c15e3e2f84f569def2f6f4d8bbcab_Out_0 = _Speed;
            float _Multiply_ed2b4a35ed9147c59203777f0c7b2b2e_Out_2;
            Unity_Multiply_float_float(_Property_d94c15e3e2f84f569def2f6f4d8bbcab_Out_0, IN.TimeParameters.x, _Multiply_ed2b4a35ed9147c59203777f0c7b2b2e_Out_2);
            float _Property_f4a3c549f0874108876488320540244d_Out_0 = _SpinDir;
            float _Multiply_5f0c40c7364e4dcf81df67e0b9a85adb_Out_2;
            Unity_Multiply_float_float(_Property_f4a3c549f0874108876488320540244d_Out_0, -1, _Multiply_5f0c40c7364e4dcf81df67e0b9a85adb_Out_2);
            float _Multiply_7f7f94f247b24711bae1d438f6d4cdfa_Out_2;
            Unity_Multiply_float_float(_Multiply_ed2b4a35ed9147c59203777f0c7b2b2e_Out_2, _Multiply_5f0c40c7364e4dcf81df67e0b9a85adb_Out_2, _Multiply_7f7f94f247b24711bae1d438f6d4cdfa_Out_2);
            float2 _Rotate_35cdf476638c4ddfbfc5a472b4555a08_Out_3;
            Unity_Rotate_Radians_float(IN.uv0.xy, float2 (0.5, 0.5), _Multiply_7f7f94f247b24711bae1d438f6d4cdfa_Out_2, _Rotate_35cdf476638c4ddfbfc5a472b4555a08_Out_3);
            float2 _PolarCoordinates_137488258455411ea60aa4c0c170754f_Out_4;
            Unity_PolarCoordinates_float(_Rotate_35cdf476638c4ddfbfc5a472b4555a08_Out_3, float2 (0.5, 0.5), 0.7, 1.2, _PolarCoordinates_137488258455411ea60aa4c0c170754f_Out_4);
            float _Property_319ca09021c945c2857c1b7ea2a1fa6d_Out_0 = _Width;
            float _Rectangle_fa3b515096cf4847937839ee010b43e4_Out_3;
            Unity_Rectangle_Fastest_float(_PolarCoordinates_137488258455411ea60aa4c0c170754f_Out_4, _Property_319ca09021c945c2857c1b7ea2a1fa6d_Out_0, 3, _Rectangle_fa3b515096cf4847937839ee010b43e4_Out_3);
            float2 _Rotate_e221fbd58f8d426aa1a4907dd2ada1e1_Out_3;
            Unity_Rotate_Degrees_float(_Rotate_35cdf476638c4ddfbfc5a472b4555a08_Out_3, float2 (0.5, 0.5), -45, _Rotate_e221fbd58f8d426aa1a4907dd2ada1e1_Out_3);
            float _Property_860963b9226a4064a3c9487923211bfc_Out_0 = _TailLength;
            float2 _PolarCoordinates_dd40abfe6e5842c2b45c2a148178b7f6_Out_4;
            Unity_PolarCoordinates_float(_Rotate_e221fbd58f8d426aa1a4907dd2ada1e1_Out_3, float2 (0.5, 0.5), 1, _Property_860963b9226a4064a3c9487923211bfc_Out_0, _PolarCoordinates_dd40abfe6e5842c2b45c2a148178b7f6_Out_4);
            float _Split_e374f964ab2f4dcfb3b5d389cac488ee_R_1 = _PolarCoordinates_dd40abfe6e5842c2b45c2a148178b7f6_Out_4[0];
            float _Split_e374f964ab2f4dcfb3b5d389cac488ee_G_2 = _PolarCoordinates_dd40abfe6e5842c2b45c2a148178b7f6_Out_4[1];
            float _Split_e374f964ab2f4dcfb3b5d389cac488ee_B_3 = 0;
            float _Split_e374f964ab2f4dcfb3b5d389cac488ee_A_4 = 0;
            float _Preview_1959caf9d2b54c5a88bf965479f95f73_Out_1;
            Unity_Preview_float(_Split_e374f964ab2f4dcfb3b5d389cac488ee_G_2, _Preview_1959caf9d2b54c5a88bf965479f95f73_Out_1);
            float _Saturate_7a8a4ea4de02406dbafae645cfaa04ee_Out_1;
            Unity_Saturate_float(_Preview_1959caf9d2b54c5a88bf965479f95f73_Out_1, _Saturate_7a8a4ea4de02406dbafae645cfaa04ee_Out_1);
            float _Multiply_12051c8d973743848260fcea125d26ff_Out_2;
            Unity_Multiply_float_float(_Rectangle_fa3b515096cf4847937839ee010b43e4_Out_3, _Saturate_7a8a4ea4de02406dbafae645cfaa04ee_Out_1, _Multiply_12051c8d973743848260fcea125d26ff_Out_2);
            float _Power_40414304ee674ccdb6c4065358e09770_Out_2;
            Unity_Power_float(_Multiply_12051c8d973743848260fcea125d26ff_Out_2, 1.56, _Power_40414304ee674ccdb6c4065358e09770_Out_2);
            float _Smoothstep_91dc040ce0744d8880e840faeacb9a07_Out_3;
            Unity_Smoothstep_float(0.03, 0.35, _Power_40414304ee674ccdb6c4065358e09770_Out_2, _Smoothstep_91dc040ce0744d8880e840faeacb9a07_Out_3);
            surface.BaseColor = (_Property_bec96e8a375c459c9444e2dec1d14a9a_Out_0.xyz);
            surface.Alpha = _Smoothstep_91dc040ce0744d8880e840faeacb9a07_Out_3;
            return surface;
        }
        
        // --------------------------------------------------
        // Build Graph Inputs
        #ifdef HAVE_VFX_MODIFICATION
        #define VFX_SRP_ATTRIBUTES Attributes
        #define VFX_SRP_VARYINGS Varyings
        #define VFX_SRP_SURFACE_INPUTS SurfaceDescriptionInputs
        #endif
        VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
        {
            VertexDescriptionInputs output;
            ZERO_INITIALIZE(VertexDescriptionInputs, output);
        
            output.ObjectSpaceNormal =                          input.normalOS;
            output.ObjectSpaceTangent =                         input.tangentOS.xyz;
            output.ObjectSpacePosition =                        input.positionOS;
        
            return output;
        }
        SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
        {
            SurfaceDescriptionInputs output;
            ZERO_INITIALIZE(SurfaceDescriptionInputs, output);
        
        #ifdef HAVE_VFX_MODIFICATION
            // FragInputs from VFX come from two places: Interpolator or CBuffer.
            /* WARNING: $splice Could not find named fragment 'VFXSetFragInputs' */
        
        #endif
        
            
        
        
        
        
        
            output.uv0 = input.texCoord0;
            output.TimeParameters = _TimeParameters.xyz; // This is mainly for LW as HD overwrite this value
        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign =                    IS_FRONT_VFACE(input.cullFace, true, false);
        #else
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        #endif
        #undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        
                return output;
        }
        
        // --------------------------------------------------
        // Main
        
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/Varyings.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/UnlitPass.hlsl"
        
        // --------------------------------------------------
        // Visual Effect Vertex Invocations
        #ifdef HAVE_VFX_MODIFICATION
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/VisualEffectVertex.hlsl"
        #endif
        
        ENDHLSL
        }
        Pass
        {
            Name "DepthNormalsOnly"
            Tags
            {
                "LightMode" = "DepthNormalsOnly"
            }
        
        // Render State
        Cull Back
        ZTest LEqual
        ZWrite On
        
        // Debug
        // <None>
        
        // --------------------------------------------------
        // Pass
        
        HLSLPROGRAM
        
        // Pragmas
        #pragma target 2.0
        #pragma only_renderers gles gles3 glcore d3d11
        #pragma multi_compile_instancing
        #pragma vertex vert
        #pragma fragment frag
        
        // DotsInstancingOptions: <None>
        // HybridV1InjectedBuiltinProperties: <None>
        
        // Keywords
        // PassKeywords: <None>
        // GraphKeywords: <None>
        
        // Defines
        
        #define ATTRIBUTES_NEED_NORMAL
        #define ATTRIBUTES_NEED_TANGENT
        #define ATTRIBUTES_NEED_TEXCOORD0
        #define ATTRIBUTES_NEED_TEXCOORD1
        #define VARYINGS_NEED_NORMAL_WS
        #define VARYINGS_NEED_TANGENT_WS
        #define VARYINGS_NEED_TEXCOORD0
        #define FEATURES_GRAPH_VERTEX
        /* WARNING: $splice Could not find named fragment 'PassInstancing' */
        #define SHADERPASS SHADERPASS_DEPTHNORMALSONLY
        /* WARNING: $splice Could not find named fragment 'DotsInstancingVars' */
        
        
        // custom interpolator pre-include
        /* WARNING: $splice Could not find named fragment 'sgci_CustomInterpolatorPreInclude' */
        
        // Includes
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
        
        // --------------------------------------------------
        // Structs and Packing
        
        // custom interpolators pre packing
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPrePacking' */
        
        struct Attributes
        {
             float3 positionOS : POSITION;
             float3 normalOS : NORMAL;
             float4 tangentOS : TANGENT;
             float4 uv0 : TEXCOORD0;
             float4 uv1 : TEXCOORD1;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : INSTANCEID_SEMANTIC;
            #endif
        };
        struct Varyings
        {
             float4 positionCS : SV_POSITION;
             float3 normalWS;
             float4 tangentWS;
             float4 texCoord0;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        struct SurfaceDescriptionInputs
        {
             float4 uv0;
             float3 TimeParameters;
        };
        struct VertexDescriptionInputs
        {
             float3 ObjectSpaceNormal;
             float3 ObjectSpaceTangent;
             float3 ObjectSpacePosition;
        };
        struct PackedVaryings
        {
             float4 positionCS : SV_POSITION;
             float3 interp0 : INTERP0;
             float4 interp1 : INTERP1;
             float4 interp2 : INTERP2;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        
        PackedVaryings PackVaryings (Varyings input)
        {
            PackedVaryings output;
            ZERO_INITIALIZE(PackedVaryings, output);
            output.positionCS = input.positionCS;
            output.interp0.xyz =  input.normalWS;
            output.interp1.xyzw =  input.tangentWS;
            output.interp2.xyzw =  input.texCoord0;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        Varyings UnpackVaryings (PackedVaryings input)
        {
            Varyings output;
            output.positionCS = input.positionCS;
            output.normalWS = input.interp0.xyz;
            output.tangentWS = input.interp1.xyzw;
            output.texCoord0 = input.interp2.xyzw;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        
        // --------------------------------------------------
        // Graph
        
        // Graph Properties
        CBUFFER_START(UnityPerMaterial)
        float _Speed;
        float _SpinDir;
        float _Width;
        float _TailLength;
        float4 _Color;
        CBUFFER_END
        
        // Object and Global properties
        
        // Graph Includes
        // GraphIncludes: <None>
        
        // -- Property used by ScenePickingPass
        #ifdef SCENEPICKINGPASS
        float4 _SelectionID;
        #endif
        
        // -- Properties used by SceneSelectionPass
        #ifdef SCENESELECTIONPASS
        int _ObjectId;
        int _PassValue;
        #endif
        
        // Graph Functions
        
        void Unity_Multiply_float_float(float A, float B, out float Out)
        {
            Out = A * B;
        }
        
        void Unity_Rotate_Radians_float(float2 UV, float2 Center, float Rotation, out float2 Out)
        {
            //rotation matrix
            UV -= Center;
            float s = sin(Rotation);
            float c = cos(Rotation);
        
            //center rotation matrix
            float2x2 rMatrix = float2x2(c, -s, s, c);
            rMatrix *= 0.5;
            rMatrix += 0.5;
            rMatrix = rMatrix*2 - 1;
        
            //multiply the UVs by the rotation matrix
            UV.xy = mul(UV.xy, rMatrix);
            UV += Center;
        
            Out = UV;
        }
        
        void Unity_PolarCoordinates_float(float2 UV, float2 Center, float RadialScale, float LengthScale, out float2 Out)
        {
            float2 delta = UV - Center;
            float radius = length(delta) * 2 * RadialScale;
            float angle = atan2(delta.x, delta.y) * 1.0/6.28 * LengthScale;
            Out = float2(radius, angle);
        }
        
        void Unity_Rectangle_Fastest_float(float2 UV, float Width, float Height, out float Out)
        {
            float2 d = abs(UV * 2 - 1) - float2(Width, Height);
        #if defined(SHADER_STAGE_RAY_TRACING)
            d = saturate((1 - saturate(d * 1e7)));
        #else
            d = saturate(1 - d / fwidth(d));
        #endif
            Out = min(d.x, d.y);
        }
        
        void Unity_Rotate_Degrees_float(float2 UV, float2 Center, float Rotation, out float2 Out)
        {
            //rotation matrix
            Rotation = Rotation * (3.1415926f/180.0f);
            UV -= Center;
            float s = sin(Rotation);
            float c = cos(Rotation);
        
            //center rotation matrix
            float2x2 rMatrix = float2x2(c, -s, s, c);
            rMatrix *= 0.5;
            rMatrix += 0.5;
            rMatrix = rMatrix*2 - 1;
        
            //multiply the UVs by the rotation matrix
            UV.xy = mul(UV.xy, rMatrix);
            UV += Center;
        
            Out = UV;
        }
        
        void Unity_Preview_float(float In, out float Out)
        {
            Out = In;
        }
        
        void Unity_Saturate_float(float In, out float Out)
        {
            Out = saturate(In);
        }
        
        void Unity_Power_float(float A, float B, out float Out)
        {
            Out = pow(A, B);
        }
        
        void Unity_Smoothstep_float(float Edge1, float Edge2, float In, out float Out)
        {
            Out = smoothstep(Edge1, Edge2, In);
        }
        
        // Custom interpolators pre vertex
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreVertex' */
        
        // Graph Vertex
        struct VertexDescription
        {
            float3 Position;
            float3 Normal;
            float3 Tangent;
        };
        
        VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
        {
            VertexDescription description = (VertexDescription)0;
            description.Position = IN.ObjectSpacePosition;
            description.Normal = IN.ObjectSpaceNormal;
            description.Tangent = IN.ObjectSpaceTangent;
            return description;
        }
        
        // Custom interpolators, pre surface
        #ifdef FEATURES_GRAPH_VERTEX
        Varyings CustomInterpolatorPassThroughFunc(inout Varyings output, VertexDescription input)
        {
        return output;
        }
        #define CUSTOMINTERPOLATOR_VARYPASSTHROUGH_FUNC
        #endif
        
        // Graph Pixel
        struct SurfaceDescription
        {
            float Alpha;
        };
        
        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            float _Property_d94c15e3e2f84f569def2f6f4d8bbcab_Out_0 = _Speed;
            float _Multiply_ed2b4a35ed9147c59203777f0c7b2b2e_Out_2;
            Unity_Multiply_float_float(_Property_d94c15e3e2f84f569def2f6f4d8bbcab_Out_0, IN.TimeParameters.x, _Multiply_ed2b4a35ed9147c59203777f0c7b2b2e_Out_2);
            float _Property_f4a3c549f0874108876488320540244d_Out_0 = _SpinDir;
            float _Multiply_5f0c40c7364e4dcf81df67e0b9a85adb_Out_2;
            Unity_Multiply_float_float(_Property_f4a3c549f0874108876488320540244d_Out_0, -1, _Multiply_5f0c40c7364e4dcf81df67e0b9a85adb_Out_2);
            float _Multiply_7f7f94f247b24711bae1d438f6d4cdfa_Out_2;
            Unity_Multiply_float_float(_Multiply_ed2b4a35ed9147c59203777f0c7b2b2e_Out_2, _Multiply_5f0c40c7364e4dcf81df67e0b9a85adb_Out_2, _Multiply_7f7f94f247b24711bae1d438f6d4cdfa_Out_2);
            float2 _Rotate_35cdf476638c4ddfbfc5a472b4555a08_Out_3;
            Unity_Rotate_Radians_float(IN.uv0.xy, float2 (0.5, 0.5), _Multiply_7f7f94f247b24711bae1d438f6d4cdfa_Out_2, _Rotate_35cdf476638c4ddfbfc5a472b4555a08_Out_3);
            float2 _PolarCoordinates_137488258455411ea60aa4c0c170754f_Out_4;
            Unity_PolarCoordinates_float(_Rotate_35cdf476638c4ddfbfc5a472b4555a08_Out_3, float2 (0.5, 0.5), 0.7, 1.2, _PolarCoordinates_137488258455411ea60aa4c0c170754f_Out_4);
            float _Property_319ca09021c945c2857c1b7ea2a1fa6d_Out_0 = _Width;
            float _Rectangle_fa3b515096cf4847937839ee010b43e4_Out_3;
            Unity_Rectangle_Fastest_float(_PolarCoordinates_137488258455411ea60aa4c0c170754f_Out_4, _Property_319ca09021c945c2857c1b7ea2a1fa6d_Out_0, 3, _Rectangle_fa3b515096cf4847937839ee010b43e4_Out_3);
            float2 _Rotate_e221fbd58f8d426aa1a4907dd2ada1e1_Out_3;
            Unity_Rotate_Degrees_float(_Rotate_35cdf476638c4ddfbfc5a472b4555a08_Out_3, float2 (0.5, 0.5), -45, _Rotate_e221fbd58f8d426aa1a4907dd2ada1e1_Out_3);
            float _Property_860963b9226a4064a3c9487923211bfc_Out_0 = _TailLength;
            float2 _PolarCoordinates_dd40abfe6e5842c2b45c2a148178b7f6_Out_4;
            Unity_PolarCoordinates_float(_Rotate_e221fbd58f8d426aa1a4907dd2ada1e1_Out_3, float2 (0.5, 0.5), 1, _Property_860963b9226a4064a3c9487923211bfc_Out_0, _PolarCoordinates_dd40abfe6e5842c2b45c2a148178b7f6_Out_4);
            float _Split_e374f964ab2f4dcfb3b5d389cac488ee_R_1 = _PolarCoordinates_dd40abfe6e5842c2b45c2a148178b7f6_Out_4[0];
            float _Split_e374f964ab2f4dcfb3b5d389cac488ee_G_2 = _PolarCoordinates_dd40abfe6e5842c2b45c2a148178b7f6_Out_4[1];
            float _Split_e374f964ab2f4dcfb3b5d389cac488ee_B_3 = 0;
            float _Split_e374f964ab2f4dcfb3b5d389cac488ee_A_4 = 0;
            float _Preview_1959caf9d2b54c5a88bf965479f95f73_Out_1;
            Unity_Preview_float(_Split_e374f964ab2f4dcfb3b5d389cac488ee_G_2, _Preview_1959caf9d2b54c5a88bf965479f95f73_Out_1);
            float _Saturate_7a8a4ea4de02406dbafae645cfaa04ee_Out_1;
            Unity_Saturate_float(_Preview_1959caf9d2b54c5a88bf965479f95f73_Out_1, _Saturate_7a8a4ea4de02406dbafae645cfaa04ee_Out_1);
            float _Multiply_12051c8d973743848260fcea125d26ff_Out_2;
            Unity_Multiply_float_float(_Rectangle_fa3b515096cf4847937839ee010b43e4_Out_3, _Saturate_7a8a4ea4de02406dbafae645cfaa04ee_Out_1, _Multiply_12051c8d973743848260fcea125d26ff_Out_2);
            float _Power_40414304ee674ccdb6c4065358e09770_Out_2;
            Unity_Power_float(_Multiply_12051c8d973743848260fcea125d26ff_Out_2, 1.56, _Power_40414304ee674ccdb6c4065358e09770_Out_2);
            float _Smoothstep_91dc040ce0744d8880e840faeacb9a07_Out_3;
            Unity_Smoothstep_float(0.03, 0.35, _Power_40414304ee674ccdb6c4065358e09770_Out_2, _Smoothstep_91dc040ce0744d8880e840faeacb9a07_Out_3);
            surface.Alpha = _Smoothstep_91dc040ce0744d8880e840faeacb9a07_Out_3;
            return surface;
        }
        
        // --------------------------------------------------
        // Build Graph Inputs
        #ifdef HAVE_VFX_MODIFICATION
        #define VFX_SRP_ATTRIBUTES Attributes
        #define VFX_SRP_VARYINGS Varyings
        #define VFX_SRP_SURFACE_INPUTS SurfaceDescriptionInputs
        #endif
        VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
        {
            VertexDescriptionInputs output;
            ZERO_INITIALIZE(VertexDescriptionInputs, output);
        
            output.ObjectSpaceNormal =                          input.normalOS;
            output.ObjectSpaceTangent =                         input.tangentOS.xyz;
            output.ObjectSpacePosition =                        input.positionOS;
        
            return output;
        }
        SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
        {
            SurfaceDescriptionInputs output;
            ZERO_INITIALIZE(SurfaceDescriptionInputs, output);
        
        #ifdef HAVE_VFX_MODIFICATION
            // FragInputs from VFX come from two places: Interpolator or CBuffer.
            /* WARNING: $splice Could not find named fragment 'VFXSetFragInputs' */
        
        #endif
        
            
        
        
        
        
        
            output.uv0 = input.texCoord0;
            output.TimeParameters = _TimeParameters.xyz; // This is mainly for LW as HD overwrite this value
        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign =                    IS_FRONT_VFACE(input.cullFace, true, false);
        #else
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        #endif
        #undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        
                return output;
        }
        
        // --------------------------------------------------
        // Main
        
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/Varyings.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/DepthNormalsOnlyPass.hlsl"
        
        // --------------------------------------------------
        // Visual Effect Vertex Invocations
        #ifdef HAVE_VFX_MODIFICATION
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/VisualEffectVertex.hlsl"
        #endif
        
        ENDHLSL
        }
        Pass
        {
            Name "ShadowCaster"
            Tags
            {
                "LightMode" = "ShadowCaster"
            }
        
        // Render State
        Cull Back
        ZTest LEqual
        ZWrite On
        ColorMask 0
        
        // Debug
        // <None>
        
        // --------------------------------------------------
        // Pass
        
        HLSLPROGRAM
        
        // Pragmas
        #pragma target 2.0
        #pragma only_renderers gles gles3 glcore d3d11
        #pragma multi_compile_instancing
        #pragma vertex vert
        #pragma fragment frag
        
        // DotsInstancingOptions: <None>
        // HybridV1InjectedBuiltinProperties: <None>
        
        // Keywords
        #pragma multi_compile_vertex _ _CASTING_PUNCTUAL_LIGHT_SHADOW
        // GraphKeywords: <None>
        
        // Defines
        
        #define ATTRIBUTES_NEED_NORMAL
        #define ATTRIBUTES_NEED_TANGENT
        #define ATTRIBUTES_NEED_TEXCOORD0
        #define VARYINGS_NEED_NORMAL_WS
        #define VARYINGS_NEED_TEXCOORD0
        #define FEATURES_GRAPH_VERTEX
        /* WARNING: $splice Could not find named fragment 'PassInstancing' */
        #define SHADERPASS SHADERPASS_SHADOWCASTER
        /* WARNING: $splice Could not find named fragment 'DotsInstancingVars' */
        
        
        // custom interpolator pre-include
        /* WARNING: $splice Could not find named fragment 'sgci_CustomInterpolatorPreInclude' */
        
        // Includes
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
        
        // --------------------------------------------------
        // Structs and Packing
        
        // custom interpolators pre packing
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPrePacking' */
        
        struct Attributes
        {
             float3 positionOS : POSITION;
             float3 normalOS : NORMAL;
             float4 tangentOS : TANGENT;
             float4 uv0 : TEXCOORD0;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : INSTANCEID_SEMANTIC;
            #endif
        };
        struct Varyings
        {
             float4 positionCS : SV_POSITION;
             float3 normalWS;
             float4 texCoord0;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        struct SurfaceDescriptionInputs
        {
             float4 uv0;
             float3 TimeParameters;
        };
        struct VertexDescriptionInputs
        {
             float3 ObjectSpaceNormal;
             float3 ObjectSpaceTangent;
             float3 ObjectSpacePosition;
        };
        struct PackedVaryings
        {
             float4 positionCS : SV_POSITION;
             float3 interp0 : INTERP0;
             float4 interp1 : INTERP1;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        
        PackedVaryings PackVaryings (Varyings input)
        {
            PackedVaryings output;
            ZERO_INITIALIZE(PackedVaryings, output);
            output.positionCS = input.positionCS;
            output.interp0.xyz =  input.normalWS;
            output.interp1.xyzw =  input.texCoord0;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        Varyings UnpackVaryings (PackedVaryings input)
        {
            Varyings output;
            output.positionCS = input.positionCS;
            output.normalWS = input.interp0.xyz;
            output.texCoord0 = input.interp1.xyzw;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        
        // --------------------------------------------------
        // Graph
        
        // Graph Properties
        CBUFFER_START(UnityPerMaterial)
        float _Speed;
        float _SpinDir;
        float _Width;
        float _TailLength;
        float4 _Color;
        CBUFFER_END
        
        // Object and Global properties
        
        // Graph Includes
        // GraphIncludes: <None>
        
        // -- Property used by ScenePickingPass
        #ifdef SCENEPICKINGPASS
        float4 _SelectionID;
        #endif
        
        // -- Properties used by SceneSelectionPass
        #ifdef SCENESELECTIONPASS
        int _ObjectId;
        int _PassValue;
        #endif
        
        // Graph Functions
        
        void Unity_Multiply_float_float(float A, float B, out float Out)
        {
            Out = A * B;
        }
        
        void Unity_Rotate_Radians_float(float2 UV, float2 Center, float Rotation, out float2 Out)
        {
            //rotation matrix
            UV -= Center;
            float s = sin(Rotation);
            float c = cos(Rotation);
        
            //center rotation matrix
            float2x2 rMatrix = float2x2(c, -s, s, c);
            rMatrix *= 0.5;
            rMatrix += 0.5;
            rMatrix = rMatrix*2 - 1;
        
            //multiply the UVs by the rotation matrix
            UV.xy = mul(UV.xy, rMatrix);
            UV += Center;
        
            Out = UV;
        }
        
        void Unity_PolarCoordinates_float(float2 UV, float2 Center, float RadialScale, float LengthScale, out float2 Out)
        {
            float2 delta = UV - Center;
            float radius = length(delta) * 2 * RadialScale;
            float angle = atan2(delta.x, delta.y) * 1.0/6.28 * LengthScale;
            Out = float2(radius, angle);
        }
        
        void Unity_Rectangle_Fastest_float(float2 UV, float Width, float Height, out float Out)
        {
            float2 d = abs(UV * 2 - 1) - float2(Width, Height);
        #if defined(SHADER_STAGE_RAY_TRACING)
            d = saturate((1 - saturate(d * 1e7)));
        #else
            d = saturate(1 - d / fwidth(d));
        #endif
            Out = min(d.x, d.y);
        }
        
        void Unity_Rotate_Degrees_float(float2 UV, float2 Center, float Rotation, out float2 Out)
        {
            //rotation matrix
            Rotation = Rotation * (3.1415926f/180.0f);
            UV -= Center;
            float s = sin(Rotation);
            float c = cos(Rotation);
        
            //center rotation matrix
            float2x2 rMatrix = float2x2(c, -s, s, c);
            rMatrix *= 0.5;
            rMatrix += 0.5;
            rMatrix = rMatrix*2 - 1;
        
            //multiply the UVs by the rotation matrix
            UV.xy = mul(UV.xy, rMatrix);
            UV += Center;
        
            Out = UV;
        }
        
        void Unity_Preview_float(float In, out float Out)
        {
            Out = In;
        }
        
        void Unity_Saturate_float(float In, out float Out)
        {
            Out = saturate(In);
        }
        
        void Unity_Power_float(float A, float B, out float Out)
        {
            Out = pow(A, B);
        }
        
        void Unity_Smoothstep_float(float Edge1, float Edge2, float In, out float Out)
        {
            Out = smoothstep(Edge1, Edge2, In);
        }
        
        // Custom interpolators pre vertex
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreVertex' */
        
        // Graph Vertex
        struct VertexDescription
        {
            float3 Position;
            float3 Normal;
            float3 Tangent;
        };
        
        VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
        {
            VertexDescription description = (VertexDescription)0;
            description.Position = IN.ObjectSpacePosition;
            description.Normal = IN.ObjectSpaceNormal;
            description.Tangent = IN.ObjectSpaceTangent;
            return description;
        }
        
        // Custom interpolators, pre surface
        #ifdef FEATURES_GRAPH_VERTEX
        Varyings CustomInterpolatorPassThroughFunc(inout Varyings output, VertexDescription input)
        {
        return output;
        }
        #define CUSTOMINTERPOLATOR_VARYPASSTHROUGH_FUNC
        #endif
        
        // Graph Pixel
        struct SurfaceDescription
        {
            float Alpha;
        };
        
        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            float _Property_d94c15e3e2f84f569def2f6f4d8bbcab_Out_0 = _Speed;
            float _Multiply_ed2b4a35ed9147c59203777f0c7b2b2e_Out_2;
            Unity_Multiply_float_float(_Property_d94c15e3e2f84f569def2f6f4d8bbcab_Out_0, IN.TimeParameters.x, _Multiply_ed2b4a35ed9147c59203777f0c7b2b2e_Out_2);
            float _Property_f4a3c549f0874108876488320540244d_Out_0 = _SpinDir;
            float _Multiply_5f0c40c7364e4dcf81df67e0b9a85adb_Out_2;
            Unity_Multiply_float_float(_Property_f4a3c549f0874108876488320540244d_Out_0, -1, _Multiply_5f0c40c7364e4dcf81df67e0b9a85adb_Out_2);
            float _Multiply_7f7f94f247b24711bae1d438f6d4cdfa_Out_2;
            Unity_Multiply_float_float(_Multiply_ed2b4a35ed9147c59203777f0c7b2b2e_Out_2, _Multiply_5f0c40c7364e4dcf81df67e0b9a85adb_Out_2, _Multiply_7f7f94f247b24711bae1d438f6d4cdfa_Out_2);
            float2 _Rotate_35cdf476638c4ddfbfc5a472b4555a08_Out_3;
            Unity_Rotate_Radians_float(IN.uv0.xy, float2 (0.5, 0.5), _Multiply_7f7f94f247b24711bae1d438f6d4cdfa_Out_2, _Rotate_35cdf476638c4ddfbfc5a472b4555a08_Out_3);
            float2 _PolarCoordinates_137488258455411ea60aa4c0c170754f_Out_4;
            Unity_PolarCoordinates_float(_Rotate_35cdf476638c4ddfbfc5a472b4555a08_Out_3, float2 (0.5, 0.5), 0.7, 1.2, _PolarCoordinates_137488258455411ea60aa4c0c170754f_Out_4);
            float _Property_319ca09021c945c2857c1b7ea2a1fa6d_Out_0 = _Width;
            float _Rectangle_fa3b515096cf4847937839ee010b43e4_Out_3;
            Unity_Rectangle_Fastest_float(_PolarCoordinates_137488258455411ea60aa4c0c170754f_Out_4, _Property_319ca09021c945c2857c1b7ea2a1fa6d_Out_0, 3, _Rectangle_fa3b515096cf4847937839ee010b43e4_Out_3);
            float2 _Rotate_e221fbd58f8d426aa1a4907dd2ada1e1_Out_3;
            Unity_Rotate_Degrees_float(_Rotate_35cdf476638c4ddfbfc5a472b4555a08_Out_3, float2 (0.5, 0.5), -45, _Rotate_e221fbd58f8d426aa1a4907dd2ada1e1_Out_3);
            float _Property_860963b9226a4064a3c9487923211bfc_Out_0 = _TailLength;
            float2 _PolarCoordinates_dd40abfe6e5842c2b45c2a148178b7f6_Out_4;
            Unity_PolarCoordinates_float(_Rotate_e221fbd58f8d426aa1a4907dd2ada1e1_Out_3, float2 (0.5, 0.5), 1, _Property_860963b9226a4064a3c9487923211bfc_Out_0, _PolarCoordinates_dd40abfe6e5842c2b45c2a148178b7f6_Out_4);
            float _Split_e374f964ab2f4dcfb3b5d389cac488ee_R_1 = _PolarCoordinates_dd40abfe6e5842c2b45c2a148178b7f6_Out_4[0];
            float _Split_e374f964ab2f4dcfb3b5d389cac488ee_G_2 = _PolarCoordinates_dd40abfe6e5842c2b45c2a148178b7f6_Out_4[1];
            float _Split_e374f964ab2f4dcfb3b5d389cac488ee_B_3 = 0;
            float _Split_e374f964ab2f4dcfb3b5d389cac488ee_A_4 = 0;
            float _Preview_1959caf9d2b54c5a88bf965479f95f73_Out_1;
            Unity_Preview_float(_Split_e374f964ab2f4dcfb3b5d389cac488ee_G_2, _Preview_1959caf9d2b54c5a88bf965479f95f73_Out_1);
            float _Saturate_7a8a4ea4de02406dbafae645cfaa04ee_Out_1;
            Unity_Saturate_float(_Preview_1959caf9d2b54c5a88bf965479f95f73_Out_1, _Saturate_7a8a4ea4de02406dbafae645cfaa04ee_Out_1);
            float _Multiply_12051c8d973743848260fcea125d26ff_Out_2;
            Unity_Multiply_float_float(_Rectangle_fa3b515096cf4847937839ee010b43e4_Out_3, _Saturate_7a8a4ea4de02406dbafae645cfaa04ee_Out_1, _Multiply_12051c8d973743848260fcea125d26ff_Out_2);
            float _Power_40414304ee674ccdb6c4065358e09770_Out_2;
            Unity_Power_float(_Multiply_12051c8d973743848260fcea125d26ff_Out_2, 1.56, _Power_40414304ee674ccdb6c4065358e09770_Out_2);
            float _Smoothstep_91dc040ce0744d8880e840faeacb9a07_Out_3;
            Unity_Smoothstep_float(0.03, 0.35, _Power_40414304ee674ccdb6c4065358e09770_Out_2, _Smoothstep_91dc040ce0744d8880e840faeacb9a07_Out_3);
            surface.Alpha = _Smoothstep_91dc040ce0744d8880e840faeacb9a07_Out_3;
            return surface;
        }
        
        // --------------------------------------------------
        // Build Graph Inputs
        #ifdef HAVE_VFX_MODIFICATION
        #define VFX_SRP_ATTRIBUTES Attributes
        #define VFX_SRP_VARYINGS Varyings
        #define VFX_SRP_SURFACE_INPUTS SurfaceDescriptionInputs
        #endif
        VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
        {
            VertexDescriptionInputs output;
            ZERO_INITIALIZE(VertexDescriptionInputs, output);
        
            output.ObjectSpaceNormal =                          input.normalOS;
            output.ObjectSpaceTangent =                         input.tangentOS.xyz;
            output.ObjectSpacePosition =                        input.positionOS;
        
            return output;
        }
        SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
        {
            SurfaceDescriptionInputs output;
            ZERO_INITIALIZE(SurfaceDescriptionInputs, output);
        
        #ifdef HAVE_VFX_MODIFICATION
            // FragInputs from VFX come from two places: Interpolator or CBuffer.
            /* WARNING: $splice Could not find named fragment 'VFXSetFragInputs' */
        
        #endif
        
            
        
        
        
        
        
            output.uv0 = input.texCoord0;
            output.TimeParameters = _TimeParameters.xyz; // This is mainly for LW as HD overwrite this value
        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign =                    IS_FRONT_VFACE(input.cullFace, true, false);
        #else
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        #endif
        #undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        
                return output;
        }
        
        // --------------------------------------------------
        // Main
        
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/Varyings.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShadowCasterPass.hlsl"
        
        // --------------------------------------------------
        // Visual Effect Vertex Invocations
        #ifdef HAVE_VFX_MODIFICATION
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/VisualEffectVertex.hlsl"
        #endif
        
        ENDHLSL
        }
        Pass
        {
            Name "SceneSelectionPass"
            Tags
            {
                "LightMode" = "SceneSelectionPass"
            }
        
        // Render State
        Cull Off
        
        // Debug
        // <None>
        
        // --------------------------------------------------
        // Pass
        
        HLSLPROGRAM
        
        // Pragmas
        #pragma target 2.0
        #pragma only_renderers gles gles3 glcore d3d11
        #pragma multi_compile_instancing
        #pragma vertex vert
        #pragma fragment frag
        
        // DotsInstancingOptions: <None>
        // HybridV1InjectedBuiltinProperties: <None>
        
        // Keywords
        // PassKeywords: <None>
        // GraphKeywords: <None>
        
        // Defines
        
        #define ATTRIBUTES_NEED_NORMAL
        #define ATTRIBUTES_NEED_TANGENT
        #define ATTRIBUTES_NEED_TEXCOORD0
        #define VARYINGS_NEED_TEXCOORD0
        #define FEATURES_GRAPH_VERTEX
        /* WARNING: $splice Could not find named fragment 'PassInstancing' */
        #define SHADERPASS SHADERPASS_DEPTHONLY
        #define SCENESELECTIONPASS 1
        #define ALPHA_CLIP_THRESHOLD 1
        /* WARNING: $splice Could not find named fragment 'DotsInstancingVars' */
        
        
        // custom interpolator pre-include
        /* WARNING: $splice Could not find named fragment 'sgci_CustomInterpolatorPreInclude' */
        
        // Includes
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
        
        // --------------------------------------------------
        // Structs and Packing
        
        // custom interpolators pre packing
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPrePacking' */
        
        struct Attributes
        {
             float3 positionOS : POSITION;
             float3 normalOS : NORMAL;
             float4 tangentOS : TANGENT;
             float4 uv0 : TEXCOORD0;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : INSTANCEID_SEMANTIC;
            #endif
        };
        struct Varyings
        {
             float4 positionCS : SV_POSITION;
             float4 texCoord0;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        struct SurfaceDescriptionInputs
        {
             float4 uv0;
             float3 TimeParameters;
        };
        struct VertexDescriptionInputs
        {
             float3 ObjectSpaceNormal;
             float3 ObjectSpaceTangent;
             float3 ObjectSpacePosition;
        };
        struct PackedVaryings
        {
             float4 positionCS : SV_POSITION;
             float4 interp0 : INTERP0;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        
        PackedVaryings PackVaryings (Varyings input)
        {
            PackedVaryings output;
            ZERO_INITIALIZE(PackedVaryings, output);
            output.positionCS = input.positionCS;
            output.interp0.xyzw =  input.texCoord0;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        Varyings UnpackVaryings (PackedVaryings input)
        {
            Varyings output;
            output.positionCS = input.positionCS;
            output.texCoord0 = input.interp0.xyzw;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        
        // --------------------------------------------------
        // Graph
        
        // Graph Properties
        CBUFFER_START(UnityPerMaterial)
        float _Speed;
        float _SpinDir;
        float _Width;
        float _TailLength;
        float4 _Color;
        CBUFFER_END
        
        // Object and Global properties
        
        // Graph Includes
        // GraphIncludes: <None>
        
        // -- Property used by ScenePickingPass
        #ifdef SCENEPICKINGPASS
        float4 _SelectionID;
        #endif
        
        // -- Properties used by SceneSelectionPass
        #ifdef SCENESELECTIONPASS
        int _ObjectId;
        int _PassValue;
        #endif
        
        // Graph Functions
        
        void Unity_Multiply_float_float(float A, float B, out float Out)
        {
            Out = A * B;
        }
        
        void Unity_Rotate_Radians_float(float2 UV, float2 Center, float Rotation, out float2 Out)
        {
            //rotation matrix
            UV -= Center;
            float s = sin(Rotation);
            float c = cos(Rotation);
        
            //center rotation matrix
            float2x2 rMatrix = float2x2(c, -s, s, c);
            rMatrix *= 0.5;
            rMatrix += 0.5;
            rMatrix = rMatrix*2 - 1;
        
            //multiply the UVs by the rotation matrix
            UV.xy = mul(UV.xy, rMatrix);
            UV += Center;
        
            Out = UV;
        }
        
        void Unity_PolarCoordinates_float(float2 UV, float2 Center, float RadialScale, float LengthScale, out float2 Out)
        {
            float2 delta = UV - Center;
            float radius = length(delta) * 2 * RadialScale;
            float angle = atan2(delta.x, delta.y) * 1.0/6.28 * LengthScale;
            Out = float2(radius, angle);
        }
        
        void Unity_Rectangle_Fastest_float(float2 UV, float Width, float Height, out float Out)
        {
            float2 d = abs(UV * 2 - 1) - float2(Width, Height);
        #if defined(SHADER_STAGE_RAY_TRACING)
            d = saturate((1 - saturate(d * 1e7)));
        #else
            d = saturate(1 - d / fwidth(d));
        #endif
            Out = min(d.x, d.y);
        }
        
        void Unity_Rotate_Degrees_float(float2 UV, float2 Center, float Rotation, out float2 Out)
        {
            //rotation matrix
            Rotation = Rotation * (3.1415926f/180.0f);
            UV -= Center;
            float s = sin(Rotation);
            float c = cos(Rotation);
        
            //center rotation matrix
            float2x2 rMatrix = float2x2(c, -s, s, c);
            rMatrix *= 0.5;
            rMatrix += 0.5;
            rMatrix = rMatrix*2 - 1;
        
            //multiply the UVs by the rotation matrix
            UV.xy = mul(UV.xy, rMatrix);
            UV += Center;
        
            Out = UV;
        }
        
        void Unity_Preview_float(float In, out float Out)
        {
            Out = In;
        }
        
        void Unity_Saturate_float(float In, out float Out)
        {
            Out = saturate(In);
        }
        
        void Unity_Power_float(float A, float B, out float Out)
        {
            Out = pow(A, B);
        }
        
        void Unity_Smoothstep_float(float Edge1, float Edge2, float In, out float Out)
        {
            Out = smoothstep(Edge1, Edge2, In);
        }
        
        // Custom interpolators pre vertex
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreVertex' */
        
        // Graph Vertex
        struct VertexDescription
        {
            float3 Position;
            float3 Normal;
            float3 Tangent;
        };
        
        VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
        {
            VertexDescription description = (VertexDescription)0;
            description.Position = IN.ObjectSpacePosition;
            description.Normal = IN.ObjectSpaceNormal;
            description.Tangent = IN.ObjectSpaceTangent;
            return description;
        }
        
        // Custom interpolators, pre surface
        #ifdef FEATURES_GRAPH_VERTEX
        Varyings CustomInterpolatorPassThroughFunc(inout Varyings output, VertexDescription input)
        {
        return output;
        }
        #define CUSTOMINTERPOLATOR_VARYPASSTHROUGH_FUNC
        #endif
        
        // Graph Pixel
        struct SurfaceDescription
        {
            float Alpha;
        };
        
        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            float _Property_d94c15e3e2f84f569def2f6f4d8bbcab_Out_0 = _Speed;
            float _Multiply_ed2b4a35ed9147c59203777f0c7b2b2e_Out_2;
            Unity_Multiply_float_float(_Property_d94c15e3e2f84f569def2f6f4d8bbcab_Out_0, IN.TimeParameters.x, _Multiply_ed2b4a35ed9147c59203777f0c7b2b2e_Out_2);
            float _Property_f4a3c549f0874108876488320540244d_Out_0 = _SpinDir;
            float _Multiply_5f0c40c7364e4dcf81df67e0b9a85adb_Out_2;
            Unity_Multiply_float_float(_Property_f4a3c549f0874108876488320540244d_Out_0, -1, _Multiply_5f0c40c7364e4dcf81df67e0b9a85adb_Out_2);
            float _Multiply_7f7f94f247b24711bae1d438f6d4cdfa_Out_2;
            Unity_Multiply_float_float(_Multiply_ed2b4a35ed9147c59203777f0c7b2b2e_Out_2, _Multiply_5f0c40c7364e4dcf81df67e0b9a85adb_Out_2, _Multiply_7f7f94f247b24711bae1d438f6d4cdfa_Out_2);
            float2 _Rotate_35cdf476638c4ddfbfc5a472b4555a08_Out_3;
            Unity_Rotate_Radians_float(IN.uv0.xy, float2 (0.5, 0.5), _Multiply_7f7f94f247b24711bae1d438f6d4cdfa_Out_2, _Rotate_35cdf476638c4ddfbfc5a472b4555a08_Out_3);
            float2 _PolarCoordinates_137488258455411ea60aa4c0c170754f_Out_4;
            Unity_PolarCoordinates_float(_Rotate_35cdf476638c4ddfbfc5a472b4555a08_Out_3, float2 (0.5, 0.5), 0.7, 1.2, _PolarCoordinates_137488258455411ea60aa4c0c170754f_Out_4);
            float _Property_319ca09021c945c2857c1b7ea2a1fa6d_Out_0 = _Width;
            float _Rectangle_fa3b515096cf4847937839ee010b43e4_Out_3;
            Unity_Rectangle_Fastest_float(_PolarCoordinates_137488258455411ea60aa4c0c170754f_Out_4, _Property_319ca09021c945c2857c1b7ea2a1fa6d_Out_0, 3, _Rectangle_fa3b515096cf4847937839ee010b43e4_Out_3);
            float2 _Rotate_e221fbd58f8d426aa1a4907dd2ada1e1_Out_3;
            Unity_Rotate_Degrees_float(_Rotate_35cdf476638c4ddfbfc5a472b4555a08_Out_3, float2 (0.5, 0.5), -45, _Rotate_e221fbd58f8d426aa1a4907dd2ada1e1_Out_3);
            float _Property_860963b9226a4064a3c9487923211bfc_Out_0 = _TailLength;
            float2 _PolarCoordinates_dd40abfe6e5842c2b45c2a148178b7f6_Out_4;
            Unity_PolarCoordinates_float(_Rotate_e221fbd58f8d426aa1a4907dd2ada1e1_Out_3, float2 (0.5, 0.5), 1, _Property_860963b9226a4064a3c9487923211bfc_Out_0, _PolarCoordinates_dd40abfe6e5842c2b45c2a148178b7f6_Out_4);
            float _Split_e374f964ab2f4dcfb3b5d389cac488ee_R_1 = _PolarCoordinates_dd40abfe6e5842c2b45c2a148178b7f6_Out_4[0];
            float _Split_e374f964ab2f4dcfb3b5d389cac488ee_G_2 = _PolarCoordinates_dd40abfe6e5842c2b45c2a148178b7f6_Out_4[1];
            float _Split_e374f964ab2f4dcfb3b5d389cac488ee_B_3 = 0;
            float _Split_e374f964ab2f4dcfb3b5d389cac488ee_A_4 = 0;
            float _Preview_1959caf9d2b54c5a88bf965479f95f73_Out_1;
            Unity_Preview_float(_Split_e374f964ab2f4dcfb3b5d389cac488ee_G_2, _Preview_1959caf9d2b54c5a88bf965479f95f73_Out_1);
            float _Saturate_7a8a4ea4de02406dbafae645cfaa04ee_Out_1;
            Unity_Saturate_float(_Preview_1959caf9d2b54c5a88bf965479f95f73_Out_1, _Saturate_7a8a4ea4de02406dbafae645cfaa04ee_Out_1);
            float _Multiply_12051c8d973743848260fcea125d26ff_Out_2;
            Unity_Multiply_float_float(_Rectangle_fa3b515096cf4847937839ee010b43e4_Out_3, _Saturate_7a8a4ea4de02406dbafae645cfaa04ee_Out_1, _Multiply_12051c8d973743848260fcea125d26ff_Out_2);
            float _Power_40414304ee674ccdb6c4065358e09770_Out_2;
            Unity_Power_float(_Multiply_12051c8d973743848260fcea125d26ff_Out_2, 1.56, _Power_40414304ee674ccdb6c4065358e09770_Out_2);
            float _Smoothstep_91dc040ce0744d8880e840faeacb9a07_Out_3;
            Unity_Smoothstep_float(0.03, 0.35, _Power_40414304ee674ccdb6c4065358e09770_Out_2, _Smoothstep_91dc040ce0744d8880e840faeacb9a07_Out_3);
            surface.Alpha = _Smoothstep_91dc040ce0744d8880e840faeacb9a07_Out_3;
            return surface;
        }
        
        // --------------------------------------------------
        // Build Graph Inputs
        #ifdef HAVE_VFX_MODIFICATION
        #define VFX_SRP_ATTRIBUTES Attributes
        #define VFX_SRP_VARYINGS Varyings
        #define VFX_SRP_SURFACE_INPUTS SurfaceDescriptionInputs
        #endif
        VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
        {
            VertexDescriptionInputs output;
            ZERO_INITIALIZE(VertexDescriptionInputs, output);
        
            output.ObjectSpaceNormal =                          input.normalOS;
            output.ObjectSpaceTangent =                         input.tangentOS.xyz;
            output.ObjectSpacePosition =                        input.positionOS;
        
            return output;
        }
        SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
        {
            SurfaceDescriptionInputs output;
            ZERO_INITIALIZE(SurfaceDescriptionInputs, output);
        
        #ifdef HAVE_VFX_MODIFICATION
            // FragInputs from VFX come from two places: Interpolator or CBuffer.
            /* WARNING: $splice Could not find named fragment 'VFXSetFragInputs' */
        
        #endif
        
            
        
        
        
        
        
            output.uv0 = input.texCoord0;
            output.TimeParameters = _TimeParameters.xyz; // This is mainly for LW as HD overwrite this value
        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign =                    IS_FRONT_VFACE(input.cullFace, true, false);
        #else
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        #endif
        #undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        
                return output;
        }
        
        // --------------------------------------------------
        // Main
        
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/Varyings.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/SelectionPickingPass.hlsl"
        
        // --------------------------------------------------
        // Visual Effect Vertex Invocations
        #ifdef HAVE_VFX_MODIFICATION
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/VisualEffectVertex.hlsl"
        #endif
        
        ENDHLSL
        }
        Pass
        {
            Name "ScenePickingPass"
            Tags
            {
                "LightMode" = "Picking"
            }
        
        // Render State
        Cull Back
        
        // Debug
        // <None>
        
        // --------------------------------------------------
        // Pass
        
        HLSLPROGRAM
        
        // Pragmas
        #pragma target 2.0
        #pragma only_renderers gles gles3 glcore d3d11
        #pragma multi_compile_instancing
        #pragma vertex vert
        #pragma fragment frag
        
        // DotsInstancingOptions: <None>
        // HybridV1InjectedBuiltinProperties: <None>
        
        // Keywords
        // PassKeywords: <None>
        // GraphKeywords: <None>
        
        // Defines
        
        #define ATTRIBUTES_NEED_NORMAL
        #define ATTRIBUTES_NEED_TANGENT
        #define ATTRIBUTES_NEED_TEXCOORD0
        #define VARYINGS_NEED_TEXCOORD0
        #define FEATURES_GRAPH_VERTEX
        /* WARNING: $splice Could not find named fragment 'PassInstancing' */
        #define SHADERPASS SHADERPASS_DEPTHONLY
        #define SCENEPICKINGPASS 1
        #define ALPHA_CLIP_THRESHOLD 1
        /* WARNING: $splice Could not find named fragment 'DotsInstancingVars' */
        
        
        // custom interpolator pre-include
        /* WARNING: $splice Could not find named fragment 'sgci_CustomInterpolatorPreInclude' */
        
        // Includes
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
        
        // --------------------------------------------------
        // Structs and Packing
        
        // custom interpolators pre packing
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPrePacking' */
        
        struct Attributes
        {
             float3 positionOS : POSITION;
             float3 normalOS : NORMAL;
             float4 tangentOS : TANGENT;
             float4 uv0 : TEXCOORD0;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : INSTANCEID_SEMANTIC;
            #endif
        };
        struct Varyings
        {
             float4 positionCS : SV_POSITION;
             float4 texCoord0;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        struct SurfaceDescriptionInputs
        {
             float4 uv0;
             float3 TimeParameters;
        };
        struct VertexDescriptionInputs
        {
             float3 ObjectSpaceNormal;
             float3 ObjectSpaceTangent;
             float3 ObjectSpacePosition;
        };
        struct PackedVaryings
        {
             float4 positionCS : SV_POSITION;
             float4 interp0 : INTERP0;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        
        PackedVaryings PackVaryings (Varyings input)
        {
            PackedVaryings output;
            ZERO_INITIALIZE(PackedVaryings, output);
            output.positionCS = input.positionCS;
            output.interp0.xyzw =  input.texCoord0;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        Varyings UnpackVaryings (PackedVaryings input)
        {
            Varyings output;
            output.positionCS = input.positionCS;
            output.texCoord0 = input.interp0.xyzw;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        
        // --------------------------------------------------
        // Graph
        
        // Graph Properties
        CBUFFER_START(UnityPerMaterial)
        float _Speed;
        float _SpinDir;
        float _Width;
        float _TailLength;
        float4 _Color;
        CBUFFER_END
        
        // Object and Global properties
        
        // Graph Includes
        // GraphIncludes: <None>
        
        // -- Property used by ScenePickingPass
        #ifdef SCENEPICKINGPASS
        float4 _SelectionID;
        #endif
        
        // -- Properties used by SceneSelectionPass
        #ifdef SCENESELECTIONPASS
        int _ObjectId;
        int _PassValue;
        #endif
        
        // Graph Functions
        
        void Unity_Multiply_float_float(float A, float B, out float Out)
        {
            Out = A * B;
        }
        
        void Unity_Rotate_Radians_float(float2 UV, float2 Center, float Rotation, out float2 Out)
        {
            //rotation matrix
            UV -= Center;
            float s = sin(Rotation);
            float c = cos(Rotation);
        
            //center rotation matrix
            float2x2 rMatrix = float2x2(c, -s, s, c);
            rMatrix *= 0.5;
            rMatrix += 0.5;
            rMatrix = rMatrix*2 - 1;
        
            //multiply the UVs by the rotation matrix
            UV.xy = mul(UV.xy, rMatrix);
            UV += Center;
        
            Out = UV;
        }
        
        void Unity_PolarCoordinates_float(float2 UV, float2 Center, float RadialScale, float LengthScale, out float2 Out)
        {
            float2 delta = UV - Center;
            float radius = length(delta) * 2 * RadialScale;
            float angle = atan2(delta.x, delta.y) * 1.0/6.28 * LengthScale;
            Out = float2(radius, angle);
        }
        
        void Unity_Rectangle_Fastest_float(float2 UV, float Width, float Height, out float Out)
        {
            float2 d = abs(UV * 2 - 1) - float2(Width, Height);
        #if defined(SHADER_STAGE_RAY_TRACING)
            d = saturate((1 - saturate(d * 1e7)));
        #else
            d = saturate(1 - d / fwidth(d));
        #endif
            Out = min(d.x, d.y);
        }
        
        void Unity_Rotate_Degrees_float(float2 UV, float2 Center, float Rotation, out float2 Out)
        {
            //rotation matrix
            Rotation = Rotation * (3.1415926f/180.0f);
            UV -= Center;
            float s = sin(Rotation);
            float c = cos(Rotation);
        
            //center rotation matrix
            float2x2 rMatrix = float2x2(c, -s, s, c);
            rMatrix *= 0.5;
            rMatrix += 0.5;
            rMatrix = rMatrix*2 - 1;
        
            //multiply the UVs by the rotation matrix
            UV.xy = mul(UV.xy, rMatrix);
            UV += Center;
        
            Out = UV;
        }
        
        void Unity_Preview_float(float In, out float Out)
        {
            Out = In;
        }
        
        void Unity_Saturate_float(float In, out float Out)
        {
            Out = saturate(In);
        }
        
        void Unity_Power_float(float A, float B, out float Out)
        {
            Out = pow(A, B);
        }
        
        void Unity_Smoothstep_float(float Edge1, float Edge2, float In, out float Out)
        {
            Out = smoothstep(Edge1, Edge2, In);
        }
        
        // Custom interpolators pre vertex
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreVertex' */
        
        // Graph Vertex
        struct VertexDescription
        {
            float3 Position;
            float3 Normal;
            float3 Tangent;
        };
        
        VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
        {
            VertexDescription description = (VertexDescription)0;
            description.Position = IN.ObjectSpacePosition;
            description.Normal = IN.ObjectSpaceNormal;
            description.Tangent = IN.ObjectSpaceTangent;
            return description;
        }
        
        // Custom interpolators, pre surface
        #ifdef FEATURES_GRAPH_VERTEX
        Varyings CustomInterpolatorPassThroughFunc(inout Varyings output, VertexDescription input)
        {
        return output;
        }
        #define CUSTOMINTERPOLATOR_VARYPASSTHROUGH_FUNC
        #endif
        
        // Graph Pixel
        struct SurfaceDescription
        {
            float Alpha;
        };
        
        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            float _Property_d94c15e3e2f84f569def2f6f4d8bbcab_Out_0 = _Speed;
            float _Multiply_ed2b4a35ed9147c59203777f0c7b2b2e_Out_2;
            Unity_Multiply_float_float(_Property_d94c15e3e2f84f569def2f6f4d8bbcab_Out_0, IN.TimeParameters.x, _Multiply_ed2b4a35ed9147c59203777f0c7b2b2e_Out_2);
            float _Property_f4a3c549f0874108876488320540244d_Out_0 = _SpinDir;
            float _Multiply_5f0c40c7364e4dcf81df67e0b9a85adb_Out_2;
            Unity_Multiply_float_float(_Property_f4a3c549f0874108876488320540244d_Out_0, -1, _Multiply_5f0c40c7364e4dcf81df67e0b9a85adb_Out_2);
            float _Multiply_7f7f94f247b24711bae1d438f6d4cdfa_Out_2;
            Unity_Multiply_float_float(_Multiply_ed2b4a35ed9147c59203777f0c7b2b2e_Out_2, _Multiply_5f0c40c7364e4dcf81df67e0b9a85adb_Out_2, _Multiply_7f7f94f247b24711bae1d438f6d4cdfa_Out_2);
            float2 _Rotate_35cdf476638c4ddfbfc5a472b4555a08_Out_3;
            Unity_Rotate_Radians_float(IN.uv0.xy, float2 (0.5, 0.5), _Multiply_7f7f94f247b24711bae1d438f6d4cdfa_Out_2, _Rotate_35cdf476638c4ddfbfc5a472b4555a08_Out_3);
            float2 _PolarCoordinates_137488258455411ea60aa4c0c170754f_Out_4;
            Unity_PolarCoordinates_float(_Rotate_35cdf476638c4ddfbfc5a472b4555a08_Out_3, float2 (0.5, 0.5), 0.7, 1.2, _PolarCoordinates_137488258455411ea60aa4c0c170754f_Out_4);
            float _Property_319ca09021c945c2857c1b7ea2a1fa6d_Out_0 = _Width;
            float _Rectangle_fa3b515096cf4847937839ee010b43e4_Out_3;
            Unity_Rectangle_Fastest_float(_PolarCoordinates_137488258455411ea60aa4c0c170754f_Out_4, _Property_319ca09021c945c2857c1b7ea2a1fa6d_Out_0, 3, _Rectangle_fa3b515096cf4847937839ee010b43e4_Out_3);
            float2 _Rotate_e221fbd58f8d426aa1a4907dd2ada1e1_Out_3;
            Unity_Rotate_Degrees_float(_Rotate_35cdf476638c4ddfbfc5a472b4555a08_Out_3, float2 (0.5, 0.5), -45, _Rotate_e221fbd58f8d426aa1a4907dd2ada1e1_Out_3);
            float _Property_860963b9226a4064a3c9487923211bfc_Out_0 = _TailLength;
            float2 _PolarCoordinates_dd40abfe6e5842c2b45c2a148178b7f6_Out_4;
            Unity_PolarCoordinates_float(_Rotate_e221fbd58f8d426aa1a4907dd2ada1e1_Out_3, float2 (0.5, 0.5), 1, _Property_860963b9226a4064a3c9487923211bfc_Out_0, _PolarCoordinates_dd40abfe6e5842c2b45c2a148178b7f6_Out_4);
            float _Split_e374f964ab2f4dcfb3b5d389cac488ee_R_1 = _PolarCoordinates_dd40abfe6e5842c2b45c2a148178b7f6_Out_4[0];
            float _Split_e374f964ab2f4dcfb3b5d389cac488ee_G_2 = _PolarCoordinates_dd40abfe6e5842c2b45c2a148178b7f6_Out_4[1];
            float _Split_e374f964ab2f4dcfb3b5d389cac488ee_B_3 = 0;
            float _Split_e374f964ab2f4dcfb3b5d389cac488ee_A_4 = 0;
            float _Preview_1959caf9d2b54c5a88bf965479f95f73_Out_1;
            Unity_Preview_float(_Split_e374f964ab2f4dcfb3b5d389cac488ee_G_2, _Preview_1959caf9d2b54c5a88bf965479f95f73_Out_1);
            float _Saturate_7a8a4ea4de02406dbafae645cfaa04ee_Out_1;
            Unity_Saturate_float(_Preview_1959caf9d2b54c5a88bf965479f95f73_Out_1, _Saturate_7a8a4ea4de02406dbafae645cfaa04ee_Out_1);
            float _Multiply_12051c8d973743848260fcea125d26ff_Out_2;
            Unity_Multiply_float_float(_Rectangle_fa3b515096cf4847937839ee010b43e4_Out_3, _Saturate_7a8a4ea4de02406dbafae645cfaa04ee_Out_1, _Multiply_12051c8d973743848260fcea125d26ff_Out_2);
            float _Power_40414304ee674ccdb6c4065358e09770_Out_2;
            Unity_Power_float(_Multiply_12051c8d973743848260fcea125d26ff_Out_2, 1.56, _Power_40414304ee674ccdb6c4065358e09770_Out_2);
            float _Smoothstep_91dc040ce0744d8880e840faeacb9a07_Out_3;
            Unity_Smoothstep_float(0.03, 0.35, _Power_40414304ee674ccdb6c4065358e09770_Out_2, _Smoothstep_91dc040ce0744d8880e840faeacb9a07_Out_3);
            surface.Alpha = _Smoothstep_91dc040ce0744d8880e840faeacb9a07_Out_3;
            return surface;
        }
        
        // --------------------------------------------------
        // Build Graph Inputs
        #ifdef HAVE_VFX_MODIFICATION
        #define VFX_SRP_ATTRIBUTES Attributes
        #define VFX_SRP_VARYINGS Varyings
        #define VFX_SRP_SURFACE_INPUTS SurfaceDescriptionInputs
        #endif
        VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
        {
            VertexDescriptionInputs output;
            ZERO_INITIALIZE(VertexDescriptionInputs, output);
        
            output.ObjectSpaceNormal =                          input.normalOS;
            output.ObjectSpaceTangent =                         input.tangentOS.xyz;
            output.ObjectSpacePosition =                        input.positionOS;
        
            return output;
        }
        SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
        {
            SurfaceDescriptionInputs output;
            ZERO_INITIALIZE(SurfaceDescriptionInputs, output);
        
        #ifdef HAVE_VFX_MODIFICATION
            // FragInputs from VFX come from two places: Interpolator or CBuffer.
            /* WARNING: $splice Could not find named fragment 'VFXSetFragInputs' */
        
        #endif
        
            
        
        
        
        
        
            output.uv0 = input.texCoord0;
            output.TimeParameters = _TimeParameters.xyz; // This is mainly for LW as HD overwrite this value
        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign =                    IS_FRONT_VFACE(input.cullFace, true, false);
        #else
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        #endif
        #undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        
                return output;
        }
        
        // --------------------------------------------------
        // Main
        
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/Varyings.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/SelectionPickingPass.hlsl"
        
        // --------------------------------------------------
        // Visual Effect Vertex Invocations
        #ifdef HAVE_VFX_MODIFICATION
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/VisualEffectVertex.hlsl"
        #endif
        
        ENDHLSL
        }
        Pass
        {
            Name "DepthNormals"
            Tags
            {
                "LightMode" = "DepthNormalsOnly"
            }
        
        // Render State
        Cull Back
        ZTest LEqual
        ZWrite On
        
        // Debug
        // <None>
        
        // --------------------------------------------------
        // Pass
        
        HLSLPROGRAM
        
        // Pragmas
        #pragma target 2.0
        #pragma only_renderers gles gles3 glcore d3d11
        #pragma multi_compile_instancing
        #pragma multi_compile_fog
        #pragma instancing_options renderinglayer
        #pragma vertex vert
        #pragma fragment frag
        
        // DotsInstancingOptions: <None>
        // HybridV1InjectedBuiltinProperties: <None>
        
        // Keywords
        // PassKeywords: <None>
        // GraphKeywords: <None>
        
        // Defines
        
        #define ATTRIBUTES_NEED_NORMAL
        #define ATTRIBUTES_NEED_TANGENT
        #define ATTRIBUTES_NEED_TEXCOORD0
        #define VARYINGS_NEED_NORMAL_WS
        #define VARYINGS_NEED_TEXCOORD0
        #define FEATURES_GRAPH_VERTEX
        /* WARNING: $splice Could not find named fragment 'PassInstancing' */
        #define SHADERPASS SHADERPASS_DEPTHNORMALSONLY
        #define _SURFACE_TYPE_TRANSPARENT 1
        /* WARNING: $splice Could not find named fragment 'DotsInstancingVars' */
        
        
        // custom interpolator pre-include
        /* WARNING: $splice Could not find named fragment 'sgci_CustomInterpolatorPreInclude' */
        
        // Includes
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
        
        // --------------------------------------------------
        // Structs and Packing
        
        // custom interpolators pre packing
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPrePacking' */
        
        struct Attributes
        {
             float3 positionOS : POSITION;
             float3 normalOS : NORMAL;
             float4 tangentOS : TANGENT;
             float4 uv0 : TEXCOORD0;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : INSTANCEID_SEMANTIC;
            #endif
        };
        struct Varyings
        {
             float4 positionCS : SV_POSITION;
             float3 normalWS;
             float4 texCoord0;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        struct SurfaceDescriptionInputs
        {
             float4 uv0;
             float3 TimeParameters;
        };
        struct VertexDescriptionInputs
        {
             float3 ObjectSpaceNormal;
             float3 ObjectSpaceTangent;
             float3 ObjectSpacePosition;
        };
        struct PackedVaryings
        {
             float4 positionCS : SV_POSITION;
             float3 interp0 : INTERP0;
             float4 interp1 : INTERP1;
            #if UNITY_ANY_INSTANCING_ENABLED
             uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
             uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
             uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
             FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        
        PackedVaryings PackVaryings (Varyings input)
        {
            PackedVaryings output;
            ZERO_INITIALIZE(PackedVaryings, output);
            output.positionCS = input.positionCS;
            output.interp0.xyz =  input.normalWS;
            output.interp1.xyzw =  input.texCoord0;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        Varyings UnpackVaryings (PackedVaryings input)
        {
            Varyings output;
            output.positionCS = input.positionCS;
            output.normalWS = input.interp0.xyz;
            output.texCoord0 = input.interp1.xyzw;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        
        
        // --------------------------------------------------
        // Graph
        
        // Graph Properties
        CBUFFER_START(UnityPerMaterial)
        float _Speed;
        float _SpinDir;
        float _Width;
        float _TailLength;
        float4 _Color;
        CBUFFER_END
        
        // Object and Global properties
        
        // Graph Includes
        // GraphIncludes: <None>
        
        // -- Property used by ScenePickingPass
        #ifdef SCENEPICKINGPASS
        float4 _SelectionID;
        #endif
        
        // -- Properties used by SceneSelectionPass
        #ifdef SCENESELECTIONPASS
        int _ObjectId;
        int _PassValue;
        #endif
        
        // Graph Functions
        
        void Unity_Multiply_float_float(float A, float B, out float Out)
        {
            Out = A * B;
        }
        
        void Unity_Rotate_Radians_float(float2 UV, float2 Center, float Rotation, out float2 Out)
        {
            //rotation matrix
            UV -= Center;
            float s = sin(Rotation);
            float c = cos(Rotation);
        
            //center rotation matrix
            float2x2 rMatrix = float2x2(c, -s, s, c);
            rMatrix *= 0.5;
            rMatrix += 0.5;
            rMatrix = rMatrix*2 - 1;
        
            //multiply the UVs by the rotation matrix
            UV.xy = mul(UV.xy, rMatrix);
            UV += Center;
        
            Out = UV;
        }
        
        void Unity_PolarCoordinates_float(float2 UV, float2 Center, float RadialScale, float LengthScale, out float2 Out)
        {
            float2 delta = UV - Center;
            float radius = length(delta) * 2 * RadialScale;
            float angle = atan2(delta.x, delta.y) * 1.0/6.28 * LengthScale;
            Out = float2(radius, angle);
        }
        
        void Unity_Rectangle_Fastest_float(float2 UV, float Width, float Height, out float Out)
        {
            float2 d = abs(UV * 2 - 1) - float2(Width, Height);
        #if defined(SHADER_STAGE_RAY_TRACING)
            d = saturate((1 - saturate(d * 1e7)));
        #else
            d = saturate(1 - d / fwidth(d));
        #endif
            Out = min(d.x, d.y);
        }
        
        void Unity_Rotate_Degrees_float(float2 UV, float2 Center, float Rotation, out float2 Out)
        {
            //rotation matrix
            Rotation = Rotation * (3.1415926f/180.0f);
            UV -= Center;
            float s = sin(Rotation);
            float c = cos(Rotation);
        
            //center rotation matrix
            float2x2 rMatrix = float2x2(c, -s, s, c);
            rMatrix *= 0.5;
            rMatrix += 0.5;
            rMatrix = rMatrix*2 - 1;
        
            //multiply the UVs by the rotation matrix
            UV.xy = mul(UV.xy, rMatrix);
            UV += Center;
        
            Out = UV;
        }
        
        void Unity_Preview_float(float In, out float Out)
        {
            Out = In;
        }
        
        void Unity_Saturate_float(float In, out float Out)
        {
            Out = saturate(In);
        }
        
        void Unity_Power_float(float A, float B, out float Out)
        {
            Out = pow(A, B);
        }
        
        void Unity_Smoothstep_float(float Edge1, float Edge2, float In, out float Out)
        {
            Out = smoothstep(Edge1, Edge2, In);
        }
        
        // Custom interpolators pre vertex
        /* WARNING: $splice Could not find named fragment 'CustomInterpolatorPreVertex' */
        
        // Graph Vertex
        struct VertexDescription
        {
            float3 Position;
            float3 Normal;
            float3 Tangent;
        };
        
        VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
        {
            VertexDescription description = (VertexDescription)0;
            description.Position = IN.ObjectSpacePosition;
            description.Normal = IN.ObjectSpaceNormal;
            description.Tangent = IN.ObjectSpaceTangent;
            return description;
        }
        
        // Custom interpolators, pre surface
        #ifdef FEATURES_GRAPH_VERTEX
        Varyings CustomInterpolatorPassThroughFunc(inout Varyings output, VertexDescription input)
        {
        return output;
        }
        #define CUSTOMINTERPOLATOR_VARYPASSTHROUGH_FUNC
        #endif
        
        // Graph Pixel
        struct SurfaceDescription
        {
            float Alpha;
        };
        
        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            float _Property_d94c15e3e2f84f569def2f6f4d8bbcab_Out_0 = _Speed;
            float _Multiply_ed2b4a35ed9147c59203777f0c7b2b2e_Out_2;
            Unity_Multiply_float_float(_Property_d94c15e3e2f84f569def2f6f4d8bbcab_Out_0, IN.TimeParameters.x, _Multiply_ed2b4a35ed9147c59203777f0c7b2b2e_Out_2);
            float _Property_f4a3c549f0874108876488320540244d_Out_0 = _SpinDir;
            float _Multiply_5f0c40c7364e4dcf81df67e0b9a85adb_Out_2;
            Unity_Multiply_float_float(_Property_f4a3c549f0874108876488320540244d_Out_0, -1, _Multiply_5f0c40c7364e4dcf81df67e0b9a85adb_Out_2);
            float _Multiply_7f7f94f247b24711bae1d438f6d4cdfa_Out_2;
            Unity_Multiply_float_float(_Multiply_ed2b4a35ed9147c59203777f0c7b2b2e_Out_2, _Multiply_5f0c40c7364e4dcf81df67e0b9a85adb_Out_2, _Multiply_7f7f94f247b24711bae1d438f6d4cdfa_Out_2);
            float2 _Rotate_35cdf476638c4ddfbfc5a472b4555a08_Out_3;
            Unity_Rotate_Radians_float(IN.uv0.xy, float2 (0.5, 0.5), _Multiply_7f7f94f247b24711bae1d438f6d4cdfa_Out_2, _Rotate_35cdf476638c4ddfbfc5a472b4555a08_Out_3);
            float2 _PolarCoordinates_137488258455411ea60aa4c0c170754f_Out_4;
            Unity_PolarCoordinates_float(_Rotate_35cdf476638c4ddfbfc5a472b4555a08_Out_3, float2 (0.5, 0.5), 0.7, 1.2, _PolarCoordinates_137488258455411ea60aa4c0c170754f_Out_4);
            float _Property_319ca09021c945c2857c1b7ea2a1fa6d_Out_0 = _Width;
            float _Rectangle_fa3b515096cf4847937839ee010b43e4_Out_3;
            Unity_Rectangle_Fastest_float(_PolarCoordinates_137488258455411ea60aa4c0c170754f_Out_4, _Property_319ca09021c945c2857c1b7ea2a1fa6d_Out_0, 3, _Rectangle_fa3b515096cf4847937839ee010b43e4_Out_3);
            float2 _Rotate_e221fbd58f8d426aa1a4907dd2ada1e1_Out_3;
            Unity_Rotate_Degrees_float(_Rotate_35cdf476638c4ddfbfc5a472b4555a08_Out_3, float2 (0.5, 0.5), -45, _Rotate_e221fbd58f8d426aa1a4907dd2ada1e1_Out_3);
            float _Property_860963b9226a4064a3c9487923211bfc_Out_0 = _TailLength;
            float2 _PolarCoordinates_dd40abfe6e5842c2b45c2a148178b7f6_Out_4;
            Unity_PolarCoordinates_float(_Rotate_e221fbd58f8d426aa1a4907dd2ada1e1_Out_3, float2 (0.5, 0.5), 1, _Property_860963b9226a4064a3c9487923211bfc_Out_0, _PolarCoordinates_dd40abfe6e5842c2b45c2a148178b7f6_Out_4);
            float _Split_e374f964ab2f4dcfb3b5d389cac488ee_R_1 = _PolarCoordinates_dd40abfe6e5842c2b45c2a148178b7f6_Out_4[0];
            float _Split_e374f964ab2f4dcfb3b5d389cac488ee_G_2 = _PolarCoordinates_dd40abfe6e5842c2b45c2a148178b7f6_Out_4[1];
            float _Split_e374f964ab2f4dcfb3b5d389cac488ee_B_3 = 0;
            float _Split_e374f964ab2f4dcfb3b5d389cac488ee_A_4 = 0;
            float _Preview_1959caf9d2b54c5a88bf965479f95f73_Out_1;
            Unity_Preview_float(_Split_e374f964ab2f4dcfb3b5d389cac488ee_G_2, _Preview_1959caf9d2b54c5a88bf965479f95f73_Out_1);
            float _Saturate_7a8a4ea4de02406dbafae645cfaa04ee_Out_1;
            Unity_Saturate_float(_Preview_1959caf9d2b54c5a88bf965479f95f73_Out_1, _Saturate_7a8a4ea4de02406dbafae645cfaa04ee_Out_1);
            float _Multiply_12051c8d973743848260fcea125d26ff_Out_2;
            Unity_Multiply_float_float(_Rectangle_fa3b515096cf4847937839ee010b43e4_Out_3, _Saturate_7a8a4ea4de02406dbafae645cfaa04ee_Out_1, _Multiply_12051c8d973743848260fcea125d26ff_Out_2);
            float _Power_40414304ee674ccdb6c4065358e09770_Out_2;
            Unity_Power_float(_Multiply_12051c8d973743848260fcea125d26ff_Out_2, 1.56, _Power_40414304ee674ccdb6c4065358e09770_Out_2);
            float _Smoothstep_91dc040ce0744d8880e840faeacb9a07_Out_3;
            Unity_Smoothstep_float(0.03, 0.35, _Power_40414304ee674ccdb6c4065358e09770_Out_2, _Smoothstep_91dc040ce0744d8880e840faeacb9a07_Out_3);
            surface.Alpha = _Smoothstep_91dc040ce0744d8880e840faeacb9a07_Out_3;
            return surface;
        }
        
        // --------------------------------------------------
        // Build Graph Inputs
        #ifdef HAVE_VFX_MODIFICATION
        #define VFX_SRP_ATTRIBUTES Attributes
        #define VFX_SRP_VARYINGS Varyings
        #define VFX_SRP_SURFACE_INPUTS SurfaceDescriptionInputs
        #endif
        VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
        {
            VertexDescriptionInputs output;
            ZERO_INITIALIZE(VertexDescriptionInputs, output);
        
            output.ObjectSpaceNormal =                          input.normalOS;
            output.ObjectSpaceTangent =                         input.tangentOS.xyz;
            output.ObjectSpacePosition =                        input.positionOS;
        
            return output;
        }
        SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
        {
            SurfaceDescriptionInputs output;
            ZERO_INITIALIZE(SurfaceDescriptionInputs, output);
        
        #ifdef HAVE_VFX_MODIFICATION
            // FragInputs from VFX come from two places: Interpolator or CBuffer.
            /* WARNING: $splice Could not find named fragment 'VFXSetFragInputs' */
        
        #endif
        
            
        
        
        
        
        
            output.uv0 = input.texCoord0;
            output.TimeParameters = _TimeParameters.xyz; // This is mainly for LW as HD overwrite this value
        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign =                    IS_FRONT_VFACE(input.cullFace, true, false);
        #else
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        #endif
        #undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        
                return output;
        }
        
        // --------------------------------------------------
        // Main
        
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/Varyings.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/DepthNormalsOnlyPass.hlsl"
        
        // --------------------------------------------------
        // Visual Effect Vertex Invocations
        #ifdef HAVE_VFX_MODIFICATION
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/VisualEffectVertex.hlsl"
        #endif
        
        ENDHLSL
        }
    }
    CustomEditorForRenderPipeline "UnityEditor.ShaderGraphUnlitGUI" "UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset"
    CustomEditor "UnityEditor.ShaderGraph.GenericShaderGraphMaterialGUI"
    FallBack "Hidden/Shader Graph/FallbackError"
}