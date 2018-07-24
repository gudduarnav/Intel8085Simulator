; Add 2 signed numbers in 0200h, 0201h and store result in 0202h
; Save the sign in 0203h - 0: Positive, 1:Negative
; Written by Arnav Mukhopadhyay
;
; Nov 3, 2011

	XRA A
	LDA 0201H
	MOV B, A
	LDA 0200H
	ADD B
	STA 0202H
	JP POS
	MVI A, 01H
	JMP STR
POS:	MVI A, 0
STR:	STA 0203H
	HLT