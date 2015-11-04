
//Camara Lucida shader
//https://github.com/chparsons/ofxCamaraLucida/blob/master/src/cml/shaders/render.h

Shader "Custom/CamaraLucidaShader" {

	SubShader {
      Pass {
         GLSLPROGRAM

         #ifdef VERTEX

			sampler2D render_tex;
			 
			const float epsilon = 1e-30;

		    uniform sampler2DRect depth_tex;
		    uniform sampler2DRect render_tex;

		    uniform float near;
		    uniform float far;
		    uniform float far_clamp;
		    uniform float width;
		    uniform float height;
		    uniform float fx;
		    uniform float fy;
		    uniform float cx;
		    uniform float cy;
		    uniform float xoff;
		    
		    vec3 unproject( vec2 p2, float z ) 
		    {
		      return vec3( 
		        (p2.x + xoff - cx) * z / fx, 
		        (p2.y - cy) * z / fy, z 
		      );
		    }

         void main() // all vertex shaders define a main() function
         {
            //gl_Position = gl_ModelViewProjectionMatrix * gl_Vertex;
            
	          vec2 render_tex_size = vec2( textureSize2DRect( render_tex, 0 ) );

	      	  gl_TexCoord[0] = gl_MultiTexCoord0;

		      vec2 p2 = gl_TexCoord[0].st;

		      vec2 d2 = vec2( 
		        p2.x / render_tex_size.x * width,
		        p2.y / render_tex_size.y * height
		      );

		      float zmm = texture2DRect( depth_tex, d2 ).r;
		      zmm = clamp( ( zmm < epsilon ? far_clamp : zmm ), 0.0, far_clamp );
		      vec4 p3 = vec4( unproject( d2, zmm ), 1.);

		      gl_Position = gl_ModelViewProjectionMatrix * p3;
		      gl_FrontColor = gl_Color;
         }

         #endif


         #ifdef FRAGMENT

		sampler2D render_tex;

         void main() 
         {
            //gl_FragColor = vec4(1.0, 0.0, 0.0, 1.0);
            
            vec2 p2 = gl_TexCoord[0].st;
      		vec4 color = texture2DRect( render_tex, p2 );
      		gl_FragColor = color;
         }

         #endif

         ENDGLSL
      }
   }
}
