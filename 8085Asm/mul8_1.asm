; Multiplication by Repeated Addition method.
; Add 2 8-bit numbers in 0200h, 0201h, and save the 8 bit result
; in 0202h
; Written by Arnav Mukhopadhyay
;
; 10 Nov, 2011

	LHLD 0200H
	LXI B, 0000H
RPT:	XRA A
	ADD L
	JZ PTR
	MOV A, C
	ADD H
	MOV C, A
	DCR L
	JMP RPT
PTR:	MOV H, B
	MOV L, C
	SHLD 0202H
	HLT