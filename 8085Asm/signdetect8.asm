; Place an 8 bit number in memory location 0200h. Fetch the number
; from memory and to the next memory location store 00h, if the number
; is zero and 01h if positive, 02h if negative
; Written by Arnav Mukhopadhyay
;
; Nov 08, 2011
 
	LXI H, 0200H
	XRA A
	ADD M
	JZ ZER
	JP POS
	MVI A, 02H
	JMP STR
POS:	MVI A, 01H
	JMP STR
ZER:	MVI A, 00H
STR:	INX H
	MOV M, A
	HLT
