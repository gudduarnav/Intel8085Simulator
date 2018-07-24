; Addition of 2 signed 16-bit integers at 0200h, 0202h, and store
; the result in 0204h. The store the sign in 0206h
; Written by Arnav Mukhopadhyay
;
; Nov 04, 2011

	LHLD 0200H
	MOV B, H
	MOV C, L
	LHLD 0202H
	MOV D, H
	MOV E, L
	XRA A
	MOV A, C
	ADD E
	MOV L, A
	MOV A, B
	ADC D
	MOV H, A
	SHLD 0204H
	JP POS
	MVI A, 1
	JMP STR
POS:	MVI A, 0
STR:	STA 0206H
	HLT
