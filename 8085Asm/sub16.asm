; Subtraction of 2 16 bit integers at 0200h - 0202h and store the
; result in 0204h. Save the sign flag in 0206h
; Written by Arnav Mukhopadhyay
;
; Nov 4, 2011
	
	LHLD 0200H
	MOV B, H
	MOV C, L
	LHLD 0202H
	MOV D, H
	MOV E, L
	XRA A
	MOV A, C
	SUB E
	MOV L, A
	MOV A, B
	SBB D
	MOV H, A
	SHLD 0204H
	JP POS
	MVI A, 1
	JMP STR
POS:	MVI A, 00H
STR:	STA 0206H
	HLT 