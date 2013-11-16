using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class UIString{


	private static StringBuilder sb = new StringBuilder(1000);
//	private static List<int> spacePositions = new List<int>();

	private const char NewLine = '\n';
	private const char Space = ' ';
	private const char Tab = '\t';
	private const char HTMLStart = '<';
	private const char HTMLEnd = '>';
	
	//a simple rich text solver can set all character
	
//	
	//TODO: Finish truncations
	public static string TruncateString(string text, UIFont uiFont, float maxLength){
		return text;
	}
	
	public static string WrapString(string text, UIFont uiFont, float wrapLength, int size){
		
		
		sb.Length = 0;  //Clear does not exist in .net2.0, so we set length to 0
		
		float currentLength = 0;
		int lastStart = 0;
		int lastSpace = -1;
		bool isHTML = false;

		//We iterate over all characters in the string
		for (int i = 0; i< text.Length; i++){
			char currentChar = text[i];
			bool forceBreak = false;
			bool terminate = false;
			//This allows the formatting to accept newline characters in the input text
			if(currentChar == HTMLStart){
				isHTML = true;	
			}
			if(currentChar == HTMLEnd){
				isHTML = false;	
			}
			
			if(isHTML){
				// currentLength remains the same
			}
			else{
				if(currentChar == NewLine){
					
					lastSpace = i;
					forceBreak = true;
				}
				else{
					//If we arent already flagged for a new line, keep track of the most recent space character
					if(currentChar == Space){
						lastSpace = i;
					}
					currentLength += size*uiFont.GetLength(currentChar);
				}
			}
			
			//if we've reached the end, we need to write remaining text without a break
			if (i==text.Length-1){
				lastSpace = i;
				terminate = true;
			}
			
			//If we have exceeded the length, or forced a line break
			if(  (currentLength > wrapLength) || forceBreak || terminate){
				
				//We only add the 1 if the last character isn't a space or newline
				int count;
				
				
					if(currentChar == Space || currentChar == NewLine){
						count = lastSpace - lastStart;
					}
					else{
						count = lastSpace - lastStart+1;	
					}
				
				
				if(count < 1){
//					Debug.LogError("wrap failed" + count);	
					//currentLength = 0;
				}
				else{  //break a line - can we justify it here?
									
					
					sb.Append(text, lastStart, count);
					if(!terminate){
						sb.Append(NewLine);
					}
					//reset position to next character after lastSpace
					i = lastSpace +1;
					lastStart = i;
					currentLength = 0;
					

				}
			}
			
		}
		return sb.ToString();

	}
	
}


					//Justify Text
//					float remainder = wrapLength - lastLength;
//					
//					if(spacePositions.Count > 0 && !forceBreak){
//						int numberOfInserts = (int)(remainder/(spacePositions.Count*uiFont.GetLength(Space)));
////						Debug.Log(numberOfInserts);
//						for(int j=0; j<spacePositions.Count; j++){
//							for(int k=0; k<numberOfInserts; k++){
//								sb.Insert(spacePositions[j] + spaceIndex,Space);
//								
//							}
//							spaceIndex += numberOfInserts;
//						}
//					}
//					
//					spacePositions.Clear();