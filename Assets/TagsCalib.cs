﻿using UnityEngine;
using System.Collections;

public class TagsCalib : MonoBehaviour {

	public float[] tags_matrix = new float[16];
	private Vector3[] source = new Vector3[4];
	private Vector3[] destination = new Vector3[4];

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

//		Debug.Log("Source0: " + source[0]);
//		Debug.Log("Dest0: " + destination[0]);
//		Debug.Log("Source1: " + source[1]);
//		Debug.Log("Dest1: " + destination[1]);
//		Debug.Log("Source2: " + source[2]);
//		Debug.Log("Dest2: " + destination[2]);
//		Debug.Log("Source3: " + source[3]);
//		Debug.Log("Dest3: " + destination[3]);
//
//		FindHomography(ref source, ref destination, ref matrix);
	
	}

	void FindHomography(ref Vector3[] src, ref Vector3[] dest, ref float[] homography) 
	{
		// originally by arturo castro - 08/01/2010  
		//  
		// create the equation system to be solved  
		//  
		// from: Multiple View Geometry in Computer Vision 2ed  
		//       Hartley R. and Zisserman A.  
		//  
		// x' = xH  
		// where H is the homography: a 3 by 3 matrix  
		// that transformed to inhomogeneous coordinates for each point  
		// gives the following equations for each point:  
		//  
		// x' * (h31*x + h32*y + h33) = h11*x + h12*y + h13  
		// y' * (h31*x + h32*y + h33) = h21*x + h22*y + h23  
		//  
		// as the homography is scale independent we can let h33 be 1 (indeed any of the terms)  
		// so for 4 points we have 8 equations for 8 terms to solve: h11 - h32  
		// after ordering the terms it gives the following matrix  
		// that can be solved with gaussian elimination:  
		
		float[,] P = new float [,]{  
			{-src[0].x, -src[0].y, -1,   0,   0,  0, src[0].x*dest[0].x, src[0].y*dest[0].x, -dest[0].x }, // h11  
			{  0,   0,  0, -src[0].x, -src[0].y, -1, src[0].x*dest[0].y, src[0].y*dest[0].y, -dest[0].y }, // h12  
			
			{-src[1].x, -src[1].y, -1,   0,   0,  0, src[1].x*dest[1].x, src[1].y*dest[1].x, -dest[1].x }, // h13  
			{  0,   0,  0, -src[1].x, -src[1].y, -1, src[1].x*dest[1].y, src[1].y*dest[1].y, -dest[1].y }, // h21  
			
			{-src[2].x, -src[2].y, -1,   0,   0,  0, src[2].x*dest[2].x, src[2].y*dest[2].x, -dest[2].x }, // h22  
			{  0,   0,  0, -src[2].x, -src[2].y, -1, src[2].x*dest[2].y, src[2].y*dest[2].y, -dest[2].y }, // h23  
			
			{-src[3].x, -src[3].y, -1,   0,   0,  0, src[3].x*dest[3].x, src[3].y*dest[3].x, -dest[3].x }, // h31  
			{  0,   0,  0, -src[3].x, -src[3].y, -1, src[3].x*dest[3].y, src[3].y*dest[3].y, -dest[3].y }, // h32  
		};  
		
		GaussianElimination(ref P,9);  
		
		// gaussian elimination gives the results of the equation system  
		// in the last column of the original matrix.  
		// opengl needs the transposed 4x4 matrix:  
		float[] aux_H={ P[0,8],P[3,8],0,P[6,8], // h11  h21 0 h31  
			P[1,8],P[4,8],0,P[7,8], // h12  h22 0 h32  
			0      ,      0,0,0,       // 0    0   0 0  
			P[2,8],P[5,8],0,1};      // h13  h23 0 h33  
		
		for(int i=0;i<16;i++) homography[i] = aux_H[i];  
		
	}

	void GaussianElimination (ref float[,] A, int n)
	{
		// originally by arturo castro - 08/01/2010  
		//  
		// ported to c from pseudocode in  
		// http://en.wikipedia.org/wiki/Gaussian_elimination  
		
		int i = 0;  
		int j = 0;  
		int m = n-1;  
		while (i < m && j < n){  
			// Find pivot in column j, starting in row i:  
			int maxi = i;  
			for(int k = i+1; k<m; k++){  
				if(Mathf.Abs(A[k,j]) > Mathf.Abs(A[maxi,j])){  
					maxi = k;  
				}  
			}  
			if (A[maxi,j] != 0){  
				//swap rows i and maxi, but do not change the value of i  
				if(i!=maxi)  
				for(int k=0;k<n;k++){  
					float aux = A[i,k];  
					A[i,k]=A[maxi,k];  
					A[maxi,k]=aux;  
				}  
				//Now A[i,j] will contain the old value of A[maxi,j].  
				//divide each entry in row i by A[i,j]  
				float A_ij=A[i,j];  
				for(int k=0;k<n;k++){  
					A[i,k]/=A_ij;  
				}  
				//Now A[i,j] will have the value 1.  
				for(int u = i+1; u< m; u++){  
					//subtract A[u,j] * row i from row u  
					float A_uj = A[u,j];  
					for(int k=0;k<n;k++){  
						A[u,k]-=A_uj*A[i,k];  
					}  
					//Now A[u,j] will be 0, since A[u,j] - A[i,j] * A[u,j] = A[u,j] - 1 * A[u,j] = 0.  
				}  
				
				i++;  
			}  
			j++;  
		}  
		
		//back substitution  
		for(int k=m-2;k>=0;k--){  
			for(int l=k+1;l<n-1;l++){  
				A[k,m]-=A[k,l]*A[l,m];  
				//A[i*n+j]=0;  
			}  
		}  
	}
}
