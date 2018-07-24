; Multiplication using Shift
; Multiply 2 numbers in 0200h and 0201h, and store the result in
; 0202h.
; Written by Arnav Mukhopadhyay
;
; Nov 10, 2011

	LHLD 0200H
	MVI B, 08H
	MVI C, 00H
RPT:	XRA A
	ADD B
	JZ EXT
	DCR B	
	XRA A
	MOV A, L
	ANI 01H
	JZ SKP	
	MOV A, C
	ADD H
	MOV C, A 	
SKP:	MOV A, L
	RRC
	MOV L, A
	MOV A, H
	RLC
	MOV H, A
	JMP RPT	
EXT:	MOV A, C
	STA 0202H
	HLT