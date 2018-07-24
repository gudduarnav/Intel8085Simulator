; Given an array of 10 8 bit numbers stored in 10 consecutive
; memory locations. Fetch the numbers from memory, check each 
; number to determine whether it is even or odd. Save even numbers
; in a set of consecutive memory locations and odd numbers in
; another consecutive memory locations.
; Written by Arnav Mukhopadhyay
;
; Nov 08, 2011

	LXI H, 0200H
	SHLD 0210H
	LXI H, 0300H
	SHLD 0212H
	LXI H, 0400H
	SHLD 0214H
RPT:	LHLD 0210H
	XRA A
	ADD M
	MOV B, A
	INX H
	SHLD 0210H
	ANI 01H
	JZ EVN
	LHLD 0214H
	MOV M, B
	INX H
	SHLD 0214H
	JMP TST
EVN:	LHLD 0212H
	MOV M, B
	INX H
	SHLD 0212H
TST:	XRA A
	LHLD 0210H
	ADD L
	CPI 0AH
	JZ EXT
	JMP RPT
EXT:	HLT

