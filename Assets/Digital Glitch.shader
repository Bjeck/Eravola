// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:1,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:True,enco:False,rmgx:True,imps:False,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:1,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:6,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:False,igpj:True,qofs:1,qpre:4,rntp:5,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:2865,x:32885,y:33346,varname:node_2865,prsc:2|emission-6627-OUT;n:type:ShaderForge.SFN_TexCoord,id:4219,x:31806,y:33213,cmnt:Default coordinates,varname:node_4219,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Relay,id:8397,x:32136,y:33203,cmnt:Refract here,varname:node_8397,prsc:2|IN-4219-UVOUT;n:type:ShaderForge.SFN_Relay,id:4676,x:32523,y:33354,cmnt:Modify color here,varname:node_4676,prsc:2|IN-7542-RGB;n:type:ShaderForge.SFN_Tex2dAsset,id:4430,x:31864,y:33433,ptovrint:False,ptlb:MainTex,ptin:_MainTex,cmnt:MainTex contains the color of the scene,varname:node_9933,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:7542,x:32238,y:33400,varname:node_1672,prsc:2,ntxv:0,isnm:False|UVIN-8397-OUT,TEX-4430-TEX;n:type:ShaderForge.SFN_Tex2d,id:8114,x:31969,y:33750,ptovrint:False,ptlb:Other Visual,ptin:_OtherVisual,varname:node_8114,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:fb5311bc7779fdc448d359e342c455a8,ntxv:0,isnm:False|UVIN-440-UVOUT;n:type:ShaderForge.SFN_Multiply,id:5772,x:32493,y:33481,varname:node_5772,prsc:2|A-7542-RGB,B-8114-RGB;n:type:ShaderForge.SFN_Add,id:6523,x:32327,y:33831,varname:node_6523,prsc:2|A-8114-RGB,B-440-UVOUT;n:type:ShaderForge.SFN_Panner,id:440,x:31723,y:34165,varname:node_440,prsc:2,spu:0,spv:1|UVIN-1055-UVOUT,DIST-9904-OUT;n:type:ShaderForge.SFN_TexCoord,id:1055,x:31391,y:34111,varname:node_1055,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_ValueProperty,id:9904,x:31562,y:34417,ptovrint:False,ptlb:node_9904,ptin:_node_9904,varname:node_9904,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Multiply,id:6627,x:32686,y:33481,varname:node_6627,prsc:2|A-5772-OUT,B-8664-OUT;n:type:ShaderForge.SFN_Vector1,id:8664,x:32525,y:33748,varname:node_8664,prsc:2,v1:5;n:type:ShaderForge.SFN_Time,id:4834,x:31346,y:34354,varname:node_4834,prsc:2;n:type:ShaderForge.SFN_Multiply,id:2562,x:32683,y:33886,cmnt:Can use this for opacity instead,varname:node_2562,prsc:2|A-8114-B,B-7735-OUT;n:type:ShaderForge.SFN_ValueProperty,id:7735,x:32514,y:33979,ptovrint:False,ptlb:node_7735,ptin:_node_7735,varname:node_7735,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2;n:type:ShaderForge.SFN_Add,id:7405,x:32481,y:33621,varname:node_7405,prsc:2|A-7542-RGB,B-8114-RGB;proporder:4430-8114-9904-7735;pass:END;sub:END;*/

Shader "Shader Forge/Digital Glitch" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _OtherVisual ("Other Visual", 2D) = "white" {}
        _node_9904 ("node_9904", Float ) = 0
        _node_7735 ("node_7735", Float ) = 2
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Overlay+1"
            "RenderType"="Overlay"
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
            #pragma multi_compile_fwdbase
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _OtherVisual; uniform float4 _OtherVisual_ST;
            uniform float _node_9904;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float2 node_8397 = i.uv0; // Refract here
                float4 node_1672 = tex2D(_MainTex,TRANSFORM_TEX(node_8397, _MainTex));
                float2 node_440 = (i.uv0+_node_9904*float2(0,1));
                float4 _OtherVisual_var = tex2D(_OtherVisual,TRANSFORM_TEX(node_440, _OtherVisual));
                float3 emissive = ((node_1672.rgb*_OtherVisual_var.rgb)*5.0);
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
