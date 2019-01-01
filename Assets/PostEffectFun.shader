// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:3,spmd:1,trmd:1,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:True,enco:False,rmgx:True,imps:False,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:1,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:6,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:1,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:2865,x:32740,y:33254,varname:node_2865,prsc:2|emission-7542-RGB;n:type:ShaderForge.SFN_TexCoord,id:4219,x:31941,y:33166,cmnt:Default coordinates,varname:node_4219,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Tex2dAsset,id:4430,x:32279,y:33861,ptovrint:False,ptlb:MainTex,ptin:_MainTex,cmnt:MainTex contains the color of the scene,varname:node_9933,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:7542,x:32411,y:33361,varname:node_1672,prsc:2,ntxv:0,isnm:False|UVIN-8719-OUT,TEX-4430-TEX;n:type:ShaderForge.SFN_Multiply,id:3226,x:32569,y:33520,varname:node_3226,prsc:2|B-1534-RGB;n:type:ShaderForge.SFN_Color,id:1534,x:32495,y:33760,ptovrint:False,ptlb:node_1534,ptin:_node_1534,varname:node_1534,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.4280883,c2:0.4400521,c3:0.490566,c4:1;n:type:ShaderForge.SFN_Tex2d,id:8626,x:31396,y:33494,ptovrint:False,ptlb:FlowDisplacement,ptin:_FlowDisplacement,varname:node_8626,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:7bbfb8818476e4641ba3e75f5225eb69,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Add,id:8719,x:32201,y:33314,varname:node_8719,prsc:2|A-4219-UVOUT,B-9511-OUT;n:type:ShaderForge.SFN_ComponentMask,id:2260,x:31562,y:33494,varname:node_2260,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-8626-RGB;n:type:ShaderForge.SFN_Multiply,id:2784,x:31775,y:33454,varname:node_2784,prsc:2|A-2260-OUT,B-6158-OUT;n:type:ShaderForge.SFN_ValueProperty,id:6158,x:31534,y:33695,ptovrint:False,ptlb:The Number 2,ptin:_TheNumber2,varname:node_6158,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2;n:type:ShaderForge.SFN_Subtract,id:7829,x:31934,y:33502,varname:node_7829,prsc:2|A-2784-OUT,B-4213-OUT;n:type:ShaderForge.SFN_ValueProperty,id:4213,x:31747,y:33749,ptovrint:False,ptlb:The Number 1,ptin:_TheNumber1,varname:node_4213,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:9511,x:32048,y:33686,varname:node_9511,prsc:2|A-7829-OUT,B-7259-OUT;n:type:ShaderForge.SFN_ValueProperty,id:7259,x:31853,y:33836,ptovrint:False,ptlb:Scalar,ptin:_Scalar,varname:node_7259,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.25;proporder:4430-1534-8626-6158-4213-7259;pass:END;sub:END;*/

Shader "Shader Forge/PostEffectFun" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _node_1534 ("node_1534", Color) = (0.4280883,0.4400521,0.490566,1)
        _FlowDisplacement ("FlowDisplacement", 2D) = "white" {}
        _TheNumber2 ("The Number 2", Float ) = 2
        _TheNumber1 ("The Number 1", Float ) = 1
        _Scalar ("Scalar", Float ) = 0.25
    }
    SubShader {
        Tags {
            "Queue"="Geometry+1"
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            ZTest Always
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _FlowDisplacement; uniform float4 _FlowDisplacement_ST;
            uniform float _TheNumber2;
            uniform float _TheNumber1;
            uniform float _Scalar;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
////// Lighting:
////// Emissive:
                float4 _FlowDisplacement_var = tex2D(_FlowDisplacement,TRANSFORM_TEX(i.uv0, _FlowDisplacement));
                float2 node_8719 = (i.uv0+(((_FlowDisplacement_var.rgb.rg*_TheNumber2)-_TheNumber1)*_Scalar));
                float4 node_1672 = tex2D(_MainTex,TRANSFORM_TEX(node_8719, _MainTex));
                float3 emissive = node_1672.rgb;
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
