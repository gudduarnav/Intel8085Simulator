; Subtract 2 numbers in 0200h - 0201h and store the result in 0202h.
; Store the sign in 0203h
; Written by Arnav Mukhopadhyay
;
; Nov 4, 2011

	LDA 0201H
	MOV B, A
	LDA 0200H
	SUB B
	STA 0202H
	JP POS
	MVI A, 1
	JMP STR
POS:	MVI A, 0
STR:	STA 0203H
	HLT
