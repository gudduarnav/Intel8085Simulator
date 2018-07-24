; Add 2 signed numbers in 0200h, 0201h and store magnitude result 
; in 0202h
; Save the sign in 0203h - 0: Positive, 1:Negative
; Written by Arnav Mukhopadhyay
;
; Nov 3, 2011

	XRA A
	LDA 0201H
	MOV B, A
	LDA 0200H
	ADD B
	JP POS

	CMA		
	INR A
	STA 0202H
	MVI A, 01H
	STA 0203H
	JMP EXT

POS:	STA 0202H
	MVI A, 00H
	STA 0203H

EXT:	HLT
