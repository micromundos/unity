Shader "Custom/SurfacesViz" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_Color2 ("Color2", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}
	 
	SubShader {
	 	Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
	 
 Pass {
  GLSLPROGRAM // here begins the part in Unity's GLSL

         #ifdef VERTEX // here begins the vertex shader
		 #extension GL_EXT_gpu_shader4 : enable

		
		
         uniform sampler2D	_MainTex;
 
         void main() // all vertex shaders define a main() function
         {
         
         	
        	gl_TexCoord[0] = gl_MultiTexCoord0;

      
			gl_Position = gl_ModelViewProjectionMatrix * gl_Vertex;
			gl_FrontColor = gl_Color;
         }

         #endif // here ends the definition of the vertex shader


         #ifdef FRAGMENT // here begins the fragment shader

 		 uniform sampler2D	_MainTex;
 		 uniform vec4 _Color;
 		 uniform vec4 _Color2;

		 float lerp2d( float x, float x1, float x2, float y1, float y2 ) 
		 {
		   return (x-x1) / (x2-x1) * (y2-y1) + y1;
		 }
 		
         void main()
         {
         	
         	vec2 tc = gl_TexCoord[0].st;
         	
         	vec4 render_color = texture2D(_MainTex, tc);
         	float height = render_color.r;
         	render_color = vec4(
         		lerp2d( height, 0.0, 0.6, _Color.r, _Color2.r ),
         		lerp2d( height, 0.0, 0.6, _Color.g, _Color2.g ),
         		lerp2d( height, 0.0, 0.6, _Color.b, _Color2.b ),
         		height > 0.015 ? 1.0 : 0.0
         	);
   			//render_color = vec4(_Color.r, _Color.g, _Color.b, height > 0.02 ? 1.0 : 0.0);
         	
//         	float radius = 3.0;
//         	vec4 sum = vec4(0.0);
//			for(int y=-radius; y<=radius; y++) {
//				for(int x=-radius; x<=radius; x++) {
//					sum += texture2D(_MainTex, tc + vec2(x, y));
//				}
//			}
//			int d = (radius*2+1);
//			int n = d * d;
//			float m = 1. / float(n);
//			if(radius > 0) sum = sum * m;
			
//			height = sum.r;
//   			render_color = vec4(_Color.r, _Color.g, _Color.b, height);

//			gl_FragColor = sum;

         	gl_FragColor = render_color;
         }

         #endif // here ends the definition of the fragment shader

         ENDGLSL // here ends the part in GLSL 
 	}
 }
}
