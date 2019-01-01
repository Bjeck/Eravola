// Shader created with Shader Forge v1.38 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:1,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:True,enco:False,rmgx:True,imps:False,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:1,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:6,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:False,igpj:True,qofs:1,qpre:4,rntp:5,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:2865,x:34982,y:32961,varname:node_2865,prsc:2|emission-2882-OUT;n:type:ShaderForge.SFN_TexCoord,id:4219,x:31116,y:33394,cmnt:Default coordinates,varname:node_4219,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Tex2dAsset,id:4430,x:34262,y:33143,ptovrint:False,ptlb:MainTex,ptin:_MainTex,cmnt:MainTex contains the color of the scene,varname:node_9933,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:7542,x:34434,y:33102,varname:node_1672,prsc:2,ntxv:0,isnm:False|TEX-4430-TEX;n:type:ShaderForge.SFN_Color,id:1822,x:31340,y:33086,ptovrint:False,ptlb:node_1822,ptin:_node_1822,varname:node_1822,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.9245283,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_Color,id:1942,x:31398,y:32868,ptovrint:False,ptlb:node_1942,ptin:_node_1942,varname:node_1942,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.2122642,c2:0.5141964,c3:1,c4:1;n:type:ShaderForge.SFN_Lerp,id:8074,x:31799,y:33079,varname:node_8074,prsc:2|A-1942-RGB,B-1822-RGB,T-6059-OUT;n:type:ShaderForge.SFN_ComponentMask,id:317,x:31544,y:33599,varname:node_317,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-4219-UVOUT;n:type:ShaderForge.SFN_Length,id:6059,x:31720,y:33840,varname:node_6059,prsc:2|IN-6474-OUT;n:type:ShaderForge.SFN_RemapRange,id:6474,x:31434,y:33827,varname:node_6474,prsc:2,frmn:0,frmx:1,tomn:-1,tomx:1|IN-4219-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:9465,x:31976,y:32486,varname:node_9465,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_ComponentMask,id:6738,x:32162,y:32486,varname:node_6738,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-9465-UVOUT;n:type:ShaderForge.SFN_Lerp,id:2308,x:32876,y:32400,varname:node_2308,prsc:2|A-1347-RGB,B-762-RGB,T-6896-OUT;n:type:ShaderForge.SFN_Color,id:1347,x:32602,y:32139,ptovrint:False,ptlb:node_1347,ptin:_node_1347,varname:node_1347,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0.9676377,c3:0,c4:1;n:type:ShaderForge.SFN_Color,id:762,x:32465,y:32290,ptovrint:False,ptlb:node_762,ptin:_node_762,varname:node_762,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_Slider,id:306,x:32040,y:32900,ptovrint:False,ptlb:node_306,ptin:_node_306,varname:node_306,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:4,max:9;n:type:ShaderForge.SFN_Clamp01,id:6896,x:32731,y:32443,varname:node_6896,prsc:2|IN-2024-OUT;n:type:ShaderForge.SFN_Sin,id:3375,x:32751,y:32639,varname:node_3375,prsc:2|IN-1350-OUT;n:type:ShaderForge.SFN_Multiply,id:1350,x:32560,y:32639,varname:node_1350,prsc:2|A-306-OUT,B-945-OUT,C-8784-OUT;n:type:ShaderForge.SFN_ValueProperty,id:483,x:32923,y:31935,ptovrint:False,ptlb:node_483,ptin:_node_483,varname:node_483,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:9;n:type:ShaderForge.SFN_RemapRange,id:2024,x:32575,y:32443,varname:node_2024,prsc:2,frmn:-1,frmx:1,tomn:0,tomx:1|IN-3375-OUT;n:type:ShaderForge.SFN_Tau,id:6989,x:32484,y:32986,varname:node_6989,prsc:2;n:type:ShaderForge.SFN_Pi,id:8752,x:32523,y:33114,varname:node_8752,prsc:2;n:type:ShaderForge.SFN_ValueProperty,id:8784,x:32423,y:32878,ptovrint:False,ptlb:node_8784,ptin:_node_8784,varname:node_8784,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:6.28318;n:type:ShaderForge.SFN_Time,id:2872,x:32119,y:32682,varname:node_2872,prsc:2;n:type:ShaderForge.SFN_Add,id:945,x:32376,y:32607,varname:node_945,prsc:2|A-6738-OUT,B-2872-TSL;n:type:ShaderForge.SFN_TexCoord,id:3497,x:33530,y:31967,varname:node_3497,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Tex2dAsset,id:445,x:35175,y:32613,ptovrint:False,ptlb:Ramp,ptin:_Ramp,varname:node_445,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:271f5ee3273dd7f4fae6e204d4f8c4bf,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:4079,x:35433,y:32573,varname:node_4079,prsc:2,tex:271f5ee3273dd7f4fae6e204d4f8c4bf,ntxv:0,isnm:False|UVIN-7890-OUT,TEX-445-TEX;n:type:ShaderForge.SFN_RemapRange,id:1910,x:33703,y:31967,varname:node_1910,prsc:2,frmn:0,frmx:1,tomn:-1,tomx:1|IN-3497-UVOUT;n:type:ShaderForge.SFN_ComponentMask,id:3904,x:33873,y:31974,varname:node_3904,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-1910-OUT;n:type:ShaderForge.SFN_ArcTan2,id:4745,x:34061,y:31974,varname:node_4745,prsc:2,attp:2|A-3904-G,B-3904-R;n:type:ShaderForge.SFN_Append,id:7890,x:35039,y:32223,varname:node_7890,prsc:2|A-5872-OUT,B-127-OUT;n:type:ShaderForge.SFN_Tex2d,id:1775,x:34069,y:32671,ptovrint:False,ptlb:Noise,ptin:_Noise,varname:node_1775,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:0,isnm:False|UVIN-7304-UVOUT;n:type:ShaderForge.SFN_Slider,id:9532,x:33477,y:32680,ptovrint:False,ptlb:Dissolve Amount,ptin:_DissolveAmount,varname:node_9532,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.3689972,max:1;n:type:ShaderForge.SFN_Add,id:6912,x:34678,y:32615,varname:node_6912,prsc:2|A-5680-OUT,B-7007-OUT;n:type:ShaderForge.SFN_RemapRange,id:5680,x:34507,y:32478,varname:node_5680,prsc:2,frmn:0,frmx:1,tomn:-1,tomx:1|IN-9016-OUT;n:type:ShaderForge.SFN_OneMinus,id:9016,x:34332,y:32478,varname:node_9016,prsc:2|IN-3156-OUT;n:type:ShaderForge.SFN_RemapRange,id:8935,x:34839,y:32471,varname:node_8935,prsc:2,frmn:0,frmx:1,tomn:-2,tomx:2|IN-6912-OUT;n:type:ShaderForge.SFN_Clamp01,id:5872,x:35039,y:32455,varname:node_5872,prsc:2|IN-8935-OUT;n:type:ShaderForge.SFN_ValueProperty,id:127,x:34296,y:31949,ptovrint:False,ptlb:node_127,ptin:_node_127,varname:node_127,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Multiply,id:2882,x:34770,y:32841,varname:node_2882,prsc:2|A-4079-RGB,B-7542-RGB;n:type:ShaderForge.SFN_Time,id:9698,x:33079,y:33438,varname:node_9698,prsc:2;n:type:ShaderForge.SFN_Panner,id:7304,x:33913,y:32858,varname:node_7304,prsc:2,spu:0,spv:1|UVIN-110-UVOUT,DIST-9257-OUT;n:type:ShaderForge.SFN_TexCoord,id:110,x:33736,y:32858,varname:node_110,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_ValueProperty,id:6303,x:33718,y:33254,ptovrint:False,ptlb:Scroll Speed,ptin:_ScrollSpeed,varname:node_6303,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:9257,x:33718,y:33060,varname:node_9257,prsc:2|A-4143-TSL,B-6303-OUT;n:type:ShaderForge.SFN_Sin,id:6975,x:33927,y:32384,varname:node_6975,prsc:2|IN-2571-OUT;n:type:ShaderForge.SFN_RemapRange,id:4278,x:34137,y:32431,varname:node_4278,prsc:2,frmn:-1,frmx:1,tomn:0.1,tomx:0.2|IN-6975-OUT;n:type:ShaderForge.SFN_Time,id:471,x:33567,y:32374,varname:node_471,prsc:2;n:type:ShaderForge.SFN_Multiply,id:7007,x:34330,y:32746,varname:node_7007,prsc:2|A-1775-R,B-7542-RGB;n:type:ShaderForge.SFN_ValueProperty,id:5380,x:33556,y:32525,ptovrint:False,ptlb:Dissolve Speed,ptin:_DissolveSpeed,varname:node_5380,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:2571,x:33754,y:32457,varname:node_2571,prsc:2|A-471-TSL,B-5380-OUT;n:type:ShaderForge.SFN_RemapRangeAdvanced,id:3156,x:34407,y:32328,varname:node_3156,prsc:2|IN-6975-OUT,IMIN-5655-X,IMAX-5655-Y,OMIN-5655-Z,OMAX-5655-W;n:type:ShaderForge.SFN_Vector4Property,id:5655,x:33927,y:32221,ptovrint:False,ptlb:Remap Vector,ptin:_RemapVector,varname:node_5655,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:-1,v2:1,v3:0.1,v4:0.3;n:type:ShaderForge.SFN_Tex2d,id:2544,x:33220,y:32779,ptovrint:False,ptlb:node_2544,ptin:_node_2544,varname:node_2544,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:8680,x:33477,y:32828,varname:node_8680,prsc:2|A-2544-R,B-8181-UVOUT;n:type:ShaderForge.SFN_Panner,id:8181,x:33301,y:33005,varname:node_8181,prsc:2,spu:0,spv:1|UVIN-9835-UVOUT,DIST-9698-TSL;n:type:ShaderForge.SFN_TexCoord,id:9835,x:32996,y:32909,varname:node_9835,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Panner,id:8663,x:33945,y:33459,varname:node_8663,prsc:2,spu:1,spv:1|UVIN-3674-UVOUT,DIST-1698-OUT;n:type:ShaderForge.SFN_TexCoord,id:3674,x:33746,y:33459,varname:node_3674,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Tex2d,id:2570,x:33518,y:33665,ptovrint:False,ptlb:node_2570,ptin:_node_2570,varname:node_2570,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:1698,x:33781,y:33640,varname:node_1698,prsc:2|A-9932-TSL,B-2570-R;n:type:ShaderForge.SFN_Time,id:9932,x:33740,y:33779,varname:node_9932,prsc:2;n:type:ShaderForge.SFN_ComponentMask,id:5681,x:34160,y:33459,varname:node_5681,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-8663-UVOUT;n:type:ShaderForge.SFN_Sin,id:7338,x:33079,y:33232,varname:node_7338,prsc:2|IN-9698-TSL;n:type:ShaderForge.SFN_RemapRange,id:8855,x:33257,y:33232,varname:node_8855,prsc:2,frmn:-1,frmx:1,tomn:0,tomx:1|IN-7338-OUT;n:type:ShaderForge.SFN_Time,id:4143,x:33401,y:33399,varname:node_4143,prsc:2;n:type:ShaderForge.SFN_Add,id:8729,x:33453,y:33215,varname:node_8729,prsc:2|A-8855-OUT,B-4143-TSL;proporder:4430-1775-9532-445-127-6303-5380-5655-2544-2570;pass:END;sub:END;*/

Shader "Shader Forge/FunWithGradients" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _Noise ("Noise", 2D) = "white" {}
        _DissolveAmount ("Dissolve Amount", Range(0, 1)) = 0.3689972
        _Ramp ("Ramp", 2D) = "white" {}
        _node_127 ("node_127", Float ) = 0
        _ScrollSpeed ("Scroll Speed", Float ) = 1
        _DissolveSpeed ("Dissolve Speed", Float ) = 1
        _RemapVector ("Remap Vector", Vector) = (-1,1,0.1,0.3)
        _node_2544 ("node_2544", 2D) = "white" {}
        _node_2570 ("node_2570", 2D) = "white" {}
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
            uniform sampler2D _Ramp; uniform float4 _Ramp_ST;
            uniform sampler2D _Noise; uniform float4 _Noise_ST;
            uniform float _node_127;
            uniform float _ScrollSpeed;
            uniform float _DissolveSpeed;
            uniform float4 _RemapVector;
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
                float4 node_471 = _Time;
                float node_6975 = sin((node_471.r*_DissolveSpeed));
                float4 node_4143 = _Time;
                float2 node_7304 = (i.uv0+(node_4143.r*_ScrollSpeed)*float2(0,1));
                float4 _Noise_var = tex2D(_Noise,TRANSFORM_TEX(node_7304, _Noise));
                float4 node_1672 = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float4 node_7890 = float4(saturate(((((1.0 - (_RemapVector.b + ( (node_6975 - _RemapVector.r) * (_RemapVector.a - _RemapVector.b) ) / (_RemapVector.g - _RemapVector.r)))*2.0+-1.0)+(_Noise_var.r*node_1672.rgb))*4.0+-2.0)),_node_127);
                float4 node_4079 = tex2D(_Ramp,TRANSFORM_TEX(node_7890, _Ramp));
                float3 emissive = (node_4079.rgb*node_1672.rgb);
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
