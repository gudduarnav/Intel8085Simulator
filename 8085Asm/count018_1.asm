; Fetch a 8 bit number from memory location 0200h. Count number of 
; 1's and 0's in that number. Store these values in 0201h amd 0202h
; Written by Arnav Mukhopadhyay
;
; Nov 08, 2011

	LDA 0200H
	MVI B, 08H
	LXI H, 0000H
LOOP: RAR
     JC ONE
     INR H
     JMP NXT
ONE: INR L
NXT: DCR B
     JNZ LOOP
     SHLD 0201H
     HLT      