; Fetch a 8 bit number from memory location 0200h. Count number of 
; 1's and 0's in that number. Store these values in 0201h amd 0202h
; Written by Arnav Mukhopadhyay
;
; Nov 08, 2011

	LDA 0200H
	MOV B, A
	MVI C, 08H
	MVI D, 00H
	MVI E, 00H
RPT:	XRA A
	MOV A, B
	ANI 01H
	JZ ZCNT
	INR D
	JMP SHP
ZCNT:	INR E
SHP:	XRA A
	MOV A, B
	RRC
	MOV B, A
	DCR C
	JZ EXT
	JMP RPT
EXT:	MOV A, D
	STA 0201H
	MOV A, E
	STA 0202H
	HLT


