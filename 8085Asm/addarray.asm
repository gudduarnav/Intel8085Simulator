; Add 10 elements from Index 0 to Index 9 with base address 0200h
; and store the result in 020Ah. All inputs are 8 bit in length but
; result at 0210h is 16 bit integer
; Written by Arnav Mukhopadhyay
;
; Nov 08, 2011
	
	LXI B, 0000H
	LXI H, 0200H
REP:	XRA A
	MOV A, C
	ADD M
	MOV C, A
	MVI A, 00H
	ADC B
	MOV B, A
	INX H
	MOV A, L
	CPI 0AH
	JP EXT
	JMP REP
EXT:	MOV M, C
	INX H
	MOV M, B
	HLT
 
